#define OEMRESOURCE

#include <objbase.h>
#include <windows.h>
#include <WinBase.h>
#include <winreg.h>
#include <stdio.h>
//#include "Factory.h"
#include "ExcelAddinFactory.h"
#include "Global.h"
//#include "Report.h"
#include "Form.h"

#include <time.h>

Form* myForm;
//Report* report;

STDAPI DllCanUnloadNow()
{
	if((g_cComponents == 0) && (g_cServerLocks == 0))
	{
		return S_OK;
	}
	else
	{
		return S_FALSE;
	}
}

STDAPI DllGetClassObject(const CLSID& clsid,
						 const IID& iid,
						 void** ppv)
{
	if(clsid != CLSID_Report && clsid != CLSID_Addin)
	{
		return CLASS_E_CLASSNOTAVAILABLE;
	}

	HRESULT hr;

	//if(clsid == CLSID_Report)
	//{
	//	CFactory* pFactory = new CFactory;
	//	hr = pFactory->QueryInterface(iid, ppv);
	//	pFactory->Release();
	//}
	//else
	//{
		AddinFactory* pAddinFactory = new AddinFactory;
		hr = pAddinFactory->QueryInterface(iid, ppv);
		pAddinFactory->Release();
	//}

	return hr;
}

void ShowMessage(LONG input)
{
	LPVOID lpMsgBuf;

	FormatMessage( 
    FORMAT_MESSAGE_ALLOCATE_BUFFER | 
    FORMAT_MESSAGE_FROM_SYSTEM | 
    FORMAT_MESSAGE_IGNORE_INSERTS,
    NULL,
    GetLastError(),
    MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT), // Default language
    (LPTSTR) &lpMsgBuf,
    0,
    NULL 
	);

	MessageBox( NULL, (LPCTSTR)lpMsgBuf, "Error", MB_OK | MB_ICONINFORMATION );
}

