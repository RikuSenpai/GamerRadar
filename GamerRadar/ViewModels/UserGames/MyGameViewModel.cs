using System.ComponentModel.DataAnnotations;
using GamerRadar.Models;

namespace GamerRadar.ViewModels.UserGames
{
    public class MyGameViewModel
    {
        public MyGameViewModel()
        {

        }

        public MyGameViewModel(UserGame userGame)
        {
            this.GameId = userGame.Game?.ID;
            this.UserGameId = userGame.ID;
            this.GameName = userGame.Game?.Name;
            this.GameTime = userGame.GameTime;
            this.GameplayType = userGame.GameplayType;
        }

        public int? GameId{ get; set; }

        public int UserGameId { get; set; }

        [Display(Name = "Game")]
        public string GameName { get; set; }

        [Display(Name = "Game time")]
        public GameTime GameTime { get; set; }

        [Display(Name = "Gameplay type")]
        public GameplayType GameplayType { get; set; }
    }
}