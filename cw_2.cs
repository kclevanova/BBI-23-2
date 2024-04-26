using System;
using System.ComponentModel.Design;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

abstract class Task
{
    protected string text = "В нашем огромном мире большинство не задумываются о сохранении экологии и культурног наследия так был создано сообщество Разум. В нем огромное количество полезных умений и замечательных людей так все они помогают нашему миру. Давайте с ними!";
    public string Text
    {
        get { return text; }
        protected set { text = value; }
    }

    public virtual void ParseText() { }
    public Task(string text)
    {
        this.text = text;
    }
}
class Task1 : Task
{
    private string answer;
    public string Answer
    {
        get { return answer; }
        protected set { answer = value; }
    }
    [JsonConstructor]
    public Task1(string text) : base(text)
    {
        this.text = text;
        answer = "";
        ParseText();
    }
    public override void ParseText()
    {
        string[] sentences = text.Split(new char[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < sentences.Length; i++)
        {
            string[] words = sentences[i].Split(new char[] { ' ', ';', ':', '(', ')', '"', '-', ',' }, StringSplitOptions.RemoveEmptyEntries);
            answer += words[words.Length / 2] + "\n";
        }

    }
    public override string ToString()
    {
        return answer;
    }
}
class Task2 : Task
{
    [JsonConstructor]
    public Task2(string text) : base(text)
    {
        this.text = text;
        ParseText();
    }
    public override void ParseText()
    {
        int counter = 0;
        for (int i = 0; i < text.Length; i++)
        {
            if (char.IsLetter(text[i]))
            {
                char current;
                counter++;
                if (counter % 2 == 0)
                {
                    current = char.ToUpper(text[i]);
                }
                else
                {
                    current = char.ToLower(text[i]);
                }
                text = text.Remove(i, 1).Insert(i, current.ToString());
            }
        }
    }
    public override string ToString()
    {
        return text;
    }
}
class JsonIO
{
    public static void Write<T>(T obj, string filePath)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
        {
            JsonSerializer.Serialize(fs, obj);
        }
    }
    public static T Read<T>(string filePath)
    {
        using (FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate))
        {
            return JsonSerializer.Deserialize<T>(stream);
        }
        return default(T);
    }
}
class Program
{
    static void Main()
    {
        string text = "using System;
using System.ComponentModel.Design;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

abstract class Task
{
    protected string text = "";
    public string Text
    {
        get { return text; }
        protected set { text = value; }
    }

    public virtual void ParseText() { }
    public Task(string text)
    {
        this.text = text;
    }
}
class Task1 : Task
{
    private string answer;
    public string Answer
    {
        get { return answer; }
        protected set { answer = value; }
    }
    [JsonConstructor]
    public Task1(string text) : base(text)
    {
        this.text = text;
        answer = "";
        ParseText();
    }
    public override void ParseText()
    {
        string[] sentences = text.Split(new char[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < sentences.Length; i++)
        {
            string[] words = sentences[i].Split(new char[] { ' ', ';', ':', '(', ')', '"', '-', ',' }, StringSplitOptions.RemoveEmptyEntries);
            answer += words[words.Length / 2] + "\n";
        }

    }
    public override string ToString()
    {
        return answer;
    }
}
class Task2 : Task
{
    [JsonConstructor]
    public Task2(string text) : base(text)
    {
        this.text = text;
        ParseText();
    }
    public override void ParseText()
    {
        int counter = 0;
        for (int i = 0; i < text.Length; i++)
        { 
            if (char.IsLetter(text[i]))
            {
                char current;
                counter++;
                if(counter % 2 == 0)
                {
                    current = char.ToUpper(text[i]);
                }
                else
                {
                    current = char.ToLower(text[i]);
                }
                text = text.Remove(i, 1).Insert(i, current.ToString());
            }
        }
    }
    public override string ToString()
    {
        return text;
    }
}
class JsonIO
{
    public static void Write<T>(T obj, string filePath)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
        {
            JsonSerializer.Serialize(fs, obj);
        }
    }
    public static T Read<T>(string filePath)
    {
        using (FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate))
        {
            return JsonSerializer.Deserialize<T>(stream);
        }
        return default(T);
    }
}
class Program
{
    static void Main()
    {
        string text = "В нашем огромном мире большинство не задумываются о сохранении экологии и культурног наследия так был создано сообщество Разум. В нем огромное количество полезных умений и замечательных людей так все они помогают нашему миру. Давайте с ними!\";";
        Task[] tasks = {
            new Task1(text),
            new Task2(text)
        };
        Console.WriteLine(tasks[0]);
        Console.WriteLine(tasks[1]);

        string path = @"C:\Users\m2304156\Desktop";
        string folderName = "CW2";
        path = Path.Combine(path, folderName);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        string fileName1 = "cw2_task1.json";
        string fileName2 = "cw2_task2.json";

        fileName1 = Path.Combine(path, fileName1);
        fileName2 = Path.Combine(path, fileName2);
        if (!File.Exists(fileName1))
        {
            JsonIO.Write<Task1>(tasks[0] as Task1, fileName1);
        }
        else
        {
            var t1 = JsonIO.Read<Task2>(fileName1);
            Console.WriteLine(t1);
        }
        if (!File.Exists(fileName2))
        {
            JsonIO.Write<Task2>(tasks[1] as Task2, fileName2);
        }
        else
        {
            var t2 = JsonIO.Read<Task2>(fileName2);
            Console.WriteLine(t2);
        }
        Console.ReadKey();
    }
}
        Task[] tasks = {
            new Task1(text),
            new Task2(text)
        };
        Console.WriteLine(tasks[0]);
        Console.WriteLine(tasks[1]);

        string path = @"C:\Users\m2304156\Desktop";
        string folderName = "CW2";
        path = Path.Combine(path, folderName);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        string fileName1 = "cw2_task1.json";
        string fileName2 = "cw2_task2.json";

        fileName1 = Path.Combine(path, fileName1);
        fileName2 = Path.Combine(path, fileName2);
        if (!File.Exists(fileName1))
        {
            JsonIO.Write<Task1>(tasks[0] as Task1, fileName1);
        }
        else
        {
            var t1 = JsonIO.Read<Task2>(fileName1);
            Console.WriteLine(t1);
        }
        if (!File.Exists(fileName2))
        {
            JsonIO.Write<Task2>(tasks[1] as Task2, fileName2);
        }
        else
        {
            var t2 = JsonIO.Read<Task2>(fileName2);
            Console.WriteLine(t2);
        }
        Console.ReadKey();
    }
}
