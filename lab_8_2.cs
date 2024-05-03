using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

abstract class Task
{
    public string _text;
    public Task(string text) { _text = text; }
    public virtual void ParseText(string text) { }
}

class Task1 : Task
{
    private string result;
    public Task1(string text) : base(text) { }
    public override void ParseText(string text)
    {
        text = text.ToLower();
        string pattern = "[а-я]";
        Regex regex = new Regex(pattern);
        MatchCollection matches = regex.Matches(text);

        string newtext = "";

        if (matches.Count > 0)
        {
            foreach (Match match in matches)
            {
                newtext += match.Value;
            }
            Frequency(newtext);
        }

        else
        {
            Console.WriteLine("Нет русских букв");
        }
    }
    private void Frequency(string text)
    {
        int[] letterFrqsy = new int[32];
        for (int i = 0; i < 32; i++) { letterFrqsy[i] = 1072 + i; }
        double cnt = 0;
        for (int i = 0; i < letterFrqsy.Length; i++)
        {
            for (int j = 0; j < text.Length; j++)
            {
                if (text[j] == letterFrqsy[i]) { cnt++; }
            }
            result += $"Частота {(char)letterFrqsy[i]} составляет {cnt / text.Length * 100}%" + "\n";
            cnt = 0;
        }
    }
    public override string ToString()
    {
        return result;
    }
}

class Task3 : Task
{
    private string split;
    public Task3(string text) : base(text) { }
    public override void ParseText(string text)
    {
        string pattern = @".{1,50}(\s)";
        Regex regex = new Regex(pattern);
        MatchCollection matches = regex.Matches(text);

        if (matches.Count > 0)
        {
            foreach (Match match in matches)
            {
                split += match.Value + "\n";
            }
        }
    }
    public override string ToString()
    {
        return split;
    }
}

class Task6 : Task
{
    private string res = "Слово          Кол-во слогов\n----------------------------\n";
    public Task6(string text) : base(text) { }
    public override void ParseText(string text)
    {
        text = text.ToLower();
        string pattern = @"[.,!?;:-]";
        string textWithoutPunc = Regex.Replace(text, pattern, "");
        string[] words = textWithoutPunc.Split(' ');

        Regex regex = new Regex("[аеёиоуыэюяaeiouy]");

        MatchCollection match = regex.Matches(text);


        foreach (string word in words)
        {
            int cnt = regex.Matches(word).Count;
            if (cnt > 0) { res += $"{word,-20}{cnt}\n"; }
        }
    }
    public override string ToString()
    {
        return res;
    }
}
class Task12 : Task
{
    private WordCodes[] wordCode;
    private string[] decodedWords;
    public Task12(string text) : base(text) { }
    private struct WordCodes
    {
        private string _word;
        private string _code;
        public WordCodes(string word, string code)
        {
            _word = word;
            _code = code;
        }

        public string GetWord()
        {
            return _word;
        }

        public string GetCode()
        {
            return _code;
        }
    }

    public override void ParseText(string text)
    {
        wordCode =
        [
            new WordCodes("древесины", "AbCd"),
            new WordCodes("многих", "EFGH"),
            new WordCodes("разрушения", "IjKl"),
            new WordCodes("движение", "MnOp"),
            new WordCodes("расширению", "QrSt"),
            new WordCodes("себя", "UvWx"),
            new WordCodes("дефолта", "YzIx"),
            new WordCodes("международных", "kIpj"),
            new WordCodes("финансовой", "gSaf"),
            new WordCodes("Фьорды", "HAdt"),
            new WordCodes("окруженные", "LAUb"),
            new WordCodes("продолжая", "JncS"),
            new WordCodes("and", "HSbc"),
            new WordCodes("around", "QyuZ"),
            new WordCodes("the", "TvxS"),
            new WordCodes("путешествие", "UySx"),
            new WordCodes("круглую", "PokS"),
            new WordCodes("значение", "TuZX"),
        ];

        string[] words = text.Split(' ');
        CodingWords(words);
        DecodingWords(words);
    }

