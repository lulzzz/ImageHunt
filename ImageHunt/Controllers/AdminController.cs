using ImageHunt.Model;
using ImageHunt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageHunt.Controllers
{
  [Route("api/[Controller]")]
  public class AdminController : Microsoft.AspNetCore.Mvc.Controller
  {
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
      _adminService = adminService;
    }
    [HttpGet("GetAllAdmins")]
    //[Authorize]
    public IActionResult GetAllAdmins()
    {
      return Ok(_adminService.GetAllAdmins());
    }
    [HttpGet("ById/{adminId}")]
    public IActionResult GetAdminById(int adminId)
    {
      return Ok(_adminService.GetAdminById(adminId));
    }
    [HttpGet("ByEmail/{email}")]
    public IActionResult GetAdminByEmail(string email)
    {
      return Ok(_adminService.GetAdminByEmail(email));
    }
    [HttpPost]
    public IActionResult InsertAdmin([FromBody] Admin admin)
    {
      _adminService.InsertAdmin(admin);
      return Ok();
    }
    [HttpDelete("{adminId}")]
    public IActionResult DeleteAdmin(int adminId)
    {
      var admin = _adminService.GetAdminById(adminId);
      _adminService.DeleteAdmin(admin);
      return Ok();
    }
  }
}
