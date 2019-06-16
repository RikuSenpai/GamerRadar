using GamerRadar.Helpers;
using GamerRadar.Models;
using GamerRadar.ViewModels;
using GamerRadar.ViewModels.Games;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace GamerRadar.Controllers
{
    public class GamesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public ActionResult Index(string sortOrder)
        {
            var games = db.Games.Where(p => p.Valid && p.Approved).Include(g => g.Publisher).Include(g => g.Developer).ToList();
            var gamesViewModels = games.Select(g => new GameViewModel(g)).ToList();

            ViewBag.SortByName = sortOrder == "Name" ? "Name_Desc" : "Name";
            ViewBag.SortByDate = sortOrder == "Date" ? "Date_Desc" : "Date";
            ViewBag.SortByDeveloper = sortOrder == "Developer" ? "Developer_Desc" : "Developer";
            ViewBag.SortByPublisher = sortOrder == "Publisher" ? "Publisher_Desc" : "Publisher";
            ViewBag.SortByPegi = sortOrder == "Pegi" ? "Pegi_Desc" : "Pegi";

            return View(SortHelper.SortGames(sortOrder, gamesViewModels));
        }

        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var game = db.Games.Where(p => p.Valid).Include(g => g.Publisher).Include(g => g.Developer).FirstOrDefault(g => g.ID == id);
            if (game == null)
            {
                return HttpNotFound();
            }

            var gameViewModel = new GameDetailsViewModel(game);
            return View(gameViewModel);
        }

        [Authorize]
        public ActionResult Create()
        {
            var publishersSelectListItem = GetPublishersSelectList();
            var developersSelectListItem = GetDevelopersSelectList();

            var gameViewModel = new GameCreateViewModel(publishersSelectListItem, developersSelectListItem);
            return View(gameViewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GameCreateViewModel gameCreateViewModel)
        {
            gameCreateViewModel.Publishers = GetPublishersSelectList();
            gameCreateViewModel.Developers = GetDevelopersSelectList();

            gameCreateViewModel.Game.Publisher = db.Publishers.FirstOrDefault(p=>p.ID == gameCreateViewModel.SelectedPublisherId);
            gameCreateViewModel.Game.Developer = db.Developers.FirstOrDefault(d => d.ID == gameCreateViewModel.SelectedDeveloperId);

            ClearPublisherAndDeveloperModelStateErrors(gameCreateViewModel);

            if (ModelState.IsValid)
            {
                db.Games.Add(gameCreateViewModel.Game);
                db.SaveChanges();
                return View("Report");
            }

            return View(gameCreateViewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private SelectList GetDevelopersSelectList()
        {
            var developers = db.Developers.Where(d => d.Valid).ToList();
            developers.Add(new Developer());
            developers = developers.OrderBy(d => d.Name).ToList();
            var developersSelectListItem = new SelectList(developers, "ID", "Name");
            return developersSelectListItem;
        }

        private SelectList GetPublishersSelectList()
        {
            var publishers = db.Publishers.Where(p => p.Valid).ToList();
            publishers.Add(new Publisher());
            publishers = publishers.OrderBy(p => p.Name).ToList();
            var publishersSelectListItem = new SelectList(publishers, "ID", "Name");
            return publishersSelectListItem;
        }

        private void ClearPublisherAndDeveloperModelStateErrors(GameCreateViewModel gameCreateViewModel)
        {
            if (gameCreateViewModel.Game.Publisher != null)
            {
                ModelState["Game.Publisher"].Errors.Clear();
            }

            if (gameCreateViewModel.Game.Developer != null)
            {
                ModelState["Game.Developer"].Errors.Clear();
            }
        }
    }
}
