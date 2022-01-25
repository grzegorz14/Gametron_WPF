using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Gametron.GamesCollection
{
    class GamesList
    {
        public static List<Game> ImportGamesFromBGGJson()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "Gametron.Resources.siefczyk.json";
            string jsonGames;

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    jsonGames = reader.ReadToEnd();
                }
            }

            return JsonConvert.DeserializeObject<List<Game>>(jsonGames);
        }
    }
}
