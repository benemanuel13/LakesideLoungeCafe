#include <windows.h>
#include <windowsx.h>
#include <winuser.h>
#include <stdio.h>
#include <time.h>

#include <OleAuto.h>
#include <oaIdl.h>

#include "LakesideLoungeOffice_h.h"

#pragma warning(disable: 4786)
#pragma warning(disable: 4800)
#pragma warning(disable:4996)

static HINSTANCE g_hModule = NULL;
static long g_cComponents = 0;
static long g_cServerLocks = 0;

const char g_szFriendlyName[] = "Friendly Name Goes Here";
const char g_szVerIndProgID[] = "MyComponent.MyClass";
const char g_szProgID[] = "MyComponent.MyClass.1";

HRESULT GetPropertyEx(IDispatch* d, LPOLESTR name, VARIANT* rtn);
//HRESULT GetPropertyEx(IDispatch*d, OLECHAR* name, char arg, VARIANT* rtn);
HRESULT GetPropertyEx(IDispatch*d, LPOLESTR name, const char* arg, VARIANT* rtn);
HRESULT GetPropertyEx(IDispatch*d, LPOLESTR name, long arg1, long arg2, VARIANT* rtn);
HRESULT GetPropertyEx(IDispatch*d, LPOLESTR name, long arg, VARIANT* rtn, int type);

HRESULT PutPropertyEx(IDispatch*d, LPOLESTR name, const char* arg, VARIANT* rtn);
HRESULT PutPropertyCharEx(IDispatch*d, LPOLESTR name, char* arg, VARIANT* rtn);
HRESULT PutPropertyEx(IDispatch*d, LPOLESTR name, IDispatch* arg, VARIANT* rtn);
HRESULT PutPropertyEx(IDispatch*d, LPOLESTR name, int arg, VARIANT* rtn);
HRESULT PutPropertyEx(IDispatch*d, LPOLESTR name, LPSYSTEMTIME arg, VARIANT* rtn);
HRESULT PutPropertyEx(IDispatch*d, LPOLESTR name, long arg, VARIANT* rtn);
HRESULT PutPropertyEx(IDispatch* d, LPOLESTR name, DATE arg, VARIANT* rtn);

HRESULT CallMethod(IDispatch* d, LPOLESTR name, VARIANT* rtn);
HRESULT CallMethod(IDispatch* d, LPOLESTR name, const char* arg, VARIANT* rtn);
HRESULT CallMethod(IDispatch* d, LPOLESTR name, int arg, VARIANT* rtn);
HRESULT CallMethod(IDispatch* d, LPOLESTR name, IDispatch* arg, VARIANT* rtn);
HRESULT CallMethod(IDispatch* d, LPOLESTR name, const char* arg, int arg2, int arg3, VARIANT* rtn);
HRESULT CallMethod(IDispatch* d, LPOLESTR name, VARIANT* arg, VARIANT* arg2, long arg3, VARIANT* rtn);
HRESULT CallMethod(IDispatch* d, LPOLESTR name, const char* command, IDispatch* arg, long arg2, long arg3, long arg4, VARIANT* rtn);

BSTR ConvertCharToBstr(char* inVal);
BSTR ConvertUCharToBstr(unsigned char* inVal, int length);
BSTR ConvertConstCharToBstr(const char* inVal);
char* ConvertBstrToChar(BSTR inVal);

char* ConvertToChar(double value);
char* ConvertToChar(int value);

char* ConvertDateToChar(DATE date);
tm* ConvertDateToTm(DATE date);

//char* BuildDate(int day, int month, int year);

char* ConvertTmToChar(const tm* date);
DATE ConvertDate(tm* date);

//HRESULT ShowForm(IDispatch* disp, reportType repType);
void ShowMessage(LONG input);