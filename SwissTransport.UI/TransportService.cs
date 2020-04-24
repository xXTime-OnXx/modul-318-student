using System;
using System.Collections.Generic;
using System.Linq;

namespace SwissTransport.UI
{
    /// <summary>
    /// Service for interface between SwissTransport & SwissTransport.UI
    /// </summary>
    public class TransportService
    {
        private readonly ITransport _transport;

        public TransportService()
        {
            _transport = new Transport();
        }

        /// <summary>
        /// get names of all founded stations
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<string> GetStationsName(string query)
        {
            return _transport.GetStations(query).StationList.ConvertAll(s => s.Name);
        }

        /// <summary>
        /// get connections between stations at the given date & time
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public List<Connection> GetConnections(string from, string to, DateTime dateTime)
        {
            return _transport.GetConnections(from, to, dateTime).ConnectionList;
        }

        /// <summary>
        /// get all connections available at the given station
        /// </summary>
        /// <param name="stationName"></param>
        /// <returns></returns>
        public List<StationBoard> GetStationConnections(string stationName)
        {
            var station = _transport.GetStations(stationName).StationList.First();
            return _transport.GetStationBoard(station.Name, station.Id).Entries;
        }

    }
}