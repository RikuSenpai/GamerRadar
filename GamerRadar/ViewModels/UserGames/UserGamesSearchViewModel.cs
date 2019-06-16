using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GamerRadar.ViewModels.UserGames
{
    public class UserGamesSearchViewModel
    {
        public UserGamesSearchViewModel()
        {
        }

        public UserGamesSearchViewModel(string scannedGameName)
        {
            this.ScannedGameName = scannedGameName;
        }

        public UserGamesSearchViewModel(IEnumerable<SelectListItem> games, int selectedGameId)
        {
            this.Games = games;
            this.SelectedGameId = selectedGameId;
        }

        [Display(Name = "Game")]
        public int SelectedGameId { get; set; }

        public string ScannedGameName { get; set; }

        public IEnumerable<SelectListItem> Games { get; set; }
    }
}