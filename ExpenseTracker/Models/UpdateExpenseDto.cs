using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Models
{
    public class UpdateExpenseDto
    {
        [Required(ErrorMessage = "Expense name is required.")]
        [StringLength(100)]
        public required string Name { get; set; }
        [StringLength(500)]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Type of expense is required.")]
        public required string Type { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