STDAPI DllRegisterServer()
{
	HKEY result;

	const char* objClass = "String Value\0";

	LPDWORD* dispos = new LPDWORD;

	//32bit CLSID of Report
	const char* subKey = "CLSID\\{E30E5FB4-0111-4198-90B6-8EE44CDCCD75}\\InprocServer32\0";

	LONG regCreate = RegCreateKeyEx(HKEY_CLASSES_ROOT, subKey, 0, LPSTR(objClass), REG_OPTION_NON_VOLATILE, 
		KEY_ALL_ACCESS, NULL, &result, NULL);

	RegCloseKey(result);

	//Set Site .dll
	const char* value = "C:\\MyCOM\\LakesideLoungeOffice.dll";
	LONG regSetVal = RegSetValue(HKEY_CLASSES_ROOT, subKey, REG_SZ, value, sizeof(value));

	//32bit CLSID of Addin
	subKey = "CLSID\\{0D2CCDFD-51D3-4882-A88C-164CCE65EB38}\\InprocServer32\0";

	regCreate = RegCreateKeyEx(HKEY_CLASSES_ROOT, subKey, 0,LPSTR(objClass), REG_OPTION_NON_VOLATILE, 
		KEY_ALL_ACCESS, NULL, &result, NULL);

	RegCloseKey(result);

	//Set Addin .dll
	value = "C:\\MyCOM\\LakesideLoungeOffice.dll";
	regSetVal = RegSetValue(HKEY_CLASSES_ROOT, subKey, REG_SZ, value, sizeof(value));


	//32bit on 64bit CLSID of Report
	subKey = "WOW6432Node\\CLSID\\{E30E5FB4-0111-4198-90B6-8EE44CDCCD75}\\InprocServer32\0";

	regCreate = RegCreateKeyEx(HKEY_CLASSES_ROOT, subKey, 0, LPSTR(objClass), REG_OPTION_NON_VOLATILE, 
		KEY_ALL_ACCESS, NULL, &result, NULL);

	RegCloseKey(result);

	//Set 32bit on 64bit Report .dll
	value = "C:\\MyCOM\\LakesideLoungeOffice.dll";
	regSetVal = RegSetValue(HKEY_CLASSES_ROOT, subKey, REG_SZ, value, sizeof(value));

	//32bit on 64bit CLSID of Addin
	subKey = "WOW6432Node\\CLSID\\{0D2CCDFD-51D3-4882-A88C-164CCE65EB38}\\InprocServer32\0";

	regCreate = RegCreateKeyEx(HKEY_CLASSES_ROOT, subKey, 0, LPSTR(objClass), REG_OPTION_NON_VOLATILE, 
		KEY_ALL_ACCESS, NULL, &result, NULL);

	RegCloseKey(result);

	//Set 32bit on 64bit Addin .dll
	value = "C:\\MyCOM\\LakesideLoungeOffice.dll";
	regSetVal = RegSetValue(HKEY_CLASSES_ROOT, subKey, REG_SZ, value, sizeof(value));

	//ProgId of Report
	const char* clsId = "{E30E5FB4-0111-4198-90B6-8EE44CDCCD75}";

	subKey = "LakesideLoungeOffice.Report\\CLSID\0";

	regCreate = RegCreateKeyEx(HKEY_CLASSES_ROOT, subKey, 0, LPSTR(objClass), REG_OPTION_NON_VOLATILE, 
		KEY_ALL_ACCESS, NULL, &result, NULL);

	regSetVal = RegSetValue(HKEY_CLASSES_ROOT, subKey, REG_SZ, clsId, sizeof(clsId));

	RegCloseKey(result);

	//ProgId of Addin
	clsId = "{0D2CCDFD-51D3-4882-A88C-164CCE65EB38}";

	subKey = "LakesideLoungeOffice.Addin\\CLSID\0";

	regCreate = RegCreateKeyEx(HKEY_CLASSES_ROOT, subKey, 0, LPSTR(objClass), REG_OPTION_NON_VOLATILE, 
		KEY_ALL_ACCESS, NULL, &result, NULL);

	regSetVal = RegSetValue(HKEY_CLASSES_ROOT, subKey, REG_SZ, clsId, sizeof(clsId));

	RegCloseKey(result);


	//CLSID of TypeLib
	subKey = "TypeLib\\{4349A0F0-A6AC-417b-B74A-A4409E867E3B}";

	regCreate = RegCreateKeyEx(HKEY_CLASSES_ROOT, subKey, 0, LPSTR(objClass), REG_OPTION_NON_VOLATILE, 
		KEY_ALL_ACCESS, NULL, &result, NULL);

	const char* typeLibDesc = "Lakeside Lounge Reporting TypeLib";
	regSetVal = RegSetValue(HKEY_CLASSES_ROOT, subKey, REG_SZ, typeLibDesc, sizeof(typeLibDesc));

	RegCloseKey(result);

	subKey = "TypeLib\\{4349A0F0-A6AC-417b-B74A-A4409E867E3B}\\1.0\\0\\Win32";

	regCreate = RegCreateKeyEx(HKEY_CLASSES_ROOT, subKey, 0, LPSTR(objClass), REG_OPTION_NON_VOLATILE, 
		KEY_ALL_ACCESS, NULL, &result, NULL);
	
	const char* typeLibPath = "C:\\MyCOM\\LakesideLoungeOffice.tlb";
	regSetVal = RegSetValue(HKEY_CLASSES_ROOT, subKey, REG_SZ, typeLibPath, sizeof(typeLibDesc));

	RegCloseKey(result);

	subKey = "TypeLib\\{4349A0F0-A6AC-417b-B74A-A4409E867E3B}\\1.0\\Flags";

	regCreate = RegCreateKeyEx(HKEY_CLASSES_ROOT, subKey, 0, LPSTR(objClass), REG_OPTION_NON_VOLATILE, 
		KEY_ALL_ACCESS, NULL, &result, NULL);
	
	const char* typeLibFlags = "0";
	regSetVal = RegSetValue(HKEY_CLASSES_ROOT, subKey, REG_SZ, typeLibFlags, sizeof(typeLibDesc));

	RegCloseKey(result);

	subKey = "TypeLib\\{4349A0F0-A6AC-417b-B74A-A4409E867E3B}\\1.0\\HELPDIR";

	regCreate = RegCreateKeyEx(HKEY_CLASSES_ROOT, subKey, 0, LPSTR(objClass), REG_OPTION_NON_VOLATILE, 
		KEY_ALL_ACCESS, NULL, &result, NULL);
	
	RegCloseKey(result);

	return S_OK;
}

STDAPI DllUnregisterServer()
{
	//return UnregisterServer(CLSID_component, g_szVerIndProgId,
	//	g_szProgId);
	return S_OK;
}

BOOL APIENTRY DllMain(HINSTANCE hModule, DWORD dwReason,
					  void* lpReserved)
{
	if(dwReason == DLL_PROCESS_ATTACH)
	{
		g_hModule = hModule;
		DisableThreadLibraryCalls(hModule);
	}

	return TRUE;
}

//void Click()
//{
//	MessageBox(0, "Click Handled", "Click", 0);
//}

