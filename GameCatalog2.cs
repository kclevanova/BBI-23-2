using System;
using otherClasses;

namespace GameCatalogs
{
    public partial class GameCatalog : IGameCatalog
    {
        public void DisplayPreference()
        {
            Console.WriteLine();
            Console.WriteLine("Особенности игр:");
            Console.WriteLine("{0,-40} {1,-25} {2,-15} {3,-25} {4,-25}", "Название", "Жанр", "Год релиза", "Игровые режимы" , "Поддерживаемые платформы");
            Console.WriteLine();
            foreach (var game in _games)
            {
                Console.WriteLine(game.ToString() + string.Format("{0,-25} {1,-25}", game.Type(),game.Platforms));
            }
        }
    }
}

