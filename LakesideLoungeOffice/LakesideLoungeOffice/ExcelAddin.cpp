#include <string>

#include <windows.h>
#include <winuser.h>

#include <stdio.h>
#include <OleAuto.h>
#include <oaIdl.h>

#include "LakesideLoungeOffice_h.h"
#include "ExcelAddin.h"
#include "Global.h"

#include "Database.h"
#include "Form.h"

#include "resource.h"

#include <ctime>

#include "Olectl.h"

extern Form* myForm;

#pragma warning(disable:4996)

//IUnknown

IDispatch *appl;

HRESULT __stdcall Addin::QueryInterface(const IID& iid, void** ppv)
{
	if(iid == IID_IUnknown)
	{
		*ppv = static_cast<IAddin*>(this);
	}
	else if(iid == IID_IDispatch)
	{
		*ppv = static_cast<IAddin*>(this);
	}
	else if(iid == IID_IAddin)
	{
		*ppv = static_cast<IAddin*>(this);
	}
	else if(iid == IID__IDTExtensibility2)
	{
		*ppv = static_cast<IAddin*>(this);
	}
	else if(iid == IID_IRibbonExtensibility)
	{
		*ppv = static_cast<IRibbonExtensibility*>(this);
	}
	else
	{
		*ppv = NULL;
		return E_NOINTERFACE;
	}
	
	reinterpret_cast<IUnknown*>(*ppv)->AddRef();

	return S_OK;
}

ULONG __stdcall Addin::AddRef()
{
	return InterlockedIncrement(&m_cRef);
}

ULONG __stdcall Addin::Release()
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

STDMETHODIMP Addin::GetTypeInfo(UINT it, LCID lcid, ITypeInfo **ppti)
{
	(*ppti = m_pTypeInfo)->AddRef();
	return S_OK;
}

STDMETHODIMP Addin::GetTypeInfoCount(UINT *pit)
{
	*pit = 0;
	return S_OK;
}

STDMETHODIMP Addin::GetIDsOfNames(REFIID riid, OLECHAR **pNames, UINT cNames, LCID lcid, DISPID *pdispids)
{
	char buffer[30];

	int iLen = WideCharToMultiByte(CP_ACP, 0, *pNames, -1, buffer, 0, NULL, 0);
	WideCharToMultiByte(CP_ACP, 0, *pNames, -1, buffer, iLen, NULL, 0);

	DISPID dispMinus1 = DISPID(-1);
	DISPID disp0 = DISPID(0);
	DISPID disp1 = DISPID(1);
	DISPID disp2 = DISPID(2);
	DISPID disp3 = DISPID(3);
	DISPID disp4 = DISPID(4);

	char v0[] = "SetName";
	char v1[] = "GetName";
	char v2[] = "GetSalesImage";
	char v3[] = "WeeklyReport";
	char v4[] = "DatedReport";

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

	cmp = strcmp(buffer, v3);

	if(cmp == 0)
	{
		*pdispids = disp3;
		return S_OK;
	}

	cmp = strcmp(buffer, v4);

	if (cmp == 0)
	{
		*pdispids = disp4;
		return S_OK;
	}

	*pdispids = dispMinus1;

	return S_OK;
}

STDMETHODIMP Addin::Invoke(DISPID id, REFIID riid, LCID lcid, WORD wFlags, DISPPARAMS *pd, VARIANT *pVarResult,
		EXCEPINFO *pe, UINT *pu)
{
	int numParams = pd->cArgs;

	VARIANT myV;

	myV.vt = VT_BSTR;

	BSTR myVal = SysAllocString(OLESTR("Value0"));

	if(id == DISPID(0))
	{
		myVal = SysAllocString(OLESTR("Value0"));
	}
	else if(id == DISPID(1))
		myVal = SysAllocString(OLESTR("Value1"));
	else if(id == DISPID(2))
	{
		//myVal = SysAllocString(OLESTR("SheetBackground"));
		//myV.vt = VT_I4;
		//myV.llVal = (LONGLONG)GetRibbonImage(0);

		//return S_OK;

		myV = GetRibbonImage(0);

		*pVarResult = myV;

		return S_OK;
	}
	else if (id == DISPID(3))
	{
		myForm->Show(0);

		return S_OK;
	}
	else if (id == DISPID(4))
	{
		myForm->Show(1);

		return S_OK;
	}

		//myVal = SysAllocString(OLESTR("Failure"));

	myV.bstrVal = myVal;

	*pVarResult = myV;

	SysFreeString(myVal);

	return S_OK;
}

