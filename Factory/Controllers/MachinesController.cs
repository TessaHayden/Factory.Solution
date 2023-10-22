using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using Factory.Models;

namespace Factory.Controllers
{
  public class MachinesController : Controller
  {
    private readonly FactoryContext _db;
    public MachinesController(FactoryContext db)
    {
      _db = db;
    }
    public ActionResult Index()
    {
      List<Machine> model = _db.Machines
                            .Include(machine => machine.Engineers)
                            .ToList();
      return View(model);
    }
    public ActionResult Details(int id)
    {
      Machine thisMachine = _db.Machines
                            .Include(mach => mach.JoinEntities)
                            .ThenInclude(join => join.Engineer)
                            .FirstOrDefault(mach => mach.MachineId == id);
      return View(thisMachine);
    }
  }
}