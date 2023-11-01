using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;

namespace Factory.Models
{
  public class Machine
  {
    public int MachineId { get; set; }
    [Required(ErrorMessage = "This field cannot be empty.  Please add the machine model.")]
    public string MachineName { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "You must add a machine to a licensed engineer.")]
    // public List<Engineer> Engineers { get; set; }
    // public Engineer Engineer { get; set; }
    public List<EngineerMachine> JoinEntities { get; }
  }
}