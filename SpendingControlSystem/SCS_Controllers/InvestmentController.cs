using Microsoft.AspNetCore.Mvc;
using SpendingControlSystem.Data;
using SpendingControlSystem.ViewModels;
using SpendingControlSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace SpendingControlSystem.SCS_Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class InvestmentController : Controller
    {
        private readonly SpendingControlSystemDBContext _context;

        public InvestmentController(SpendingControlSystemDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddInvestment([FromBody] InvestmentRequestViewModel investmentViewModel)
        {
            if (investmentViewModel == null)
            {
                return BadRequest("Investment data is required and cannot be null.");
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == investmentViewModel.UserId);
            if (user == null)
            {
                return NotFound(new { message = "User not found for the provided UserId." });
            }

            try
            {

                var investment = new Investment()
                {
                    Name = investmentViewModel.Name,
                    Value = investmentViewModel.Value,
                    Date = investmentViewModel.Date,
                    DataHoraInclusao = DateTime.Now,
                    UsuarioInclusao = "gabriel.lara",
                    DataHoraAlteracao = DateTime.Now,
                    UsuarioAlteracao = "gabriel.lara",
                    IsActive = true,
                    User = user
                };

                _context.Investments.Add(investment);
                _context.SaveChanges();

                return CreatedAtAction(nameof(AddInvestment), new { id = investment.Id }, investmentViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error: " + ex.Message });
            }
        }

        [HttpGet("GetAllInvestments")]
        public ActionResult<IEnumerable<Investment>> GetInvestments()
        {
            try
            {
                var investment = _context.Investments.ToList();
                return Ok(investment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error: " + ex.Message });
            }
        }

        [HttpGet("GetInvestmentBy/{id}")]
        public IActionResult GetInvestmentById(int id)
        {
            var invesment = _context.Investments.AsNoTracking().FirstOrDefault(b => b.Id == id);
            if (invesment == null)
            {
                return NotFound(new { message = "Investment not found." });
            }

            return Ok(invesment);
        }

        [HttpPut("UpdateInvestmentBy/{id}")]
        public IActionResult UpdateInvestment(int id, [FromBody] InvestmentRequestViewModel investmentRequest)
        {
            if (investmentRequest == null)
            {
                return BadRequest("Request data cannot be null.");
            }

            var existinginvestment = _context.Investments.FirstOrDefault(iv => iv.Id == id);
            if (existinginvestment == null)
            {
                return NotFound("Investment not found.");
            }

            try
            {
                // Atualiza os dados do orçamento
                existinginvestment.Name = investmentRequest.Name;
                existinginvestment.Value = investmentRequest.Value;
                existinginvestment.Date = investmentRequest.Date;
                existinginvestment.DataHoraAlteracao = DateTime.Now;

                _context.Investments.Update(existinginvestment);
                _context.SaveChanges();

                return Ok(existinginvestment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the investment: {ex.Message}");
            }
        }

        [HttpDelete("DeleteBy/{id}")]
        public IActionResult DeleteInvestment(int id)
        {
            var investment = _context.Investments.FirstOrDefault(b => b.Id == id);
            if (investment == null)
            {
                return NotFound(new { message = "Investment not found." });
            }

            try
            {
                _context.Investments.Remove(investment);
                _context.SaveChanges();

                return Ok(new { message = "Investment deleted successfully.", investment});
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the investment: {ex.Message}");
            }
        }
    }
}
