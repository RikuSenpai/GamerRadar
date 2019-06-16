using System.ComponentModel.DataAnnotations;

namespace GamerRadar.Models
{
    public class ObjectWithIdentity
    {
        public ObjectWithIdentity()
        {
            Valid = true;
        }

        [Key]
        public int ID { get; set; }

        public bool Valid { get; set; }
    }
}