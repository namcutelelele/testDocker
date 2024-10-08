using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingSpace_BusinessObject.DTOs
{
    public class AddNewAndDeleteWorkbookInWorkingSpaceDTO
    {
        public string? workingSpaceName { get; set; }
        public int studentID { get; set; }
        public int workbookID { get; set; }
    }
}
