
using Serializers;
using otherClasses;
using GameCatalogs;
using System;
using System.IO;

class Program
{
    static void Main()
    {
        Game[] games = new Game[30]{ new SingleGame("The Witcher 3: Wild Hunt", "RPG", 2015),
            new SingleGame("Celeste", "Platformer", 2018),
            new SingleGame("The Elder Scrolls V: Skyrim", "RPG", 2011),
            new SingleGame("Monument Valley", "Puzzle", 2014),
            new SingleGame("Subnautica", "Survival", 2018),
            new SingleGame("Stardew Valley", "Simulation", 2016),
            new SingleGame("Limbo", "Platformer", 2010),
            new SingleGame("Firewatch", "Adventure", 2016),
            new SingleGame("Inside", "Adventure", 2016),
            new SingleGame("Papers, Please", "Puzzle", 2013),
            new OnlineGame("Fortnite", "Battle Royale", 2017),
            new OnlineGame("League of Legends", "MOBA", 2009),
            new OnlineGame("Counter-Strike: Global Offensive", "FPS", 2012),
            new OnlineGame("World of Warcraft", "MMORPG", 2004),
            new OnlineGame("PUBG Mobile", "Battle Royale", 2018),
            new OnlineGame("Dota 2", "MOBA", 2013),
            new OnlineGame("Apex Legends", "Battle Royale", 2019),
            new OnlineGame("Call of Duty: Mobile", "FPS", 2019),
            new OnlineGame("Valorant", "FPS", 2020),
            new OnlineGame("Genshin Impact", "Action RPG", 2020),
            new MultiplayerGame("Minecraft", "Sandbox", 2011),
            new MultiplayerGame("Overwatch", "FPS", 2016),
            new MultiplayerGame("Rocket League", "Sports", 2015),
            new MultiplayerGame("Rainbow Six Siege", "FPS", 2015),
            new MultiplayerGame("Among Us", "Social Deduction", 2018),
            new MultiplayerGame("FIFA 21", "Sports", 2020),
            new MultiplayerGame("Dead by Daylight", "Horror", 2016),
            new MultiplayerGame("Rust", "Survival", 2013),
            new MultiplayerGame("Warframe", "Action RPG", 2013),
            new MultiplayerGame("Sea of Thieves", "Adventure", 2018)
        };
        Game[] games1 = new Game[10];
        Array.Copy(games, games1, 10);
        GameCatalog gamecatalog1 = new GameCatalog(games1);
 
        MySerializer[] serializers = new MySerializer[] { new MyJsonSerializer(), new MyXmlSerializer() };

        string[] files = new string[] { "raw_data.json", "raw_data.xml" };

        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        for (int i = 0; i < serializers.Length; i++)
        {
            serializers[i].Write(gamecatalog1, Path.Combine(path, files[i]));
        }
        Game[] games2 = new Game[10];
        Array.Copy(games, 10, games2,0, 10);
        gamecatalog1.AddGame(games2);
        gamecatalog1.AddGame(games[20]);

        serializers[0].Write(gamecatalog1, Path.Combine(path, "data.json"));
        
        for (int i = 0; i < 5; i++)
        {
            gamecatalog1.RemoveGame();
        }
        gamecatalog1.RemoveGame(6);
        serializers[1].Write(gamecatalog1, Path.Combine(path, "data.xml"));
        
        gamecatalog1.DisplayCatalog();

        Console.WriteLine();
        Console.WriteLine("Десериализация:");
        Console.WriteLine();
        string[] allFiles = new string[4] { "raw_data.json", "data.json", "data.xml", "raw_data.xml" };
        for (int i=0;i<allFiles.Length; i++)
        {
            GameCatalog gamecatalog2 = serializers[i/2].Read<GameCatalog>(Path.Combine(path, allFiles[i]));
            Console.WriteLine();
            Console.WriteLine($"From {allFiles[i]}");
            Console.WriteLine();
            gamecatalog2.DisplayCatalog();

        }
        

        MyBinSerializer binSerializer= new MyBinSerializer();
        Game[] games3 = new Game[10];
        
        Array.Copy(games, 20, games3, 0, 10);
        
        gamecatalog1.AddGame(games3);
        binSerializer.Write(gamecatalog1, Path.Combine(path, "raw_data.bin"));

        gamecatalog1.SortGames();
        binSerializer.Write(gamecatalog1, Path.Combine(path, "sorted_data.bin"));

        gamecatalog1.DisplayPreference();

        gamecatalog1.RemoveAll();
        string[] binFiles= new string[] { "raw_data.bin", "sorted_data.bin" };
        for (int i=0; i<binFiles.Length; i++)
        {
            Console.WriteLine();
            Console.WriteLine($"From {binFiles[i]}");
            Console.WriteLine();
            GameCatalog gamecatalog6 = binSerializer.Read<GameCatalog>(Path.Combine(path, binFiles[i]));
            gamecatalog6.DisplayCatalog();
        }
        Console.ReadKey();
    }
}
