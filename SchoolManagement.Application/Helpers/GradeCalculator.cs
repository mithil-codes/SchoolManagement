using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Helpers
{
    public static class GradeCalculator
    {
        public static string CalculateGrade(double percentage)
        {
            return percentage switch
            {
                >= 90 => "A+",
                >= 80 => "A",
                >= 70 => "B+",
                >= 60 => "B",
                >= 50 => "C",
                >= 40 => "D",
                _ => "F"
            };
        }
      
    }
}
