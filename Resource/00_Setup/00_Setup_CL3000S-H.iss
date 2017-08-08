; 脚本用 Inno Setup 脚本向导生成。
; 查阅文档获取创建 INNO SETUP 脚本文件详细资料!

[Setup]
AppName=CL3000S-H
AppVerName=CL3000S-H
AppVersion=3.1.2.30
VersionInfoVersion=3.1.2.30
VersionInfoTextVersion=3, 1, 2, 30
AppPublisher=深圳科陆电子科技股份有限公司
AppPublisherURL=http://www.szclou.com
AppSupportURL=http://www.szclou.com
AppUpdatesURL=http://www.szclou.com
DefaultDirName=D:\CL3000S-H\
DefaultGroupName=CL3000S-H
OutputDir=.\00_setup\
OutputBaseFilename=CL3000S-H_3.1.2.30
Compression=lzma
SolidCompression=yes
;ShowUndisplayableLanguages=yes

[Languages] Name: "chi"; MessagesFile: "compiler:Default.isl"
;Name: "en"; MessagesFile: "compiler:Default.isl"
;Name: "fr"; MessagesFile: "compiler:Languages\french.isl";Name: "zh"; MessagesFile: "compiler:Languages\chinese.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}";

[Files]

Source: "..\Client\*.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\Client\*.vbs"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\Client\*.ini"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\Client\*.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\Client\*.bat"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\Client\*.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\Client\*.config"; DestDir: "{app}"; Flags: ignoreversion
;Source: "..\Client\BaoWenLog\*"; DestDir: "{app}\database"; Flags: ignoreversion
Source: "..\Client\Const\*"; DestDir: "{app}\Const"; Flags: ignoreversion
Source: "..\Client\DataBase\*"; DestDir: "{app}\DataBase"; Flags: ignoreversion

Source: "..\Client\Encryption\*"; DestDir: "{app}\Encryption"; Flags: ignoreversion
Source: "..\Client\Encryption\DLL_SERVER_2009\*"; DestDir: "{app}\Encryption\DLL_SERVER_2009"; Flags: ignoreversion
Source: "..\Client\Encryption\DLL_SERVER_2013\*"; DestDir: "{app}\Encryption\DLL_SERVER_2013"; Flags: ignoreversion
Source: "..\Client\Encryption\DLL_SIMPLE_2009\*"; DestDir: "{app}\Encryption\DLL_SIMPLE_2009"; Flags: ignoreversion
Source: "..\Client\Encryption\DLL_SIMPLE_2013\*"; DestDir: "{app}\Encryption\DLL_SIMPLE_2013"; Flags: ignoreversion
Source: "..\Client\Encryption\DLL_DEVELOP_2009\*"; DestDir: "{app}\Encryption\DLL_DEVELOP_2009"; Flags: ignoreversion
Source: "..\Client\Encryption\DLL_DEVELOP_2009_RT\*"; DestDir: "{app}\Encryption\DLL_DEVELOP_2009_RT"; Flags: ignoreversion
Source: "..\Client\Encryption\DLL_ENTERPRISE_2013\*"; DestDir: "{app}\Encryption\DLL_ENTERPRISE_2013"; Flags: ignoreversion

Source: "..\Client\DX\Carrier\*"; DestDir: "{app}\DX\Carrier"; Flags: ignoreversion
Source: "..\Client\DX\ConnProtocol\*"; DestDir: "{app}\DX\ConnProtocol"; Flags: ignoreversion
Source: "..\Client\DX\CostControl\*"; DestDir: "{app}\DX\CostControl"; Flags: ignoreversion
Source: "..\Client\DX\Dgn\*"; DestDir: "{app}\DX\Dgn"; Flags: ignoreversion
Source: "..\Client\DX\EventLog\*"; DestDir: "{app}\DX\EventLog"; Flags: ignoreversion
Source: "..\Client\DX\Freeze\*"; DestDir: "{app}\DX\Freeze"; Flags: ignoreversion
Source: "..\Client\DX\Function\*"; DestDir: "{app}\DX\Function"; Flags: ignoreversion
Source: "..\Client\DX\GongHao\*"; DestDir: "{app}\DX\GongHao"; Flags: ignoreversion

