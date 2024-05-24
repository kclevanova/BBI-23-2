using System;
using otherClasses;

using ProtoBuf;
using System.Text.Json.Serialization;

namespace GameCatalogs
{
    [Serializable]
    [ProtoContract]
    public partial class GameCatalog : IGameCatalog
    {
        [JsonIgnore]
        [NonSerialized]
        private Game[] _games;
        [ProtoMember(1)]
        [JsonInclude]
        public Game[] Games
        {
            get { return _games; }
            set { _games = value; }
        }
        [JsonConstructor]
        public GameCatalog(Game[] games)
        {
            _games = games;
        }
        public GameCatalog() { }
        public void AddGame(Game game)
        {
            Game[] newgames = new Game[_games.Length + 1];
            for (int i = 0; i < _games.Length; i++)
            {
                newgames[i] = _games[i];
            }
            newgames[newgames.Length - 1] = game;
            _games = newgames;
        }
        public void RemoveGame()
        {
            Game[] newgames = new Game[_games.Length - 1];
            for (int i = 0; i < newgames.Length; i++)
            {
                newgames[i] = _games[i];
            }
            _games = newgames;
        }
        public void AddGame(Game[] games)
        {
            Game[] newgames = new Game[_games.Length + games.Length];

            for (int i = 0; i < _games.Length; i++)
            {
                newgames[i] = _games[i];
            }

            for (int i = 0; i < games.Length; i++)
            {
                newgames[i + _games.Length] = games[i];
            }

            _games = newgames;
        }
        public void RemoveGame(int ind)
        {
            Game[] newgames = new Game[_games.Length - 1];
            for (int i = 0; i < ind; i++)
            {
                newgames[i] = _games[i];
            }
            for (int i = ind + 1; i < _games.Length; i++)
            {
                newgames[i - 1] = _games[i];
            }
            _games = newgames;
        }
        public void DisplayCatalog()
        {
            Console.WriteLine();
            Console.WriteLine("{0,-40} {1,-25} {2,-15}", "Название", "Жанр", "Год релиза");
            Console.WriteLine();
            foreach (var game in _games)
            {
                Console.WriteLine(game.ToString());
            }
        }
        public void RemoveAll()
        {
            for (int i=0; i < _games.Length; i++)
            {
                RemoveGame();
            }
        }
    }
}