//HRESULT ShowForm(IDispatch* disp, reportType repType)
//{
//	myForm = new Form();
//	myForm->SetParentEx(g_hModule);
//
//	Button* button = new Button();
//	//button->SetParentEx(myForm->hWnd);
//	button->SetTitleEx("Click Me!");
//	button->SetPosAndSize(10, 10, 150, 50);
//	button->Click = Click;

//	myForm->AddControl("button1", button);

//	myForm->Show();

//	return S_OK;
//}

HRESULT GetPropertyEx(IDispatch* d, LPOLESTR name, VARIANT* rtn)
{
	DISPID thisId;

	//HRESULT res = d->GetIDsOfNames(IID_NULL, &name, 1, LOCALE_USER_DEFAULT, &thisId);

	if (name == LPOLESTR("ActiveConnection"))
		thisId = 1;
	else if (name == LPOLESTR("Parameters"))
		thisId = 0;
	else if (name == LPOLESTR("Fields"))
		thisId = 0;
	else if (name == LPOLESTR("Value"))
		thisId = 0;
	else if (name == LPOLESTR("Workbooks"))
		thisId = 572;
	else if (name == LPOLESTR("Worksheets"))
		thisId = 494;
	else if (name == LPOLESTR("EOF"))
		thisId = 1006;
	
	DISPPARAMS dispparamsNoArgs = {NULL, NULL, 0, 0};

	HRESULT res = d->Invoke(thisId, IID_NULL, LOCALE_SYSTEM_DEFAULT,
		DISPATCH_PROPERTYGET, &dispparamsNoArgs, rtn, NULL, NULL);

	return res;
}

HRESULT GetPropertyEx(IDispatch*d, LPOLESTR name, const char arg, VARIANT* rtn)
{
	DISPID thisId;

	HRESULT res = d->GetIDsOfNames(IID_NULL, &name, 1, LOCALE_USER_DEFAULT, &thisId);

	VARIANT myVar;
	VariantInit(&myVar);

	myVar.vt = VT_UI2;
	myVar.bVal = arg;

	DISPPARAMS* dispparamsArgs = new DISPPARAMS;
	dispparamsArgs->rgvarg = &myVar;
	dispparamsArgs->cArgs = 1;
	dispparamsArgs->rgdispidNamedArgs = NULL;
	dispparamsArgs->cNamedArgs = 0;

	res = d->Invoke(thisId, IID_NULL, LOCALE_SYSTEM_DEFAULT,
		DISPATCH_PROPERTYGET, dispparamsArgs, rtn, NULL, NULL);

	return res;
}

HRESULT GetPropertyEx(IDispatch*d, LPOLESTR name, const char* arg, VARIANT* rtn)
{
	DISPID thisId;

	HRESULT res = d->GetIDsOfNames(IID_NULL, &name, 1, LOCALE_USER_DEFAULT, &thisId);

	VARIANT myVar;
	VariantInit(&myVar);

	BSTR myB;
	myB = ConvertConstCharToBstr(arg);

	myVar.vt = VT_BSTR;
	myVar.bstrVal = myB;

	SysFreeString(myB);

	DISPPARAMS* dispparamsArgs = new DISPPARAMS;
	dispparamsArgs->rgvarg = &myVar;
	dispparamsArgs->cArgs = 1;
	dispparamsArgs->rgdispidNamedArgs = NULL;
	dispparamsArgs->cNamedArgs = 0;

	res = d->Invoke(thisId, IID_NULL, LOCALE_SYSTEM_DEFAULT,
		DISPATCH_PROPERTYGET, dispparamsArgs, rtn, NULL, NULL);

	return res;
}

HRESULT GetPropertyEx(IDispatch*d, LPOLESTR name, long arg1, long arg2, VARIANT* rtn)
{
	DISPID thisId;

	//HRESULT res = d->GetIDsOfNames(IID_NULL, &name, 1, LOCALE_USER_DEFAULT, &thisId);

	if (name == LPOLESTR("Cells"))
		thisId = 238;

	VARIANT myVar[2];
	VariantInit(&myVar[0]);
	VariantInit(&myVar[1]);

	myVar[0].vt = VT_I4;
	myVar[0].lVal = arg2;

	myVar[1].vt = VT_I4;
	myVar[1].lVal = arg1;

	DISPPARAMS* dispparamsArgs = new DISPPARAMS;
	dispparamsArgs->rgvarg = myVar;
	dispparamsArgs->cArgs = 2;
	dispparamsArgs->rgdispidNamedArgs = NULL;
	dispparamsArgs->cNamedArgs = 0;

	HRESULT res = d->Invoke(thisId, IID_NULL, LOCALE_SYSTEM_DEFAULT,
		DISPATCH_PROPERTYGET, dispparamsArgs, rtn, NULL, NULL);

	return res;
}

