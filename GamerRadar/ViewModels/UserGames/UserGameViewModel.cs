using GamerRadar.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace GamerRadar.ViewModels.UserGames
{
    public class UserGameViewModel
    {
        public UserGameViewModel()
        {
            this.UserGame = new UserGame();
        }

        public UserGameViewModel(UserGame userGame)
        {
            this.UserGame = userGame;
            this.Age = DateTime.Now.Year - userGame.User.Birthday.Year;
            this.Gender = userGame.User.Gender;
            this.Location = userGame.User.Location;
            this.GameTime = userGame.GameTime;
            this.GameplayType = userGame.GameplayType;
        }

        public UserGame UserGame { get; set; }

        [Display(Name = "Game time")]
        public GameTime GameTime { get; set; }

        [Display(Name = "Gameplay type")]
        public GameplayType GameplayType { get; set; }

        [Display(Name = "Age")]
        public int Age { get; set; }

        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }

        [Display(Name = "Distance")]
        public string Distance { get; set; }
    }
}