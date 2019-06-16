using GamerRadar.Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GamerRadar.ViewModels
{
    public class GameViewModel
    {
        public GameViewModel(Game game)
        {
            this.ID = game.ID;
            this.Name = game.Name;
            this.ReleaseDate = game.ReleaseDate;
            this.Publisher = game.Publisher.Name;
            this.Developer = game.Developer.Name;
            this.PegiImagePath = "~/Images/" + game.Pegi.ToString() + ".png";
            this.Pegi = game.Pegi;
        }

        public int ID { get; set; }

        [StringLength(60, MinimumLength = 1)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public string Developer { get; set; }

        [Required]
        public string Publisher { get; set; }

        [DisplayName("Pegi")]
        public string PegiImagePath { get; set; }

        public Pegi Pegi { get; set; }
    }
}