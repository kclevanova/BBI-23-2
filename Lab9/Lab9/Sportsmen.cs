using ProtoBuf;
using System.Xml.Serialization;


[Serializable]
[ProtoContract]
public class Sportsmen
{
    private string lastName;
    private string club;
    private double firstAttempt;
    private double secondAttempt;
    private bool disqualified;

    public double TotalDistance => firstAttempt + secondAttempt;

    [XmlElement(ElementName = "LastName")]
    [ProtoMember(1)]
    public string LastName { get => lastName; set => lastName = value; }

    [XmlElement(ElementName = "Club")]
    [ProtoMember(2)]
    public string Club { get => club; set => club = value; }

    [XmlElement(ElementName = "FirstAttempt")]
    [ProtoMember(3)]
    public double FirstAttempt { get => firstAttempt; set => firstAttempt = value; }

    [XmlElement(ElementName = "SecondAttempt")]
    [ProtoMember(4)]
    public double SecondAttempt { get => secondAttempt; set => secondAttempt = value; }

    [XmlElement(ElementName = "Disqualified")]
    [ProtoMember(5)]
    public bool Disqualified { get => disqualified; set => disqualified = value; }

    public Sportsmen() { }
    public Sportsmen(string lastName, string club, double firstAttempt, double secondAttempt)
    {
        this.lastName = lastName;
        this.club = club;
        this.firstAttempt = firstAttempt;
        this.secondAttempt = secondAttempt;
        this.disqualified = false;
    }

    public void Disqualify()
    {
        disqualified = true;
        firstAttempt = 0;
        secondAttempt = 0;
    }

    public string GetSportsmenInfo(int place)
    {
        if (TotalDistance > 0) { return $"Место {place} | Фамилия {lastName} \n"; }
        else { return $"{lastName} дисквалифицирован\n"; }

    }
}

class Program
{
    static void ShellSort(Sportsmen[] array)
    {
        int n = array.Length;
        for (int gap = n / 2; gap > 0; gap /= 2)
        {
            for (int i = gap; i < n; i++)
            {
                Sportsmen temp = array[i];
                int j;
                for (j = i; j >= gap && array[j - gap].TotalDistance < temp.TotalDistance; j -= gap)
                {
                    array[j] = array[j - gap];
                }
                array[j] = temp;
            }
        }
    }

    static void Main()
    {
        Sportsmen[] sports = new Sportsmen[5];

        sports[0] = new Sportsmen("Иванов", "Спартак", 5.6, 6.2);
        sports[1] = new Sportsmen("Петров", "ЦСК", 6.0, 5.8);
        sports[2] = new Sportsmen("Сидоров", "Локомотив", 6.5, 6.4);
        sports[3] = new Sportsmen("Смирнов", "Динамо", 6.6, 6.9);
        sports[4] = new Sportsmen("Кузнецов", "Торпедо", 2.1, 3.4);

        sports[0].Disqualify();

        ShellSort(sports);

        for (int i = 0; i < sports.Length; i++)
        {
            Console.Write(sports[i].GetSportsmenInfo(i + 1));
        }

        Console.WriteLine();

        List<Sportsmen> sportsmenList = new();
        sportsmenList.AddRange(sports);

        Stream file = File.Create(@"data\sportsmen.json");
        Stream data = JSONProcessing<Sportsmen>.Write(sportsmenList);
        data.CopyTo(file);
        file.Dispose();

        file = File.Create(@"data\sportsmen.xml");
        data = XMLProcessing<Sportsmen>.Write(sportsmenList);
        data.CopyTo(file);
        file.Dispose();

        file = File.Create(@"data\sportsmen.bin");
        data = BinProcessing<Sportsmen>.Write(sportsmenList);
        data.CopyTo(file);
        file.Dispose();

        sportsmenList = JSONProcessing<Sportsmen>.Read(new FileStream(@"data\sportsmen.json", FileMode.Open));
        Console.WriteLine("JSON файл:");
        for (int i = 0; i < sportsmenList.Count; i++)
        {
            Console.WriteLine(sportsmenList[i].GetSportsmenInfo(i + 1));
        }

        sportsmenList = XMLProcessing<Sportsmen>.Read(new FileStream(@"data\sportsmen.xml", FileMode.Open));
        Console.WriteLine("XML файл:");
        for (int i = 0; i < sportsmenList.Count; i++)
        {
            Console.WriteLine(sportsmenList[i].GetSportsmenInfo(i + 1));
        }

        sportsmenList = BinProcessing<Sportsmen>.Read(new FileStream(@"data\sportsmen.bin", FileMode.Open));
        Console.WriteLine("Bin файл:");
        for (int i = 0; i < sportsmenList.Count; i++)
        {
            Console.WriteLine(sportsmenList[i].GetSportsmenInfo(i + 1));
        }
    }
}
