using System.Collections.Generic;
using IManager_CE.Abstracts;
using IManager_CE.Models;

namespace IManager_CE.Services
{
    /// <summary>This derived class implemented all operations specified for flight scheduling.</summary>
    /// <remarks>This class add schedule to a global schedule variable and list the schedule</remarks>
    public class ScheduleManager : ScheduleOperations<FlightSchedule>
    {
        public readonly List<FlightSchedule> AllSchedule = new List<FlightSchedule>();

        public override void Add(FlightSchedule schedule) => AllSchedule.Add(schedule);

        public override List<FlightSchedule> Load() => AllSchedule;
    }
}