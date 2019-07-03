
#include "Button.h"

HWND Button::Show()
{
	return Control::Show("BUTTON", WS_VISIBLE | WS_CHILD | BS_DEFPUSHBUTTON);
}

HRESULT Button::Clicked()
{
	Click();

	return S_OK;
}
