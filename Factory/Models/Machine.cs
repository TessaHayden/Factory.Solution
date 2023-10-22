using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;

namespace Factory.Models
{
  public class Machine
  {
    public int MachineId { get; set; }
    public string MachineName { get; set; }
    public int EngineerId { get; set; }
    public Engineer Engineers { get; set; }
    public List<EngineerMachine> JoinEntities { get; }
  }
}