using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IManager_CE.Abstracts;
using IManager_CE.Models;
using Newtonsoft.Json;

namespace IManager_CE.Services
{
    /// <summary>This derived class implemented all operations specified for flight Itinerary.</summary>
    /// <remarks>This class fetch flight orders from json file, generate flight itineraries </remarks>
    public class ItineraryManager : ItineraryOperation<FlightItinerary>
    {
        private readonly int _planeCapacity;
        private readonly int _totalOrders;
        private readonly int _dailyCapacity;

        public ItineraryManager(int planeCapacity, int totalOrders, int dailyCapacity)
        {
            _planeCapacity = planeCapacity;
            _totalOrders = totalOrders;
            _dailyCapacity = dailyCapacity;
        }

        private string ReadOrdersFromFile
        {
            get
            {
                string json;
                var templatepath = $"{AppDomain.CurrentDomain.BaseDirectory}coding-assigment-orders.json";
                using (var sr = new StreamReader(templatepath)) json = sr.ReadToEnd();
                return json;
            }
        }

        public override List<FlightItinerary> LoadItineraries()
        {
            //deserialize orders to type dictionary
            //its assumed all orders are sorted.
            var sortedOrders = JsonConvert.DeserializeObject<Dictionary<string, FlightItinerary>>(ReadOrdersFromFile);
            var day = 1;
            var flight = 1;

            // loop through the total orders available
            for (var i = 1; i <= _totalOrders; ++i)
            {
                //generate orderId
                var orderId = $"order-{i:000}";
                if (!sortedOrders.ContainsKey(orderId))
                {
                    var itenary = new FlightItinerary()
                    {
                        OrderId = $"order-{i}"
                    };
                    sortedOrders.Add(orderId, itenary);
                }
                else
                {
                    //determine flight number and schedule day
                    if (i > 1 && (i - 1) % _planeCapacity == 0) ++flight;
                    if (i > 1 && (i - 1) % _dailyCapacity == 0) ++day;

                    //update itinerary
                    sortedOrders[orderId].Flight = flight;
                    sortedOrders[orderId].Day = day;
                    sortedOrders[orderId].OrderId = orderId;
                }
            }

            //convert dictionary to list
            return sortedOrders.Select(a => a.Value).ToList();
        }
    }
}