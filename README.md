# PlaylistBuilder
Have a jumbled playlist of your favorite internet radio stations? PlaylistBuilder takes a collection of internet radio stations in an XML file and outputs them into a PLS playlist file, optionally alphabetizing them.

# Downloading
The latest release can be found on the [Releases](https://github.com/ducttape12/PlaylistBuilder/releases) page.  A pre-built binary has been made for Windows 10 x64.  For any other platform (Linux, Mac OS), you'll need to download .NET Core 1.1 SDK.  Instructions for running the app is below.

# Usage
From a command-prompt, navigate to the directory where you extracted either the prebuilt binary or executable.  If you're using the prebuilt binary, run `PlaylistBuilder.exe`; if you're running from source, run the application with `dotnet run`.  Preceeding the run command, use the following flags:

[OPTIONS] StationsXmlFile.xml [PlaylistOutputFilename]

* --help: Prints out the usage
* --alphabetize, -a: Alphabetizes the stations by name when writing to the playlist
* StationsXmlFile: Path to the XML file containing stations for the playlist
* PlaylistOutputFilename: Optionally where the playlist should go. If omitted will be written to Playlist.pls

For example, on Windows 10 you may run the application as `PlaylistBuilder -a samplestations.xml`

# Stations.xml
samplestations.xml is a sample XML file that can be read by PlaylistBuilder.  Your file should follow a similar structure.  It looks as follows:

```xml
<?xml version="1.0" encoding="utf-8"?>
<ArrayOfStation xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Station>
    <Name>Radio Hunter - The Hitz Channel</Name>
    <Url>http://listen.shoutcast.com:80/RadioHunter-TheHitzChannel</Url>
  </Station>
  <Station>
    <Name>SmoothJazz.com</Name>
    <Url>http://144.217.153.67:80/live</Url>
  </Station>
  <Station>
    <Name>Calm Radio - Solo Piano</Name>
    <Url>http://184.173.142.117:30228/stream</Url>
  </Station>
  <!-- ... --->
</ArrayOfStation>
```

# More info
Check out [www.keithott.com](www.keithott.com) for more awesomeness!

# License

Copyright (C) 2017 Keith Ott

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program. If not, see <http://www.gnu.org/licenses/>.