    private void CodingWords(string[] words)
    {
        string[] codedWords = new string[words.Length];
        for (int i = 0; i < words.Length; i++)
        {
            codedWords[i] = words[i];
        }

        for (int i = 0; i < codedWords.Length; i++)
        {
            for (int j = 0; j < wordCode.Length; j++)
            {
                if (codedWords[i] == wordCode[j].GetWord()) { codedWords[i] = wordCode[j].GetCode(); }
            }
        }
    }
    private void DecodingWords(string[] codedWords)
    {
        decodedWords = new string[codedWords.Length];
        for (int i = 0; i < codedWords.Length; i++)
        {
            decodedWords[i] = codedWords[i];
        }
        {
            for (int i = 0; i < decodedWords.Length; i++)
            {
                for (int j = 0; j < wordCode.Length; j++)
                {
                    if (decodedWords[i] == wordCode[j].GetCode()) { decodedWords[i] = wordCode[j].GetWord(); }
                }
            }
        }
    }
    public override string ToString()
    {
        Console.WriteLine("Таблица ключей-кодов\n");
        foreach (WordCodes key in wordCode) { Console.WriteLine($"{key.GetWord(), -20}{key.GetCode()}"); }
        Console.WriteLine("-------------------------");
        string result = "";
        foreach (string word in decodedWords)
        {
            result += word + " ";
        }
        return result;
    }
}

class Task13 : Task
{
    private string result;
    private int totalWords = 0;
    public Task13(string text) : base(text) { }

    public override void ParseText(string text)
    {
        text = text.ToLower();
        string firstletter = "";
        Regex regex = new Regex(@"\b[а-яa-z]\w*\b");
        MatchCollection matches = regex.Matches(text);

        totalWords = matches.Count;

        if (matches.Count > 0)
        {
            foreach (Match match in matches)
            {
                firstletter += match.Value[0];
            }
        }

        Frqsy(firstletter);
    }

    private void Frqsy(string text)
    {
        char[] chars = text.ToCharArray();
        int[] codes = new int[chars.Length];
        for (int i = 0; i < chars.Length; i++) { codes[i] = (int)chars[i]; }

        for (int i = 0; i < codes.Length; i++)
        {
            if (codes[i] == -1)
            {
                continue;
            }

            int cnt = 1;
            for (int j = i + 1; j < codes.Length; j++)
            {
                if (codes[i] == codes[j])
                {
                    cnt++;
                    codes[j] = -1;
                }
            }
            if (cnt >= 1)
            {
                double frequency = (double)cnt / totalWords;
                result += $"На букву {(char)codes[i]} встречается с частотой {((double)cnt / totalWords*100):f2}%" + "\n";
            }
        }
    }

    public override string ToString()
    {

        return result;
    }
}

class Task15 : Task
{
    private string result;
    public Task15(string text) : base(text) { }

    public override void ParseText(string text)
    {
        Regex regex = new Regex(@"[0-9]+");
        MatchCollection matches = regex.Matches(text);

        int sum = 0;
        if (matches.Count > 0)
        {
            foreach (Match match in matches)
            {
                sum += Convert.ToInt32(match.Value);
            }
            result += sum.ToString();
        }
    }
    public override string ToString()
    {
        return result;
    }
}

