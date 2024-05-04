# ApplicationBlocker
Application Blocker Software for Windows

What is Application Blocker?

-Application Blocker allows the user to restrict the selected EXE files. For example, if you want to block Google Chrome, you need to goto the Program Files folder, find the chrome.exe and add it to the list of the Application Blocker.

How does it work?

It adds values to registry to block the selected EXE file.

Computer\HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options

-Here, you may see a lot of EXE files. If you want to block an EXE file manually, you need to create a new key inside "Image File Execution Options". Make a sure to include the extension of the EXE file you want to block. After that, you need to add a string value inside the key you just created. Name the string value as "Debugger". Then, add "ntsd -c qd" to it's value. After that, Windows will not be able to find that file even it exist. To cancel block, just delete the key you have just created. This application is simple!

Which Programming Language Is Used?

-I used Microsoft Visual Studio 2019 C# to make this application. You can download the source code.

Which Software Did You Used To Create The Setup File?

-I used "InstallSimple 3.5" to generate the setup file. I have just wanted to use the Publish feature of Visual Studio, but ClickOnce does not support requireAdministrator. This application must run as administrator to edit registry.
