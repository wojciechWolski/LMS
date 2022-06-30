using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Models
{
    public class Borrow
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int StudentId { get; set; }
        public string BookTitle { get; set; } = null!;
        public string StudentEnroll { get; set; } = null!;
        
        public DateTime BookLend { get; set; }
        public DateTime? BookReturn { get; set; }
    }
}
