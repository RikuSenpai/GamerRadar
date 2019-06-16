using GamerRadar.ViewModels;
using GamerRadar.ViewModels.UserGames;
using System.Collections.Generic;
using System.Linq;

namespace GamerRadar.Helpers
{
    public static class SortHelper
    {
        public static List<GameViewModel> SortGames(string sortOrder, IList<GameViewModel> gamesViewModels)
        {
            IList<GameViewModel> sortedGames;

            switch (sortOrder)
            {
                case "Name":
                {
                    sortedGames = gamesViewModels.OrderBy(g => g.Name).ToList();
                }
                    break;

                case "Name_Desc":
                {
                    sortedGames = gamesViewModels.OrderByDescending(g => g.Name).ToList();
                }
                    break;

                case "Date":
                {
                    sortedGames = gamesViewModels.OrderBy(g => g.ReleaseDate).ToList();
                }
                    break;

                case "Date_Desc":
                {
                    sortedGames = gamesViewModels.OrderByDescending(g => g.ReleaseDate).ToList();
                }
                    break;

                case "Developer":
                {
                    sortedGames = gamesViewModels.OrderBy(g => g.Developer).ToList();
                }
                    break;

                case "Developer_Desc":
                {
                    sortedGames = gamesViewModels.OrderByDescending(g => g.Developer).ToList();
                }
                    break;

                case "Publisher":
                {
                    sortedGames = gamesViewModels.OrderBy(g => g.Publisher).ToList();
                }
                    break;

                case "Publisher_Desc":
                {
                    sortedGames = gamesViewModels.OrderByDescending(g => g.Publisher).ToList();
                }
                    break;

                case "Pegi":
                {
                    sortedGames = gamesViewModels.OrderBy(g => g.Pegi).ToList();
                }
                    break;

                case "Pegi_Desc":
                {
                    sortedGames = gamesViewModels.OrderByDescending(g => g.Pegi).ToList();
                }
                    break;

                default:
                {
                    sortedGames = gamesViewModels;
                }
                    break;
            }

            return sortedGames.ToList();
        }

        public static List<MyGameViewModel> SortUserGames(string sortOrder, IList<MyGameViewModel> userGameViewModels)
        {
            IList<MyGameViewModel> sortedUserGames;

            switch (sortOrder)
            {
                case "Name":
                {
                    sortedUserGames = userGameViewModels.OrderBy(g => g.GameName).ToList();
                }
                    break;

                case "Name_Desc":
                {
                    sortedUserGames = userGameViewModels.OrderByDescending(g => g.GameName).ToList();
                }
                    break;

                case "GameTime":
                {
                    sortedUserGames = userGameViewModels.OrderBy(g => g.GameTime).ToList();
                }
                    break;

                case "GameTime_Desc":
                {
                    sortedUserGames = userGameViewModels.OrderByDescending(g => g.GameTime).ToList();
                }
                    break;

                case "GameplayType":
                {
                    sortedUserGames = userGameViewModels.OrderBy(g => g.GameplayType).ToList();
                }
                    break;

                case "GameplayType_Desc":
                {
                    sortedUserGames = userGameViewModels.OrderByDescending(g => g.GameplayType).ToList();
                }
                    break;

                default:
                {
                    sortedUserGames = userGameViewModels.ToList();
                }
                    break;
            }

            return sortedUserGames.ToList();
        }
    }
}