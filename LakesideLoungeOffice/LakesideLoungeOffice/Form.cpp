#include <objbase.h>
#include <windows.h>
#include <winuser.h>

#include "Global.h"
#include "Form.h"

extern Form* myForm;

LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	int wmId, wmEvent;

	HWND hWndControl;
	HRESULT res;
	Control* thisControl;
	Button* btn;

	PAINTSTRUCT ps;
	HDC hdc;

	//TCHAR szHello[MAX_LOADSTRING];
	//LoadString(hInst, IDS_HELLO, szHello, MAX_LOADSTRING);

	switch (message) 
	{
		case WM_COMMAND:
			wmId    = LOWORD(wParam); 
			wmEvent = HIWORD(wParam);

			if(wmEvent == BN_CLICKED)
			{
				hWndControl = (HWND) lParam;

				thisControl = myForm->Controls()[hWndControl];

				btn = (Button*)thisControl;
				res = ((IClickable*)btn)->Clicked();
			}
			else if (wmEvent == CBN_SELCHANGE)
			{
				hWndControl = (HWND)lParam;

				thisControl = myForm->Controls()[hWndControl];

				const char* name = thisControl->GetName();

				if (name == "StartDay")
				{
					int dayIndex = SendMessage(hWndControl, CB_GETCURSEL, 0, 0);
					myForm->SetStartDay(dayIndex + 1);
				}
				else if (name == "StartMonth")
				{
					Control* daysCombo = myForm->InnerControls()["startDay"];
					Control* yearCombo = myForm->InnerControls()["startYear"];
					
					int dayIndex = SendMessage(daysCombo->hWnd, CB_GETCURSEL, 0, 0);
					int index = SendMessage(hWndControl, CB_GETCURSEL, 0, 0);
					int yearIndex = SendMessage(yearCombo->hWnd, CB_GETCURSEL, 0, 0);

					myForm->FillStartDays(dayIndex, index, yearIndex + 2018, daysCombo->hWnd);

					dayIndex = SendMessage(daysCombo->hWnd, CB_GETCURSEL, 0, 0);
					
					myForm->SetStartDay(dayIndex + 1);
					myForm->SetStartMonth(index + 1);
				}
				else if (name == "StartYear")
				{
					int yearIndex = SendMessage(hWndControl, CB_GETCURSEL, 0, 0);
					myForm->SetStartYear(2018 + yearIndex);
				}
				else if (name == "EndDay")
				{
					int dayIndex = SendMessage(hWndControl, CB_GETCURSEL, 0, 0);
					myForm->SetEndDay(dayIndex + 1);
				}
				else if (name == "EndMonth")
				{
					Control* daysCombo = myForm->InnerControls()["endDay"];
					Control* yearCombo = myForm->InnerControls()["endYear"];

					int dayIndex = SendMessage(daysCombo->hWnd, CB_GETCURSEL, 0, 0);
					int index = SendMessage(hWndControl, CB_GETCURSEL, 0, 0);
					int yearIndex = SendMessage(yearCombo->hWnd, CB_GETCURSEL, 0, 0);

					myForm->FillStartDays(dayIndex, index, yearIndex + 2018, daysCombo->hWnd);

					dayIndex = SendMessage(daysCombo->hWnd, CB_GETCURSEL, 0, 0);

					myForm->SetEndDay(dayIndex + 1);
					myForm->SetEndMonth(index + 1);
				}
				else if (name == "EndYear")
				{
					int yearIndex = SendMessage(hWndControl, CB_GETCURSEL, 0, 0);
					myForm->SetEndYear(2018 + yearIndex);
				}
			}
			break;
		case WM_PAINT:
			hdc = BeginPaint(hWnd, &ps);
			// TODO: Add any drawing code here...
			RECT rt;
			GetClientRect(hWnd, &rt);
			//DrawText(hdc, szHello, strlen(szHello), &rt, DT_CENTER);
			EndPaint(hWnd, &ps);
			break;
		case WM_DESTROY:
			myForm->ClearControls();
			PostQuitMessage(0);
			break;
		default:
			return DefWindowProc(hWnd, message, wParam, lParam);
   }
	return 0;
}

