using System.Collections.Generic;

namespace GamerRadar.ViewModels.UserGames
{
    public class MyGamesViewModel
    {
        public MyGamesViewModel()
        {
            this.MyGameViewModels = new List<MyGameViewModel>();
        }

        public MyGamesViewModel(IList<MyGameViewModel> myGameViewModels)
        {
            this.MyGameViewModels = myGameViewModels;
        }

        public IList<MyGameViewModel> MyGameViewModels { get; set; }
    }
}