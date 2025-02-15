using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.DTOs
{
    public class VacationRequestDTO
    {
        public string EmployeeNumber { get; set; } = null!;
        public string Description { get; set; } = null!;
        public char VacationTypeCode { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}
