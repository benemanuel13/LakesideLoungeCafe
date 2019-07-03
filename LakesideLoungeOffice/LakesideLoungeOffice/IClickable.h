
#if !defined(IClickableEx)
#define IClickableEx

class IClickable
{
public:
	virtual HRESULT Clicked() = 0;
};

#endif