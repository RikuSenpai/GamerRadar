using System.ComponentModel.DataAnnotations;

namespace GamerRadar.Models
{
    public class Developer : ObjectWithIdentity
    {
        public Developer() : base()
        {

        }

        [StringLength(60, MinimumLength = 1)]
        public string Name { get; set; }
    }
}