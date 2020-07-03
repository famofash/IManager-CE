using IManager_CE.Interfaces;
using Newtonsoft.Json;

namespace IManager_CE.Models
{
  public class FlightItinerary : IOrderId, ISchedule
  {
    public string OrderId { get; set; }

    public int? Flight { get; set; }

    public string Depature { get; set; } = "YUL";

    [JsonProperty("Destination")]
    public string Arrival { get; set; }

    public int? Day { get; set; }
  }
}