Source: "..\Client\DX\Group\*"; DestDir: "{app}\DX\Group"; Flags: ignoreversion
Source: "..\Client\DX\Infrared\*"; DestDir: "{app}\DX\Infrared"; Flags: ignoreversion
Source: "..\Client\DX\Insulation\*"; DestDir: "{app}\DX\Insulation"; Flags: ignoreversion
Source: "..\Client\DX\PrepareTest\*"; DestDir: "{app}\DX\PrepareTest"; Flags: ignoreversion

Source: "..\Client\DX\QianDong\*"; DestDir: "{app}\DX\QianDong"; Flags: ignoreversion
Source: "..\Client\DX\QiDong\*"; DestDir: "{app}\DX\QiDong"; Flags: ignoreversion
Source: "..\Client\DX\TeSu\*"; DestDir: "{app}\DX\TeSu"; Flags: ignoreversion
Source: "..\Client\DX\WC\*"; DestDir: "{app}\DX\WC"; Flags: ignoreversion
Source: "..\Client\DX\WcAccord\*"; DestDir: "{app}\DX\WcAccord"; Flags: ignoreversion

Source: "..\Client\DX\WGJC\*"; DestDir: "{app}\DX\WGJC"; Flags: ignoreversion
;;Source: "..\Client\DX\WishStandVoltage\*"; DestDir: "{app}\DX\WishStandVoltage"; Flags: ignoreversion
Source: "..\Client\DX\Yure\*"; DestDir: "{app}\DX\Yure"; Flags: ignoreversion
Source: "..\Client\DX\ZouZi\*"; DestDir: "{app}\DX\ZouZi"; Flags: ignoreversion

Source: "..\Client\SX\Carrier\*"; DestDir: "{app}\SX\Carrier"; Flags: ignoreversion
Source: "..\Client\SX\ConnProtocol\*"; DestDir: "{app}\SX\ConnProtocol"; Flags: ignoreversion
Source: "..\Client\SX\CostControl\*"; DestDir: "{app}\SX\CostControl"; Flags: ignoreversion
Source: "..\Client\SX\Dgn\*"; DestDir: "{app}\SX\Dgn"; Flags: ignoreversion

Source: "..\Client\SX\EventLog\*"; DestDir: "{app}\SX\EventLog"; Flags: ignoreversion
Source: "..\Client\SX\Freeze\*"; DestDir: "{app}\SX\Freeze"; Flags: ignoreversion
Source: "..\Client\SX\Function\*"; DestDir: "{app}\SX\Function"; Flags: ignoreversion
Source: "..\Client\SX\GongHao\*"; DestDir: "{app}\SX\GongHao"; Flags: ignoreversion
Source: "..\Client\SX\Group\*"; DestDir: "{app}\SX\Group"; Flags: ignoreversion
Source: "..\Client\SX\QianDong\*"; DestDir: "{app}\SX\QianDong"; Flags: ignoreversion
Source: "..\Client\SX\QiDong\*"; DestDir: "{app}\SX\QiDong"; Flags: ignoreversion
Source: "..\Client\SX\TeSu\*"; DestDir: "{app}\SX\TeSu"; Flags: ignoreversion
Source: "..\Client\SX\WC\*"; DestDir: "{app}\SX\WC"; Flags: ignoreversion
Source: "..\Client\SX\WcAccord\*"; DestDir: "{app}\SX\WcAccord"; Flags: ignoreversion

Source: "..\Client\SX\WGJC\*"; DestDir: "{app}\SX\WGJC"; Flags: ignoreversion
;Source: "..\Client\SX\WishStandVoltage\*"; DestDir: "{app}\SX\WishStandVoltage"; Flags: ignoreversion
Source: "..\Client\SX\Yure\*"; DestDir: "{app}\SX\Yure"; Flags: ignoreversion
Source: "..\Client\SX\ZouZi\*"; DestDir: "{app}\SX\ZouZi"; Flags: ignoreversion

;Source: "..\Client\ErrLog\*"; DestDir: "{app}\ErrLog"; Flags: ignoreversion
;Source: "..\Client\Error\*"; DestDir: "{app}\Error"; Flags: ignoreversion
Source: "..\Client\Log\*"; DestDir: "{app}\Log"; Flags: ignoreversion
Source: "..\Client\Log\Thread\*"; DestDir: "{app}\Log\Thread"; Flags: ignoreversion
Source: "..\Client\ErrLog\*"; DestDir: "{app}\ErrLog"; Flags: ignoreversion

