using Microsoft.AspNetCore.Mvc;
using SpendingControlSystem.Data;
using SpendingControlSystem.ViewModels;
using SpendingControlSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace SpendingControlSystem.SCS_Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CostController : Controller
    {
        private readonly SpendingControlSystemDBContext _context;

        public CostController(SpendingControlSystemDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddCost([FromBody] CostRequestViewModel costViewModel)
        {
            if (costViewModel == null)
            {
                return BadRequest(new { message = "Cost data is required and cannot be null." });
            }

            try
            {
                var user = _context.Users.Include(u => u.Costs).FirstOrDefault(u => u.Id == costViewModel.UserId);
                if (user == null)
                {
                    return NotFound(new { message = "User not found for the provided UserId." });
                }

                var costCategory = _context.CostCategories.Include(cc => cc.Costs).FirstOrDefault(c => c.Id == costViewModel.CostCategoryId);
                if (costCategory == null)
                {
                    return NotFound(new { message = "CostCategory not found for the provided CostCategoryId." });
                }

                var paymentType = _context.PaymentTypes.Include(pt => pt.Costs).FirstOrDefault(p => p.Id == costViewModel.PaymentTypeId);
                if (paymentType == null)
                {
                    return NotFound(new { message = "PaymentType not found for the provided PaymentTypeId." });
                }

                // Criar e preencher a entidade Cost
                var cost = new Cost()
                {
                    Value = costViewModel.Value,
                    Date = costViewModel.Date,
                    Description = costViewModel.Description,
                    DataHoraInclusao = DateTime.Now,
                    UsuarioInclusao = "gabriel.lara",
                    DataHoraAlteracao = DateTime.Now,
                    UsuarioAlteracao = "gabriel.lara",
                    IsActive = true,
                    User = user,
                    CostCategory = costCategory,
                    PaymentType = paymentType
                };

                _context.Costs.Add(cost);
                _context.SaveChanges();

                return CreatedAtAction(nameof(AddCost), new { id = cost.Id }, new { cost.Id, cost.Value, cost.Date });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error: " + ex.Message });
            }
        }


        [HttpGet("GetAllCosts")]
        public ActionResult<IEnumerable<Cost>> GetCosts()
        {
            try
            {
                var costs = _context.Costs
                    .Include(c => c.CostCategory)
                    .Include(c => c.PaymentType)
                    .ToList();
                    return Ok(costs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error: " + ex.Message });
            }
        }

        [HttpGet("GetCostBy/{id}")]
        public IActionResult GetCostById(int id)
        {
            var costs = _context.Costs.AsNoTracking().FirstOrDefault(b => b.Id == id);
            if (costs == null)
            {
                return NotFound(new { message = "Costs not found." });
            }

            return Ok(costs);
        }

        [HttpPut("UpdateCostBy/{id}")]
        public IActionResult UpdateCost(int id, [FromBody] CostRequestViewModel costRequest)
        {
            if (costRequest == null)
            {
                return BadRequest("Request data cannot be null.");
            }

            var existingCost = _context.Costs.FirstOrDefault(cs => cs.Id == id);
            if (existingCost == null)
            {
                return NotFound("Cost not found.");
            }

            try
            {
                // Atualiza os dados do orçamento
                existingCost.Value = costRequest.Value;
                existingCost.Date = costRequest.Date;
                existingCost.Description = costRequest.Description;
                existingCost.DataHoraAlteracao = DateTime.Now;

                _context.Costs.Update(existingCost);
                _context.SaveChanges();

                return Ok(existingCost);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the cost: {ex.Message}");
            }
        }

        [HttpDelete("DeleteBy/{id}")]
        public IActionResult DeleteCost(int id)
        {
            var costs = _context.Costs.FirstOrDefault(cs => cs.Id == id);
            if (costs == null)
            {
                return NotFound(new { message = "Cost not found." });
            }

            try
            {
                _context.Costs.Remove(costs);
                _context.SaveChanges();

                return Ok(new { message = "Cost deleted successfully.", costs });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the cost: {ex.Message}");
            }
        }
    }
}
