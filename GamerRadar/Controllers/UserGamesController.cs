using GamerRadar.Models;
using GamerRadar.ViewModels.UserGames;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace GamerRadar.Controllers
{
    public class UserGamesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public ActionResult Search()
        {
            Session["Game"] = null;
            var gamesSelectListItem = GetGamesSelectList();
            var userGamesSearchViewModel = new UserGamesSearchViewModel(gamesSelectListItem, 0);
            return View(userGamesSearchViewModel);
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Search(UserGamesSearchViewModel userGamesSearchViewModel)
        {
            Session["Game"] = userGamesSearchViewModel;
            return RedirectToAction("Result");
        }

        private UserGamesViewModel CreateUserGamesViewModel(UserGamesSearchViewModel userGamesSearchViewModel)
        {
            if (userGamesSearchViewModel == null)
            {
                return null;
            }

            var userGames = new List<UserGame>();

            var userId = User.Identity.GetUserId();

            Game game = null;
            if (!string.IsNullOrEmpty(userGamesSearchViewModel.ScannedGameName))
            {
                userGames = db.UserGames.Where(ug => ug.Game.Name == userGamesSearchViewModel.ScannedGameName && ug.User.Id != userId).ToList();
                game = db.Games.FirstOrDefault(g => g.Name == userGamesSearchViewModel.ScannedGameName);
            }
            else if (userGamesSearchViewModel.SelectedGameId != 0)
            {
                userGames = db.UserGames.Where(ug => ug.Game.ID == userGamesSearchViewModel.SelectedGameId && ug.User.Id != userId).ToList();
                game = db.Games.FirstOrDefault(g => g.ID == userGamesSearchViewModel.SelectedGameId);
            }

            var userGameViewModels = new List<UserGameViewModel>(userGames.Select(ug => new UserGameViewModel(ug)));

            string address = db.Users.First(u => u.Id == userId).Location;

            Tuple<double, double> userLatitudeAndLongitude = GetLatitudeAndLongitudeOfUser(address);

            foreach (var userGameViewModel in userGameViewModels)
            {
                userGameViewModel.Distance =
                    GetLocationsOfUserAndStrangerAndCalculateDistanceBetweenThem(userLatitudeAndLongitude,
                        userGameViewModel.Location);
            }

            var userGamesViewModel = new UserGamesViewModel(userGameViewModels, game?.Name, game?.ID);
            return userGamesViewModel;
        }

        [Authorize]
        public ActionResult Result()
        {
            UserGamesSearchViewModel userGamesSearchViewModel = Session["Game"] as UserGamesSearchViewModel;
            var userGamesViewModel = CreateUserGamesViewModel(userGamesSearchViewModel);

            if (userGamesViewModel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(userGamesViewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private IEnumerable<SelectListItem> GetGamesSelectList()
        {
            var games = db.Games.Where(g => g.Valid && g.Approved).ToList();
            games.Add(new Game());
            games = games.OrderBy(p => p.Name).ToList();
            return new SelectList(games, "ID", "Name");
        }

        private static double CalculateDistanceUsingHaversine(double userLatitude, double strangerLatitude, double userLongitude, double strangerLongitude)
        {
            const double earthRadius = 6371;

            userLatitude = (Math.PI / 180) * userLatitude;
            strangerLatitude = (Math.PI / 180) * strangerLatitude;
            userLongitude = (Math.PI / 180) * userLongitude;
            strangerLongitude = (Math.PI / 180) * strangerLongitude;

            var x = Math.Sin((strangerLatitude - userLatitude) / 2);
            var y = Math.Sin((strangerLongitude - userLongitude) / 2);
            var z = x * x + Math.Cos(userLatitude) * Math.Cos(strangerLatitude) * y * y;

            var distance = 2 * earthRadius * Math.Asin(Math.Sqrt(z));

            return distance;
        }

        private Tuple<double, double> GetLatitudeAndLongitudeOfUser(string address)
        {
            string userRequestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?address={0}&key=AIzaSyAq8BJjhCh1zFmk5AvcHOdVb645HFgV5QE", address);

            WebRequest request = WebRequest.Create(userRequestUri);
            WebResponse response = request.GetResponse();
            XDocument xdoc = XDocument.Load(response.GetResponseStream());

            XElement result = xdoc.Element("GeocodeResponse").Element("result");
            XElement locationElement = result.Element("geometry").Element("location");
            XElement latitudeElement = locationElement.Element("lat");
            XElement longitudeElement = locationElement.Element("lng");

            var userLatitude = double.Parse(latitudeElement.Value, CultureInfo.InvariantCulture);
            var userLongitude = double.Parse(longitudeElement.Value, CultureInfo.InvariantCulture);

            Tuple<double, double> userLatitudeAndLongitude = new Tuple<double, double>(userLatitude, userLongitude);

            return userLatitudeAndLongitude;
        }

        private string GetLocationsOfUserAndStrangerAndCalculateDistanceBetweenThem(Tuple<double, double> userLatitudeAndLongitude, string strangerAddress)
        {
            string strangerRequestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?address={0}&key=AIzaSyAq8BJjhCh1zFmk5AvcHOdVb645HFgV5QE", strangerAddress);

            WebRequest request = WebRequest.Create(strangerRequestUri);
            WebResponse response = request.GetResponse();
            XDocument xdoc = XDocument.Load(response.GetResponseStream());


            XElement result = xdoc.Element("GeocodeResponse").Element("result");

            if (result == null)
            {
                return string.Format("N/A");
            }

            XElement locationElement = result.Element("geometry").Element("location");
            XElement latitudeElement = locationElement.Element("lat");
            XElement longitudeElement = locationElement.Element("lng");

            var strangerLatitude = double.Parse(latitudeElement.Value, CultureInfo.InvariantCulture);
            var strangerLongitude = double.Parse(longitudeElement.Value, CultureInfo.InvariantCulture);

            var distance = CalculateDistanceUsingHaversine(userLatitudeAndLongitude.Item1, strangerLatitude, userLatitudeAndLongitude.Item2, strangerLongitude);

            return distance.ToString("F");
        }
    }
}
