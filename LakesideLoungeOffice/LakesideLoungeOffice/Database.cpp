#include "Database.h"
#include "Connection.h"
#include "Command.h"
#include "RecordSet.h"
#include "Variation.h"
#include "Order.h"

#include "Global.h"

void Database::Init(IDispatch* application)
{
	appl = application;
}

void Database::Terminate()
{
	appl = NULL;
}

void Database::AllReport()
{
	Orders* orders = new Orders();
	orders->FillAll();

	int row = 1;

	while (!orders->EndOfFile())
	{
		Order* order = orders->GetCurrentOrder();

		SetCell(row, 1, ConvertToChar(order->GetId()));
		SetCell(row, 2, order->GetName());
		SetCell(row, 3, ConvertToChar(order->GetCustomerType()));
		SetCell(row, 4, ConvertTmToChar(&order->GetDate()));

		row++;
		orders->MoveNext();
	}
}

void Database::WeeklyReport(tm* startDate)
{
	Orders* myOrders = new Orders();
	myOrders->FillWeekly(startDate);

	SalesReport(myOrders, 0);
}

void Database::DatedReport(tm* startDate, tm* endDate)
{
	Orders* myOrders = new Orders();
	myOrders->FillDated(startDate, endDate);

	SalesReport(myOrders, 1);
}

void Database::SalesReport(Orders* orders, int reportType)
{
	int row = 1;

	bool eof = orders->EndOfFile();

	if (eof)
		return;

	double totalForDay = 0;
	double vatForDay = 0;
	//double orderTotal = 0;

	tm currentDate;

	Order* order = orders->GetCurrentOrder();

	while (!eof)
	{
		//SetCell(row, 9, ConvertToChar(order->GetId()));

		currentDate = order->GetDate();

		while (!order->EndOfFile())
		{
			OrderItem* item = order->GetCurrentOrderItem();
			double thisPrice = item->GetPrice();

			if (item->GetVatAble())
				vatForDay += thisPrice * 0.20;

			totalForDay += thisPrice;

			//orderTotal += item->GetPrice();

			//SetCell(row, 6, ConvertToChar(item->GetPrice()));
			//SetCell(row, 7, ConvertToChar(item->GetId()));
			//row++;

			order->MoveNext();
		}

		//SetCell(row, 4, ConvertToChar(orderTotal));
		//row++;
		//return;

		//orderTotal = 0;

		orders->MoveNext();
		eof = orders->EndOfFile();

		if (!eof)
			order = orders->GetCurrentOrder();

		tm theDate = order->GetDate();

		time_t t1 = mktime(&theDate);
		time_t t2 = mktime(&currentDate);

		double diff = difftime(t1, t2);

		if (eof || diff != 0)
		{
			SetCell(row, 1, ConvertTmToChar(&currentDate));
			SetCell(row, 2, ConvertToChar(vatForDay));
			SetCell(row, 3, ConvertToChar(totalForDay));

			vatForDay = 0;
			totalForDay = 0;
			
			row++;
		}
	}
}

void Database::Test()
{
	Connection* myCon = new Connection();
	Command* myComm = new Command();
	myComm->SetConnection(myCon->GetConnection());
	myComm->SetCommandText("GetSubVariations");
	myComm->SetCommandType(4);
	myComm->AddParameter("@ParentId", 2);

	RecordSet* rs = myComm->ExecuteStoredProcedure();
	int row = 1;

	bool eof = rs->EndOF();

	while(!eof)
	{
		VARIANT vVal = rs->Value(1);

		//if(vVal.vt == VT_BSTR)
		//	MessageBox(0, "XXX", "XXX", 0);

			
		SetCell(row, 1, ConvertBstrToChar(vVal.bstrVal));
		row++;

		rs->MoveNext();
		eof = rs->EndOF();
	}

	RecordSet* records = myComm->ExecuteNonQuery("Select * from Variations where Id = 1");

	eof = records->EndOF();

	while(!eof)
	{
		
		records->MoveNext();

		MessageBox(0, "In loop Text Based", "Ok", 0);

		eof = records->EndOF();
	}
}

void Database::SetCell(int r, int c, char* val)
{
	VARIANT wrkBooksVar;
	HRESULT res = GetPropertyEx(appl, LPOLESTR("Workbooks"), &wrkBooksVar);
	IDispatch* wrkBooks = wrkBooksVar.pdispVal;

	VARIANT wrkBookItemVar;
	res = GetPropertyEx(wrkBooks, LPOLESTR("Item"), 1, &wrkBookItemVar, 0);

	IDispatch* wrkBook = wrkBookItemVar.pdispVal;

	VARIANT wrkSheetsVar;
	res = GetPropertyEx(wrkBook, LPOLESTR("Worksheets"), &wrkSheetsVar);

	IDispatch* wrkSheets = wrkSheetsVar.pdispVal;

	VARIANT wrkSheetItemVar;
	res = GetPropertyEx(wrkSheets, LPOLESTR("Item"), 1, &wrkSheetItemVar, 0);

	IDispatch* sheet = wrkSheetItemVar.pdispVal;

	VARIANT cellsVar;
	res = GetPropertyEx(sheet, LPOLESTR("Cells"), r, c, &cellsVar);

	IDispatch* cells = cellsVar.pdispVal;

	res = PutPropertyEx(cells, LPOLESTR("Value2"), val, NULL);
}

Database::Database()
{
}

Database::~Database()
{	
}