VARIANT GetRibbonImage(int image)
{
	IPictureDisp* picture;
	IID IID_Picture;
	HRESULT hRes = E_FAIL;
	IIDFromString(L"{7BF80980-BF32-101A-8BBB-00AA00300CAB}", &IID_Picture);

	HBITMAP image1 = (HBITMAP)::LoadImage(GetModuleHandle("LakesideLoungeOffice"), MAKEINTRESOURCE(IDB_BITMAP2), IMAGE_BITMAP, 0, 0, 0);
	
	if (image1 == NULL)
		MessageBox(0, "Could not load image,", "Error", 0);

	PICTDESC picDesc = { 0 };
	picDesc.bmp.hbitmap = image1;
	picDesc.picType = PICTYPE_BITMAP;
	picDesc.cbSizeofstruct = sizeof(picDesc);
	OleCreatePictureIndirect(&picDesc, IID_Picture, true, reinterpret_cast<LPVOID*>(&picture));

	VARIANT myV;
	myV.vt = VT_DISPATCH;
	myV.pdispVal = picture;

	return myV;
}

void RunReport()
{
	if (myForm->GetMode() == 0)
		WeeklyReport();
	else if (myForm->GetMode() == 1)
		DatedReport();

	myForm->Close();
}

void WeeklyReport()
{
	Database* newDB = new Database();
	newDB->Init(appl);

	struct tm * startTime = new tm();
	startTime->tm_mday = myForm->GetStartDay();
	startTime->tm_mon = myForm->GetStartMonth() - 1;
	startTime->tm_year = myForm->GetStartYear() - 1900;

	newDB->WeeklyReport(startTime);
	newDB->Terminate();
}

void DatedReport()
{
	Database* newDB = new Database();
	newDB->Init(appl);

	struct tm * startTime = new tm();
	startTime->tm_mday = myForm->GetStartDay();
	startTime->tm_mon = myForm->GetStartMonth() - 1;
	startTime->tm_year = myForm->GetStartYear() - 1900;

	struct tm * endTime = new tm();
	endTime->tm_mday = myForm->GetEndDay();
	endTime->tm_mon = myForm->GetEndMonth() - 1;
	endTime->tm_year = myForm->GetEndYear() - 1900;

	newDB->DatedReport(startTime, endTime);
	newDB->Terminate();
}

void Cancel()
{
	myForm->Close();
}

