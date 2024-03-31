using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _lab7_2_2
{

    class Program
    {

        abstract class Person
        {
            private string _name;
            private int[] _grades;
            protected static int index = 0;

            protected string Name { get { return _name; } }
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

        class ludi : Person
        {
            private int _id;
            public int ID { get { return _id; } }
            public ludi(string name, int[] grades) : base(name, grades)
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
                    Console.WriteLine($"{Name,-20}отчислен    {ID,-10}");
                }
            }
        }

        static void Main()
        {
            ludi[] ludi = new ludi[5];
            ludi[0] = new ludi("Горин", [3, 3, 3]);
            ludi[1] = new ludi("Фирсов", [3, 5, 3]);
            ludi[2] = new ludi("Каперин", [5, 3, 4]);
            ludi[3] = new ludi("Слуцкий", [4, 4, 5]);
            ludi[4] = new ludi("Полиш", [5, 5, 5]);

            GnomeSort(ludi);

            foreach (ludi student in ludi)
            {
                student.Print();
            }

            Console.ReadLine();


        }
        static void GnomeSort(ludi[] students)
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
                    ludi temp = students[i]; ;
                    students[i] = students[i - 1];
                    students[i - 1] = temp;
                    i--;
                }
            }
        }
    }
}