HRESULT GetPropertyEx(IDispatch*d, LPOLESTR name, int arg, VARIANT* rtn)
{
	DISPID thisId;

	HRESULT res = d->GetIDsOfNames(IID_NULL, &name, 1, LOCALE_USER_DEFAULT, &thisId);

	VARIANT myVar;
	VariantInit(&myVar);

	myVar.vt = VT_UI1;
	myVar.bVal = arg;

	DISPPARAMS* dispparamsArgs = new DISPPARAMS;
	dispparamsArgs->rgvarg = &myVar;
	dispparamsArgs->cArgs = 1;
	dispparamsArgs->rgdispidNamedArgs = NULL;
	dispparamsArgs->cNamedArgs = 0;

	res = d->Invoke(thisId, IID_NULL, LOCALE_SYSTEM_DEFAULT,
		DISPATCH_PROPERTYGET, dispparamsArgs, rtn, NULL, NULL);

	return res;
}

HRESULT GetPropertyEx(IDispatch*d, LPOLESTR name, long arg, VARIANT* rtn, int type)
{
	DISPID thisId;

	//HRESULT res = d->GetIDsOfNames(IID_NULL, &name, 1, LOCALE_USER_DEFAULT, &thisId);

	if (name == LPOLESTR("Item") && type == 1)//Unknow as yet (RecordSet ?).
		thisId = 0;
	else if (name == LPOLESTR("Item") && type == 0)//Worksheets
		thisId = 170;
	else if (name == LPOLESTR("Item") && type == 2)//RecordSet
		thisId = 170;//This is wrong

	VARIANT myVar;
	VariantInit(&myVar);

	myVar.vt = VT_I4;
	myVar.lVal = arg;

	DISPPARAMS* dispparamsArgs = new DISPPARAMS;
	dispparamsArgs->rgvarg = &myVar;
	dispparamsArgs->cArgs = 1;
	dispparamsArgs->rgdispidNamedArgs = NULL;
	dispparamsArgs->cNamedArgs = 0;

	HRESULT res = d->Invoke(thisId, IID_NULL, LOCALE_SYSTEM_DEFAULT,
		DISPATCH_PROPERTYGET, dispparamsArgs, rtn, NULL, NULL);

	return res;
}

HRESULT PutPropertyEx(IDispatch* d, LPOLESTR name, const char* arg, VARIANT* rtn)
{
	DISPID mydispid = DISPID_PROPERTYPUT;

	DISPID thisId;

	//HRESULT res = d->GetIDsOfNames(IID_NULL, &name, 1, LOCALE_USER_DEFAULT, &thisId);

	if (name == LPOLESTR("CommandText"))
		thisId = 2;
	else if (name == LPOLESTR("Value2"))
		thisId = 1388;

	VARIANT myVar;
	VariantInit(&myVar);

	BSTR myB;
	myB = ConvertConstCharToBstr(arg);

	myVar.vt = VT_BSTR;
	myVar.bstrVal = myB;

	DISPPARAMS* dispparamsArgs = new DISPPARAMS;
	dispparamsArgs->rgvarg = &myVar;
	dispparamsArgs->cArgs = 1;
	dispparamsArgs->rgdispidNamedArgs = &mydispid;
	dispparamsArgs->cNamedArgs = 1;

	HRESULT res = d->Invoke(thisId, IID_NULL, LOCALE_SYSTEM_DEFAULT,
		DISPATCH_PROPERTYPUT, dispparamsArgs, NULL, NULL, NULL);

	return res;
}

HRESULT PutPropertyEx(IDispatch* d, LPOLESTR name, DATE arg, VARIANT* rtn)
{
	DISPID mydispid = DISPID_PROPERTYPUT;

	DISPID thisId;

	//HRESULT res = d->GetIDsOfNames(IID_NULL, &name, 1, LOCALE_USER_DEFAULT, &thisId);

	if (name == LPOLESTR("Value"))
		thisId = 0;

	VARIANT myVar;
	VariantInit(&myVar);

	myVar.vt = VT_DATE;
	myVar.date = arg;

	DISPPARAMS* dispparamsArgs = new DISPPARAMS;
	dispparamsArgs->rgvarg = &myVar;
	dispparamsArgs->cArgs = 1;
	dispparamsArgs->rgdispidNamedArgs = &mydispid;
	dispparamsArgs->cNamedArgs = 1;

	HRESULT res = d->Invoke(thisId, IID_NULL, LOCALE_SYSTEM_DEFAULT,
		DISPATCH_PROPERTYPUT, dispparamsArgs, NULL, NULL, NULL);

	return res;
}