Form::Form()
{
	TCHAR *szDoc = NULL;
	WNDCLASSEX wcex;

	wcex.cbSize = sizeof(WNDCLASSEX); 

	wcex.style			= CS_HREDRAW | CS_VREDRAW;
	wcex.lpfnWndProc	= (WNDPROC)WndProc;
	wcex.cbClsExtra		= 0;
	wcex.cbWndExtra		= 0;
	wcex.hInstance		= _parent;
	//wcex.hIcon			= LoadIcon(g_hModule, (LPCTSTR)IDI_TRIAL2);
	wcex.hIcon			= NULL;
	wcex.hCursor		= LoadCursor(NULL, IDC_ARROW);
	wcex.hbrBackground	= (HBRUSH)(COLOR_WINDOW+1);
	//wcex.lpszMenuName	= (LPCSTR)IDC_TRIAL2;
	wcex.lpszMenuName	= NULL;
	wcex.lpszClassName	= "LakesideLoungeWindow";
	//wcex.hIconSm		= LoadIcon(wcex.g_hModule, (LPCTSTR)IDI_SMALL);
	wcex.hIconSm		= NULL;

	BOOL ans = RegisterClassEx(&wcex);

	if(!ans)
	{
		MessageBoxA(NULL, "Unable to REGISTER main window", NULL, MB_ICONSTOP | MB_OK);
	}
}

Form::~Form()
{
}

void Form::SetMode(int vMode)
{
	mode = vMode;
}

int Form::GetMode()
{
	return mode;
}

void Form::ClearControls()
{
	controlsMessagedMap.clear();
}

ControlsMessagedMap Form::Controls()
{
	return controlsMessagedMap;
}

ControlsMap Form::InnerControls()
{
	return controlsMap;
}

void Form::SetParentEx(HINSTANCE parent)
{
	_parent = parent;
}

