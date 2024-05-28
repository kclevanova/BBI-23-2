using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace _lab7_2_2
{
    public class Program
    {
        [XmlInclude(typeof(Ludi))]
        [ProtoInclude(111, typeof(Ludi))]
        [Serializable]
        [ProtoContract]
        public abstract class Person
        {
            private string _name;
            private int[] _grades;
            protected static int index = 0;

            [XmlElement(ElementName = "Name")]
            [ProtoMember(1)]
            public string Name { get => _name; set => _name = value; }

            [XmlElement(ElementName = "Grades")]
            [ProtoMember(2)]
            public int[] Grades { get => _grades; set => _grades = value; }

            [XmlElement(ElementName = "Index")]
            [ProtoMember(3)]
            public static int Index { get => index; set => index = value; }

            public Person() { }
            public Person(string name, int[] grades)
            {
                _name = name;
                _grades = grades;
                index++;
            }

            public float AverageGrade()
            {
                float res = 0;
                for (int i = 0; i < _grades.Length; i++)
                {
                    if (_grades[i] == 2)
                    {
                        return 0;
                    }
                    res += _grades[i];
                }
                res = res / _grades.Length;
                return res;
            }


            public virtual void Print()
            {
                if (AverageGrade() != 0)
                {
                    Console.WriteLine($"{Name} {AverageGrade()}");
                }
                else
                {
                    Console.WriteLine($"{Name} отчислен");
                }
            }
        }

        [Serializable]
        [ProtoContract]
        public class Ludi : Person
        {
            private int _id;

            [XmlElement(ElementName = "ID")]
            [ProtoMember(1)]
            public int ID { get { return _id; } set { _id = value; } }

            public Ludi() { }
            public Ludi(string name, params int[] grades) : base(name, grades)
            {
                _id = index;
            }
            public override void Print()
            {
                if (AverageGrade() != 0)
                {
                    Console.WriteLine($"{Name,-20} {AverageGrade(),-10:F1} {ID,-10}");
                }
                else
                {
                    Console.WriteLine($"{Name,-20} отчислен    {ID,-10}");
                }
            }
        }

        static void Main()
        {
            Ludi[] ludi = new Ludi[5];
            ludi[0] = new Ludi("Горин", 3, 3, 3);
            ludi[1] = new Ludi("Фирсов", 3, 5, 3);
            ludi[2] = new Ludi("Каперин", 5, 3, 4);
            ludi[3] = new Ludi("Слуцкий", 4, 4, 5);
            ludi[4] = new Ludi("Полиш", 5, 5, 5);

            GnomeSort(ludi);

            foreach (Ludi student in ludi)
            {
                student.Print();
            }

            List<Person> personList = new();
            personList.AddRange(ludi);

            Stream file = File.Create(@"data\person.json");
            Stream data = JSONProcessing<Person>.Write(personList);
            data.CopyTo(file);
            file.Dispose();

            file = File.Create(@"data\person.xml");
            data = XMLProcessing<Person>.Write(personList);
            data.CopyTo(file);
            file.Dispose();

            file = File.Create(@"data\person.bin");
            data = BinProcessing<Person>.Write(personList);
            data.CopyTo(file);
            file.Dispose();

            personList = JSONProcessing<Person>.Read(new FileStream(@"data\person.json", FileMode.Open));
            Console.WriteLine("JSON файл:");
            for (int i = 0; i < personList.Count; i++)
            {
                personList[i].Print();
            }

            personList = XMLProcessing<Person>.Read(new FileStream(@"data\person.xml", FileMode.Open));
            Console.WriteLine("XML файл:");
            for (int i = 0; i < personList.Count; i++)
            {
                personList[i].Print();
            }

            personList = BinProcessing<Person>.Read(new FileStream(@"data\person.bin", FileMode.Open));
            Console.WriteLine("Bin файл:");
            for (int i = 0; i < personList.Count; i++)
            {
                personList[i].Print();
            }
        }
        static void GnomeSort(Ludi[] students)
        {
            int i = 1;
            int j = i + 1;
            while (i < students.Length)
            {
                if (i == 0 || students[i].AverageGrade() <= students[i - 1].AverageGrade())
                {
                    i = j;
                    j++;
                }
                else
                {
                    Ludi temp = students[i]; ;
                    students[i] = students[i - 1];
                    students[i - 1] = temp;
                    i--;
                }
            }
        }
    }
}