HRESULT PutPropertyCharEx(IDispatch* d, LPOLESTR name, char* arg, VARIANT* rtn)
{
	DISPID mydispid = DISPID_PROPERTYPUT;

	DISPID thisId;

	//MessageBox(0, arg, "ll", 0);

	//HRESULT res = d->GetIDsOfNames(IID_NULL, &name, 1, LOCALE_USER_DEFAULT, &thisId);

	if (name == LPOLESTR("CommandText"))
		thisId = 2;
	else if (name == LPOLESTR("Value2"))
		thisId = 1388;
	else if (name == LPOLESTR("Value"))
		thisId = 0;

	VARIANT myVar;
	VariantInit(&myVar);

	BSTR myB;
	myB = ConvertConstCharToBstr(arg);

	myVar.vt = VT_BSTR;
	myVar.bstrVal = myB;

	DISPPARAMS* dispparamsArgs = new DISPPARAMS;
	dispparamsArgs->rgvarg = &myVar;
	dispparamsArgs->cArgs = 1;
	dispparamsArgs->rgdispidNamedArgs = &mydispid;
	dispparamsArgs->cNamedArgs = 1;

	HRESULT res = d->Invoke(thisId, IID_NULL, LOCALE_SYSTEM_DEFAULT,
		DISPATCH_PROPERTYPUT, dispparamsArgs, NULL, NULL, NULL);

	return res;
}

HRESULT PutPropertyEx(IDispatch* d, LPOLESTR name, IDispatch* arg, VARIANT* rtn)
{
	DISPID mydispid = DISPID_PROPERTYPUT;

	DISPID thisId;

	//HRESULT res = d->GetIDsOfNames(IID_NULL, &name, 1, LOCALE_USER_DEFAULT, &thisId);
	
	if (name == LPOLESTR("ActiveConnection"))
		thisId = 1;

	VARIANT myVar;
	VariantInit(&myVar);

	myVar.vt = VT_DISPATCH;
	myVar.pdispVal = arg;

	DISPPARAMS* dispparamsArgs = new DISPPARAMS;
	dispparamsArgs->rgvarg = &myVar;
	dispparamsArgs->cArgs = 1;
	dispparamsArgs->rgdispidNamedArgs = &mydispid;
	dispparamsArgs->cNamedArgs = 1;

	HRESULT res = d->Invoke(thisId, IID_NULL, LOCALE_SYSTEM_DEFAULT,
		DISPATCH_PROPERTYPUT, dispparamsArgs, NULL, NULL, NULL);

	return res;
}

HRESULT PutPropertyEx(IDispatch* d, LPOLESTR name, int arg, VARIANT* rtn)
{
	DISPID mydispid = DISPID_PROPERTYPUT;

	DISPID thisId;

	//HRESULT res = d->GetIDsOfNames(IID_NULL, &name, 1, LOCALE_USER_DEFAULT, &thisId);

	if (name == LPOLESTR("CursorLocation"))
		thisId = 15;
	else if (name == LPOLESTR("CommandType"))
		thisId = 7;
	else if (name == LPOLESTR("Value"))
		thisId = 0;
	else if (name == LPOLESTR("Type"))
		thisId = 2;

	VARIANT myVar;
	VariantInit(&myVar);

	myVar.vt = VT_UI1;
	myVar.bVal = arg;

	DISPPARAMS* dispparamsArgs = new DISPPARAMS;
	dispparamsArgs->rgvarg = &myVar;
	dispparamsArgs->cArgs = 1;
	dispparamsArgs->rgdispidNamedArgs = &mydispid;
	dispparamsArgs->cNamedArgs = 1;

	HRESULT res = d->Invoke(thisId, IID_NULL, LOCALE_SYSTEM_DEFAULT,
		DISPATCH_PROPERTYPUT, dispparamsArgs, NULL, NULL, NULL);

	return res;
}

