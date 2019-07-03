enum {
    ext_cm_AfterStartup = 0,
    ext_cm_Startup = 1,
    ext_cm_External = 2,
    ext_cm_CommandLine = 3
} ext_ConnectMode;

enum {
    ext_dm_HostShutdown = 0,
    ext_dm_UserClosed = 1
} ext_DisconnectMode;

interface _IDTExtensibility2 : IDispatch {
    [id(0x00000001)]
    HRESULT OnConnection(
                    [in] IDispatch* Application,
                    [in] ext_ConnectMode ConnectMode,
                    [in] IDispatch* AddInInst,
                    [in] SAFEARRAY(VARIANT)* custom);
    [id(0x00000002)]
    HRESULT OnDisconnection(
                    [in] ext_DisconnectMode RemoveMode,
                    [in] SAFEARRAY(VARIANT)* custom);
    [id(0x00000003)]
    HRESULT OnAddInsUpdate([in] SAFEARRAY(VARIANT)* custom);
    [id(0x00000004)]
    HRESULT OnStartupComplete([in] SAFEARRAY(VARIANT)* custom);
    [id(0x00000005)]
    HRESULT OnBeginShutdown([in] SAFEARRAY(VARIANT)* custom);
};