using System;
using GamerRadar.Models;
using GamerRadar.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using GamerRadar.Helpers;
using GamerRadar.ViewModels.UserGames;

namespace GamerRadar.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId),
                EditAccountDetailsViewModel = new EditAccountDetailsViewModel(db.Users.First(u => u.Id == userId)),
            };

            var location = GetLatitudeAndLongitudeOfUser(model.EditAccountDetailsViewModel.Location);

            ViewBag.Lat = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(location.Item1);
            ViewBag.Lng = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(location.Item2);

            return View(model);
        }

        //
        // GET: /Manage/Index
        [HttpPost]
        public ActionResult Index(IndexViewModel indexViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.First(u => u.Email == indexViewModel.EditAccountDetailsViewModel.Email);
                user.Birthday = indexViewModel.EditAccountDetailsViewModel.Birthday;
                user.Location = indexViewModel.EditAccountDetailsViewModel.Location;
                user.UserName = indexViewModel.EditAccountDetailsViewModel.UserName;
                user.Gender = indexViewModel.EditAccountDetailsViewModel.Gender;
                db.Users.AddOrUpdate(user);
                db.SaveChanges();
            }

            var userId = User.Identity.GetUserId();
            indexViewModel.HasPassword = HasPassword();
            indexViewModel.PhoneNumber = UserManager.GetPhoneNumberAsync(userId).Result;
            indexViewModel.TwoFactor = UserManager.GetTwoFactorEnabledAsync(userId).Result;
            indexViewModel.Logins = UserManager.GetLoginsAsync(userId).Result;
            indexViewModel.BrowserRemembered = AuthenticationManager.TwoFactorBrowserRememberedAsync(userId).Result;

            return View(indexViewModel);
        }

        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        public ActionResult Games(string sortOrder)
        {
            var userId = User.Identity.GetUserId();
            var userGames = db.UserGames.Where(p => p.Valid && p.User.Id == userId).ToList();
            var gamesViewModels = userGames.Select(g => new MyGameViewModel(g)).ToList();

            ViewBag.SortByName = sortOrder == "Name" ? "Name_Desc" : "Name";
            ViewBag.SortByGameTime = sortOrder == "GameTime" ? "GameTime_Desc" : "GameTime";
            ViewBag.SortByGameplayType = sortOrder == "GameplayType" ? "GameplayType_Desc" : "GameplayType";
       
            var myGamesViewModel = new MyGamesViewModel(SortHelper.SortUserGames(sortOrder, gamesViewModels));
            return View("Games", myGamesViewModel);
        }

        private IEnumerable<SelectListItem> GetGames(IList<string> gamesToIgnore)
        {
            var games = db.Games.Where(g => g.Valid && g.Approved).ToList();
            games.Add(new Game());
            games = games.Where(g => !gamesToIgnore.Contains(g.Name)).OrderBy(p => p.Name).ToList();
            IEnumerable<SelectListItem> gamesSelectListItem = new SelectList(games, "ID", "Name");
            return gamesSelectListItem;
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var game = db.UserGames.Where(p => p.Valid).SingleOrDefault(g => g.ID == id);
            if (game == null)
            {
                return HttpNotFound();
            }

            var gameViewModel = new MyGameViewModel(game);
            return View(gameViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(MyGameViewModel myGameViewModel)
        {
            if (myGameViewModel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var myGame = new UserGame();
            myGame.Game = db.Games.First(g => g.Name == myGameViewModel.GameName);
            myGame.GameTime = myGameViewModel.GameTime;
            myGame.GameplayType = myGameViewModel.GameplayType;
            myGame.ID = myGameViewModel.UserGameId;
            var userId = User.Identity.GetUserId();
            myGame.User = db.Users.Find(userId);
            db.UserGames.AddOrUpdate(myGame);
            db.SaveChanges();
            return RedirectToAction("Games");
        }

        public ActionResult AddGame()
        {
            var userId = User.Identity.GetUserId();
            var userGames = db.UserGames.Where(p => p.Valid && p.User.Id == userId).ToList();

            var myNewGameViewModel = new MyNewGameViewModel(new UserGame(), GetGames(userGames.Select(ug=>ug.Game.Name).ToList()),0);
            return View(myNewGameViewModel);
        }

        [HttpPost]
        public ActionResult AddGame(MyNewGameViewModel myNewGameViewModel)
        {
            if (myNewGameViewModel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var myNewGame = new UserGame();
            myNewGame.Game = db.Games.First(g => g.ID == myNewGameViewModel.SelectedGameId);
            myNewGame.GameTime = myNewGameViewModel.GameTime;
            myNewGame.GameplayType = myNewGameViewModel.GameplayType;
            var userId = User.Identity.GetUserId();
            myNewGame.User = db.Users.First(u => u.Id == userId);
            db.UserGames.Add(myNewGame);
            db.SaveChanges();

            return Games(null);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userGame = db.UserGames.Find(id);
            if (userGame == null)
            {
                return HttpNotFound();
            }
            return View(userGame);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var userGame = db.UserGames.Find(id);
            db.UserGames.Remove(userGame);
            db.SaveChanges();
            return RedirectToAction("Games");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
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

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        #endregion
    }
}