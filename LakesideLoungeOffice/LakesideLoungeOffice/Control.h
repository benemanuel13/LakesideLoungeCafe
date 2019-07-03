#include <objbase.h>
#include <windows.h>

#include "IControl.h"

#if !defined(ControlEx)
#define ControlEx

class Control : public IControl
{
public:
	Control();
	virtual ~Control();

	HRESULT SetTitleEx(const char* title);
	HRESULT SetName(const char* vName);
	HRESULT SetPosAndSize(int x, int y, int width, int height);
	HRESULT SetParentEx(HWND parent);

	const char* GetName();

	HWND hWnd;
protected:
	virtual HWND Show() = 0;
	HWND Show(const char* controlName, int styles);

private:
	HWND _parent;
	
	const char* name;
	const char* _title;

	int _x;
	int _y;
	int	_width;
	int _height;
};

#endif