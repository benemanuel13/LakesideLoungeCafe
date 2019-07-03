
#include "ComboBox.h"

HWND ComboBox::Show()
{
	return Control::Show("COMBOBOX", WS_VISIBLE | WS_CHILD | CBS_DROPDOWNLIST | CBS_HASSTRINGS | WS_OVERLAPPED | CBS_AUTOHSCROLL);
}

HRESULT ComboBox::Clicked()
{
	Click();

	return S_OK;
}
