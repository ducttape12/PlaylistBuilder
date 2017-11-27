del PlaylistBuilder\bin\Release\PlaylistBuilder-*.zip
rmdir /q /s PlaylistBuilder\bin\Release\Publish
dotnet publish --output bin\Release\Publish --configuration Release --self-contained --runtime win-x64

cd PlaylistBuilder\bin\Release\Publish

..\..\..\..\7za.exe a ..\PlaylistBuilder-%date:~10,4%%date:~4,2%%date:~7,2%.zip *.* -mx9

cd ..\..\..\..\

explorer PlaylistBuilder\bin\Release\