class Program
{
    static void Main()
    {
        Task1 task1 = new Task1("Первое кругосветное путешествие было осуществлено флотом, возглавляемым португальским исследователем Фернаном Магелланом. Путешествие началось 20 сентября 1519 года, когда флот, состоящий из пяти кораблей и примерно 270 человек, отправился из порту Сан-Лукас в Испании. Хотя Магеллан не закончил свое путешествие из-за гибели в битве на Филиппинах в 1521 году, его экспедиция стала первой, которая успешно обогнула Землю и доказала ее круглую форму. Это путешествие открыло новые морские пути и имело огромное значение для картографии и географических открытий. ");
        task1.ParseText(task1._text);
        Console.WriteLine(task1.ToString());

        Task3 task3 = new Task3("Фьорды – это ущелья, формирующиеся ледниками и заполняющиеся морской водой. Название происходит от древнескандинавского слова \"fjǫrðr\". Эти глубокие заливы, окруженные высокими горами, представляют захватывающие виды и природную красоту. Они популярны среди туристов и известны под разными названиями: в Норвегии – \"фьорды\", в Шотландии – \"фьордс\", в Исландии – \"фьордар\". Фьорды играют важную роль в культуре и туризме региона, продолжая вдохновлять людей со всего мира.");
        task3.ParseText(task3._text);
        Console.WriteLine(task3.ToString());

        Task6 task6 = new Task6("Двигатель самолета – это сложная инженерная конструкция, обеспечивающая подъем, управление и движение в воздухе. Он состоит из множества компонентов, каждый из которых играет важную роль в общей работе механизма. Внутреннее устройство двигателя включает в себя компрессор, камеру сгорания, турбину и системы управления и охлаждения. Принцип работы основан на воздушно-топливной смеси, которая подвергается сжатию, воспламенению и расширению, обеспечивая движение воздушного судна.");
        task6.ParseText(task6._text);
        Console.WriteLine(task6.ToString());

        Task12 task12 = new Task12("William Shakespeare, widely regarded as one of the greatest writers in the English language, authored a total of 37 plays, along with numerous poems and sonnets. He was born in Stratford-upon-Avon, England, in 1564, and died in 1616. Shakespeare's most famous works, including \"Romeo and Juliet,\" \"Hamlet,\" \"Macbeth,\" and \"Othello,\" were written during the late 16th and early 17th centuries. \"Romeo and Juliet,\" a tragic tale of young love, was penned around 1595. \"Hamlet,\" one of his most celebrated tragedies, was written in the early 1600s, followed by \"Macbeth,\" a gripping drama exploring themes of ambition and power, around 1606. \"Othello,\" a tragedy revolving around jealousy and deceit, was also composed during this period, believed to be around 1603.");
        task12.ParseText(task12._text);
        Console.WriteLine(task12.ToString());

        Task13 task13 = new Task13("Фьорды – это ущелья, формирующиеся ледниками и заполняющиеся морской водой. Название происходит от древнескандинавского слова \"fjǫrðr\". Эти глубокие заливы, окруженные высокими горами, представляют захватывающие виды и природную красоту. Они популярны среди туристов и известны под разными названиями: в Норвегии – \"фьорды\", в Шотландии – \"фьордс\", в Исландии – \"фьордар\". Фьорды играют важную роль в культуре и туризме региона, продолжая вдохновлять людей со всего мира.");
        task13.ParseText(task13._text);
        Console.WriteLine(task13.ToString());

        Task15 task15 = new Task15("1 июля 2015 года Греция объявила о дефолте по государственному долгу, став первой развитой страной в истории, которая не смогла выплатить свои долговые обязательства в полном объеме. Сумма дефолта составила порядка 1,6 миллиарда евро. Этому предшествовали долгие переговоры с международными кредиторами, такими как Международный валютный фонд (МВФ), Европейский центральный банк (ЕЦБ) и Европейская комиссия (ЕК), о программах финансовой помощи и реструктуризации долга. Основными причинами дефолта стали недостаточная эффективность реформ, направленных на улучшение финансовой стабильности страны, а также политическая нестабильность, что вызвало потерю доверия со стороны международных инвесторов и кредиторов. Последствия дефолта оказались глубокими и долгосрочными: сокращение кредитного рейтинга страны, увеличение затрат на заемный капитал, рост стоимости заимствований и утрата доверия со стороны международных инвесторов.");
        task15.ParseText(task15._text);
        Console.WriteLine(task15.ToString());

    }
}
