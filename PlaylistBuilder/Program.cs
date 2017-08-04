using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace PlaylistBuilder
{
    public class Program
    {
        private const string HelpArgument = "--help";
        private static readonly string[] AlphabetizeArguments = { "--alphabetize", "--a" };

        public static void Main(string[] args)
        {
            if(args.Any(arg => arg.Equals(HelpArgument, StringComparison.OrdinalIgnoreCase)))
            {
                DisplayHelp();
                return;
            }

            var alphabetizePlaylist = args.Any(arg =>
                AlphabetizeArguments.Any(alphabetizeArgument => alphabetizeArgument.Equals(arg, StringComparison.OrdinalIgnoreCase)));

            var stationFileName = args.FirstOrDefault(arg => !arg.StartsWith("--"));
            var playlistOutput = args.LastOrDefault(arg => !arg.StartsWith("--"));

            if(string.IsNullOrWhiteSpace(stationFileName))
            {
                Console.WriteLine("StationsXmlFile.xml not specified!");
                Console.WriteLine();
                DisplayHelp();
                return;
            }

            if(stationFileName.Equals(playlistOutput, StringComparison.OrdinalIgnoreCase) &&
                args.Count(arg => arg.Equals(stationFileName, StringComparison.OrdinalIgnoreCase)) == 1)
            {
                Console.WriteLine("StationsXmlFile.xml and PlaylistOutputFilename cannot be equal");
                Console.WriteLine();
                DisplayHelp();
            }

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

        private static void DisplayHelp()
        {
            Console.WriteLine("Builds a PLS playlist file from the stations specified in the given StationsXmlFile.xml");
            Console.WriteLine("usage: playlistbuilder [OPTIONS] StationsXmlFile.xml [PlaylistOutputFilename]");
            Console.WriteLine("");
            Console.WriteLine($"  {HelpArgument}                    Display this help message");
            Console.WriteLine("  --alphabetize, -a         Alphabetize stations by name before saving them to the playlist");
            Console.WriteLine("  StationsXmlFile           Path to the XML file containing stations for the playlist");
            Console.WriteLine("  PlaylistOutputFilename    Optionally where the playlist should go. If omitted will be written to Playlist.pls");
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