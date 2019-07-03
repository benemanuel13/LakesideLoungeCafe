#include <string>

#include <windows.h>
#include <winuser.h>

#include <stdio.h>
#include <OleAuto.h>
#include <oaIdl.h>

#include "LakesideLoungeOffice_h.h"
#include "Report.h"
#include "Global.h"
#include "Form.h"

//#include <comutil.h>

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

//IUnknown

IDispatch* application;
extern Form* myForm;

HRESULT __stdcall Report::QueryInterface(const IID& iid, void** ppv)
{
	if(iid == IID_IUnknown)
	{
		*ppv = static_cast<IUnknown*>(this);
	}
	else if(iid == IID_IDispatch)
	{
		*ppv = static_cast<IDispatch*>(this);
	}
	else if(iid == IID_IReport)
	{
		*ppv = static_cast<IReport*>(this);
	}
	else
	{
		*ppv = NULL;
		return E_NOINTERFACE;
	}
	
	reinterpret_cast<IUnknown*>(*ppv)->AddRef();

	return S_OK;
}

ULONG __stdcall Report::AddRef()
{
	return InterlockedIncrement(&m_cRef);
}

ULONG __stdcall Report::Release()
{
	if(InterlockedDecrement(&m_cRef) == 0)
	{
		delete this;
		return 0;
	}

	return m_cRef;
}

//------------
//IDispatch

STDMETHODIMP Report::GetTypeInfo(UINT it, LCID lcid, ITypeInfo **ppti)
{
	(*ppti = m_pTypeInfo)->AddRef();
	return S_OK;
}

STDMETHODIMP Report::GetTypeInfoCount(UINT *pit)
{
	*pit = 0;
	return S_OK;
}

STDMETHODIMP Report::GetIDsOfNames(REFIID riid, OLECHAR **pNames, UINT cNames, LCID lcid, DISPID *pdispids)
{
	char buffer[30];

	int iLen = WideCharToMultiByte(CP_ACP, 0, *pNames, -1, buffer, 0, NULL, 0);
	WideCharToMultiByte(CP_ACP, 0, *pNames, -1, buffer, iLen, NULL, 0);

	DISPID dispMinus1 = DISPID(-1);
	DISPID disp0 = DISPID(0);
	DISPID disp1 = DISPID(1);
	DISPID disp2 = DISPID(2);

	char v0[] = "SetName";
	char v1[] = "GetName";
	char v2[] = "Save";

	int cmp = strcmp(buffer, v0);

	if(cmp == 0)
	{
		*pdispids = disp0;
		return S_OK;
	}

	cmp = strcmp(buffer, v1);

	if(cmp == 0)
	{
		*pdispids = disp1;
		return S_OK;
	}

	cmp = strcmp(buffer, v2);

	if(cmp == 0)
	{
		*pdispids = disp2;
		return S_OK;
	}

	*pdispids = dispMinus1;

	return S_OK;
}

STDMETHODIMP Report::Invoke(DISPID id, REFIID riid, LCID lcid, WORD wFlags, DISPPARAMS *pd, VARIANT *pVarResult,
		EXCEPINFO *pe, UINT *pu)
{
	int numParams = pd->cArgs;

	VARIANT myV;

	myV.vt = VT_BSTR;

	BSTR myVal;

	if(id == DISPID(0))
	{
		if(numParams == 1 && pd->rgvarg[0].vt == VT_BSTR)
		{
			currentFile.name = ConvertBstrToChar(pd->rgvarg[0].bstrVal);

			myVal = pd->rgvarg[0].bstrVal;
		}
		else
		{
			myVal = SysAllocString(OLESTR("Value0"));
		}
	}
	else if(id == DISPID(1))
		myVal = ConvertCharToBstr(currentFile.name);
	else if(id == DISPID(2))
	{
		if(numParams == 1 && pd->rgvarg[0].vt == VT_ERROR)
		{
			//char* msj = new char[30];
			//sprintf(msj, "%ld", pd->rgvarg[0].vt);
			//MessageBox(0, "Error", "o", 0);
		}
		else if(numParams == 1 && pd->rgvarg[0].vt == (VT_BYREF | VT_VARIANT))
		{
			
			myVal = SysAllocString(OLESTR("Success"));
		}
		else
			myVal = SysAllocString(OLESTR("SetBytes"));
	}

		myVal = SysAllocString(OLESTR("Failure"));

	myV.bstrVal = myVal;

	*pVarResult = myV;

	SysFreeString(myVal);

	return S_OK;
}

STDMETHODIMP __stdcall Report::Init(IDispatch *arg1, reportType repType, VARIANT *arg2)
{
	application = arg1;

	myForm = new Form();
	myForm->SetParentEx(::g_hModule);

	Button* button = new Button();
	button->SetTitleEx("Click Me!");
	button->SetPosAndSize(10, 10, 150, 50);
	button->Click = WeeklyReportOld;

	myForm->AddControl("button1", button);

	myForm->Show(0);

	VARIANT myVar;
	VariantInit(&myVar);

	BSTR myB;
	myB = SysAllocString(OLESTR("Hello From Ben !"));

	myVar.vt = VT_BSTR;
	myVar.bstrVal = myB;

	*arg2 = myVar;

	SysFreeString(myB);
 
	return S_OK;
}

//------------
HRESULT __stdcall Report::MyValue(std::string* myVal)
{
	myVal->append("Ben Emanuel");
	return S_OK;
}

void WeeklyReportOld()
{
	VARIANT wrkBooksVar;
	HRESULT res = GetPropertyEx(application, LPOLESTR("Workbooks"), &wrkBooksVar);
	IDispatch* wrkBooks = wrkBooksVar.pdispVal;

	VARIANT wrkBookItemVar;
	res = GetPropertyEx(wrkBooks, LPOLESTR("Item"), 1, &wrkBookItemVar, 0);

	IDispatch* wrkBook = wrkBookItemVar.pdispVal;

	VARIANT wrkSheetsVar;
	res = GetPropertyEx(wrkBook, LPOLESTR("Worksheets"), &wrkSheetsVar);

	IDispatch* wrkSheets = wrkSheetsVar.pdispVal;

	VARIANT wrkSheetItemVar;
	res = GetPropertyEx(wrkSheets, LPOLESTR("Item"), 1, &wrkSheetItemVar, 0);

	IDispatch* sheet = wrkSheetItemVar.pdispVal;

	VARIANT cellsVar;
	
	res = GetPropertyEx(sheet, LPOLESTR("Cells"), 3, 4, &cellsVar);

	IDispatch* cells = cellsVar.pdispVal;

	
	res = PutPropertyEx(cells, LPOLESTR("Value2"), "Weekly123", NULL);

	Database* newDB = new Database();
}

Report::Report() : m_cRef(1)
{
	InterlockedIncrement(&g_cComponents);
}

Report::~Report()
{
	InterlockedDecrement(&g_cComponents);
}
