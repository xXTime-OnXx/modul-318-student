using System;
using System.Collections.Generic;
using System.Linq;

namespace SwissTransport.UI
{
    public class TransportService
    {
        private readonly ITransport _transport;

        public TransportService()
        {
            _transport = new Transport();
        }

        public List<string> GetStationsName(string query)
        {
            return _transport.GetStations(query).StationList.ConvertAll(s => s.Name);
        }

        public List<Connection> GetConnections(string from, string to, DateTime dateTime)
        {
            return _transport.GetConnections(from, to, dateTime).ConnectionList;
        }

        public List<StationBoard> GetStationConnections(string stationName)
        {
            var station = _transport.GetStations(stationName).StationList.First();
            return _transport.GetStationBoard(station.Name, station.Id).Entries;
        }

    }
}