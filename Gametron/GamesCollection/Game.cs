using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Gametron.GamesCollection
{
    public class Game
    {
        [JsonProperty("gameId")]
        public int ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }

        [JsonProperty("minPlayers")]
        public int MinPlayers { get; set; }

        [JsonProperty("maxPlayers")]
        public int MaxPlayers { get; set; }

        [JsonProperty("playingTime")]
        public int PlayingTime { get; set; }

        [JsonProperty("isExpansion")]
        public bool IsExpansion { get; set; }

        [JsonProperty("yearPublished")]
        public int Year { get; set; }

        [JsonProperty("bggRating")]
        public float BGGRating { get; set; }

        [JsonProperty("averageRating")]
        public float AverageRating { get; set; }


        private int _rank;

        [JsonProperty("rank")]
        public int Rank
        {
            get => _rank;
            set
            {
                _rank = value == -1 ? 2137 : value;
            }
        }

        [JsonProperty("numPlays")]
        public int NumberOfPlays { get; set; }

        [JsonProperty("rating")]
        public float Rating { get; set; }

        [JsonProperty("owned")]
        public bool Owned { get; set; }

        [JsonProperty("preordered")]
        public bool Preordered { get; set; }

        [JsonProperty("forTrade")]
        public bool ForTrade { get; set; }

        [JsonProperty("previousOwned")]
        public bool PreviousOwned { get; set; }

        [JsonProperty("want")]
        public bool Want { get; set; }

        [JsonProperty("wantToPlay")]
        public bool WantToPlay { get; set; }

        [JsonProperty("wantToBuy")]
        public bool WantToBuy { get; set; }

        [JsonProperty("wishList")]
        public bool WishList { get; set; }

        [JsonProperty("userComment")]
        public string UserComment { get; set; }
    }
}
