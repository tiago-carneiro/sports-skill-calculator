using LiteDB;

namespace TrueSport
{
    public static class StoreService
    {
        static ILiteDatabase _instance;
        static object __lock = new object();
        static string Path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "database.db");

        public static ILiteDatabase Instance
        {
            get
            {
                lock (__lock)
                {
                    if (_instance == null)
                    {
                        _instance = new LiteDatabase(Path);
                        InitializeDatabase();
                    }

                    return _instance;
                }
            }
        }

        static void InitializeDatabase()
        {
            if (VersionTracking.IsFirstLaunchEver)
            {
                var friendsList = new List<string> { "Layla Lynch",
                                                "Cristopher Kline",
                                                "Aria Graves",
                                                "Melody Mcguire",
                                                "Samson Wolfe",
                                                "Rigoberto Weiss",
                                                "Tripp Flores",
                                                "Mattie Greer",
                                                "Lila Montoya",
                                                "Kolton Lynn",
                                                "Ryan Kelley",
                                                "Mina Bernard" };

                var collection = _instance.GetCollection<FriendModel>();
                friendsList.ForEach(fn => collection.Insert(new FriendModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = fn,
                    Skill = new Random().Next(0, 10)
                }));

                _instance.GetCollection<UserModel>().Insert(new UserModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Tiago Carneiro",
                    Skill = new Random().Next(0, 10)
                });
            }
        }
    }
}