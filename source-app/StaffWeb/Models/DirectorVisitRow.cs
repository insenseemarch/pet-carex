using System;

namespace StaffWeb.Models;

public class DirectorVisitRow
{
    public DateTime Date { get; set; }
    public int ExamCount { get; set; }
    public int VaccinationCount { get; set; }
    public int Total => ExamCount + VaccinationCount;
}