HRESULT PutPropertyEx(IDispatch* d, LPOLESTR name, long arg, VARIANT* rtn)
{
	DISPID mydispid = DISPID_PROPERTYPUT;

	DISPID thisId;

	//HRESULT res = d->GetIDsOfNames(IID_NULL, &name, 1, LOCALE_USER_DEFAULT, &thisId);

	if (name == LPOLESTR("CursorLocation"))
		thisId = 15;
	else if (name == LPOLESTR("CommandType"))
		thisId = 7;
	else if (name == LPOLESTR("Value"))
		thisId = 0;
	else if (name == LPOLESTR("Type"))
		thisId = 2;

	VARIANT myVar;
	VariantInit(&myVar);

	myVar.vt = VT_I4;
	myVar.lVal = arg;

	DISPPARAMS* dispparamsArgs = new DISPPARAMS;
	dispparamsArgs->rgvarg = &myVar;
	dispparamsArgs->cArgs = 1;
	dispparamsArgs->rgdispidNamedArgs = &mydispid;
	dispparamsArgs->cNamedArgs = 1;

	HRESULT res = d->Invoke(thisId, IID_NULL, LOCALE_SYSTEM_DEFAULT,
		DISPATCH_PROPERTYPUT, dispparamsArgs, NULL, NULL, NULL);

	return res;
}

HRESULT PutPropertyEx(IDispatch* d, LPOLESTR name, LPSYSTEMTIME value, VARIANT* rtn)
{
	DISPID mydispid = DISPID_PROPERTYPUT;

	DISPID thisId;

	//HRESULT res = d->GetIDsOfNames(IID_NULL, &name, 1, LOCALE_USER_DEFAULT, &thisId);

	if (name == LPOLESTR("CursorLocation"))
		thisId = 15;
	else if (name == LPOLESTR("CommandType"))
		thisId = 7;
	else if (name == LPOLESTR("Value"))
		thisId = 0;

	VARIANT myVar;
	VariantInit(&myVar);

	DATE thisTime;
	SystemTimeToVariantTime(value, &thisTime);

	myVar.vt = VT_DATE;
	myVar.date = thisTime;

	DISPPARAMS* dispparamsArgs = new DISPPARAMS;
	dispparamsArgs->rgvarg = &myVar;
	dispparamsArgs->cArgs = 1;
	dispparamsArgs->rgdispidNamedArgs = &mydispid;
	dispparamsArgs->cNamedArgs = 1;

	HRESULT res = d->Invoke(thisId, IID_NULL, LOCALE_SYSTEM_DEFAULT,
		DISPATCH_PROPERTYPUT, dispparamsArgs, NULL, NULL, NULL);

	return res;
}

HRESULT GetIdsFromNames(IDispatch* d, LPOLESTR names, DISPID* rtns)
{
	HRESULT res = d->GetIDsOfNames(IID_NULL, &names, 1, LOCALE_USER_DEFAULT, rtns);

	return S_OK;
}

HRESULT CallMethod(IDispatch* d, LPOLESTR name, VARIANT* rtn)
{
	DISPID thisId;

	//HRESULT res = d->GetIDsOfNames(IID_NULL, &name, 1, LOCALE_USER_DEFAULT, &thisId);

	if (name == LPOLESTR("Close"))
		thisId = 5;
	else if (name == LPOLESTR("Execute"))
		thisId = 5;
	else if (name == LPOLESTR("MoveNext"))
		thisId = 1018;

	DISPPARAMS dispparamsNoArgs = {NULL, NULL, 0, 0};

	HRESULT res = d->Invoke(thisId, IID_NULL, LOCALE_SYSTEM_DEFAULT,
		DISPATCH_METHOD, &dispparamsNoArgs, rtn, NULL, NULL);

	return res;
}

HRESULT CallMethod(IDispatch* d, LPOLESTR name, const char* arg, VARIANT* rtn)
{
	DISPID thisId;

	if (name == LPOLESTR("Open"))
		thisId = 10;

	//d->GetIDsOfNames(IID_NULL, &name, 1, LOCALE_USER_DEFAULT, &thisId);

	//char* msg = new char[30];
	//sprintf_s(msg, 30, "%i", thisId);
	//MessageBox(0, msg, "Id", 0);

	VARIANT myVar;
	VariantInit(&myVar);

	BSTR myB;
	myB = ConvertConstCharToBstr(arg);

	myVar.vt = VT_BSTR;
	myVar.bstrVal = myB;

	DISPPARAMS* dispparamsArgs = new DISPPARAMS;
	dispparamsArgs->rgvarg = &myVar;
	dispparamsArgs->cArgs = 1;
	dispparamsArgs->rgdispidNamedArgs = NULL;
	dispparamsArgs->cNamedArgs = 0;

	HRESULT res = d->Invoke(thisId, IID_NULL, LOCALE_SYSTEM_DEFAULT,
		DISPATCH_METHOD, dispparamsArgs, rtn, NULL, NULL);

	return res;
}

