using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;

namespace Factory.Models
{
  public class Engineer
  {
    public int EngineerId { get; set; }
    [Required(ErrorMessage = "This field is required.  Please enter the name of the engineer.")]
    public string EngineerName { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "You must start by adding an engineer.")]
    // public List<Machine> Machines { get; set; }
    public List<EngineerMachine> JoinEntities { get; }
  }
}