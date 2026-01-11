using System;
using System.Collections.Generic;

namespace KhachHangWeb.Models;

public class PetHistoryViewModel
{
    public string? CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public string? OrderId { get; set; }
    public string? PetId { get; set; }
    public string? PetName { get; set; }
    public string? Message { get; set; }

    public List<SelectOption> Pets { get; set; } = new();
    public List<PurchaseHistoryItem> Purchases { get; set; } = new();
    public List<ExamHistoryItem> Exams { get; set; } = new();
    public List<VaccineHistoryItem> Vaccines { get; set; } = new();
}

public class PurchaseHistoryItem
{
    public string OrderId { get; set; } = "";
    public DateTime OrderDate { get; set; }
    public string? BranchName { get; set; }
    public string? ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Amount { get; set; }
    public string? Status { get; set; }
    public string? PaymentMethod { get; set; }
}

public class ExamHistoryItem
{
    public string ServiceId { get; set; } = "";
    public DateTime ExamDate { get; set; }
    public string? DoctorId { get; set; }
    public string? DoctorName { get; set; }
    public string? Symptom { get; set; }
    public string? Diagnosis { get; set; }
    public DateOnly? RevisitDate { get; set; }
}

public class VaccineHistoryItem
{
    public int Stt { get; set; }
    public string VaccineId { get; set; } = "";
    public string? VaccineName { get; set; }
    public string? DoctorId { get; set; }
    public string? DoctorName { get; set; }
    public DateTime Date { get; set; }
    public string? Status { get; set; }
}
