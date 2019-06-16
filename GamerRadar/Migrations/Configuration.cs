namespace GamerRadar.Migrations
{
    using GamerRadar.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GamerRadar.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "GamerRadar.Models.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            GenerateDevelopers(context);
            GeneratePublishers(context);
            GenerateGames(context);
            GenerateUsers(context);
            GenerateUserGames(context);

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }

        private static void GenerateDevelopers(ApplicationDbContext context)
        {
            var developers = new List<Developer>()
            {
                new Developer()
                {
                    ID = 1,
                    Name = "CD Projekt Red",
                },
                new Developer()
                {
                    ID = 2,
                    Name = "DICE",
                },
                new Developer()
                {
                    ID = 3,
                    Name = "Blizzard Entertainment",
                },
                new Developer()
                {
                    ID = 4,
                    Name = "Rockstar Games",
                },
                new Developer()
                {
                    ID = 5,
                    Name = "Ubisoft",
                },
                new Developer()
                {
                    ID = 6,
                    Name = "Warhorse Studios",
                },
                new Developer()
                {
                    ID = 7,
                    Name = "Bethesda Softworks",
                },
                new Developer()
                {
                    ID = 8,
                    Name = "4A Games",
                },
                new Developer()
                {
                    ID = 9,
                    Name = "Larian Studios",
                },
                new Developer()
                {
                    ID = 10,
                    Name = "The Farm 51",
                },
                new Developer()
                {
                    ID = 11,
                    Name = "Epic Games",
                },
                new Developer()
                {
                    ID = 12,
                    Name = "GIANTS Software",
                },
                new Developer()
                {
                    ID = 13,
                    Name = "Bungie Software",
                }
            };

            context.Developers.AddOrUpdate(developers.ToArray());
            context.SaveChanges();
        }

        private static void GeneratePublishers(ApplicationDbContext context)
        {
            var publishers = new List<Publisher>()
            {
                new Publisher()
                {
                    ID = 1,
                    Name = "CD Projekt",
                },
                new Publisher()
                {
                    ID = 2,
                    Name = "Electronic Arts",
                },
                new Publisher()
                {
                    ID = 3,
                    Name = "Blizzard Entertainment",
                },
                new Publisher()
                {
                    ID = 4,
                    Name = "Rockstar Games",
                },
                new Publisher()
                {
                    ID = 5,
                    Name = "Ubisoft",
                },
                new Publisher()
                {
                    ID = 6,
                    Name = "Deep Silver",
                },
                new Publisher()
                {
                    ID = 7,
                    Name = "Bethesda Softworks",
                },
                new Publisher()
                {
                    ID = 8,
                    Name = "Larian Studios",
                },
                new Publisher()
                {
                    ID = 9,
                    Name = "The Farm 51",
                },
                new Publisher()
                {
                    ID = 10,
                    Name = "Epic Games",
                },
                new Publisher()
                {
                    ID = 11,
                    Name = "Focus Home Interactive",
                },
                new Publisher()
                {
                    ID = 12,
                    Name = "Activision Blizzard",
                }
            };

            context.Publishers.AddOrUpdate(publishers.ToArray());
            context.SaveChanges();
        }

        private static void GenerateGames(ApplicationDbContext context)
        {
            var games = new List<Game>()
            {
                new Game()
                {
                    ID = 1,
                    Name = "Battlefield V",
                    Description =
                        "World War 2 as You\'ve Never Seen It Before - Take the fight to unexpected but crucial moments of the war, as Battlefield goes back to where it all began.",
                    ReleaseDate = new DateTime(2018, 11, 20),
                    Publisher = context.Publishers.First(p => p.Name.Equals("Electronic Arts")),
                    Developer = context.Developers.First(p => p.Name.Equals("Dice")),
                    Pegi = Pegi.Pegi18,
                    Approved = true,
                    VideoPath = @"https://www.youtube.com/embed/9OTkhsJUK0U",
                    ImagePath = "~/Images/Games/BFV.jpg",
                },
                new Game()
                {
                    ID = 2,
                    Name = "Battlefield 1",
                    Description =
                        "Experience the Dawn of All-out War - Be a part of the greatest battles ever known to man. From the heavily defended Alps to the scorching deserts of Arabia, war is raging on an epic scale on land, air and sea as you witness the birth of modern warfare.",
                    ReleaseDate = new DateTime(2016, 10, 21),
                    Publisher = context.Publishers.First(p => p.Name.Equals("Electronic Arts")),
                    Developer = context.Developers.First(p => p.Name.Equals("Dice")),
                    Pegi = Pegi.Pegi18,
                    Approved = true,
                    VideoPath = @"https://www.youtube.com/embed/4pY3hlQEOc0",
                    ImagePath = "~/Images/Games/BF1.jpg",
                },
                new Game()
                {
                    ID = 3,
                    Name = "Grand Theft Auto V",
                    Description =
                        "When a young street hustler, a retired bank robber and a terrifying psychopath find themselves entangled with some of the most frightening and deranged elements of the criminal underworld, the U.S. government and the entertainment industry, they must pull off a series of dangerous heists to survive in a ruthless city in which they can trust nobody, least of all each other.",
                    ReleaseDate = new DateTime(2013, 09, 17),
                    Publisher = context.Publishers.First(p => p.Name.Equals("Rockstar Games")),
                    Developer = context.Developers.First(p => p.Name.Equals("Rockstar Games")),
                    Pegi = Pegi.Pegi18,
                    Approved = true,
                    VideoPath = @"https://www.youtube.com/embed/QkkoHAzjnUs",
                    ImagePath = "~/Images/Games/GTAV.jpg",
                },
                new Game()
                {
                    ID = 4,
                    Name = "Red Dead Redemption 2",
                    Description =
                        "America, 1899. The end of the wild west era has begun as lawmen hunt down the last remaining outlaw gangs. Those who will not surrender or succumb are killed.",
                    ReleaseDate = new DateTime(2018, 10, 26),
                    Publisher = context.Publishers.First(p => p.Name.Equals("Rockstar Games")),
                    Developer = context.Developers.First(p => p.Name.Equals("Rockstar Games")),
                    Pegi = Pegi.Pegi18,
                    Approved = true,
                    VideoPath = @"https://www.youtube.com/embed/gmA6MrX81z4",
                    ImagePath = "~/Images/Games/RDR2.jpg",
                },
                new Game()
                {
                    ID = 5,
                    Name = "Assassin's Creed Odyssey",
                    Description =
                        "Write your own legendary odyssey and live epic adventures in a world where every choice matters. Sentenced to death by your family, embark on an epic journey from outcast mercenary to legendary Greek hero, and uncover the truth about your past.",
                    ReleaseDate = new DateTime(2018, 10, 26),
                    Publisher = context.Publishers.First(p => p.Name.Equals("Ubisoft")),
                    Developer = context.Developers.First(p => p.Name.Equals("Ubisoft")),
                    Pegi = Pegi.Pegi18,
                    Approved = true,
                    VideoPath = @"https://www.youtube.com/embed/s_SJZSAtLBA",
                    ImagePath = "~/Images/Games/ACO.jpg",
                },
                new Game()
                {
                    ID = 6,
                    Name = "Kingdom Come: Deliverance",
                    Description =
                        "Story-driven open-world RPG that immerses you in an epic adventure in the Holy Roman Empire. Explore majestic castles, deep forests, thriving villages and countless other realistic settings in medieval Bohemia!",
                    ReleaseDate = new DateTime(2018, 02, 13),
                    Publisher = context.Publishers.First(p => p.Name.Equals("Deep Silver")),
                    Developer = context.Developers.First(p => p.Name.Equals("Warhorse Studios")),
                    Pegi = Pegi.Pegi18,
                    Approved = true,
                    VideoPath = @"https://www.youtube.com/embed/tpnuBdG9txM",
                    ImagePath = "~/Images/Games/KCD.jpg",
                },
                new Game()
                {
                    ID = 7,
                    Name = "Fallout 76",
                    Description =
                        "Online prequel where every surviving human is a real person. Work together, or not, to survive. Under the threat of nuclear annihilation, you'll experience the largest, most dynamic world ever created in the legendary Fallout universe.",
                    ReleaseDate = new DateTime(2018, 11, 14),
                    Publisher = context.Publishers.First(p => p.Name.Equals("Bethesda Softworks")),
                    Developer = context.Developers.First(p => p.Name.Equals("Bethesda Softworks")),
                    Pegi = Pegi.Pegi18,
                    Approved = true,
                    VideoPath = @"https://www.youtube.com/embed/M9FGaan35s0",
                    ImagePath = "~/Images/Games/F76.jpg",
                },
                new Game()
                {
                    ID = 8,
                    Name = "Metro Exodus",
                    Description =
                        "An epic, story-driven first person shooter from 4A Games that blends deadly combat and stealth with exploration and survival horror in one of the most immersive game worlds ever created.",
                    ReleaseDate = new DateTime(2019, 02, 22),
                    Publisher = context.Publishers.First(p => p.Name.Equals("Deep Silver")),
                    Developer = context.Developers.First(p => p.Name.Equals("4A Games")),
                    Pegi = Pegi.Pegi18,
                    Approved = true,
                    VideoPath = @"https://www.youtube.com/embed/fbbqlvuovQ0",
                    ImagePath = "~/Images/Games/ME.jpg",
                },
                new Game()
                {
                    ID = 9,
                    Name = "Divinity: Original Sin II",
                    Description =
                        "The Divine is dead. The Void approaches. And the powers lying dormant within you are soon to awaken. The battle for Divinity has begun. Choose wisely and trust sparingly; darkness lurks within every heart in cooperative sandbox RPG.",
                    ReleaseDate = new DateTime(2017, 09, 14),
                    Publisher = context.Publishers.First(p => p.Name.Equals("Larian Studios")),
                    Developer = context.Developers.First(p => p.Name.Equals("Larian Studios")),
                    Pegi = Pegi.Pegi18,
                    Approved = true,
                    VideoPath = @"https://www.youtube.com/embed/bTWTFX8qzPI",
                    ImagePath = "~/Images/Games/DOSII.jpg",
                },
                new Game()
                {
                    ID = 9,
                    Name = "World War 3",
                    Description =
                        "Multiplayer military FPS set in a modern, global conflict. Strong teamplay, national armed forces, real locations, full body awareness, and a versatile customization system all contribute to the authenticity of the modern combat experience enhanced by other essential elements such as a robust ballistic system, advanced armors and life-like weapons.",
                    ReleaseDate = new DateTime(2018, 10, 19),
                    Publisher = context.Publishers.First(p => p.Name.Equals("The Farm 51")),
                    Developer = context.Developers.First(p => p.Name.Equals("The Farm 51")),
                    Pegi = Pegi.Pegi18,
                    Approved = true,
                    VideoPath = @"https://www.youtube.com/embed/G17mpdSbJBU",
                    ImagePath = "~/Images/Games/WW3.jpg",
                },
                new Game()
                {
                    ID = 10,
                    Name = "Fortnite: Battle Royale",
                    Description =
                        "FREE 100-player PvP mode in Fortnite. One giant map. A battle bus. Fortnite building skills and destructible environments combined with intense PvP combat. The last one standing wins.",
                    ReleaseDate = new DateTime(2017, 09, 26),
                    Publisher = context.Publishers.First(p => p.Name.Equals("Epic Games")),
                    Developer = context.Developers.First(p => p.Name.Equals("Epic Games")),
                    Pegi = Pegi.Pegi12,
                    Approved = true,
                    VideoPath = @"https://www.youtube.com/embed/2gUtfBmw86Y",
                    ImagePath = "~/Images/Games/FBR.jpg",
                },
                new Game()
                {
                    ID = 11,
                    Name = "Farming Simulator 17",
                    Description =
                        "Get to work on your fields in a new playground! A large South American environment awaits you with authentic landscapes, unique vegetation, local cows and sugarcane fields offering a total change of scenery and more gameplay possibilities!",
                    ReleaseDate = new DateTime(2016, 10, 25),
                    Publisher = context.Publishers.First(p => p.Name.Equals("Focus Home Interactive")),
                    Developer = context.Developers.First(p => p.Name.Equals("GIANTS Software")),
                    Pegi = Pegi.Pegi3,
                    Approved = true,
                    VideoPath = @"https://www.youtube.com/embed/8NLH5hgYBrs",
                    ImagePath = "~/Images/Games/FS17.jpg",
                },
                new Game()
                {
                    ID = 12,
                    Name = "Destiny 2",
                    Description = "An action shooter that takes you on an epic journey across the solar system.",
                    ReleaseDate = new DateTime(2017, 09, 06),
                    Publisher = context.Publishers.First(p => p.Name.Equals("Activision Blizzard")),
                    Developer = context.Developers.First(p => p.Name.Equals("Bungie Software")),
                    Pegi = Pegi.Pegi16,
                    Approved = true,
                    VideoPath = @"https://www.youtube.com/embed/hdWkpbPTpmE",
                    ImagePath = "~/Images/Games/D2.jpg",
                }
            };

            context.Games.AddOrUpdate(games.ToArray());
            context.SaveChanges();
        }

        private static void GenerateUsers(ApplicationDbContext context)
        {
            var users = new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    Id = "012a6be2-6c97-46ea-bd91-09f6c70a996b",
                    Gender = Gender.Male,
                    Birthday = DateTime.Parse("06.02.1988 00:00:00"),
                    Location = "Miami",
                    Email = "john.doe@test.com",
                    EmailConfirmed = false,
                    PasswordHash = "AC0cgYQm3M7brD2NaPJvmIvd4trdmkmIbgs2vTDGDLBzW2kDhayUoxDxGYRyOONJNQ==",
                    SecurityStamp = "08f88cc0-3bb5-4d57-b18e-ff19cdff12e1",
                    PhoneNumber = null,
                    TwoFactorEnabled = false,
                    LockoutEndDateUtc = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    UserName = "John Doe",
                },
                new ApplicationUser()
                {
                    Id = "16b535b1-1538-4bad-9cce-8ce796d5c751",
                    Gender = Gender.Male,
                    Birthday = DateTime.Parse("15.04.1977 00:00:00"),
                    Location = "Warszawa",
                    Email = "adam.nowak@test.com",
                    EmailConfirmed = false,
                    PasswordHash = "AN4yBRkCYu3dVzTyUEuqYf//Te4BCZGNNkjyhm7a0ExU1+j70i7gulJm8F0b+yTeCg==",
                    SecurityStamp = "856cce2d-f0ce-420e-a7f1-ad5d21c7bd77",
                    PhoneNumber = null,
                    TwoFactorEnabled = false,
                    LockoutEndDateUtc = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    UserName = "Adam Nowak",
                },
                new ApplicationUser()
                {
                    Id = "366756fc-1aa1-4071-acdf-be614414fd69",
                    Gender = Gender.Female,
                    Birthday = DateTime.Parse("20.07.1987 00:00:00"),
                    Location = "Gdañsk",
                    Email = "katarzyna.kowal@test.com",
                    EmailConfirmed = false,
                    PasswordHash = "AKE116AmB2i/aMnzGe/Xq1i/wV2KPwpkq+aqxI9EKfOzDvhZkWe1Y1jZqI7V6ClTxQ==",
                    SecurityStamp = "5a8e2fbf-aeb9-4609-beb9-fa975ebac126",
                    PhoneNumber = null,
                    TwoFactorEnabled = false,
                    LockoutEndDateUtc = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    UserName = "Katarzyna Kowal",
                },
                new ApplicationUser()
                {
                    Id = "58e27c4e-ad1d-4467-860f-7a9de9cbd383",
                    Gender = Gender.Female,
                    Birthday = DateTime.Parse("21.09.1990 00:00:00"),
                    Location = "Poznañ",
                    Email = "julia.kot@test.com",
                    EmailConfirmed = false,
                    PasswordHash = "ACNlKxhpfBIbavV8sQQovbsGcG3wV1OJXuAEMor/P8YqAGt4s6USNmeqvF3bjeVJrw==",
                    SecurityStamp = "bb4595f6-f828-42bc-8bc8-d44b4a765c0b",
                    PhoneNumber = null,
                    TwoFactorEnabled = false,
                    LockoutEndDateUtc = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    UserName = "Julia Kot",
                },
                new ApplicationUser()
                {
                    Id = "863ab1d1-8b61-4cc8-8b3e-ed4b6131d5ec",
                    Gender = Gender.Male,
                    Birthday = DateTime.Parse("28.06.1996 00:00:00"),
                    Location = "Kraków",
                    Email = "jan.kowalski@test.com",
                    EmailConfirmed = false,
                    PasswordHash = "AEAazgySFacFRdHog7jm8gdkj9NOnMllidGdQkcy2JMKQQ3Sa4qZXYDGhNgm6qm16Q==",
                    SecurityStamp = "18542664-e763-4435-ae01-744c52fbeb3c",
                    PhoneNumber = null,
                    TwoFactorEnabled = false,
                    LockoutEndDateUtc = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    UserName = "Jan Kowalski",
                },
                new ApplicationUser()
                {
                    Id = "a1fbad32-ced0-4a6e-ac5c-8537e60dd6bf",
                    Gender = Gender.Male,
                    Birthday = DateTime.Parse("12.12.2000 00:00:00"),
                    Location = "Cheltenham",
                    Email = "jack.smith@test.com",
                    EmailConfirmed = false,
                    PasswordHash = "AOKAtH/PaVhFl/xFV42OQQZhq2a64UQba9kMN16t1Kp9JFyqSzNfGl97G6cHg8Pacw==",
                    SecurityStamp = "efd87449-87bd-4e9f-a19c-728d30577174",
                    PhoneNumber = null,
                    TwoFactorEnabled = false,
                    LockoutEndDateUtc = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    UserName = "Jack Smith",
                },
                new ApplicationUser()
                {
                    Id = "d0130868-3362-4496-9093-77058e3a0afd",
                    Gender = Gender.Female,
                    Birthday = DateTime.Parse("08.06.1990 00:00:00"),
                    Location = "San Francisco",
                    Email = "alice.johnson@test.com",
                    EmailConfirmed = false,
                    PasswordHash = "AEGV0DauZkyCo4zCIVY17mQazP4rQy+riq8vNQX+TbIMNuqrMOwPSUSTOHRXr/i4/Q==",
                    SecurityStamp = "3db3f5ed-6a5f-4f7d-aa38-bb5829cef11d",
                    PhoneNumber = null,
                    TwoFactorEnabled = false,
                    LockoutEndDateUtc = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    UserName = "Alice Johnson",
                },
                new ApplicationUser()
                {
                    Id = "e25285e8-3bff-4f7a-b0fb-f684be3d700c",
                    Gender = Gender.Female,
                    Birthday = DateTime.Parse("11.10.1994 00:00:00"),
                    Location = "London",
                    Email = "jennifer.wilson@test.com",
                    EmailConfirmed = false,
                    PasswordHash = "AGk7vpbdRE3n6qnB4+7+UPFlPTAQzkYSnaPZM9kenkyCo0kz+3+SZOVJOIATvF5o+Q==",
                    SecurityStamp = "fc6e733c-653c-4b50-9bcd-54c883b1940d",
                    PhoneNumber = null,
                    TwoFactorEnabled = false,
                    LockoutEndDateUtc = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    UserName = "Jennifer Wilson",
                },
            };

            context.Users.AddOrUpdate(users.ToArray());
            context.SaveChanges();
        }

        private static void GenerateUserGames(ApplicationDbContext context)
        {
            var userGames = new List<UserGame>()
            {
                new UserGame()
                {
                    ID = 1,
                    User = context.Users.Find("012a6be2-6c97-46ea-bd91-09f6c70a996b"),
                    Game = context.Games.Find(1),
                    GameplayType = GameplayType.Hardcore,
                    GameTime = GameTime.Everyday,
                },
                new UserGame()
                {
                    ID = 2,
                    User = context.Users.Find("012a6be2-6c97-46ea-bd91-09f6c70a996b"),
                    Game = context.Games.Find(3),
                    GameplayType = GameplayType.Casual,
                    GameTime = GameTime.Weekdays,
                },
                new UserGame()
                {
                    ID = 3,
                    User = context.Users.Find("012a6be2-6c97-46ea-bd91-09f6c70a996b"),
                    Game = context.Games.Find(5),
                    GameplayType = GameplayType.Casual,
                    GameTime = GameTime.Weekend,
                },

                new UserGame()
                {
                    ID = 4,
                    User = context.Users.Find("16b535b1-1538-4bad-9cce-8ce796d5c751"),
                    Game = context.Games.Find(2),
                    GameplayType = GameplayType.Hardcore,
                    GameTime = GameTime.Everyday,
                },
                new UserGame()
                {
                    ID = 5,
                    User = context.Users.Find("16b535b1-1538-4bad-9cce-8ce796d5c751"),
                    Game = context.Games.Find(3),
                    GameplayType = GameplayType.Casual,
                    GameTime = GameTime.Weekdays,
                },
                new UserGame()
                {
                    ID = 6,
                    User = context.Users.Find("16b535b1-1538-4bad-9cce-8ce796d5c751"),
                    Game = context.Games.Find(5),
                    GameplayType = GameplayType.Casual,
                    GameTime = GameTime.Weekend,
                },

                new UserGame()
                {
                    ID = 7,
                    User = context.Users.Find("366756fc-1aa1-4071-acdf-be614414fd69"),
                    Game = context.Games.Find(1),
                    GameplayType = GameplayType.Hardcore,
                    GameTime = GameTime.Everyday,
                },
                new UserGame()
                {
                    ID = 8,
                    User = context.Users.Find("366756fc-1aa1-4071-acdf-be614414fd69"),
                    Game = context.Games.Find(7),
                    GameplayType = GameplayType.Casual,
                    GameTime = GameTime.Weekdays,
                },
                new UserGame()
                {
                    ID = 9,
                    User = context.Users.Find("366756fc-1aa1-4071-acdf-be614414fd69"),
                    Game = context.Games.Find(9),
                    GameplayType = GameplayType.Casual,
                    GameTime = GameTime.Weekend,
                },

                new UserGame()
                {
                    ID = 10,
                    User = context.Users.Find("58e27c4e-ad1d-4467-860f-7a9de9cbd383"),
                    Game = context.Games.Find(12),
                    GameplayType = GameplayType.Hardcore,
                    GameTime = GameTime.Everyday,
                },
                new UserGame()
                {
                    ID = 11,
                    User = context.Users.Find("58e27c4e-ad1d-4467-860f-7a9de9cbd383"),
                    Game = context.Games.Find(4),
                    GameplayType = GameplayType.Casual,
                    GameTime = GameTime.Weekdays,
                },
                new UserGame()
                {
                    ID = 12,
                    User = context.Users.Find("58e27c4e-ad1d-4467-860f-7a9de9cbd383"),
                    Game = context.Games.Find(11),
                    GameplayType = GameplayType.SemiHardcore,
                    GameTime = GameTime.Weekend,
                },

                new UserGame()
                {
                    ID = 13,
                    User = context.Users.Find("863ab1d1-8b61-4cc8-8b3e-ed4b6131d5ec"),
                    Game = context.Games.Find(1),
                    GameplayType = GameplayType.Hardcore,
                    GameTime = GameTime.Everyday,
                },
                new UserGame()
                {
                    ID = 14,
                    User = context.Users.Find("863ab1d1-8b61-4cc8-8b3e-ed4b6131d5ec"),
                    Game = context.Games.Find(5),
                    GameplayType = GameplayType.Casual,
                    GameTime = GameTime.Weekdays,
                },
                new UserGame()
                {
                    ID = 15,
                    User = context.Users.Find("863ab1d1-8b61-4cc8-8b3e-ed4b6131d5ec"),
                    Game = context.Games.Find(8),
                    GameplayType = GameplayType.Casual,
                    GameTime = GameTime.Weekend,
                },

                new UserGame()
                {
                    ID = 16,
                    User = context.Users.Find("a1fbad32-ced0-4a6e-ac5c-8537e60dd6bf"),
                    Game = context.Games.Find(12),
                    GameplayType = GameplayType.Hardcore,
                    GameTime = GameTime.Everyday,
                },
                new UserGame()
                {
                    ID = 17,
                    User = context.Users.Find("a1fbad32-ced0-4a6e-ac5c-8537e60dd6bf"),
                    Game = context.Games.Find(2),
                    GameplayType = GameplayType.Casual,
                    GameTime = GameTime.Weekdays,
                },
                new UserGame()
                {
                    ID = 18,
                    User = context.Users.Find("a1fbad32-ced0-4a6e-ac5c-8537e60dd6bf"),
                    Game = context.Games.Find(9),
                    GameplayType = GameplayType.Casual,
                    GameTime = GameTime.Weekend,
                },

                new UserGame()
                {
                    ID = 19,
                    User = context.Users.Find("d0130868-3362-4496-9093-77058e3a0afd"),
                    Game = context.Games.Find(11),
                    GameplayType = GameplayType.Hardcore,
                    GameTime = GameTime.Everyday,
                },
                new UserGame()
                {
                    ID = 20,
                    User = context.Users.Find("d0130868-3362-4496-9093-77058e3a0afd"),
                    Game = context.Games.Find(10),
                    GameplayType = GameplayType.Casual,
                    GameTime = GameTime.Weekdays,
                },
                new UserGame()
                {
                    ID = 21,
                    User = context.Users.Find("d0130868-3362-4496-9093-77058e3a0afd"),
                    Game = context.Games.Find(8),
                    GameplayType = GameplayType.Casual,
                    GameTime = GameTime.Weekend,
                },

                new UserGame()
                {
                    ID = 22,
                    User = context.Users.Find("e25285e8-3bff-4f7a-b0fb-f684be3d700c"),
                    Game = context.Games.Find(7),
                    GameplayType = GameplayType.Hardcore,
                    GameTime = GameTime.Everyday,
                },
                new UserGame()
                {
                    ID = 23,
                    User = context.Users.Find("e25285e8-3bff-4f7a-b0fb-f684be3d700c"),
                    Game = context.Games.Find(6),
                    GameplayType = GameplayType.Casual,
                    GameTime = GameTime.Weekdays,
                },
                new UserGame()
                {
                    ID = 24,
                    User = context.Users.Find("e25285e8-3bff-4f7a-b0fb-f684be3d700c"),
                    Game = context.Games.Find(4),
                    GameplayType = GameplayType.Casual,
                    GameTime = GameTime.Weekend,
                },
            };

            context.UserGames.AddOrUpdate(userGames.ToArray());
            context.SaveChanges();
        }
    }
}