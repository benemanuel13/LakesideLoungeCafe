
#include "Control.h"

Control::Control()
{}

Control::~Control()
{}

HRESULT Control::SetParentEx(HWND parent)
{
	_parent = parent;

	return S_OK;
}

HRESULT Control::SetTitleEx(const char* title)
{
	_title = title;

	return S_OK;
}

HRESULT Control::SetName(const char* vName)
{
	name = vName;

	return S_OK;
}

const char* Control::GetName()
{
	return name;
}

HRESULT Control::SetPosAndSize(int x, int y, int width, int height)
{
	_x = x;
	_y = y;
	_width = width;
	_height = height;

	return S_OK;
}

HWND Control::Show(const char* controlName, int styles)
{
	hWnd = CreateWindow(controlName, _title, styles,
		_x, _y, _width, _height, _parent, NULL, (HINSTANCE) GetWindowLong(hWnd, GWL_HINSTANCE), NULL);

	if(!hWnd)
	{
		MessageBoxA(NULL, "Unable to CREATE control.", NULL, MB_ICONSTOP | MB_OK);
		return 0;
	}

	ShowWindow(hWnd, 5);
	UpdateWindow(hWnd);

	return hWnd;
}