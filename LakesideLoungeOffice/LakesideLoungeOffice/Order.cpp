
#include "Order.h"
#include "Command.h"
#include "Global.h"

Order::Order()
{
	orderItemIndex = 0;
}

Order::Order(int vId, char* vName, int vCustomerType, tm* vDate, bool deep)
{
	id = vId;
	name = vName;
	customerType = vCustomerType;
	memcpy(&date, vDate, sizeof(date));

	if (deep)
		FillOrderItems();

	orderItemIndex = 0;
}

void Order::FillOrderItems()
{
	Connection* myCon = new Connection();
	Command* myComm = new Command();
	myComm->SetConnection(myCon->GetConnection());
	myComm->SetCommandText("GetOrderItems");
	myComm->SetCommandType(4);
	myComm->AddParameter("@orderId", (long)id);

	RecordSet* rs = myComm->ExecuteStoredProcedure();

	bool eof = rs->EndOF();

	while (!eof)
	{
		int itemId = rs->Value(0).lVal;
		int variationId = rs->Value(2).lVal;
		int inOutStatus = rs->Value(3).lVal;
		int discount = rs->Value(4).lVal;

		OrderItem* item = new OrderItem(itemId, id, variationId, inOutStatus, discount, &date);
		item->FillOrderItemComponents(myCon);

		orderItems.push_back(item);

		rs->MoveNext();
		eof = rs->EndOF();
	}

	myCon->Close();
}

int Order::GetId()
{
	return id;
}

char* Order::GetName()
{
	return name;
}

int Order::GetCustomerType()
{
	return customerType;
}

tm Order::GetDate()
{
	struct tm newDate;
	memcpy(&newDate, &date, sizeof(newDate));

	return newDate;
}

void Order::SetId(int vId)
{
	id = vId;
}

void Order::SetName(char* vName)
{
	name = vName;
}

void Order::SetCustomerType(int vCustomerType)
{
	customerType = vCustomerType;
}

void Order::SetDate(tm* vDate)
{
	memcpy(&date, vDate, sizeof(date));
}

OrderItem* Order::GetCurrentOrderItem()
{
	return orderItems[orderItemIndex];
}

void Order::MoveNext()
{
	orderItemIndex++;
}

bool Order::EndOfFile()
{
	return orderItemIndex == orderItems.size();
};