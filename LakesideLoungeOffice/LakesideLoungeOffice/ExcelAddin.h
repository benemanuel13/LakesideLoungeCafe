#include <string>
#include "LakesideLoungeOffice_h.h"

//void TimetToFileTime(time_t t, LPFILETIME pft);
void WeeklyReport();
void DatedReport();
VARIANT GetRibbonImage(int image);

class Addin : IAddin, IRibbonExtensibility
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

	virtual HRESULT __stdcall OnConnection(struct IDispatch * d,enum ext_ConnectMode m,struct IDispatch * i,struct tagSAFEARRAY ** array);
	virtual HRESULT __stdcall OnAddInsUpdate(struct tagSAFEARRAY ** array);
	virtual HRESULT __stdcall OnStartupComplete(struct tagSAFEARRAY ** array);
	virtual HRESULT __stdcall OnBeginShutdown(struct tagSAFEARRAY ** array);
	virtual HRESULT __stdcall OnDisconnection(enum ext_DisconnectMode m,struct tagSAFEARRAY ** array);

	virtual HRESULT __stdcall GetCustomUI(BSTR RibbonID, BSTR* RibbonXml);

	Addin();
	virtual ~Addin();
	
private:
	long m_cRef;

	ITypeInfo *m_pTypeInfo;
};