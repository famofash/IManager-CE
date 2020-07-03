using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IManager_CE.Interfaces;
using IManager_CE.Models;

namespace IManager_CE.Services
{
    /// <summary>This DI class takes ScheduleManager and ItineraryManager as dependencies.</summary>
    /// <remarks>This class display welcome message and screen options to the console, it reads key from the console and execute action attached to the key</remarks>
    public sealed class ScreenOptions : IOptions
    {
        private readonly ScheduleManager _smanager;
        private readonly ItineraryManager _imanager;

        public ScreenOptions(ScheduleManager smanager, ItineraryManager imanager)
        {
            _smanager = smanager;
            _imanager = imanager;
        }

        public void DisplayOptions()
        {
            Console.WriteLine("********************************Flight Inventory Management System********************************");
            Console.WriteLine("****************************************************************");
            Console.WriteLine("Choose an option to get started:");
            Console.WriteLine("1) Press Key I to display flight itineraries (User story two");
            Console.WriteLine("2) Press Key L to load flight schedule (User story one)");
            Console.WriteLine("3) Press Key G to display flight schedule (User story one)");
            Console.WriteLine("4) Press ESC to exit the program");
            Console.Write("\r\nSelect an option: ");
        }

        public void ReadKeys()
        {
            var consoleKeyInfo = new ConsoleKeyInfo();
            while (!Console.KeyAvailable && consoleKeyInfo.Key != ConsoleKey.Escape)
            {
                consoleKeyInfo = Console.ReadKey(true);
                switch (consoleKeyInfo.KeyChar)
                {
                    case 'G':
                        _smanager.AllSchedule.ForEach(item =>
                            Console.WriteLine($"Flight: {item.Flight},departure: {item.Depature},arrival: {item.Arrival},day: {item.Day}"));

                        //display available options to continue
                        Console.WriteLine("\n\n1) Press Key L to add another flight schedule ");
                        Console.WriteLine("2) Press Key O to view main options");

                        break;
                    case 'I':
                        var itinaries = new List<FlightItinerary>();
                        Task.Run(() => itinaries = _imanager.LoadItineraries()).Wait();

                        Console.WriteLine("\n");
                        itinaries.ForEach(itinary =>
                        {
                            string str;
                            if (!itinary.Flight.HasValue || itinary.OrderId == null) str = $"order: {itinary.OrderId}, flightNumber: not scheduled";
                            else
                                str =
                                    $"order: {itinary.OrderId} flightNumber: {itinary.Flight},departure: {itinary.Depature}, arrival: {itinary.Arrival}, day: {itinary.Day} \n";

                            Console.WriteLine(str);
                        });
                        Console.WriteLine("\n");

                        DisplayOptions();
                        break;
                    case 'L':

                        //display screen options
                        Console.WriteLine("\r\n ");
                        Console.Write("To continue, enter departure city, arrival city separated with '#' (hash) or ';' (semicolon)");
                        Console.WriteLine("in this format <departure_city>#<arrival_city>#<day>\n or <departure_city>#<arrival_city>#<day>");

                        Console.Write("\r\n Enter value: ");

                        //read input from the console
                        var userInput = Console.ReadLine();

                        if (!string.IsNullOrEmpty(userInput))
                        {
                            //split input by either # or ;
                            var strArray = userInput.Split('#', ';');

                            if (strArray.Length >= 3)
                                _smanager.Add(new FlightSchedule()
                                {
                                    Depature = strArray[0],
                                    Arrival = strArray[1],
                                    Day = Convert.ToInt16(strArray[2]),
                                    Flight = _smanager.AllSchedule.Count + 1
                                });
                        }

                        Console.Write("\r\n Record saved successfully: ");
                        Console.Write("\n\n To continue: \n");
                        //display screen options
                        Console.WriteLine("1) Press Key L to add another flight schedule ");
                        Console.WriteLine("2) Press Key G to view flight schedule");
                        Console.WriteLine("3) Press Key O to view main options");

                        break;
                    case 'O':

                        //display screen options
                        DisplayOptions();
                        break;
                    default:
                        if (Console.CapsLock)
                        {
                            Console.WriteLine($"\nYou have entered invalid key: {consoleKeyInfo.KeyChar} \n");
                        }

                        break;
                }
            }
        }
    }
}