using Microsoft.AspNetCore.Mvc;
using SpendingControlSystem.Data;
using SpendingControlSystem.ViewModels;
using SpendingControlSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace SpendingControlSystem.SCS_Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class IncomeController : Controller
    {
        private readonly SpendingControlSystemDBContext _context;

        public IncomeController(SpendingControlSystemDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddIncome([FromBody] IncomeRequestViewModel incomeViewModel)
        {
            if (incomeViewModel == null)
            {
                return BadRequest("Income data is required and cannot be null.");
            }

            var user = _context.Users.FirstOrDefault(i => i.Id == incomeViewModel.UserId);
            if (user == null)
            {
                return NotFound(new { message = "User not found for the provided UserId." });
            }

            try
            {

                var income = new Income()
                {
                    Value = incomeViewModel.Value,
                    Description = incomeViewModel.Description,
                    PaymentDate = incomeViewModel.PaymentDate,
                    DataHoraInclusao = DateTime.Now,
                    UsuarioInclusao = "gabriel.lara",
                    DataHoraAlteracao = DateTime.Now,
                    UsuarioAlteracao = "gabriel.lara",
                    IsActive = true,
                    User = user
                };

                _context.Add(income);
                _context.SaveChanges();

                return CreatedAtAction(nameof(AddIncome), new { id = income.Id }, incomeViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error: " + ex.Message });
            }
        }

        [HttpGet("GetAllIncomes")]
        public ActionResult<IEnumerable<Income>> GetIncomes()
        {
            try
            {
                var incomes = _context.Incomes.ToList();
                return Ok(incomes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error: " + ex.Message });
            }
        }

        [HttpGet("GetIncomeBy/{id}")]
        public IActionResult GetIncomeById(int id)
        {
            var income = _context.Incomes.AsNoTracking().FirstOrDefault(i => i.Id == id);
            if (income == null)
            {
                return NotFound(new { message = "Income not found." });
            }

            return Ok(income);
        }

        [HttpPut("UpdateIncomeBy/{id}")]
        public IActionResult UpdateIncome(int id, [FromBody] IncomeRequestViewModel incomeRequest)
        {
            if (incomeRequest == null)
            {
                return BadRequest("Request data cannot be null.");
            }

            var existingIncome = _context.Incomes.FirstOrDefault(i => i.Id == id);
            if (existingIncome == null)
            {
                return NotFound("Income not found.");
            }

            try
            {
                existingIncome.Value = incomeRequest.Value;
                existingIncome.Description = incomeRequest.Description;
                existingIncome.PaymentDate = incomeRequest.PaymentDate;
                existingIncome.DataHoraAlteracao = DateTime.Now;

                _context.Incomes.Update(existingIncome);
                _context.SaveChanges();

                return Ok(existingIncome);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the budget: {ex.Message}");
            }
        }

        [HttpDelete("DeleteBy/{id}")]
        public IActionResult DeleteIncome(int id)
        {
            var income = _context.Incomes.FirstOrDefault(b => b.Id == id);
            if (income == null)
            {
                return NotFound(new { message = "Income not found." });
            }

            try
            {
                _context.Incomes.Remove(income);
                _context.SaveChanges();

                return Ok(new { message = "Income deleted successfully.", income });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the income: {ex.Message}");
            }
        }
    }
}