HRESULT CallMethod(IDispatch* d, LPOLESTR name, IDispatch* arg, VARIANT* rtn)
{
	DISPID thisId;

	//HRESULT res = d->GetIDsOfNames(IID_NULL, &name, 1, LOCALE_USER_DEFAULT, &thisId);

	if (name == LPOLESTR("Append"))
		thisId = 3;

	VARIANT myVar;
	VariantInit(&myVar);

	myVar.vt = VT_DISPATCH;
	myVar.pdispVal = arg;

	DISPPARAMS* dispparamsArgs = new DISPPARAMS;
	dispparamsArgs->rgvarg = &myVar;
	dispparamsArgs->cArgs = 1;
	dispparamsArgs->rgdispidNamedArgs = NULL;
	dispparamsArgs->cNamedArgs = 0;

	HRESULT res = d->Invoke(thisId, IID_NULL, LOCALE_SYSTEM_DEFAULT,
		DISPATCH_METHOD, dispparamsArgs, rtn, NULL, NULL);

	return res;
}

HRESULT CallMethod(IDispatch* d, LPOLESTR name, int arg, VARIANT* rtn)
{
	DISPID thisId;

	HRESULT res = d->GetIDsOfNames(IID_NULL, &name, 1, LOCALE_USER_DEFAULT, &thisId);

	VARIANT myVar;
	VariantInit(&myVar);

	myVar.vt = VT_UI2;
	myVar.bVal = arg;

	DISPPARAMS* dispparamsArgs = new DISPPARAMS;
	dispparamsArgs->rgvarg = &myVar;
	dispparamsArgs->cArgs = 1;
	dispparamsArgs->rgdispidNamedArgs = NULL;
	dispparamsArgs->cNamedArgs = 0;

	res = d->Invoke(thisId, IID_NULL, LOCALE_SYSTEM_DEFAULT,
		DISPATCH_METHOD, dispparamsArgs, rtn, NULL, NULL);

	return res;
}

HRESULT CallMethod(IDispatch* d, LPOLESTR name, const char* arg, int arg2, int arg3, VARIANT* rtn)
{
	DISPID thisId;

	//HRESULT res = d->GetIDsOfNames(IID_NULL, &name, 1, LOCALE_USER_DEFAULT, &thisId);

	if (name == LPOLESTR("CreateParameter"))
		thisId = 6;

	VARIANT myVar[3];
	VariantInit(&myVar[0]);
	VariantInit(&myVar[1]);
	VariantInit(&myVar[2]);

	myVar[0].vt = VT_UI1;
	myVar[0].bVal = arg3;

	myVar[1].vt = VT_UI1;
	myVar[1].bVal = arg2;

	BSTR myArg = ConvertConstCharToBstr(arg);
	myVar[2].vt = VT_BSTR;
	myVar[2].bstrVal = myArg;

	DISPPARAMS* dispparamsArgs = new DISPPARAMS;
	dispparamsArgs->rgvarg = myVar;
	dispparamsArgs->cArgs = 3;
	dispparamsArgs->rgdispidNamedArgs = NULL;
	dispparamsArgs->cNamedArgs = 0;

	HRESULT res = d->Invoke(thisId, IID_NULL, LOCALE_SYSTEM_DEFAULT,
		DISPATCH_METHOD, dispparamsArgs, rtn, NULL, NULL);

	return res;
}

HRESULT CallMethod(IDispatch* d, LPOLESTR name, VARIANT* arg, VARIANT* arg2, long arg3, VARIANT* rtn)
{
	DISPID thisId;

	HRESULT res = d->GetIDsOfNames(IID_NULL, &name, 1, LOCALE_USER_DEFAULT, &thisId);

	VARIANT myVar[3];
	VariantInit(&myVar[0]);
	VariantInit(&myVar[1]);
	VariantInit(&myVar[2]);

	myVar[0].vt = VT_I4;
	myVar[0].lVal = arg3;

	myVar[1].vt = VT_BYREF | VT_ERROR;
	myVar[1].pscode = &arg2->scode;

	myVar[2].vt = VT_BYREF | VT_ERROR;
	myVar[2].pscode = &arg->scode;


	DISPPARAMS* dispparamsArgs = new DISPPARAMS;
	dispparamsArgs->rgvarg = myVar;
	dispparamsArgs->cArgs = 3;
	dispparamsArgs->rgdispidNamedArgs = NULL;
	dispparamsArgs->cNamedArgs = 0;

	res = d->Invoke(thisId, IID_NULL, LOCALE_SYSTEM_DEFAULT,
		DISPATCH_METHOD, dispparamsArgs, rtn, NULL, NULL);

	return res;
}

