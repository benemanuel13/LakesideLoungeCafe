// Factory.cpp: implementation of the CFactory class.
//
//////////////////////////////////////////////////////////////////////

#include <objbase.h>
#include "ExcelAddinFactory.h"
#include "ExcelAddin.h"
#include "Global.h"

#include <stdio.h>

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

HRESULT __stdcall AddinFactory::QueryInterface(const IID& iid, void** ppv)
{
	if((iid == IID_IUnknown) || (iid == IID_IClassFactory))
	{
		*ppv = static_cast<IClassFactory*>(this);
	}
	else
	{
		*ppv = NULL;
		return E_NOINTERFACE;
	}

	reinterpret_cast<IUnknown*>(*ppv)->AddRef();
	return S_OK;
}

ULONG __stdcall AddinFactory::AddRef()
{
	return InterlockedIncrement(&m_cRef);
}

ULONG __stdcall AddinFactory::Release()
{
	if(InterlockedDecrement(&m_cRef) == 0)
	{
		delete this;
		return 0;
	}

	return m_cRef;
}

HRESULT __stdcall AddinFactory::CreateInstance(IUnknown* pUnknownOuter,
										   const IID& iid, void** ppv)
{
	if(pUnknownOuter != NULL)
	{
		return CLASS_E_NOAGGREGATION;
	}

	Addin* pC = new Addin;

	if(pC == NULL)
	{
		return E_OUTOFMEMORY;
	}

	HRESULT hr = pC->QueryInterface(iid, ppv);
	pC->Release();

	return hr;
}

HRESULT __stdcall AddinFactory::LockServer(BOOL bLock)
{
	if(bLock)
	{
		//InterlockedIncrement(&g_cServerLocks);
	}
	else
	{
		//InterlockedDecrement(&g_cServerLocks);
	}

	return S_OK;
}

AddinFactory::~AddinFactory()
{

}