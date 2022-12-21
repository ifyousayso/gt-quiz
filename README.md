# Quiz

## Info

Check out the code at [https://github.dev/ifyousayso/gt-quiz](https://github.dev/ifyousayso/gt-quiz) .

## Requirements

- .NET SDK versions 3.1.425 / 6.0.403 / 7.0.100
- AspNetCore versions 3.1.31 / 6.0.11 / 7.0.0
- NETCore versions 3.1.31 / 6.0.11 / 7.0.0

## Manual publishing

In the project directory:

1. Make sure that old builds are deleted: **dotnet clean**
2. Build the Release solution: **dotnet publish -c Release**
3. Navigate to *WebApi/bin/Release/net7.0/publish* and run: **dotnet ITHS.Webapi.dll**
4. Launch [http://localhost:5000/swagger/index.html](http://localhost:5000/swagger/index.html) to inspect the endpoints and test them.

## Visual Studio

Open the project in Visual Studio, select Debug or Release mode, "Rebuild Solution" and Run!
