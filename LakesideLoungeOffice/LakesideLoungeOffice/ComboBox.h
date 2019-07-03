#pragma once
#include "Control.h"
#include "IClickable.h"
//#include "Report.h"

class ComboBox : public IClickable, public Control
{
public:
	HWND Show();
	HRESULT Clicked();

	void(*Click)(void);
};