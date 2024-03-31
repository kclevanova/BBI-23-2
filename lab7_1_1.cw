public class Sportsmen
{
    private string lastName;
    private string club;
    private double firstAttempt;
    private double secondAttempt;
    private bool disqualified;

    public double TotalDistance => firstAttempt + secondAttempt;

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

        for (int i = 0; i < sports.Length ; i++)
        {
            Console.Write(sports[i].GetSportsmenInfo(i + 1));
        }

        Console.WriteLine();
    }
}
