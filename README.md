# AppSettingsModifier
*C# console app to modify the appSettings node of a .NET web.config or app.config via command line parameters.*

## Overview:
This is a very specific, simple, application. It spawned from a need to upate app settings in a web.config as a post build event where
powershell scripting was not an option. The app simply performs the specified modification, and shuts down.

## Use
The app only deals with the appsettings node. It can add a key, modify a key, or remove a key. 

It requires three command line arguments and accepts one optional parameter.

* -a --action: "add", "modify", "remove"
* -k --key   : The key value which will select the node to be modified
* -f --file  : The file path of the .config file which will be modified.
* -v --value : (optional) The value of the node for add and modify actions. Defaults to empty string.

## Examples
*Add the following node to c:\project\web.config <add key="Debug" value="true" />*
* appsettingsmodifer.exe -a add --key=Debug --value=True --file="C:\project\web.config"
  
*Modify the existing Version key in c:\project\app.config <add key="Version" value="2.3.2" />*
* appsettingsmodifer.exe --action=add -k Version -v 2.3.2 --file="C:\project\app.config"
  
*Remove the compilation node from c:\project\web.config*
* appsettingsmodifer.exe --action=add --key=Debug -f C:\project\web.config
  
## Exit Codes
The app exits with the following error codes (0 exit code indicates success)

+ InvalidArguments = 1,
+ FileLoadException = 2,
+ XmlModifyException = 3,
+ FileSaveException = 4
