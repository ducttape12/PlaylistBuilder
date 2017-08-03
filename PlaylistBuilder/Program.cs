using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace PlaylistBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            var stationFileName = args.Any() ? args.First() : "stations.xml";

            var stations = LoadStations(stationFileName).ToList();
            stations.Sort((station1, station2) => string.CompareOrdinal(station1.ToString(), station2.ToString()));

            var playlist = new StringBuilder()
                .AppendLine("[playlist]")
                .AppendLine($"NumberOfEntries={stations.Count}");
            for (var index = 0; index < stations.Count; index++)
            {
                var station = stations[index];
                playlist.AppendLine($"File{index + 1}={station.Url}");
                playlist.AppendLine($"Title{index + 1}={station.Name}");
                playlist.AppendLine($"Length{index + 1}=-1");
            }
            playlist.AppendLine("Version=2");

            var playlistFileName = args.Length > 2 ? args.Last() : "Playlist.pls";
            File.WriteAllText(playlistFileName, playlist.ToString());
        }

        private static IEnumerable<Station> LoadStations(string stationFileName)
        {
            var xmlDeserializer = new XmlSerializer(typeof(List<Station>));
            using (var reader = File.OpenRead(stationFileName))
            {
                return (List<Station>)xmlDeserializer.Deserialize(reader);
            }
        }
    }
}