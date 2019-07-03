
#include "Orders.h"
#include "Connection.h"
#include "Command.h"
#include "Global.h"

Orders::Orders()
{
	orderIndex = 0;
}

void Orders::AddOrder(Order* order)
{
	orders.push_back(order);
}

void Orders::FillAll()
{
	Connection* myCon = new Connection();
	Command* myComm = new Command();
	myComm->SetConnection(myCon->GetConnection());
	myComm->SetCommandText("GetAllOrders");
	myComm->SetCommandType(4);

	RecordSet* rs = myComm->ExecuteStoredProcedure();

	bool eof = rs->EndOF();

	while (!eof)
	{
		int id = rs->Value(0).lVal;
		char* name = ConvertBstrToChar(rs->Value(1).bstrVal);
		int customerType = rs->Value(2).bVal;
		const tm* date = ConvertDateToTm(rs->Value(3).date);

		struct tm newDate;
		memcpy(&newDate, date, sizeof(newDate));

		Order* order = new Order(id, name, customerType, &newDate, false);
		
		orders.push_back(order);
		
		rs->MoveNext();
		eof = rs->EndOF();
	}

	myCon->Close();
}

void Orders::FillDated(tm* startDate, tm* endDate)
{
	Connection* myCon = new Connection();
	Command* myComm = new Command();
	myComm->SetConnection(myCon->GetConnection());
	myComm->SetCommandText("GetOrdersDated");
	myComm->SetCommandType(4);
	myComm->AddParameter("@startDate", ConvertDate(startDate));
	myComm->AddParameter("@endDate", ConvertDate(endDate));

	RecordSet* rs = myComm->ExecuteStoredProcedure();

	bool eof = rs->EndOF();

	while (!eof)
	{
		int id = rs->Value(0).lVal;
		char* name = ConvertBstrToChar(rs->Value(1).bstrVal);
		int customerType = rs->Value(2).bVal;
		tm* date = ConvertDateToTm(rs->Value(3).date);

		Order* order = new Order(id, name, customerType, date, true);

		orders.push_back(order);

		rs->MoveNext();
		eof = rs->EndOF();
	}

	myCon->Close();
}

void Orders::FillWeekly(tm* startDay)
{
	Connection* myCon = new Connection();
	Command* myComm = new Command();
	myComm->SetConnection(myCon->GetConnection());
	myComm->SetCommandText("GetOrdersWeekly");
	myComm->SetCommandType(4);
	myComm->AddParameter("@startDate", ConvertDate(startDay));

	RecordSet* rs = myComm->ExecuteStoredProcedure();

	bool eof = rs->EndOF();

	while (!eof)
	{
		int id = rs->Value(0).lVal;
		char* name = ConvertBstrToChar(rs->Value(1).bstrVal);
		int customerType = rs->Value(2).bVal;
		tm* myDate = ConvertDateToTm(rs->Value(3).date);

		Order* order = new Order(id, name, customerType, myDate, true);
		
		orders.push_back(order);

		rs->MoveNext();
		eof = rs->EndOF();
	}

	myCon->Close();	
}

int Orders::GetOrderCount()
{
	return orders.size();
}

Order* Orders::GetOrder(int index)
{
	return orders[index];
}

Order* Orders::GetCurrentOrder()
{
	return orders[orderIndex];
}

void Orders::MoveNext()
{
	orderIndex++;
}

bool Orders::EndOfFile()
{
	return orderIndex == orders.size();
}