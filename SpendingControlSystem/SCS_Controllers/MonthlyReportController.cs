using Microsoft.AspNetCore.Mvc;
using SpendingControlSystem.Data;
using SpendingControlSystem.ViewModels;
using SpendingControlSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace SpendingControlSystem.SCS_Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class MonthlyReportController : Controller
    {
        private readonly SpendingControlSystemDBContext _context;

        public MonthlyReportController(SpendingControlSystemDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddMonthlyReport([FromBody] MonthlyReportRequestViewModel monthlyReportViewModel)
        {
            if (monthlyReportViewModel == null)
            {
                return BadRequest("Monthly Report data is required and cannot be null.");
            }


            var user = _context.Users.FirstOrDefault(m => m.Id == monthlyReportViewModel.UserId);
            if (user == null)
            {
                return NotFound(new { message = "User not found for the provided UserId." });
            }

            try
            {

                var monthlyReport = new MonthlyReport()
                {
                    YearMonth = monthlyReportViewModel.YearMonth,
                    TotalRevenues = monthlyReportViewModel.TotalRevenues,
                    TotalCosts = monthlyReportViewModel.TotalCosts,
                    TotalInvestments = monthlyReportViewModel.TotalInvestments,
                    DataHoraInclusao = DateTime.Now,
                    UsuarioInclusao = "gabriel.lara",
                    DataHoraAlteracao = DateTime.Now,
                    UsuarioAlteracao = "gabriel.lara",
                    IsActive = true,
                    User = user
                };
                _context.MonthlyReports.Add(monthlyReport);
                _context.SaveChanges();

                return CreatedAtAction(nameof(AddMonthlyReport), new { id = monthlyReport.Id }, monthlyReportViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error: " + ex.Message });
            }
        }

        [HttpGet("GetAllMonthlyReports")]
        public ActionResult<IEnumerable<MonthlyReport>> GetMonthlyReports()
        {
            try
            {
                var monthlyReports = _context.MonthlyReports.ToList();
                return Ok(monthlyReports);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error: " + ex.Message });
            }
        }

        [HttpGet("GetMonthlyReportBy/{id}")]
        public IActionResult GetMonthlyReportById(int id)
        {
            var monthlyReport = _context.MonthlyReports.AsNoTracking().FirstOrDefault(m => m.Id == id);
            if (monthlyReport == null)
            {
                return NotFound(new { message = "MonthlyReport not found." });
            }

            return Ok(monthlyReport);
        }

        [HttpPut("UpdateMonthlyReportBy/{id}")]
        public IActionResult UpdateMonthlyReport(int id, [FromBody] MonthlyReportRequestViewModel monthlyReportRequest)
        {
            if (monthlyReportRequest == null)
            {
                return BadRequest("Request data cannot be null.");
            }

            var existingMonthlyReport = _context.MonthlyReports.FirstOrDefault(b => b.Id == id);
            if (existingMonthlyReport == null)
            {
                return NotFound("MonthlyReport not found.");
            }

            try
            {
                existingMonthlyReport.YearMonth = monthlyReportRequest.YearMonth;
                existingMonthlyReport.TotalRevenues = monthlyReportRequest.TotalRevenues;
                existingMonthlyReport.TotalCosts = monthlyReportRequest.TotalCosts;
                existingMonthlyReport.TotalInvestments = monthlyReportRequest.TotalInvestments;
                existingMonthlyReport.DataHoraAlteracao = DateTime.Now;

                _context.MonthlyReports.Update(existingMonthlyReport);
                _context.SaveChanges();

                return Ok(existingMonthlyReport);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the MonthlyReport: {ex.Message}");
            }
        }

        [HttpDelete("DeleteBy/{id}")]
        public IActionResult DeleteMonthlyReport(int id)
        {
            var monthlyReport = _context.MonthlyReports.FirstOrDefault(m => m.Id == id);
            if (monthlyReport == null)
            {
                return NotFound(new { message = "MonthlyReport not found." });
            }

            try
            {
                _context.MonthlyReports.Remove(monthlyReport);
                _context.SaveChanges();

                return Ok(new { message = "MonthlyReport deleted successfully.", monthlyReport });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the MonthlyReport: {ex.Message}");
            }
        }
    }
}
