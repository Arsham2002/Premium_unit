using System.ComponentModel.DataAnnotations;

namespace PremiumUnit.Models;

public enum ActivityType
{
    [Display(Name = "آزاد")]
    Manufacturing,
    [Display(Name = "عادی")]
    Maintenance,
    [Display(Name = "کمک دولتی")]
    Repair,
    [Display(Name = "هیچی")]
    Construction
} 

public class Workshop
{
    [Key]
    public int WorkshopCode { get; set; }
    public TimeSpan ListSubmissionInterval { get; set; }
    public ActivityType TypeOfActivity { get; set; }
    [DataType(DataType.Date)]
    public DateTime ActivityStartDate { get; set; }
    public string EmployerName { get; set; }
    public string EmployerPhoneNumber { get; set; }
    public List<Invoice>? Invoices { get; set; }
    public decimal? Premium { get; set; }
}
