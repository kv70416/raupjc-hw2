using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4
{
    public class University
    {
        public string Name { get; set; }
        public Student[] Students { get; set; }
    }

    public class HomeworkLinqQueries
    {
        public static string[] Linq1(int[] intArray)
        {
            string[] v = intArray.GroupBy(i => i)
                .OrderBy(i => i.FirstOrDefault())
                .Select(i => "Broj " + i.FirstOrDefault() + " ponavlja se " + i.Count() + " puta")
                .ToArray();

            return v;
        }
        public static University[] Linq2_1(University[] universityArray)
        {
            return universityArray.Where(u => u.Students.All(s => s.Gender == Gender.Male)).ToArray();
        }
        public static University[] Linq2_2(University[] universityArray)
        {
            return universityArray.Where(u => u.Students.Length < universityArray.Average(v => v.Students.Length))
                .ToArray();
        }
        public static Student[] Linq2_3(University[] universityArray)
        {
            return universityArray.SelectMany(u => u.Students).Distinct().ToArray();
        }
        public static Student[] Linq2_4(University[] universityArray)
        {
            return universityArray.Where(u =>
                    u.Students.All(s => s.Gender == Gender.Male) || u.Students.All(s => s.Gender == Gender.Female))
                .SelectMany(u => u.Students).Distinct().ToArray();
        }
        public static Student[] Linq2_5(University[] universityArray)
        {
            return universityArray.SelectMany(u => u.Students).GroupBy(s => s.Jmbag).Where(g => g.Count() > 1)
                .Select(g => g.FirstOrDefault()).ToArray();
        }
    }
}
