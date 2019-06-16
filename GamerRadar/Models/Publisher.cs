using System.ComponentModel.DataAnnotations;

namespace GamerRadar.Models
{
    public class Publisher : ObjectWithIdentity
    {
        public Publisher() : base()
        {
            Name = string.Empty;
        }

        [StringLength(60, MinimumLength = 1)]
        public string Name { get; set; }
    }
}