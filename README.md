# PlaylistBuilder
Have a jumbled playlist of your favorite internet radio stations? PlaylistBuilder takes a collection of internet radio stations in an XML file and outputs them into a PLS playlist file, optionally alphabetizing them.

# Downloading
If you're running Windows 10 x64, just download the ZIP in the Releases directory, extract, and run _PlaylistBuilder.exe_ from a command-line, using the usage below.

For any other platform (Linux, Mac OS), you'll need to download .NET Core 1.1 SDK.  Download the source, navigate a command prompt into the _PlaylistBuilder_ directory, and run the application as outlined below.

# Usage
From a command-prompt, navigate to the directory where you extracted either the prebuilt binary or executable.  If you're using the prebuilt binary, run `PlaylistBuilder.exe`; if you're running from source, run the application with `dotnet run`.  Preceeding the run command, use the following flags:

[OPTIONS] StationsXmlFile.xml [PlaylistOutputFilename]

* --help: Prints out the usage
* --alphabetize, -a: Alphabetizes the stations by name when writing to the playlist
* StationsXmlFile: Path to the XML file containing stations for the playlist
* PlaylistOutputFilename: Optionally where the playlist should go. If omitted will be written to Playlist.pls

For example, on Windows 10 you may run the application as `PlaylistBuilder -a samplestations.xml`

# Stations.xml
samplestations.xml is a sample XML file that can be read by PlaylistBuilder.  Your file should follow a similar structure.

# More info
Make you to check out [www.keithott.com](www.keithott.com) for more awesomeness!

# License

Copyright 2017 Keith Ott

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

[http://www.apache.org/licenses/LICENSE-2.0](http://www.apache.org/licenses/LICENSE-2.0)

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.