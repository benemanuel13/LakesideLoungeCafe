#include "Button.h"
#include "ComboBox.h"
#include "Label.h"

#include <map>
#include <windows.h>

using namespace std;

typedef map<const char*, Control*> ControlsMap;
typedef map<HWND, Control*> ControlsMessagedMap;

class Form
{
public:
	void SetParentEx(HINSTANCE parent);
	void AddControl(const char* name, Control* control);
	void ClearControls();

	void SetMode(int vMode);
	int GetMode();

	void SetStartDay(int day);
	void SetStartMonth(int month);
	void SetStartYear(int year);

	int GetStartDay();
	int GetStartMonth();
	int GetStartYear();

	void SetEndDay(int day);
	void SetEndMonth(int month);
	void SetEndYear(int year);

	int GetEndDay();
	int GetEndMonth();
	int GetEndYear();

	void Close();

	void FillStartDays(int day, int month, int year, HWND handle);

	ControlsMessagedMap Controls();
	ControlsMap InnerControls();

	HRESULT Show(int mode);

	HWND hWnd;

	Form();
	virtual ~Form();

private:
	HINSTANCE _parent;

	ControlsMessagedMap controlsMessagedMap;
	ControlsMap controlsMap;

	int mode;

	int sDay;
	int sMonth;
	int sYear;

	int eDay;
	int eMonth;
	int eYear;
};