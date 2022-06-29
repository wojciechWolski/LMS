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
        public DateOnly BookLend { get; set; }
        public DateOnly BookReturn { get; set; }
    }
}
