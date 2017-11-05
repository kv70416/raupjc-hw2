using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Student
    {
        public string Name { get; set; }
        public string Jmbag { get; set; }
        public Gender Gender { get; set; }
        public Student(string name, string jmbag)
        {
            Name = name;
            Jmbag = jmbag;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Student))
                return false;

            return this.Jmbag == ((Student) obj).Jmbag;
        }

        public override int GetHashCode()
        {
            return this.Jmbag.GetHashCode();
        }

        public static bool operator ==(Student s1, Student s2)
        {
            return s1.Jmbag == s2.Jmbag;
        }

        public static bool operator !=(Student s1, Student s2)
        {
            return s1.Jmbag != s2.Jmbag;
        }
    }

    public enum Gender
    {
        Male, Female
    }
   
}
