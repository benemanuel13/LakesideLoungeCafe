
#include "Variation.h"
#include "Connection.h"
#include "Command.h"
#include "RecordSet.h"
#include "Global.h"

void Variation::Fill(int vId, tm* date)
{
	Connection* myCon = new Connection();
	Command* myComm = new Command();
	myComm->SetConnection(myCon->GetConnection());
	myComm->SetCommandText("GetVariation");
	myComm->SetCommandType(4);
	myComm->AddParameter("@Id", (long)vId);

	RecordSet* rs = myComm->ExecuteStoredProcedure();

	id = vId;
	parentId = rs->Value(0).lVal;
	points = rs->Value(3).dblVal;
	pointPrice = (double)rs->Value(4).cyVal.int64 / 10000;
	vatStatus = rs->Value(7).bVal;

	FillComponents(date);

	Command* myComm2 = new Command();
	myComm2->SetConnection(myCon->GetConnection());
	myComm2->SetCommandText("GetPrice");
	myComm2->SetCommandType(4);
	myComm2->AddParameter("@parentId", (long)id);
	myComm2->AddParameter("@parentType", 0);

	RecordSet* rs2 = myComm2->ExecuteStoredProcedure();

	if (rs2->EndOF())
	{
		price = 0;
		myCon->Close();

		return;
	}

	while (!rs2->EndOF())
	{
		price = (double)rs2->Value(0).cyVal.int64 / 10000;

		if (!(date < ConvertDateToTm(rs2->Value(1).date)))
			break;

		rs2->MoveNext();
	}

	myCon->Close();
}

void Variation::FillComponents(tm* date)
{
	Connection* myCon = new Connection();
	Command* myComm = new Command();
	myComm->SetConnection(myCon->GetConnection());
	myComm->SetCommandText("GetVariationComponents");
	myComm->SetCommandType(4);
	myComm->AddParameter("@parentId", (long)id);

	RecordSet* rs = myComm->ExecuteStoredProcedure();

	while (!rs->EndOF())
	{
		int componentId = rs->Value(0).lVal;
		double points = rs->Value(6).dblVal;

		Component* newComponent = new Component(componentId, points);
		components.insert(ComponentsMap::value_type(componentId, newComponent));

		rs->MoveNext();
	}

	myCon->Close();
}

double Variation::GetComponentPrice(int id, tm* date)
{
	Connection* myCon = new Connection();
	Command* myComm2 = new Command();
	myComm2->SetConnection(myCon->GetConnection());
	myComm2->SetCommandText("GetPrice");
	myComm2->SetCommandType(4);
	myComm2->AddParameter("@parentId", (long)id);
	myComm2->AddParameter("@parentType", 1);

	RecordSet* rs2 = myComm2->ExecuteStoredProcedure();

	if (rs2->EndOF())
	{
		price = 0;
		return price;
	}

	while (!rs2->EndOF())
	{
		price = (double)rs2->Value(0).cyVal.int64 / 10000;

		if (!(date < ConvertDateToTm(rs2->Value(1).date)))
			break;

		rs2->MoveNext();
	}

	myCon->Close();

	return price;
}

void Variation::SetId(int value)
{
	id = value;
}

void Variation::SetParentId(int value)
{
	parentId = value;
}

void Variation::SetPrice(double value)
{
	price = value;
}

void Variation::SetPoints(float value)
{
	points = value;
}

void Variation::SetPointPrice(double value)
{
	pointPrice = value;
}

void Variation::SetVatStatus(int value)
{
	vatStatus = value;
}

int Variation::GetId()
{
	return id;
}

int Variation::GetParentId()
{
	return parentId;
}

double Variation::GetPrice()
{
	return price;
}

float Variation::GetPoints()
{
	return points;
}

double Variation::GetPointPrice()
{
	return pointPrice;
}

int Variation::GetVatStatus()
{
	return vatStatus;
}

Component* Variation::GetComponent(int id)
{
	return components[id];
};