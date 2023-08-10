# Using and distributing a fixed version of the Windows Webview2 Runtime
This sample shows how to use and distribute a fixed Version Windows WebView2 Runtime with your MAUI App. Follow these instructions to use the sample.

1. Download the Fixed version installer packages from here: https://developer.microsoft.com/en-us/microsoft-edge/webview2/#download-section
2. Extract the packages into a `Runtimex86`, `Runtimex64` and `RuntimeARM64` folders in your solution folder. This will keep the files outside your project and keep your Solution Explorer clean. First use the `expand` command to expand the .cab file and then rename the folder as appropriate.

   ```
   expand {path to the package} -F:* {path to the destination folder}   
   ```

   Example, from a PowerShell terminal:
   ```
   expand "C:\Downloads\Microsoft.WebView2.FixedVersionRuntime.114.0.1823.79.x64.cab" -F:* C:\src\MySolution\
   rename-item "C:\src\MySolution\Microsoft.WebView2.FixedVersionRuntime.114.0.1823.79.x64" "C:\src\MySolution\Runtimex64"
   ```
   
3. Add the following code to CreateMauiApp() in MauiProgram.cs: 
   ```
   #if WINDOWS
           string relativePath = @"\Runtime";
           string basePath = AppContext.BaseDirectory;
           string wvrPath = Path.Combine(basePath, relativePath);
           Environment.SetEnvironmentVariable("WEBVIEW2_BROWSER_EXECUTABLE_FOLDER", wvrPath);
   #endif

4. After the existing `<MauiAsset>' element, add the following to your .csproj file:
   ```
   <MauiAsset Include="..\Runtimex86\**" 
              LogicalName="Runtime\%(RecursiveDir)%(Filename)%(Extension)" 
              Condition="$([MSBuild]::GetTargetPlatformIdentifier('$(TargetFramework)')) == 'windows' And ('$(PlatformTarget)' == 'x86')"/>
   <MauiAsset Include="..\Runtimex64\**" 
              LogicalName="Runtime\%(RecursiveDir)%(Filename)%(Extension)" 
              Condition="$([MSBuild]::GetTargetPlatformIdentifier('$(TargetFramework)')) == 'windows' And ('$(PlatformTarget)' == 'x64')"/>
   <MauiAsset Include="..\RuntimeARM64\**" 
              LogicalName="Runtime\%(RecursiveDir)%(Filename)%(Extension)" 
              Condition="$([MSBuild]::GetTargetPlatformIdentifier('$(TargetFramework)')) == 'windows' And ('$(PlatformTarget)' == 'ARM64')"/>

   ```
