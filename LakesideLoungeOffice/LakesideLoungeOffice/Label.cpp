
#include "Label.h"

HWND Label::Show()
{
	return Control::Show("STATIC", WS_VISIBLE | WS_CHILD | SS_LEFT);
}
