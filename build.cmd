@echo off
cd %~dp0

SETLOCAL
SET NUGET_FOLDER=%LocalAppData%\NuGet
SET CACHED_NUGET=%LocalAppData%\NuGet\NuGet.exe
SET DNX_FEED=https://www.nuget.org/api/v2/

IF EXIST %CACHED_NUGET% goto getnuget
echo Downloading latest version of NuGet.exe...
IF NOT EXIST %NUGET_FOLDER% md %NUGET_FOLDER%
@powershell -Command "$ProgressPreference = 'SilentlyContinue'; Invoke-WebRequest 'https://www.nuget.org/nuget.exe' -OutFile '%CACHED_NUGET%'"

:getnuget
IF EXIST build\NuGet.exe goto getfake
IF NOT EXIST build md build
copy %CACHED_NUGET% build\NuGet.exe > nul

:getfake
IF EXIST build\FAKEX goto getdnx
build\NuGet.exe install FAKEX -ExcludeVersion -o build

:getdnx
IF "%SKIP_DNX_INSTALL%"=="1" goto run
CALL build\FAKEX\tools\dnvm install '1.0.0-beta6' -a default -runtime CLR -arch x86 -nonative
CALL build\FAKEX\tools\dnvm install default -runtime CoreCLR -arch x86 -nonative

:run
CALL build\FAKEX\tools\dnvm use default -runtime CLR -arch x86
FOR /f %%i in ('build\FAKEX\tools\dnvm name default') do SET DNX_FOLDER=%USERPROFILE%\.dnx\runtimes\%%i\bin
build\FAKE\tools\Fake.exe build.fsx %*