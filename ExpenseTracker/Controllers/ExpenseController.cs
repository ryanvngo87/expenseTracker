using ExpenseTracker.Data;
using ExpenseTracker.Models;
using ExpenseTracker.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ExpenseController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAllExpenses()
        {
            try
            {
                var expenses = _context.Expense.ToList();
                return Ok(expenses);
            } catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetExpense(int id)
        {
            try
            {
                var expense = _context.Expense.Find(id);
                if (expense == null)
                {
                    return NotFound();
                }
                return Ok(expense);
            } catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost]
        public IActionResult AddExpense(AddExpenseDto addExpenseDto)
        {
            try
            {
                var expense = new Expense
                {
                    Name = addExpenseDto.Name,
                    Description = addExpenseDto.Description,
                    Type = addExpenseDto.Type,
                    Date = addExpenseDto.Date
                };
                _context.Expense.Add(expense);
                _context.SaveChanges();
                return Ok(expense);
            } catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateExpense(int id, UpdateExpenseDto updateExpenseDto)
        {
            try
            {
                var expense = _context.Expense.Find(id);
                if (expense != null)
                {
                    expense.Name = updateExpenseDto.Name;
                    expense.Description = updateExpenseDto.Description;
                    expense.Type = updateExpenseDto.Type;
                    expense.Date = updateExpenseDto.Date;
                }
                else
                {
                    return NotFound();
                }
                _context.SaveChanges();
                return Ok(expense);
            } catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteExpense(int id)
        {
            try
            {
                var expense = _context.Expense.Find(id);
                if (expense == null)
                {
                    return NotFound();
                }
                _context.Expense.Remove(expense);
                _context.SaveChanges();
                return Ok(expense);
            } catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
