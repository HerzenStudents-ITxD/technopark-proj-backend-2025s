using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoparkProj.Core.Models
{
    public class StudentData
    {
        public StudentData(int id, string fullname) 
        {
            Id = id;
            FullName = fullname;
        }
        public int Id { get; set; }
        public string FullName { get; set; }
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        // внешние ключи
        public int SchoolId { get; set; }

        public School School { get; set; }

        public ICollection<StudProjLink> StudProjLinks { get; set; }
    }
}
