namespace IManager_CE.Interfaces
{
  public interface ISchedule
  {
    int? Flight { get; set; }

    string Depature { get; set; }

    string Arrival { get; set; }

    int? Day { get; set; }
  }
}
