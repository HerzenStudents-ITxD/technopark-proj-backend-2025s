using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoparkProj.Core.Models
{
    public class StudProjLink
    {
        public int Id { get; set; }

        // внешние ключи
        public int StudentId { get; set; }
        public int ProjectId { get; set; }

        public Student Student { get; set; }
        public Project Project { get; set; }
    }
}
