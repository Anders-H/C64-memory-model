#define MyAppName "Sprdef"
#define MyAppVersion "1.7"
#define MyAppPublisher "Anders Hesselbom"
#define MyAppURL "http://winsoft.se/"
#define MyAppExeName "Sprdef.exe"

[Setup]
AppId={{6F004E89-6EE9-42D8-A478-F6F30E4E66C2}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\{#MyAppName}
DisableProgramGroupPage=yes
PrivilegesRequiredOverridesAllowed=dialog
OutputBaseFilename=SetupSprdef
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "C:\Repos\C64MemModel\Sprdef\Sprdef\bin\Release\Sprdef.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Repos\C64MemModel\Sprdef\Sprdef\bin\Release\C64MemoryModel.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Repos\C64MemModel\Sprdef\Sprdef\bin\Release\Sprdef.exe.config"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent
