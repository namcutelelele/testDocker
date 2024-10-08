using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingSpace_BusinessObject.DTOs
{
    public class StudentNoteBookDTO
    {
        public int WorkingSpaceID { get; set; }
        public string WorkingSpaceDisplayName { get; set; }
        public int StudentId { get; set; }
        public int WorkbookID { get; set; }
        public string WorkbookName { get; set; }
        public string WorkbookDescription { get; set; }
    }
}
