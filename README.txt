Steps to Build and Run the Application via Command Line on OSX
----------------------------------------------------
1. Restore nuget packages needed by the solution.
nuget restore Finance.sln

2. Build the solution via xbuild. xbuild is the mono's implementation of msbuild by Microsoft.
xbuild /p:BuildWithMono="true" /p:Configuration=Release Finance.sln

3. Running your asp.net mvc app via fastcgi
fastcgi-mono-server4 /applications=/:/opt/www/finance/ /socket=tcp:127.0.0.1:8083 /printlog=True
