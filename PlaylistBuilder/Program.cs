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
        private const string HelpArgument = "-help";
        private const string DefaultPlaylistOutputFileName = "Playlist.pls";
        private static readonly string[] AlphabetizeArguments = { "-alphabetize", "-a" };

        private static string HelpMessage => new StringBuilder()
                .AppendLine("Builds a PLS playlist file from the stations specified in the given StationsXmlFile.xml")
                .AppendLine("usage: playlistbuilder [OPTIONS] StationsXmlFile.xml [PlaylistOutputFilename]")
                .AppendLine("")
                .AppendLine($"  {HelpArgument}                     Display this help message")
                .AppendLine($"  {string.Join(", ", AlphabetizeArguments)}          Alphabetize stations by name before saving them to the playlist")
                .AppendLine("  StationsXmlFile           Path to the XML file containing stations for the playlist")
                .AppendLine("  PlaylistOutputFilename    Optionally where the playlist should go. If omitted will be written to Playlist.pls").ToString();

        public static void Main(string[] args)
        {
            var (message, alphabetize, sourceFilename, destinationFilename) = ProcessArguments(args);
            if (!string.IsNullOrWhiteSpace(message))
            {
                Console.WriteLine(message);
                return;
            }

            var (stationsLoadError, stations) = LoadStations(sourceFilename);
            if (!string.IsNullOrWhiteSpace(stationsLoadError))
            {
                Console.WriteLine(stationsLoadError);
                return;
            }
            var stationsList = stations.ToList();

            if (alphabetize)
                stationsList.Sort((station1, station2) => string.CompareOrdinal(station1.Name, station2.Name));

            var playlist = BuildPlaylist(stationsList);

            var fileWriteError = WriteFile(destinationFilename, playlist);
            if(string.IsNullOrWhiteSpace(fileWriteError))
            {
                Console.WriteLine($"Playlist written to {destinationFilename}");
            } else
            {
                Console.WriteLine(fileWriteError);
            }
        }

        private static string WriteFile(string destinationFilename, StringBuilder playlist)
        {
            try
            {
                File.WriteAllText(destinationFilename, playlist.ToString());
                return null;
            }
            catch(IOException ex)
            {
                return $"Unable to write to playlist.  Error:{Environment.NewLine}{ex.Message}";
            }
        }

        private static (string displayMessage, bool alphabetize, string sourceFilename, string destinationFilename) ProcessArguments(IEnumerable<string> args)
        {
            var arguments = args.ToList();
            if (!arguments.Any() || arguments.Any(arg => arg.Equals(HelpArgument, StringComparison.OrdinalIgnoreCase)))
            {
                return (HelpMessage, false, null, null);
            }

            var alphabetizePlaylist = arguments.Any(arg =>
                AlphabetizeArguments.Any(alphabetizeArgument => alphabetizeArgument.Equals(arg, StringComparison.OrdinalIgnoreCase)));

            var sourceFilename = arguments.FirstOrDefault(arg => !arg.StartsWith("-"));
            var outputFilename = arguments.LastOrDefault(arg => !arg.StartsWith("-"));

            bool stationFilenameMissing = string.IsNullOrWhiteSpace(sourceFilename);
            if (stationFilenameMissing)
            {
                return ($"StationsXmlFile.xml is missing{Environment.NewLine}{HelpMessage}", false, null, null);
            }

            bool playlistOutputFilenameMayBeOmitted = sourceFilename.Equals(outputFilename, StringComparison.OrdinalIgnoreCase);
            bool singleFilenameInArgs = arguments.Count(arg => arg.Equals(sourceFilename, StringComparison.OrdinalIgnoreCase)) == 1;

            if (playlistOutputFilenameMayBeOmitted)
            {
                if (singleFilenameInArgs)
                {
                    outputFilename = DefaultPlaylistOutputFileName;
                }
                else
                {
                    return ($"StationsXmlFile.xml and PlaylistOutputFilename cannot be equal{Environment.NewLine}{HelpMessage}", false, null, null);
                }
            }

            return (null, alphabetizePlaylist, sourceFilename, outputFilename);
        }

        private static StringBuilder BuildPlaylist(IEnumerable<Station> stations)
        {
            var stationsList = stations.ToList();

            var playlist = new StringBuilder()
                            .AppendLine("[playlist]")
                            .AppendLine($"NumberOfEntries={stationsList.Count}");
            for (var index = 0; index < stationsList.Count; index++)
            {
                var station = stationsList[index];
                playlist.AppendLine($"File{index + 1}={station.Url}");
                playlist.AppendLine($"Title{index + 1}={station.Name}");
                playlist.AppendLine($"Length{index + 1}=-1");
            }
            playlist.AppendLine("Version=2");
            return playlist;
        }

        private static (string error, IEnumerable<Station> stations) LoadStations(string stationFileName)
        {
            var xmlDeserializer = new XmlSerializer(typeof(List<Station>));
            try
            {
                using (var reader = File.OpenRead(stationFileName))
                {
                    return (null, (List<Station>)xmlDeserializer.Deserialize(reader));
                }
            }
            catch(InvalidOperationException ex)
            {
                return ($"Error in XML stations file.  Error:{Environment.NewLine}{ex.Message}", null);
            }
            catch(IOException ex)
            {
                return ($"Unable to load stations from XML file.  Error:{Environment.NewLine}{ex.Message}", null);
            }
        }
    }
}