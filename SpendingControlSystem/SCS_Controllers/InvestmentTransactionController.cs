using Microsoft.AspNetCore.Mvc;
using SpendingControlSystem.Data;
using SpendingControlSystem.ViewModels;
using SpendingControlSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace SpendingControlSystem.SCS_Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class InvestmentTransactionController : Controller
    {
        private readonly SpendingControlSystemDBContext _context;

        public InvestmentTransactionController(SpendingControlSystemDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddTransaction([FromBody] InvestmentTransactionRequestViewModel transactionViewModel)
        {
            if (transactionViewModel == null)
            {
                return BadRequest("Transaction data is required and cannot be null.");
            }

            var investment = _context.Investments.FirstOrDefault(it => it.Id == transactionViewModel.InvestmentId);
            if (investment == null)
            {
                return NotFound(new { message = "Investment not found for the provided InvestmentId." });
            }

            try
            {

                var investmentTransaction = new InvestmentTransaction()
                {
                    Type = transactionViewModel.Type,
                    Value = transactionViewModel.Value,
                    Date = transactionViewModel.Date,
                    DataHoraInclusao = DateTime.Now,
                    UsuarioInclusao = "gabriel.lara",
                    DataHoraAlteracao = DateTime.Now,
                    UsuarioAlteracao = "gabriel.lara",
                    IsActive = true,
                    Investments = investment
                };

                _context.InvestmentTransactions.Add(investmentTransaction);
                _context.SaveChanges();

                return CreatedAtAction(nameof(AddTransaction), new { id = investmentTransaction.Id }, transactionViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error: " + ex.Message });
            }
        }

        [HttpGet("GetAllTransactions")]
        public ActionResult<IEnumerable<InvestmentTransaction>> GetInvestmentTransaction()
        {
            try
            {
                var investmentTransaction = _context.InvestmentTransactions.ToList();
                return Ok(investmentTransaction);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error: " + ex.Message });
            }
        }

        [HttpGet("GetInvestmentTransactionBy/{id}")]
        public IActionResult GetInvestmentTransactionById(int id)
        {
            var investmentTransaction = _context.InvestmentTransactions.AsNoTracking().FirstOrDefault(it => it.Id == id);
            if (investmentTransaction == null)
            {
                return NotFound(new { message = "Investments Transaction not found." });
            }

            return Ok(investmentTransaction);
        }

        [HttpPut("UpdateInvestmentTransactionBy/{id}")]
        public IActionResult UpdateInvestmentTransaction(int id, [FromBody] InvestmentTransactionRequestViewModel transactionRequest)
        {
            if (transactionRequest == null)
            {
                return BadRequest("Request data cannot be null.");
            }

            var existingTransaction = _context.InvestmentTransactions.FirstOrDefault(it => it.Id == id);
            if (existingTransaction == null)
            {
                return NotFound("Transaction not found.");
            }

            try
            {
                existingTransaction.Type = transactionRequest.Type;
                existingTransaction.Value = transactionRequest.Value;
                existingTransaction.Date = transactionRequest.Date;
                existingTransaction.DataHoraAlteracao = DateTime.Now;

                _context.InvestmentTransactions.Update(existingTransaction);
                _context.SaveChanges();

                return Ok(existingTransaction);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the investment transaction: {ex.Message}");
            }
        }

        [HttpDelete("DeleteBy/{id}")]
        public IActionResult DeleteInvestmentTransaction(int id)
        {
            var investmentTransaction = _context.InvestmentTransactions.FirstOrDefault(it => it.Id == id);
            if (investmentTransaction == null)
            {
                return NotFound(new { message = "Transaction not found." });
            }

            try
            {
                _context.InvestmentTransactions.Remove(investmentTransaction);
                _context.SaveChanges();

                return Ok(new { message = "Transaction deleted successfully.", investmentTransaction });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the investment transaction: {ex.Message}");
            }
        }
    }
}
