
using otherClasses;

namespace GameCatalogs
{
    public partial class GameCatalog : IGameCatalog
    {
        public void SortGames()
        {
            for (int i = 0; i < _games.Length - 1; i++)
            {
                for (int j = i + 1; j < _games.Length; j++)
                {
                    if (_games[i] > _games[j])
                    {
                        Game temp = _games[i];
                        _games[i] = _games[j];
                        _games[j] = temp;
                    }
                }
            }
        }
    }
}


