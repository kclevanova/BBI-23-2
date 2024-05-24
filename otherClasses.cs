using System;

using System.Text.Json.Serialization;
using ProtoBuf;
using System.Xml.Serialization;



namespace otherClasses
{
    public interface IGameCatalog
    {
        void AddGame(Game game);
        void RemoveGame();
        void AddGame(Game[] games);
        void RemoveGame(int ind);
        void DisplayCatalog();
        void DisplayPreference();
        void SortGames();
        void RemoveAll();
    }
    public interface IPlatform
    {
        
    }
    public interface IConsoleable : IPlatform
    {
        void PlayOnConsole();
    }
    public interface IComputerable : IPlatform
    {
        void PlayOnComputer();
    }
    public interface IMobile : IPlatform
    {
        void PlayOnMobile();
    }
    [Serializable]
    [ProtoContract]
    [ProtoInclude(5, typeof(SingleGame))]
    [ProtoInclude(6, typeof(MultiplayerGame))]
    [ProtoInclude(7, typeof(OnlineGame))]
    [XmlInclude(typeof(SingleGame))]
    [XmlInclude(typeof(SingleGame))]
    [XmlInclude(typeof(OnlineGame))]
    public class Game
    {
        [JsonIgnore]
        [NonSerialized]
        protected string _title;
        [JsonIgnore]
        [NonSerialized]
        protected string _genre;
        [JsonIgnore]
        [NonSerialized]
        protected int _releaseYear;
        [JsonIgnore]
        [NonSerialized]
        protected string _platforms;
        [ProtoMember(2)]
        [JsonInclude]
        public string Title
        {
            get { return _title; }
            set { _title = value ?? string.Empty; }
        }
        [ProtoMember(3)]
        [JsonInclude]
        public string Genre
        {
            get { return _genre; }
            set { _genre = value ?? string.Empty; }
        }
        [ProtoMember(4)]
        [JsonInclude]
        public int ReleaseYear
        {
            get { return _releaseYear; }
            set
            {
                if (value > 0) _releaseYear = value;
            }
        }
        [JsonIgnore]
        
        public virtual string Platforms
        {

            get => _platforms;
            set => _platforms = value ?? string.Empty;
        }
        
        public override string ToString()
        {
            return string.Format("{0,-40} {1,-25} {2,-15}", _title, _genre, _releaseYear);
        }
        public virtual string Type() { return "general"; }

        [JsonConstructor]
        public Game(string title, string genre, int releaseYear)
        {
            _title = title;
            _genre = genre;
            _releaseYear = releaseYear;
            _platforms = String.Empty;
        }
        public Game()
        {

        }
        public static bool operator ==(Game game1, Game game2)
        {
            return game1._title == game2._title;
        }
        public static bool operator !=(Game game1, Game game2)
        {
            return game1._title != game2._title;
        }
        public static bool operator >(Game game1, Game game2)
        {
            return string.Compare(game1._title, game2._title, StringComparison.OrdinalIgnoreCase) > 0;
        }
        public static bool operator <(Game game1, Game game2)
        {
            return string.Compare(game1._title, game2._title, StringComparison.OrdinalIgnoreCase) < 0;
        }

    }
    [Serializable]
    [ProtoContract]
    public class SingleGame : Game, IComputerable, IConsoleable
    {
        [JsonConstructor]
        public SingleGame(string title, string genre, int releaseYear) : base(title, genre, releaseYear)
        {
            _platforms = "Console, Computer";
        }
        
        public SingleGame() : base()
        {

        }
        public override string Type()
        {
            return "Single";
        }
        

        public void PlayOnComputer()
        {
            Console.WriteLine($"{Title} - Playing on computer.");
        }

        public void PlayOnConsole()
        {
            Console.WriteLine($"{Title} - Playing on console.");
        }
    }
    [Serializable]
    [ProtoContract]
    public class MultiplayerGame : Game, IConsoleable, IComputerable, IMobile
    {
        public override string Type()
        {
            return "Multiplayer";
        }
        
        [JsonConstructor]
        public MultiplayerGame(string title, string genre, int releaseYear) : base(title, genre, releaseYear)
        {
            _platforms = "Console, Computer, Mobile";
        }
        
        public MultiplayerGame() : base()
        {

        }
        public void PlayOnComputer()
        {
            Console.WriteLine($"{Title} - Playing on computer.");
        }
        public void PlayOnConsole()
        {
            Console.WriteLine($"{Title} - Playing on console.");
        }
        public void PlayOnMobile()
        {
            Console.WriteLine($"{Title} - Playing on mobile");
        }
    }
    [Serializable]
    [ProtoContract]
    public class OnlineGame : Game, IComputerable, IMobile
    {
        [JsonConstructor]
        public OnlineGame(string title, string genre, int releaseYear) : base(title, genre, releaseYear)
        {
            _platforms = "Computer, Mobile";
        }
        
        public OnlineGame() : base()
        {

        }
        public override string Type()
        {
            return "Online";
        }
        
        public void PlayOnComputer()
        {
            Console.WriteLine($"{Title} - Playing on computer.");
        }
        public void PlayOnMobile()
        {
            Console.WriteLine($"{Title} - Playing on mobile");
        }
    }
}
