[Setup]
AppName=JGoode A.I.O PC Tool
AppVersion=1.0
AppPublisher=JaidenGoode
AppPublisherURL=https://github.com/JaidenGoode/JGoode-s-AIO-PC-Tool
DefaultDirName={autopf}\JGoodeAIO
DefaultGroupName=JGoode A.I.O PC Tool
OutputDir=..\installer-output
OutputBaseFilename=JGoodeAIO-Setup
SetupIconFile=Assets\icon.ico
Compression=lzma2
SolidCompression=yes
WizardStyle=modern
PrivilegesRequired=admin
ArchitecturesInstallIn64BitMode=x64
ArchitecturesAllowed=x64
UninstallDisplayIcon={app}\JGoodeAIO.exe

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "Create a &desktop shortcut"; GroupDescription: "Additional icons:"; Flags: checked

[Files]
Source: "..\publish\win-x64\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "..\publish\vc_redist.x64.exe"; DestDir: "{tmp}"; Flags: deleteafterinstall

[Icons]
Name: "{group}\JGoode A.I.O PC Tool"; Filename: "{app}\JGoodeAIO.exe"
Name: "{commondesktop}\JGoode A.I.O PC Tool"; Filename: "{app}\JGoodeAIO.exe"; Tasks: desktopicon

[Run]
Filename: "{tmp}\vc_redist.x64.exe"; Parameters: "/quiet /norestart"; StatusMsg: "Installing Visual C++ Runtime (required)..."; Check: VCRedistNeedsInstall; Flags: waituntilterminated
Filename: "{app}\JGoodeAIO.exe"; Description: "Launch JGoode A.I.O PC Tool"; Flags: nowait postinstall skipifsilent runascurrentuser

[Code]
function VCRedistNeedsInstall: Boolean;
var
  strVersion: String;
begin
  if RegQueryStringValue(HKEY_LOCAL_MACHINE,
    'SOFTWARE\Microsoft\VisualStudio\14.0\VC\Runtimes\x64',
    'Version', strVersion) then
  begin
    Result := False;
  end
  else
  begin
    Result := True;
  end;
end;
