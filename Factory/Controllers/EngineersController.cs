using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                            .Include(eng => eng.JoinEntities)
                            .ThenInclude(join => join.Machine)  
                            .ToList();
      return View(model);
    }
    public ActionResult Create()
    {
      ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "EngineerName");
      return View();
    }
    [HttpPost]
    public ActionResult Create(Engineer engineer)
    {
      if(!ModelState.IsValid)
      {
        ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "EngineerName");
        return View(engineer);
      }
      _db.Engineers.Add(engineer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Details(int id)
    {
      Engineer thisEngineer = _db.Engineers
                              .Include(join => join.JoinEntities)
                              .ThenInclude(eng => eng.Machine)
                              .FirstOrDefault(eng => eng.EngineerId == id);
      return View(thisEngineer);
    }
    public ActionResult Edit(int id)
    {
      Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
      ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "MachineName");
      return View(thisEngineer);
    }
    [HttpPost]
    public ActionResult Edit(Engineer engineer)
    {
      _db.Engineers.Update(engineer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult AddMachine(int id)
    {
      Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
      ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "MachineName");
      return View(thisEngineer);
    }
    [HttpPost]
    public ActionResult AddMachine(Engineer eng, int machineId)
    {
#nullable enable
      EngineerMachine? joinEntity = _db.EngineerMachines.FirstOrDefault(join => (join.MachineId == machineId && join.EngineerId == eng.EngineerId));
      #nullable disable
      if(joinEntity == null && machineId != 0)
      {
        _db.EngineerMachines.Add(new EngineerMachine(){
          MachineId = machineId,
          EngineerId = eng.EngineerId});
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = eng.EngineerId });
    }

    public ActionResult Delete(int id)
    {
      Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
      return View(thisEngineer);
    }
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
      _db.Engineers.Remove(thisEngineer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    [HttpPost]
    public ActionResult DeleteJoin(int joinId)
    {
      EngineerMachine joinEntry = _db.EngineerMachines.FirstOrDefault(entry => entry.EngineerMachineId == joinId);
      _db.EngineerMachines.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}