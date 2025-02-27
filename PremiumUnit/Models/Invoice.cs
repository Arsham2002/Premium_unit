using System.ComponentModel.DataAnnotations;

namespace PremiumUnit.Models;

public class Invoice
{
    [Key]
    public int Id { get; set; }
    public decimal Amount { get; set; }
    [DataType(DataType.Date)]
    public DateTime IssueDate { get; set; }
    [DataType(DataType.Date)]
    public DateTime? PaymentDate { get; set; }
    [DataType(DataType.Date)]
    public DateTime PenaltyBaseDate { get; set; }
}
