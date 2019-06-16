using GamerRadar.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GamerRadar.ViewModels.Games
{
    public class GameCreateViewModel
    {
        public GameCreateViewModel()
        {
            this.Publishers = new List<SelectListItem>();
            this.Developers = new List<SelectListItem>();
            this.Game = new Game();
        }

        public GameCreateViewModel(IEnumerable<SelectListItem> publishers, IEnumerable<SelectListItem> developers)
        {
            this.Publishers = publishers;
            this.Developers = developers;
            this.Game = new Game();
        }

        [Display(Name = "Publisher")]
        public int SelectedPublisherId { get; set; }

        public IEnumerable<SelectListItem> Publishers { get; set; }

        [Display(Name = "Developer")]
        public int SelectedDeveloperId { get; set; }

        public IEnumerable<SelectListItem> Developers { get; set; }

        public Game Game { get; set; }
    }
}