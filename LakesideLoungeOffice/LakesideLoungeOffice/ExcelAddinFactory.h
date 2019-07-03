#include <objbase.h>

class AddinFactory : IClassFactory
{
public:
	virtual HRESULT __stdcall QueryInterface(const IID& iid, void** ppv);
	virtual ULONG __stdcall AddRef();
	virtual ULONG __stdcall Release();

	virtual HRESULT __stdcall CreateInstance(IUnknown* pUnknownOuter,
		const IID& iid, void** ppv);
	virtual HRESULT __stdcall LockServer(BOOL bLock);

	AddinFactory() :m_cRef(1) {}
	virtual ~AddinFactory();

private:
	long m_cRef;
};
