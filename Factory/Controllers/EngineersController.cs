using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Factory.Models;

namespace Factory.Controllers
{
  public class EngineersController : Controller
  {
    private readonly FactoryContext _db;
    public EngineersController(FactoryContext db)
    {
      _db = db;
    }
    public ActionResult Index()
    {
      List<Engineer> model = _db.Engineers
                            .Include(eng => eng.Machines)  
                            .ToList();
      return View(model);
    }
    public ActionResult Details(int id)
    {
      Engineer thisEngineer = _db.Engineers
                              .Include(eng => eng.Machines)
                              .ThenInclude(join => join.JoinEntities)
                              .FirstOrDefault(eng => eng.EngineerId == id);
      return View(thisEngineer);
    }
  }
}