HRESULT Form::Show(int vMode)
{
	mode = vMode;

	if (mode == 0)
		hWnd = CreateWindow("LakesideLoungeWindow", "Lakeside Lounge Reports", WS_OVERLAPPEDWINDOW, CW_USEDEFAULT, CW_USEDEFAULT, 540, 290, NULL, NULL, _parent, NULL);
	else if (mode == 1)
		hWnd = CreateWindow("LakesideLoungeWindow", "Lakeside Lounge Reports", WS_OVERLAPPEDWINDOW, CW_USEDEFAULT, CW_USEDEFAULT, 540, 440, NULL, NULL, _parent, NULL);

	UnregisterClass("LakesideLoungeWindow", _parent);

	Control* reportTitleLabel = controlsMap["reportTitleLabel"];

	if (mode == 0)
		reportTitleLabel->SetTitleEx("Lakeside Weekly Sales Report");
	else if (mode == 1)
		reportTitleLabel->SetTitleEx("Lakeside Dated Sales Report");

	reportTitleLabel->SetParentEx(hWnd);
	reportTitleLabel->SetPosAndSize(15, 5, 500, 50);
	HWND controlHwnd0 = ((IControl*)reportTitleLabel)->Show();
	controlsMessagedMap.insert(ControlsMessagedMap::value_type(controlHwnd0, reportTitleLabel));

	HFONT titleFont = CreateFont(48, 0, 0, 0, FW_DONTCARE, FALSE, TRUE, FALSE, DEFAULT_CHARSET, OUT_OUTLINE_PRECIS,
			CLIP_DEFAULT_PRECIS, CLEARTYPE_QUALITY, VARIABLE_PITCH, TEXT("Impact"));

	SendMessage(controlHwnd0, WM_SETFONT, (WPARAM)titleFont, MAKELPARAM(true, 0));

	Control* startLabel = controlsMap["startLabel"];
	startLabel->SetParentEx(hWnd);
	startLabel->SetPosAndSize(15, 65, 275, 30);
	HWND controlHwnda = ((IControl*)startLabel)->Show();
	controlsMessagedMap.insert(ControlsMessagedMap::value_type(controlHwnda, startLabel));

	HFONT startLabelFont = CreateFont(28, 0, 0, 0, FW_DONTCARE, FALSE, FALSE, FALSE, DEFAULT_CHARSET, OUT_OUTLINE_PRECIS,
		CLIP_DEFAULT_PRECIS, CLEARTYPE_QUALITY, VARIABLE_PITCH, TEXT("Arial"));

	SendMessage(controlHwnda, WM_SETFONT, (WPARAM)startLabelFont, MAKELPARAM(true, 0));

	if (mode == 1)
	{
		Control* endLabel = controlsMap["endLabel"];
		endLabel->SetParentEx(hWnd);
		endLabel->SetPosAndSize(15, 195, 275, 30);
		HWND controlHwndendLabel = ((IControl*)endLabel)->Show();
		controlsMessagedMap.insert(ControlsMessagedMap::value_type(controlHwndendLabel, endLabel));

		HFONT endLabelFont = CreateFont(28, 0, 0, 0, FW_DONTCARE, FALSE, FALSE, FALSE, DEFAULT_CHARSET, OUT_OUTLINE_PRECIS,
			CLIP_DEFAULT_PRECIS, CLEARTYPE_QUALITY, VARIABLE_PITCH, TEXT("Arial"));

		SendMessage(controlHwndendLabel, WM_SETFONT, (WPARAM)endLabelFont, MAKELPARAM(true, 0));
	}

	Control* okButton = controlsMap["okButton"];
	okButton->SetParentEx(hWnd);

	if (mode == 0)
		okButton->SetPosAndSize(205, 185, 150, 50);
	else if (mode == 1)
		okButton->SetPosAndSize(205, 330, 150, 50);

	HWND controlHwnd = ((IControl*)okButton)->Show();
	controlsMessagedMap.insert(ControlsMessagedMap::value_type(controlHwnd, okButton));

	Control* cancelButton = controlsMap["cancelButton"];
	cancelButton->SetParentEx(hWnd);

	if (mode == 0)
		cancelButton->SetPosAndSize(365, 185, 150, 50);
	else if (mode == 1)
		cancelButton->SetPosAndSize(365, 330, 150, 50);

	HWND controlHwndCancel = ((IControl*)cancelButton)->Show();
	controlsMessagedMap.insert(ControlsMessagedMap::value_type(controlHwndCancel, cancelButton));

	Control* startDayLabel = controlsMap["startDayLabel"];
	startDayLabel->SetParentEx(hWnd);
	startDayLabel->SetPosAndSize(15, 105, 75, 30);
	HWND controlHwnd2 = ((IControl*)startDayLabel)->Show();
	controlsMessagedMap.insert(ControlsMessagedMap::value_type(controlHwnd2, startDayLabel));

	Control* startDay = controlsMap["startDay"];
	startDay->SetParentEx(hWnd);
	startDay->SetPosAndSize(15, 140, 75, 250);
	HWND controlHwnd3 = ((IControl*)startDay)->Show();
	startDay->hWnd = controlHwnd3;
	controlsMessagedMap.insert(ControlsMessagedMap::value_type(controlHwnd3, startDay));

	HWND controlHwndEndDay = NULL;
	if (mode == 1)
	{
		Control* endDayLabel = controlsMap["endDayLabel"];
		endDayLabel->SetParentEx(hWnd);
		endDayLabel->SetPosAndSize(15, 235, 75, 30);
		HWND controlHwndEndDayLabel = ((IControl*)endDayLabel)->Show();
		controlsMessagedMap.insert(ControlsMessagedMap::value_type(controlHwndEndDayLabel, endDayLabel));

		Control* endDay = controlsMap["endDay"];
		endDay->SetParentEx(hWnd);
		endDay->SetPosAndSize(15, 275, 75, 250);
		controlHwndEndDay = ((IControl*)endDay)->Show();
		endDay->hWnd = controlHwndEndDay;
		controlsMessagedMap.insert(ControlsMessagedMap::value_type(controlHwndEndDay, endDay));
	}

	Control* startMonthLabel = controlsMap["startMonthLabel"];
	startMonthLabel->SetParentEx(hWnd);
	startMonthLabel->SetPosAndSize(100, 105, 75, 30);
	HWND controlHwnd4 = ((IControl*)startMonthLabel)->Show();
	controlsMessagedMap.insert(ControlsMessagedMap::value_type(controlHwnd4, startMonthLabel));

	Control* startMonth = controlsMap["startMonth"];
	startMonth->SetParentEx(hWnd);
	startMonth->SetPosAndSize(100, 140, 140, 250);
	HWND controlHwnd5 = ((IControl*)startMonth)->Show();
	controlsMessagedMap.insert(ControlsMessagedMap::value_type(controlHwnd5, startMonth));

	HWND controlHwndEndMonth = NULL;
	if (mode == 1)
	{
		Control* endMonthLabel = controlsMap["endMonthLabel"];
		endMonthLabel->SetParentEx(hWnd);
		endMonthLabel->SetPosAndSize(100, 235, 75, 30);
		HWND controlHwndEndMonthLabel = ((IControl*)endMonthLabel)->Show();
		controlsMessagedMap.insert(ControlsMessagedMap::value_type(controlHwndEndMonth, endMonthLabel));

		Control* endMonth = controlsMap["endMonth"];
		endMonth->SetParentEx(hWnd);
		endMonth->SetPosAndSize(100, 275, 140, 250);
		controlHwndEndMonth = ((IControl*)endMonth)->Show();
		controlsMessagedMap.insert(ControlsMessagedMap::value_type(controlHwndEndMonth, endMonth));
	}

	Control* startYearLabel = controlsMap["startYearLabel"];
	startYearLabel->SetParentEx(hWnd);
	startYearLabel->SetPosAndSize(250, 105, 75, 30);
	HWND controlHwnd6 = ((IControl*)startYearLabel)->Show();
	controlsMessagedMap.insert(ControlsMessagedMap::value_type(controlHwnd6, startYearLabel));

	Control* startYear = controlsMap["startYear"];
	startYear->SetParentEx(hWnd);
	startYear->SetPosAndSize(250, 140, 140, 250);
	HWND controlHwnd7 = ((IControl*)startYear)->Show();
	controlsMessagedMap.insert(ControlsMessagedMap::value_type(controlHwnd7, startYear));

	HWND controlHwndEndYear = NULL;
	if (mode == 1)
	{
		Control* endYearLabel = controlsMap["endYearLabel"];
		endYearLabel->SetParentEx(hWnd);
		endYearLabel->SetPosAndSize(250, 235, 75, 30);
		HWND controlHwndEndYearLabel = ((IControl*)endYearLabel)->Show();
		controlsMessagedMap.insert(ControlsMessagedMap::value_type(controlHwndEndYearLabel, endYearLabel));

		Control* endYear = controlsMap["endYear"];
		endYear->SetParentEx(hWnd);
		endYear->SetPosAndSize(250, 275, 140, 250);
		controlHwndEndYear = ((IControl*)endYear)->Show();
		controlsMessagedMap.insert(ControlsMessagedMap::value_type(controlHwndEndYear, endYear));
	}

	ShowWindow(hWnd, 5);

	SendMessage(controlHwnd5, (UINT)CB_ADDSTRING, 0, (LPARAM)"January");
	SendMessage(controlHwnd5, (UINT)CB_ADDSTRING, 0, (LPARAM)"February");
	SendMessage(controlHwnd5, (UINT)CB_ADDSTRING, 0, (LPARAM)"March");
	SendMessage(controlHwnd5, (UINT)CB_ADDSTRING, 0, (LPARAM)"April");
	SendMessage(controlHwnd5, (UINT)CB_ADDSTRING, 0, (LPARAM)"May");
	SendMessage(controlHwnd5, (UINT)CB_ADDSTRING, 0, (LPARAM)"June");
	SendMessage(controlHwnd5, (UINT)CB_ADDSTRING, 0, (LPARAM)"July");
	SendMessage(controlHwnd5, (UINT)CB_ADDSTRING, 0, (LPARAM)"August");
	SendMessage(controlHwnd5, (UINT)CB_ADDSTRING, 0, (LPARAM)"September");
	SendMessage(controlHwnd5, (UINT)CB_ADDSTRING, 0, (LPARAM)"October");
	SendMessage(controlHwnd5, (UINT)CB_ADDSTRING, 0, (LPARAM)"November");
	SendMessage(controlHwnd5, (UINT)CB_ADDSTRING, 0, (LPARAM)"December");

	if (mode == 1)
	{
		SendMessage(controlHwndEndMonth, (UINT)CB_ADDSTRING, 0, (LPARAM)"January");
		SendMessage(controlHwndEndMonth, (UINT)CB_ADDSTRING, 0, (LPARAM)"February");
		SendMessage(controlHwndEndMonth, (UINT)CB_ADDSTRING, 0, (LPARAM)"March");
		SendMessage(controlHwndEndMonth, (UINT)CB_ADDSTRING, 0, (LPARAM)"April");
		SendMessage(controlHwndEndMonth, (UINT)CB_ADDSTRING, 0, (LPARAM)"May");
		SendMessage(controlHwndEndMonth, (UINT)CB_ADDSTRING, 0, (LPARAM)"June");
		SendMessage(controlHwndEndMonth, (UINT)CB_ADDSTRING, 0, (LPARAM)"July");
		SendMessage(controlHwndEndMonth, (UINT)CB_ADDSTRING, 0, (LPARAM)"August");
		SendMessage(controlHwndEndMonth, (UINT)CB_ADDSTRING, 0, (LPARAM)"September");
		SendMessage(controlHwndEndMonth, (UINT)CB_ADDSTRING, 0, (LPARAM)"October");
		SendMessage(controlHwndEndMonth, (UINT)CB_ADDSTRING, 0, (LPARAM)"November");
		SendMessage(controlHwndEndMonth, (UINT)CB_ADDSTRING, 0, (LPARAM)"December");
	}

	SendMessage(controlHwnd7, (UINT)CB_ADDSTRING, 0, (LPARAM)"2018");
	SendMessage(controlHwnd7, (UINT)CB_ADDSTRING, 0, (LPARAM)"2019");
	SendMessage(controlHwnd7, (UINT)CB_ADDSTRING, 0, (LPARAM)"2020");
	SendMessage(controlHwnd7, (UINT)CB_ADDSTRING, 0, (LPARAM)"2021");
	SendMessage(controlHwnd7, (UINT)CB_ADDSTRING, 0, (LPARAM)"2022");
	SendMessage(controlHwnd7, (UINT)CB_ADDSTRING, 0, (LPARAM)"2023");
	SendMessage(controlHwnd7, (UINT)CB_ADDSTRING, 0, (LPARAM)"2024");

	if (mode == 1)
	{
		SendMessage(controlHwndEndYear, (UINT)CB_ADDSTRING, 0, (LPARAM)"2018");
		SendMessage(controlHwndEndYear, (UINT)CB_ADDSTRING, 0, (LPARAM)"2019");
		SendMessage(controlHwndEndYear, (UINT)CB_ADDSTRING, 0, (LPARAM)"2020");
		SendMessage(controlHwndEndYear, (UINT)CB_ADDSTRING, 0, (LPARAM)"2021");
		SendMessage(controlHwndEndYear, (UINT)CB_ADDSTRING, 0, (LPARAM)"2022");
		SendMessage(controlHwndEndYear, (UINT)CB_ADDSTRING, 0, (LPARAM)"2023");
		SendMessage(controlHwndEndYear, (UINT)CB_ADDSTRING, 0, (LPARAM)"2024");
	}

	time_t rawtime;
	struct tm * timeinfo;
	time(&rawtime);
	timeinfo = localtime(&rawtime);

	sDay = timeinfo->tm_mday;
	sMonth = timeinfo->tm_mon + 1;
	sYear = timeinfo->tm_year + 1900;

	eDay = timeinfo->tm_mday;
	eMonth = timeinfo->tm_mon + 1;
	eYear = timeinfo->tm_year + 1900;

	SendMessage(controlHwnd5, CB_SETCURSEL, (WPARAM)sMonth - 1, (LPARAM)0);//Set month
	SendMessage(controlHwnd7, CB_SETCURSEL, (WPARAM)sYear - 2018, (LPARAM)0);//Set year

	if (mode == 1)
	{
		SendMessage(controlHwndEndMonth, CB_SETCURSEL, (WPARAM)eMonth - 1, (LPARAM)0);//Set month
		SendMessage(controlHwndEndYear, CB_SETCURSEL, (WPARAM)eYear - 2018, (LPARAM)0);//Set year
	}

	FillStartDays(sDay - 1, sMonth - 1, sYear, controlHwnd3);

	if(mode == 1)
		FillStartDays(eDay - 1, eMonth - 1, eYear, controlHwndEndDay);

	UpdateWindow(hWnd);
	UpdateWindow(controlHwnd2);

	MSG msg;
	while (GetMessage(&msg, (HWND) NULL, 0, 0)) 
    { 
        TranslateMessage(&msg); 
        DispatchMessage(&msg); 
    }

	return S_OK;
}

