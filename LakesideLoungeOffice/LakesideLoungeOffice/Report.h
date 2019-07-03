#include <string>
#include "LakesideLoungeOffice_h.h"

#if !defined(AFX_MPNT_H__AA31C8C0_EBCA_11E7_A3BD_001CF0FAD303__INCLUDED_)
#define AFX_MPNT_H__AA31C8C0_EBCA_11E7_A3BD_001CF0FAD303__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "File.h"
#include "Database.h"
//#include "Form.h"

void WeeklyReportOld();

class Report : IReport
{
public:
	virtual HRESULT __stdcall QueryInterface(const IID& iid, void** ppv);
	virtual ULONG __stdcall AddRef();
	virtual ULONG __stdcall Release();

	virtual STDMETHODIMP GetTypeInfo(UINT it, LCID lcid, ITypeInfo **ppti);
	virtual STDMETHODIMP GetTypeInfoCount(UINT *pit);
	virtual STDMETHODIMP GetIDsOfNames(REFIID riid, OLECHAR **pNames, UINT cNames, LCID lcid, DISPID *pdispids);
	virtual STDMETHODIMP Invoke(DISPID id, REFIID riid, LCID lcid, WORD wFlags, DISPPARAMS *pd, VARIANT *pVarResult,
		EXCEPINFO *pe, UINT *pu);

	virtual HRESULT __stdcall MyValue(std::string* myVal);
	virtual HRESULT __stdcall Init(IDispatch *arg1, reportType repType, VARIANT *arg2);

	Report();
	virtual ~Report();
	
private:
	long m_cRef;

	ITypeInfo *m_pTypeInfo;

	File currentFile;
};

#endif // !defined(AFX_MPNT_H__AA31C8C0_EBCA_11E7_A3BD_001CF0FAD303__INCLUDED_)
