using Microsoft.AspNetCore.Mvc;
using SpendingControlSystem.Data;
using SpendingControlSystem.ViewModels;
using SpendingControlSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace SpendingControlSystem.SCS_Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CostCategoryController : Controller
    {
        private readonly SpendingControlSystemDBContext _context;

        public CostCategoryController(SpendingControlSystemDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddCostCategory([FromBody] CostCategoryRequestViewModel categoryViewModel)
        {
            if (categoryViewModel == null)
            {
                return BadRequest("CostCategory data is required and cannot be null.");
            }

            try
            {

                var costCategory = new CostCategory()
                {
                    Name = categoryViewModel.Name,
                    DataHoraInclusao = DateTime.Now,
                    UsuarioInclusao = "gabriel.lara",
                    DataHoraAlteracao = DateTime.Now,
                    UsuarioAlteracao = "gabriel.lara",
                    IsActive = true,
                };

                _context.Add(costCategory);
                _context.SaveChanges();

                return CreatedAtAction(nameof(AddCostCategory), new { id = costCategory.Id }, categoryViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error: " + ex.Message });
            }
        }

        [HttpGet("GetAllCategory")]
        public ActionResult<IEnumerable<CostCategory>> GetCostCateroys()
        {
            try
            {
                var costCategory = _context.CostCategories.ToList();
                return Ok(costCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error: " + ex.Message });
            }
        }

        [HttpGet("GetCostCategoryBy/{id}")]
        public IActionResult GetCostCategoryById(int id)
        {
            var costCategory = _context.CostCategories.AsNoTracking().FirstOrDefault(c => c.Id == id);
            if (costCategory == null)
            {
                return NotFound(new { message = "Category not found." });
            }

            return Ok(costCategory);
        }

        [HttpPut("UpdateCostCategoryBy/{id}")]
        public IActionResult UpdateCostCategory(int id, [FromBody] CostCategoryRequestViewModel costCategoryRequest)
        {
            if (costCategoryRequest == null)
            {
                return BadRequest("Request data cannot be null.");
            }

            var existingCategory = _context.CostCategories.FirstOrDefault(b => b.Id == id);
            if (existingCategory == null)
            {
                return NotFound("Category not found.");
            }

            try
            {
                existingCategory.Name = costCategoryRequest.Name;
                existingCategory.DataHoraAlteracao = DateTime.Now;

                _context.CostCategories.Update(existingCategory);
                _context.SaveChanges();

                return Ok(existingCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the category: {ex.Message}");
            }
        }

        [HttpDelete("DeleteCostCategoryBy/{id}")]
        public IActionResult DeleteCostCategory(int id)
        {
            var costCategory = _context.CostCategories.FirstOrDefault(b => b.Id == id);
            if (costCategory == null)
            {
                return NotFound(new { message = "Category not found." });
            }

            try
            {
                _context.CostCategories.Remove(costCategory);
                _context.SaveChanges();

                return Ok(new { message = "Cost Category deleted successfully.", costCategory });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the Category: {ex.Message}");
            }
        }
    }
}
