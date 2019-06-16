using GamerRadar.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GamerRadar.ViewModels.UserGames
{
    public class MyNewGameViewModel : MyGameViewModel
    {
        public MyNewGameViewModel() : base()
        {
        }

        public MyNewGameViewModel(UserGame userGame, IEnumerable<SelectListItem> games, int selectedGameId) : base(userGame)
        {
            this.Games = games;
            this.SelectedGameId = selectedGameId;
        }

        [Display(Name = "Game")]
        public int SelectedGameId { get; set; }

        public IEnumerable<SelectListItem> Games { get; set; }
    }
}
