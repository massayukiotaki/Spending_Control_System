using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SpendingControlSystem.Data;
using SpendingControlSystem.ViewModels;
using SpendingControlSystem.Entities;
using System.Numerics;

namespace SpendingControlSystem.SCS_Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : Controller
    {
        protected readonly SpendingControlSystemDBContext _context;

        public UserController(SpendingControlSystemDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] UserRequestViewModel userViewModel)
        {
            if (userViewModel == null)
            {
                return BadRequest("User data is required and cannot be null.");
            }
            try
            {

                var checkUserExists = _context.Users.Where(u => u.Email == userViewModel.Email).FirstOrDefault();

                if (checkUserExists != null && checkUserExists.Email == userViewModel.Email) 
                {
                    return Conflict(new { message = "Email already linked to an user account" });
                };

                var user = new User()
                {

                    Name = userViewModel.Name,
                    Email = userViewModel.Email,
                    Birthdate = userViewModel.Birthdate,
                    DataHoraInclusao = DateTime.Now,
                    UsuarioInclusao = "gabriel.lara",
                    DataHoraAlteracao = DateTime.Now,
                    UsuarioAlteracao = "gabriel.lara",
                    IsActive = true
                };

                _context.Add(user);
                _context.SaveChanges();
                return CreatedAtAction(nameof(AddUser), new { id = user.Id }, userViewModel);

            }
            catch (Exception ex) 
            {
                return StatusCode(500, new { message = "Internal server error:" + ex.Message });
            }
        }

        [HttpGet("GetAllUsers")]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            try
            {
                var user = _context.Set<User>().ToList();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error:" + ex.Message });
            }
        }


        [HttpGet("GetUserBy/{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _context.Set<User>().FirstOrDefault(a => a.Id.Equals(id));

            if (user == null)
            {
                return NotFound(new { message = "User Id Not Found" });
            }
                return Ok(user);
            
        }

        [HttpPut("UpdateUserBy/{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserRequestViewModel userRequest)
        {
            if (userRequest == null)
            {
                return BadRequest("Request data cannot be null.");
            }

            var existingUser = _context.Set<User>().FirstOrDefault(u => u.Id == id);
            if (existingUser == null)
            {
                return NotFound("User not found.");
            }

            existingUser.Name = userRequest.Name;
            existingUser.Email = userRequest.Email;
            existingUser.Birthdate = userRequest.Birthdate;

            existingUser.DataHoraAlteracao = DateTime.Now;

            try
            {
                _context.Set<User>().Update(existingUser);
                _context.SaveChanges();
                return Ok(existingUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the user: {ex.Message}");
            }
        }


        [HttpDelete("DeleteBy/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Set<User>().FirstOrDefault(a => a.Id.Equals(id));

            if (user == null)
            {
                return NotFound(new { messsage = "User Not Found" });
            }
            _context.Set<User>().Remove(user);
            _context.SaveChanges();

            return Ok(user);
        }
    }
}
