using GamerRadar.Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GamerRadar.ViewModels
{
    public class GameDetailsViewModel
    {
        public GameDetailsViewModel(Game game)
        {
            this.ID = game.ID;
            this.Name = game.Name;
            this.Description = game.Description;
            this.ReleaseDate = game.ReleaseDate;
            this.Publisher = game.Publisher.Name;
            this.Developer = game.Developer.Name;
            this.PegiImagePath = this.PegiImagePath = "~/Images/" + game.Pegi.ToString() + ".png";
            this.VideoPath = game.VideoPath;
            this.ImagePath = game.ImagePath;
        }

        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        public string Developer { get; set; }

        public string Publisher { get; set; }

        [DisplayName("Pegi")]
        public string PegiImagePath { get; set; }

        public string VideoPath { get; set; }

        public string ImagePath { get; set; }
    }
}