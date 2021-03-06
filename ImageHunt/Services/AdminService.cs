using System;
using System.Collections.Generic;
using System.Linq;
using ImageHunt.Data;
using ImageHunt.Model;
using ImageHuntCore.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ImageHunt.Services
{
  public class AdminService : AbstractService, IAdminService
  {
    public AdminService(HuntContext context, ILogger<AdminService> logger) : base(context, logger)
    {
    }

    public IEnumerable<Admin> GetAllAdmins()
    {
      return Context.Admins.Include(a => a.Games);
    }

    public void InsertAdmin(Admin admin)
    {
      Context.Admins.Add(admin);
      Context.SaveChanges();
    }

    public void DeleteAdmin(Admin adminToDelete)
    {
      Context.Admins.Remove(adminToDelete);
      Context.SaveChanges();
    }

    public Admin GetAdminById(int adminId)
    {
      return Context.Admins.Include(a => a.Games).Single(a => a.Id == adminId);
    }

    public Admin GetAdminByEmail(string email)
    {
      return Context.Admins.Include(g => g.Games).Single(a => string.Equals(a.Email, email, StringComparison.InvariantCultureIgnoreCase));
    }
  }
}
