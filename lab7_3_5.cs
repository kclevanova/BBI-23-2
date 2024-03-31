class Team
{
    protected string _name;
    protected int _totalScore = 0;
    protected int _delta = 0;
    public string Name { get { return _name; } }
    public int Score { get { return _totalScore; } set { _totalScore = value; } }
    public int Delta { get { return _delta; } set { _delta = value; } }

    public Team(string name)
    {
        _name = name;
    }
    public void Print() => Console.WriteLine("{0}, {1} очков", _name, _totalScore);

    public void Win(int delta)
    {
        _totalScore += 3;
        _delta += delta;
    }
    public void Lose(int delta)
    {
        _delta -= delta;
    }
    public void Draw()
    {
        _totalScore += 1;
    }

}
class ManTeam : Team
{
    public ManTeam(string name) : base(name)
    {
        _name = name + " мужская";
    }
}
class WomanTeam : Team
{
    public WomanTeam(string name) : base(name)
    {
        _name = name + " женская";
    }
}

class Program
{
    static void Main()
    {
        static void Match(Team team1, Team team2, int score1, int score2)
        {
            Console.WriteLine(team1.Name + " - " + team2.Name + " " + score1 + ":" + score2);
            int delta = Math.Abs(score1 - score2);
            if (score1 > score2)
            {
                team1.Win(delta);
                team2.Lose(delta);
            }
            else if (score1 < score2)
            {
                team2.Win(delta);
                team1.Lose(delta);
            }
            else
            {
                team1.Draw();
                team2.Draw();
            }
        }
        static void SortTeams(Team[] teams)
        {
            Team tmp;
            for (int i = 1; i < teams.Length; i++)
            {
                for (int j = i; j > 0 && ((teams[j].Score > teams[j - 1].Score) || (teams[j].Score == teams[j - 1].Score && teams[j].Delta > teams[j - 1].Delta)); j--)
                {
                    tmp = teams[j];
                    teams[j] = teams[j - 1];
                    teams[j - 1] = tmp;
                }
            }
        }

        Team[] teams = new Team[8];
        Team[] man_teams = new Team[4];
        Team[] woman_teams = new Team[4];
        Random random = new Random();
        int score1, score2;
        for (int i = 0; i < 4; i++)
        {
            man_teams[i] = new ManTeam("Команда " + (i + 1));
        }

        for (int i = 0; i < 4; i++)
        {
            for (int j = i + 1; j < 4; j++)
            {
                score1 = random.Next(0, 6);
                score2 = random.Next(0, 6);

                Match(man_teams[i],  man_teams[j], score1, score2);
            }
        }
        Console.WriteLine();
        for (int i = 0; i < 4; i++)
        {
            woman_teams[i] = new WomanTeam("Команда " + (i + 1));
        }
        for (int i = 0; i < 4; i++)
        {
            for (int j = i + 1; j < 4; j++)
            {
                score1 = random.Next(0, 6);
                score2 = random.Next(0, 6);

                Match( woman_teams[i], woman_teams[j], score1, score2);
            }
        }
        for (int i = 0; i < 4; i++)
        {
            teams[i] = man_teams[i];
        }
        for (int i = 0; i < 4; i++)
        {
            teams[i + 4] = woman_teams[i];
        }
        SortTeams(teams);
        Console.WriteLine();
        for (int i = 0; i < 8; i++)
        {
            Console.Write((i + 1) + " место - ");
            teams[i].Print();
        }
    }
}
