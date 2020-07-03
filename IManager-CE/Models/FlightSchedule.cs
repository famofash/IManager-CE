using IManager_CE.Interfaces;

namespace IManager_CE.Models
{
  public class FlightSchedule : ISchedule
  {
    public int? Flight { get; set; }

    public string Depature { get; set; }

    public string Arrival { get; set; }

    public int? Day { get; set; }
  }
}
