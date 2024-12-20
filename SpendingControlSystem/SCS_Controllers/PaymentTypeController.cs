using Microsoft.AspNetCore.Mvc;
using SpendingControlSystem.Data;
using SpendingControlSystem.ViewModels;
using SpendingControlSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace SpendingControlSystem.SCS_Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PaymentTypeController : Controller
    {
        private readonly SpendingControlSystemDBContext _context;

        public PaymentTypeController(SpendingControlSystemDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddPaymentType([FromBody] PaymentTypeRequestViewModel paymentTypeViewModel)
        {
            if (paymentTypeViewModel == null)
            {
                return BadRequest("PaymentType data is required and cannot be null.");
            }

            try
            {

                var paymentType = new PaymentType()
                {
                    Name = paymentTypeViewModel.Name,
                    DataHoraInclusao = DateTime.Now,
                    UsuarioInclusao = "gabriel.lara",
                    DataHoraAlteracao = DateTime.Now,
                    UsuarioAlteracao = "gabriel.lara",
                    IsActive = true
                };

                _context.PaymentTypes.Add(paymentType);
                _context.SaveChanges();

                return CreatedAtAction(nameof(AddPaymentType), new { id = paymentType.Id }, paymentTypeViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error: " + ex.Message });
            }
        }

        [HttpGet("GetAllCategory")]
        public ActionResult<IEnumerable<PaymentType>> GetPaymentTypes()
        {
            try
            {
                var paymentType = _context.PaymentTypes.ToList();
                return Ok(paymentType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error: " + ex.Message });
            }
        }

        [HttpGet("GetPaymentTypeBy/{id}")]
        public IActionResult GetPaymentTypeById(int id)
        {
            var paymentType = _context.PaymentTypes.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (paymentType == null)
            {
                return NotFound(new { message = "Payment Type not found." });
            }

            return Ok(paymentType);
        }

        [HttpPut("UpdatePaymentTypeBy/{id}")]
        public IActionResult UpdatePaymentType(int id, [FromBody] PaymentTypeRequestViewModel paymentTypeRequest)
        {
            if (paymentTypeRequest == null)
            {
                return BadRequest("Request data cannot be null.");
            }

            var existingPaymentTypes = _context.PaymentTypes.FirstOrDefault(p => p.Id == id);
            if (existingPaymentTypes == null)
            {
                return NotFound("PaymentTypes not found.");
            }

            try
            {
                existingPaymentTypes.Name = paymentTypeRequest.Name;
                existingPaymentTypes.DataHoraAlteracao = DateTime.Now;

                _context.PaymentTypes.Update(existingPaymentTypes);
                _context.SaveChanges();

                return Ok(existingPaymentTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the payment type: {ex.Message}");
            }
        }

        [HttpDelete("DeletePaymentTypeBy/{id}")]
        public IActionResult DeletePaymentType(int id)
        {
            var paymentType = _context.PaymentTypes.FirstOrDefault(b => b.Id == id);
            if (paymentType == null)
            {
                return NotFound(new { message = "PaymentTypes not found." });
            }

            try
            {
                _context.PaymentTypes.Remove(paymentType);
                _context.SaveChanges();

                return Ok(new { message = "Payment type deleted successfully.", paymentType });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the payment type: {ex.Message}");
            }
        }
    }
}
