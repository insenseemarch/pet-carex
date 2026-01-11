namespace StaffWeb.Models;

public class ProcessVaccinationViewModel
{
    public int Stt { get; set; }
    public string PetId { get; set; } = "";
    public string PetName { get; set; } = "";
    public string ServiceId { get; set; } = "";
    public string ServiceName { get; set; } = "";
    public string SelectedVaccineId { get; set; } = "";
    public DateTime VisitDate { get; set; }
    public string Status { get; set; } = "";
    public List<VaccineOption> AvailableVaccines { get; set; } = new();
}

public class VaccineOption
{
    public string MaVacxin { get; set; } = "";
    public string TenVacxin { get; set; } = "";
}
