using System.Collections.Generic;

namespace IManager_CE.Abstracts
{
    /// <summary>A base class define all operations specified for flight Itinerary.</summary>
  public abstract class ItineraryOperation<T> where T : class
  {
    public abstract List<T> LoadItineraries();
  }
}