HRESULT CallMethod(IDispatch* d, LPOLESTR name, const char* command, IDispatch* arg, long arg2, long arg3, long arg4, VARIANT* rtn)
{
	DISPID thisId;

	HRESULT res = d->GetIDsOfNames(IID_NULL, &name, 1, LOCALE_USER_DEFAULT, &thisId);

	VARIANT myVar[5];
	VariantInit(&myVar[0]);
	VariantInit(&myVar[1]);
	VariantInit(&myVar[2]);
	VariantInit(&myVar[3]);
	VariantInit(&myVar[4]);

	myVar[0].vt = VT_I4;
	myVar[0].lVal = arg4;

	myVar[1].vt = VT_I4;
	myVar[1].lVal = arg3;

	myVar[2].vt = VT_I4;
	myVar[2].lVal = arg2;

	myVar[3].vt = VT_DISPATCH;
	myVar[3].pdispVal = arg;

	myVar[4].vt = VT_BSTR;
	myVar[4].bstrVal = ConvertConstCharToBstr(command);

	DISPPARAMS* dispparamsArgs = new DISPPARAMS;
	dispparamsArgs->rgvarg = myVar;
	dispparamsArgs->cArgs = 5;
	dispparamsArgs->rgdispidNamedArgs = NULL;
	dispparamsArgs->cNamedArgs = 0;

	res = d->Invoke(thisId, IID_NULL, LOCALE_SYSTEM_DEFAULT,
		DISPATCH_METHOD, dispparamsArgs, rtn, NULL, NULL);

	return res;
}

BSTR ConvertCharToBstr(char* inVal)
{
	int len = MultiByteToWideChar(CP_ACP, 0, inVal, strlen(inVal), 0, 0);
	BSTR bstr = SysAllocStringLen(0, len);
	MultiByteToWideChar(CP_ACP, 0, inVal, strlen(inVal), bstr, len);

	return bstr;
}

BSTR ConvertUCharToBstr(unsigned char* inVal, int length)
{
	int len = MultiByteToWideChar(CP_ACP, MB_PRECOMPOSED, (LPCSTR)inVal, length, 0, 0);
	BSTR bstr = SysAllocStringLen(0, len);
	MultiByteToWideChar(CP_ACP, MB_PRECOMPOSED, (LPCSTR)inVal, len, (LPWSTR)bstr, len);

	return bstr;
}

BSTR ConvertConstCharToBstr(const char* inVal)
{
	int len = MultiByteToWideChar(CP_ACP, 0, inVal, strlen(inVal), 0, 0);
	BSTR bstr = SysAllocStringLen(0, len);
	MultiByteToWideChar(CP_ACP, 0, inVal, strlen(inVal), bstr, len);

	return bstr;
}

char* ConvertBstrToChar(BSTR inVal)
{
	char* buffer = new char[3000];

	int iLen = WideCharToMultiByte(CP_ACP, 0, inVal, -1, buffer, 0, NULL, 0);
	WideCharToMultiByte(CP_ACP, 0, inVal, -1, buffer, iLen, NULL, 0);

	return buffer;
}

char* ConvertToChar(double value)
{
	char* newValue = new char[30];
	sprintf_s(newValue, 30, "%lf", value);

	return newValue;
}

char* ConvertToChar(int value)
{
	char* newValue = new char[30];
	sprintf_s(newValue, 30, "%i", value);

	return newValue;
}

char* ConvertDateToChar(DATE date)
{
	time_t newDate = static_cast<time_t>((date - 25569) * 86400);

	struct tm * myTime = localtime(&newDate);
	
	char* charDate = new char[11];
	strftime(charDate, 11, "%d/%m/%Y", myTime);

	return charDate;
}

char* ConvertTmToChar(const tm* date)
{
	char* charDate = new char[11];
	strftime(charDate, 11, "%d/%m/%Y", date);

	return charDate;
}

tm* ConvertDateToTm(DATE date)
{
	time_t newDate = static_cast<time_t>((date - 25569) * 86400);

	struct tm * myTime = localtime(&newDate);

	return myTime;
}

DATE ConvertDate(tm* date)
{
	time_t tTime;
	tTime = mktime(date);

	DATE newDate = tTime / 86400.0 + 25569;

	return newDate;
}