Source: "..\Client\Pic\*"; DestDir: "{app}\Pic"; Flags: ignoreversion
Source: "..\Client\Pic\BllDescription\*"; DestDir: "{app}\Pic\BllDescription"; Flags: ignoreversion
Source: "..\Client\Pic\Button\*"; DestDir: "{app}\Pic\Button"; Flags: ignoreversion
Source: "..\Client\Pic\ClientBack\*"; DestDir: "{app}\Pic\ClientBack"; Flags: ignoreversion
Source: "..\Client\Pic\ClientButton\*"; DestDir: "{app}\Pic\ClientButton"; Flags: ignoreversion
Source: "..\Client\Pic\ClientButton\blueberry\*"; DestDir: "{app}\Pic\ClientButton\blueberry"; Flags: ignoreversion
Source: "..\Client\Pic\Detection\*"; DestDir: "{app}\Pic\Detection"; Flags: ignoreversion
Source: "..\Client\Pic\Detection\Light\*"; DestDir: "{app}\Pic\Detection\Light"; Flags: ignoreversion
Source: "..\Client\Pic\Detection\V90Style\*"; DestDir: "{app}\Pic\Detection\V90Style"; Flags: ignoreversion
Source: "..\Client\Pic\StateBar\*"; DestDir: "{app}\Pic\StateBar"; Flags: ignoreversion
Source: "..\Client\Pic\SysIcons\*"; DestDir: "{app}\Pic\SysIcons"; Flags: ignoreversion
Source: "..\Client\Pic\UI\*"; DestDir: "{app}\Pic\UI"; Flags: ignoreversion
Source: "..\Client\Pic\UI\UI_Client\*"; DestDir: "{app}\Pic\UI\UI_Client"; Flags: ignoreversion
Source: "..\Client\Pic\images\*"; DestDir: "{app}\Pic\images"; Flags: ignoreversion

;Source: "..\Client\Plan\*"; DestDir: "{app}\Plan"; Flags: ignoreversion
;Source: "..\Client\ReportErr\*"; DestDir: "{app}\ReportErr"; Flags: ignoreversion
;Source: "..\Client\Res\*"; DestDir: "{app}\Res"; Flags: ignoreversion
Source: "..\Client\RealTimeDataDll\*"; DestDir: "{app}\RealTimeDataDll"; Flags: ignoreversion

Source: "..\Client\System\*"; DestDir: "{app}\System"; Flags: ignoreversion

;Source: "..\Client\Tmp\*"; DestDir: "{app}\Tmp"; Flags: ignoreversion
Source: "..\Client\Tmp\tmp.ini"; DestDir: "{app}\Tmp"; Flags: ignoreversion
Source: "..\Client\Tmp\Login.dat*"; DestDir: "{app}\Tmp"; Flags: ignoreversion

Source: "..\Client\Xml\*"; DestDir: "{app}\Xml"; Flags: ignoreversion
Source: "..\Client\Wav\*"; DestDir: "{app}\Wav"; Flags: ignoreversion
Source: "..\Client\zh-CHS\*"; DestDir: "{app}\zh-CHS"; Flags: ignoreversion

Source: "..\Client\AppConfigs\*"; DestDir: "{app}\AppConfigs"; Flags: ignoreversion
Source: "..\Client\Config\*"; DestDir: "{app}\Config"; Flags: ignoreversion
;Source: "..\Client\单相配置文件\*"; DestDir: "{app}\单相配置文件"; Flags: ignoreversion
;Source: "..\Client\三相配置文件\*"; DestDir: "{app}\三相配置文件"; Flags: ignoreversion
Source: "..\Client\Plugins\*"; DestDir: "{app}\Plugins"; Flags: ignoreversion

; 注意: 不要在任何共享系统文件中使用“Flags: ignoreversion”

[Icons]

Name: "{userdesktop}\CL3000S-H"; Filename: "{app}\CLDC_StartUp.exe"; WorkingDir: "{app}"; Tasks: desktopicon

Name: "{group}\CL3000S-H"; Filename: "{app}\CLDC_StartUp.exe"
Name: "{group}\{cm:UninstallProgram,程序}"; Filename: "{app}\unins000.exe"

