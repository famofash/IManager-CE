using System;
using System.Threading.Tasks;
using IManager_CE.Interfaces;
using IManager_CE.Services;

namespace IManager_CE
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                //initialize ScreenOptions passing the dependencies.
                var options = (IOptions) new ScreenOptions(new ScheduleManager(), new ItineraryManager(20, 99, 60));
                
                // display welcome message
                options.DisplayOptions();

                //create asynchronous process
                var task = new Task(options.ReadKeys);

                task.Start();

                //wait till the process is completed.
                Task.WaitAll(task);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}