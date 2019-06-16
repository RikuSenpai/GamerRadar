using System.ComponentModel.DataAnnotations;

namespace GamerRadar.Models
{
    public enum Pegi
    {
        None = 0,

        [Display(Name = @"3")]
        Pegi3 = 3,

        [Display(Name = @"7")]
        Pegi7 = 7,

        [Display(Name = @"12")]
        Pegi12 = 12,

        [Display(Name = @"16")]
        Pegi16 = 16,

        [Display(Name = @"18")]
        Pegi18 = 18
    }
}