using Microsoft.AspNetCore.Mvc;
using SpendingControlSystem.Data;
using SpendingControlSystem.ViewModels;
using SpendingControlSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace SpendingControlSystem.SCS_Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BudgetController : Controller
    {
        private readonly SpendingControlSystemDBContext _context;

        public BudgetController(SpendingControlSystemDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddBudget([FromBody] BudgetRequestViewModel budgetViewModel)
        {
            if (budgetViewModel == null)
            {
                return BadRequest("Budget data is required and cannot be null.");
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == budgetViewModel.UserId);
            if (user == null)
            {
                return NotFound(new { message = "User not found for the provided UserId." });
            }

            try
            {

                var budget = new Budget()
                {

                    MonthlyValue = budgetViewModel.MonthlyValue,
                    YearMonth = budgetViewModel.YearMonth,
                    DataHoraInclusao = DateTime.Now,
                    UsuarioInclusao = "gabriel.lara",
                    DataHoraAlteracao = DateTime.Now,
                    UsuarioAlteracao = "gabriel.lara",
                    IsActive = true,
                    User = user
                };

                _context.Budgets.Add(budget);
                _context.SaveChanges();

                return CreatedAtAction(nameof(AddBudget), new { id = budget.Id }, budgetViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error: " + ex.Message });
            }
        }

        [HttpGet("GetAllBudgets")]
        public ActionResult<IEnumerable<Budget>> GetBudgets()
        {
            try
            {
                var budgets = _context.Budgets.ToList();
                return Ok(budgets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error: " + ex.Message });
            }
        }

        [HttpGet("GetBudgetBy/{id}")]
        public IActionResult GetBudgetById(int id)
        {
            var budget = _context.Budgets.AsNoTracking().FirstOrDefault(b => b.Id == id);
            if (budget == null)
            {
                return NotFound(new { message = "Budget not found." });
            }

            return Ok(budget);
        }

        [HttpPut("UpdateBudgetBy/{id}")]
        public IActionResult UpdateBudget(int id, [FromBody] BudgetRequestViewModel budgetRequest)
        {
            if (budgetRequest == null)
            {
                return BadRequest("Request data cannot be null.");
            }

            var existingBudget = _context.Budgets.FirstOrDefault(b => b.Id == id);
            if (existingBudget == null)
            {
                return NotFound("Budget not found.");
            }

            try
            {
                existingBudget.MonthlyValue = budgetRequest.MonthlyValue;
                existingBudget.YearMonth = budgetRequest.YearMonth;
                existingBudget.DataHoraAlteracao = DateTime.Now;

                _context.Budgets.Update(existingBudget);
                _context.SaveChanges();

                return Ok(existingBudget);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the budget: {ex.Message}");
            }
        }

        [HttpDelete("DeleteBy/{id}")]
        public IActionResult DeleteBudget(int id)
        {
            var budget = _context.Budgets.FirstOrDefault(b => b.Id == id);
            if (budget == null)
            {
                return NotFound(new { message = "Budget not found." });
            }

            try
            {
                _context.Budgets.Remove(budget);
                _context.SaveChanges();

                return Ok(new { message = "Budget deleted successfully.", budget });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the budget: {ex.Message}");
            }
        }
    }
}
    