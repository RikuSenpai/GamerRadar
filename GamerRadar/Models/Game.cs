using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GamerRadar.Models
{
    public class Game : ObjectWithIdentity
    {
        public Game() : base()
        {
            Approved = false;
        }

        [Required]
        [StringLength(60, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public Developer Developer { get; set; }

        [Required]
        public Publisher Publisher { get; set; }

        public string ImagePath { get; set; }

        public string VideoPath { get; set; }

        [Required]
        public Pegi Pegi { get; set; }

        public bool Approved { get; set; }

        public virtual IList<UserGame> UserGames { get; set; }
    }
}