void Form::AddControl(const char* name, Control* control)
{
	controlsMap.insert(ControlsMap::value_type(name, control));
}

void Form::SetStartDay(int day)
{
	sDay = day;
}

void Form::SetStartMonth(int month)
{
	sMonth = month;
}

void Form::SetStartYear(int year)
{
	sYear = year;
}

int Form::GetStartDay()
{
	return sDay;
}

int Form::GetStartMonth()
{
	return sMonth;
}

int Form::GetStartYear()
{
	return sYear;
}

void Form::SetEndDay(int day)
{
	eDay = day;
}

void Form::SetEndMonth(int month)
{
	eMonth = month;
}

void Form::SetEndYear(int year)
{
	eYear = year;
}

int Form::GetEndDay()
{
	return eDay;
}

int Form::GetEndMonth()
{
	return eMonth;
}

int Form::GetEndYear()
{
	return eYear;
}

void Form::FillStartDays(int day, int month, int year, HWND handle)
{
	SendMessage(handle, CB_RESETCONTENT, 0, 0);

	int max;

	switch (month)
	{
	case 0:
	case 2:
	case 4:
	case 6:
	case 7:
	case 9:
	case 11:
			max = 31;
			break;
	case 3:
	case 5:
	case 8:
	case 10:
			max = 30;
			break;
	case 1:
		max = 28;//for now...
		break;
	}

	for (int i = 1; i <= max; i++)
	{
		char* thisDay = new char[3];
		sprintf_s(thisDay, 3, "%i", i);

		LPCTSTR* dayText = new LPCTSTR(thisDay);
		SendMessage(handle, (UINT)CB_ADDSTRING, (WPARAM)0, (LPARAM)thisDay);
	}

	if (day > max - 1)
		day = max - 1;

	SendMessage(handle, CB_SETCURSEL, (WPARAM)day, (LPARAM)0);
}

void Form::Close()
{
	DestroyWindow(hWnd);
}