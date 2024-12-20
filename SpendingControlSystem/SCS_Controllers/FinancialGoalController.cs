using Microsoft.AspNetCore.Mvc;
using SpendingControlSystem.Data;
using SpendingControlSystem.ViewModels;
using SpendingControlSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace SpendingControlSystem.SCS_Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class FinancialGoalController : Controller
    {
        private readonly SpendingControlSystemDBContext _context;

        public FinancialGoalController(SpendingControlSystemDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddFinancialGoal([FromBody] FinancialGoalRequestViewModel goalViewModel)
        {
            if (goalViewModel == null)
            {
                return BadRequest("Financial Goal data is required and cannot be null.");
            }

            // Verifica se o usuário associado existe
            var user = _context.Users.FirstOrDefault(u => u.Id == goalViewModel.UserId);
            if (user == null)
            {
                return NotFound(new { message = "User not found for the provided UserId." });
            }

            try
            {

                var financialGoal = new FinancialGoal()
                {
                    Description = goalViewModel.Description,
                    ValueTarget = goalViewModel.ValueTarget,
                    DateTarget = goalViewModel.DateTarget,
                    DataHoraInclusao = DateTime.Now,
                    UsuarioInclusao = "gabriel.lara",
                    DataHoraAlteracao = DateTime.Now,
                    UsuarioAlteracao = "gabriel.lara",
                    IsActive = true,
                    User = user
                };

                _context.FinancialGoals.Add(financialGoal);
                _context.SaveChanges();

                return CreatedAtAction(nameof(AddFinancialGoal), new { id = financialGoal.Id }, goalViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error: " + ex.Message });
            }
        }

        [HttpGet("GetAllFinancialGoals")]
        public ActionResult<IEnumerable<FinancialGoal>> GetGoals()
        {
            try
            {
                var financialGoal = _context.FinancialGoals.ToList();
                return Ok(financialGoal);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error: " + ex.Message });
            }
        }

        [HttpGet("GetFinancialGoalBy/{id}")]
        public IActionResult GetFinancialGOalById(int id)
        {
            var financialGoal = _context.FinancialGoals.AsNoTracking().FirstOrDefault(b => b.Id == id);
            if (financialGoal == null)
            {
                return NotFound(new { message = "Goal not found." });
            }

            return Ok(financialGoal);
        }

        [HttpPut("UpdateFinancialGoalBy/{id}")]
        public IActionResult UpdateFinancialGoal(int id, [FromBody] FinancialGoalRequestViewModel financialGoalRequest)
        {
            if (financialGoalRequest == null)
            {
                return BadRequest("Request data cannot be null.");
            }

            var existingGoal = _context.FinancialGoals.FirstOrDefault(f => f.Id == id);
            if (existingGoal == null)
            {
                return NotFound("Financial Goal not found.");
            }

            try
            {
                existingGoal.Description = financialGoalRequest.Description;
                existingGoal.ValueTarget = financialGoalRequest.ValueTarget;
                existingGoal.DateTarget = financialGoalRequest.DateTarget;
                existingGoal.DataHoraAlteracao = DateTime.Now;

                _context.FinancialGoals.Update(existingGoal);
                _context.SaveChanges();

                return Ok(existingGoal);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the financial goal: {ex.Message}");
            }
        }

        [HttpDelete("DeleteBy/{id}")]
        public IActionResult DeleteFinancialGoal(int id)
        {
            var financialGoal = _context.FinancialGoals.FirstOrDefault(b => b.Id == id);
            if (financialGoal == null)
            {
                return NotFound(new { message = "Goal not found." });
            }

            try
            {
                _context.FinancialGoals.Remove(financialGoal);
                _context.SaveChanges();

                return Ok(new { message = "Goal deleted successfully.", financialGoal });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the financial goal: {ex.Message}");
            }
        }
    }
}
