using System;
using System.Collections.Generic;

namespace GamerRadar.ViewModels.UserGames
{
    [Serializable]
    public class UserGamesViewModel
    {
        public UserGamesViewModel()
        {
            this.UserGameViewModels = new List<UserGameViewModel>();
        }

        public UserGamesViewModel(IList<UserGameViewModel> userGameViewModels, string gameName, int? gameId)
        {
            this.UserGameViewModels = userGameViewModels;
            this.GameName = gameName;
            this.GameId = gameId;
        }

        public IList<UserGameViewModel> UserGameViewModels { get; set; }

        public string GameName { get; set; }

        public int? GameId { get; set; }
    }
}