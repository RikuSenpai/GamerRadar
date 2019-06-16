using System;
using System.ComponentModel.DataAnnotations;

namespace GamerRadar.Models
{
    [Serializable]
    public class UserGame : ObjectWithIdentity
    {
        public UserGame() : base()
        {

        }

        [Required]
        public virtual ApplicationUser User { get; set; }

        [Required]
        public virtual Game Game { get; set; }

        public GameTime GameTime { get; set; }

        public GameplayType GameplayType { get; set; }
    }
}