HRESULT __stdcall Addin::OnConnection(struct IDispatch * d,enum ext_ConnectMode m,struct IDispatch * i,struct tagSAFEARRAY ** array)
{
	appl = d;

	myForm = new Form();
	myForm->SetParentEx(::g_hModule);

	Label* reportTitleLabel = new Label();
	reportTitleLabel->SetTitleEx("Report - ");
	myForm->AddControl("reportTitleLabel", reportTitleLabel);

	Label* startLabel = new Label();
	startLabel->SetTitleEx("Start Date");
	myForm->AddControl("startLabel", startLabel);

	Label* endLabel = new Label();
	endLabel->SetTitleEx("End Date");
	myForm->AddControl("endLabel", endLabel);

	Button* button = new Button();
	button->SetName("OkButton");
	button->SetTitleEx("Ok");
	button->Click = RunReport;
	myForm->AddControl("okButton", button);

	Button* cancelButton = new Button();
	cancelButton->SetName("CancelButton");
	cancelButton->SetTitleEx("Cancel");
	cancelButton->Click = Cancel;
	myForm->AddControl("cancelButton", cancelButton);

	Label* startDayLabel = new Label();
	startDayLabel->SetTitleEx("Day");
	myForm->AddControl("startDayLabel", startDayLabel);

	ComboBox* startDay = new ComboBox();
	startDay->SetName("StartDay");
	startDay->SetTitleEx("");
	myForm->AddControl("startDay", startDay);

	Label* endDayLabel = new Label();
	endDayLabel->SetTitleEx("Day");
	myForm->AddControl("endDayLabel", endDayLabel);

	ComboBox* endDay = new ComboBox();
	endDay->SetName("EndDay");
	endDay->SetTitleEx("");
	myForm->AddControl("endDay", endDay);

	Label* startMonthLabel = new Label();
	startMonthLabel->SetTitleEx("Month");
	myForm->AddControl("startMonthLabel", startMonthLabel);

	ComboBox* startMonth = new ComboBox();
	startMonth->SetName("StartMonth");
	startMonth->SetTitleEx("");
	myForm->AddControl("startMonth", startMonth);

	Label* endMonthLabel = new Label();
	endMonthLabel->SetTitleEx("Month");
	myForm->AddControl("endMonthLabel", endMonthLabel);

	ComboBox* endMonth = new ComboBox();
	endMonth->SetName("EndMonth");
	endMonth->SetTitleEx("");
	myForm->AddControl("endMonth", endMonth);

	Label* startYearLabel = new Label();
	startYearLabel->SetTitleEx("Year");
	myForm->AddControl("startYearLabel", startYearLabel);

	ComboBox* startYear = new ComboBox();
	startYear->SetName("StartYear");
	startYear->SetTitleEx("");
	myForm->AddControl("startYear", startYear);
	
	Label* endYearLabel = new Label();
	endYearLabel->SetTitleEx("Year");
	myForm->AddControl("endYearLabel", endYearLabel);

	ComboBox* endYear = new ComboBox();
	endYear->SetName("EndYear");
	endYear->SetTitleEx("");
	myForm->AddControl("endYear", endYear);

	return S_OK;
}

HRESULT __stdcall Addin::OnAddInsUpdate(struct tagSAFEARRAY ** array)
{
	return S_OK;
}

HRESULT __stdcall Addin::OnStartupComplete(struct tagSAFEARRAY ** array)
{
	return S_OK;
}

HRESULT __stdcall Addin::OnBeginShutdown(struct tagSAFEARRAY ** array)
{
	myForm->Close();

	return S_OK;
}

HRESULT __stdcall Addin::OnDisconnection(enum ext_DisconnectMode m,struct tagSAFEARRAY ** array)
{
	delete myForm;

	return S_OK;
}

HRESULT __stdcall Addin::GetCustomUI(BSTR RibbonID, BSTR* RibbonXml)
{
	BSTR myB;

	myB = SysAllocString(OLESTR("<customUI xmlns=\"http://schemas.microsoft.com/office/2006/01/customui\"><ribbon><tabs><tab id=\"LakesideLounge01\" label=\"Lakeside Lounge\"><group id=\"SimpleControls\" label=\"Sales Reports\"><button id=\"Button1\" label=\"Weekly Sales Report\" size=\"large\" onAction=\"WeeklyReport\" getImage=\"GetSalesImage\" /><button id=\"Button2\" label=\"Dated Sales Report\" size=\"large\" onAction=\"DatedReport\" getImage=\"GetSalesImage\" /></group></tab></tabs></ribbon></customUI>"));
	*RibbonXml = myB;

	SysFreeString(myB);

	return S_OK;
}

Addin::Addin() : m_cRef(1)
{
	InterlockedIncrement(&g_cComponents);
}

Addin::~Addin()
{
	InterlockedDecrement(&g_cComponents);
}
