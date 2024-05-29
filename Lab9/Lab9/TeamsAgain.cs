using ProtoBuf;
using System.Xml.Serialization;

[XmlInclude(typeof(WomanTeam))]
[XmlInclude(typeof(ManTeam))]
[ProtoInclude(123, typeof(WomanTeam))]
[ProtoInclude(321, typeof(ManTeam))]
[Serializable]
[ProtoContract]
public class Team
{
    protected string _name;
    protected int _totalScore = 0;
    protected int _delta = 0;

    [XmlElement(ElementName = "Name")]
    [ProtoMember(1)]
    public string Name { get { return _name; } set { _name = value; } }

    [XmlElement(ElementName = "TotalScore")]
    [ProtoMember(2)]
    public int TotalScore { get { return _totalScore; } set { _totalScore = value; } }

    [XmlElement(ElementName = "Delta")]
    [ProtoMember(3)]
    public int Delta { get { return _delta; } set { _delta = value; } }

    public Team() { }
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

[Serializable]
[ProtoContract]
public class ManTeam : Team
{
    public ManTeam() { }
    public ManTeam(string name) : base(name)
    {
        _name = name + " мужская";
    }
}

[Serializable]
[ProtoContract]
public class WomanTeam : Team
{
    public WomanTeam() { }
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
                for (int j = i; j > 0 && ((teams[j].TotalScore > teams[j - 1].TotalScore) || (teams[j].TotalScore == teams[j - 1].TotalScore && teams[j].Delta > teams[j - 1].Delta)); j--)
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

                Match(man_teams[i], man_teams[j], score1, score2);
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

                Match(woman_teams[i], woman_teams[j], score1, score2);
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
        List<Team> teamList = new();
        teamList.AddRange(man_teams);
        teamList.AddRange(woman_teams);


        Stream file = File.Create(@"data\team.json");
        Stream data = JSONProcessing<Team>.Write(teamList);
        data.CopyTo(file);
        file.Dispose();

        file = File.Create(@"data\team.xml");
        data = XMLProcessing<Team>.Write(teamList);
        data.CopyTo(file);
        file.Dispose();

        file = File.Create(@"data\team.bin");
        data = BinProcessing<Team>.Write(teamList);
        data.CopyTo(file);
        file.Dispose();

        teamList = JSONProcessing<Team>.Read(new FileStream(@"data\team.json", FileMode.Open));
        Console.WriteLine("JSON файл:");
        for (int i = 0; i < teamList.Count; i++)
        {
            teamList[i].Print();
        }

        teamList = XMLProcessing<Team>.Read(new FileStream(@"data\team.xml", FileMode.Open));
        Console.WriteLine("XML файл:");
        for (int i = 0; i < teamList.Count; i++)
        {
            teamList[i].Print();
        }

        teamList = BinProcessing<Team>.Read(new FileStream(@"data\team.bin", FileMode.Open));
        Console.WriteLine("Bin файл:");
        for (int i = 0; i < teamList.Count; i++)
        {
            teamList[i].Print();
        }
    }
}