del PlaylistBuilder\bin\Release\PlaylistBuilder-*.zip
rmdir /q /s PlaylistBuilder\bin\Release\Publish
dotnet publish --output bin\Release\Publish --configuration Release --self-contained --runtime win-x64
7za a PlaylistBuilder\bin\Release\PlaylistBuilder-%date:~10,4%%date:~4,2%%date:~7,2%.zip PlaylistBuilder\bin\Release\Publish\*.* -mx9
explorer PlaylistBuilder\bin\Release\