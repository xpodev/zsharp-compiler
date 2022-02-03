@ECHO OFF

dotnet source.exe --additionalprobingpath ./SDK

echo Program exited with error code %ERRORLEVEL%
pause
