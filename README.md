# Using and distributing a fixed version of the Windows Webview2 Runtime
This sample shows how to use and distribute a fixed Version Windows WebView2 Runtime with your MAUI App.

1. Download the Fixed version installer packages from here: https://developer.microsoft.com/en-us/microsoft-edge/webview2/#download-section
2. Extract the packages into a `Runtimex86`, `Runtimex64` and `RuntimeARM64` folders in your solution folder. This will keep the files outside your project and keep your Solution Explorer clean.

   From a terminal:
   `expand {path to the package} -F:* {path to the destination folder}`

   Example:
   ```
   expand "C:\Downloads\Microsoft.WebView2.FixedVersionRuntime.114.0.1823.79.x64.cab" -F:* C\src\MySolution\Runtimex64`
   expand "C:\Downloads\Microsoft.WebView2.FixedVersionRuntime.114.0.1823.79.x86.cab" -F:* C\src\MySolution\Runtimex86`
   expand "C:\Downloads\Microsoft.WebView2.FixedVersionRuntime.114.0.1823.79.ARM64.cab" -F:* C\src\MySolution\RuntimeARM64`
   
4. Add the following code to CreateMauiApp() in MauiProgram.cs: 
   ```
   #if WINDOWS
           string relativePath = @"..\Runtime";
           string basePath = AppContext.BaseDirectory;
           string wvrPath = Path.Combine(basePath, relativePath);
           Environment.SetEnvironmentVariable("WEBVIEW2_BROWSER_EXECUTABLE_FOLDER", wvrPath);
   #endif

5. Add the following MSBuild Target to your .csproj file
  ```
  <Target AfterTargets="Build" Name="CopyWebViewRuntime" Condition="$([MSBuild]::GetTargetPlatformIdentifier('$(TargetFramework)')) == 'windows'">      
    <ItemGroup Condition="$(PlatformTarget) == 'x86'">
      <RUNTIME Include="..\Runtimex86\**\*.*" />
    </ItemGroup>
    <ItemGroup Condition="$(PlatformTarget) == 'x64'">
      <RUNTIME Include="..\Runtimex64\**\*.*" />
    </ItemGroup>
    <ItemGroup Condition="$(PlatformTarget) == 'ARM64'">
      <RUNTIME Include="..\RuntimeARM64\**\*.*" />
    </ItemGroup>      
     <Copy SourceFiles="@(RUNTIME)" DestinationFolder="$(TargetDir)\Runtime\%(RecursiveDir)" SkipUnchangedFiles="true" />
  </Target>
     
