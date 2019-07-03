
#include "OrderItem.h"
#include "Command.h"
#include "Global.h"

OrderItem::OrderItem(int vId, int vOrderId, int vVariationId, int vInOutStatus, int vDiscount, tm* vDate)
{
	id = vId;
	orderId = vId;
	variationId = vVariationId;
	inOutStatus = vInOutStatus;
	discount = vDiscount;

	memcpy(&date, vDate, sizeof(date));
}

bool OrderItem::GetVatAble()
{
	Variation* lastVariation = new Variation();
	lastVariation->Fill(variationId, &date);

	while (lastVariation->GetId() != 1)
	{
		if (lastVariation->GetVatStatus() == 2)
			if (inOutStatus == 1)
				return true;
			else
				return false;
		else if (lastVariation->GetVatStatus() == 3)
			return true;

		int parentId = lastVariation->GetParentId();

		delete lastVariation;
		lastVariation = new Variation();
		lastVariation->Fill(parentId, &date);
	}

	return false;
}

double OrderItem::GetPrice()
{
	double total = 0;

	Variation* variation = OrderIsItemBased();
	//Variation* variation = NULL;

	if(variation != NULL)
	{ 
		float totalPoints = 0;
		float variationPoints = variation->GetPoints();
		double pointPrice = variation->GetPointPrice();

		Variation* lastVariation = new Variation();
		lastVariation->Fill(variationId, &date);

		double variationTotal = 0;

		while (lastVariation->GetId() != 1)
		{
			variationTotal = variationTotal + lastVariation->GetPrice();
			int parentId = lastVariation->GetParentId();

			delete lastVariation;
			lastVariation = new Variation();
			lastVariation->Fill(parentId, &date);
		}

		Variation* thisVariation = new Variation();
		thisVariation->Fill(variationId, &date);

		int component = 0;

		for(int i = 0; i < (int)components.size(); i++)
		{
			OrderItemComponent* component = components[i];

			Component* thisComponent = thisVariation->GetComponent(component->GetComponentId());
			float thesePoints = thisComponent->GetPoints();
			float thisTotalPoints = thesePoints * component->GetPortions();
			totalPoints += thisTotalPoints;
		}

		if (totalPoints <= variationPoints)
			total = variationTotal;
		else
			total = variationTotal + (double)(totalPoints - variationPoints) * pointPrice;
	}
	else
	{
		double componentsTotalPrice = 0;

		for(int i = 0; i < (int)components.size(); i++)
		{
			OrderItemComponent* component = components[i];

			for (int j = 0; j < component->GetPortions(); j++)
				componentsTotalPrice += GetComponentPrice(component->GetComponentId(), &date);

			for(int k = 0; k < component->GetComponentComponentCount(); k++)
			{
				OrderItemComponentComponent* subComponent = component->GetComponentComponents()[k];
			
				//for (int l = 0; l < subComponent->GetPortions() - 1; l++)
				//	componentsTotalPrice += GetComponentPrice(subComponent->GetComponentId(), &date);
			}
		}

		Variation* lastVariation = new Variation();
		lastVariation->Fill(variationId, &date);

		double variationTotal = 0;

		while (lastVariation->GetId() != 1)
		{
			variationTotal = variationTotal + lastVariation->GetPrice();
			int parentId = lastVariation->GetParentId();

			delete lastVariation;

			lastVariation = new Variation();
			lastVariation->Fill(parentId, &date);
		}

		total = variationTotal + componentsTotalPrice;
	}

	double deduction = 0;

	if (discount == 1)
		return total;
	else if (discount == 2)
		deduction = total * 0.5;
	else if (discount == 3)
		deduction = total;
	else
		deduction = 0.95;

	double finalValue = total - deduction;

	if (finalValue <= 0)
		return 0;

	double workingValue = finalValue * 10;
	int intWorking = (int)workingValue;
	double partOfPence = workingValue - intWorking;

	if (partOfPence >= 0.50)
		return ((double)intWorking / 10.0) + 0.05;

	return (double)intWorking / 10.0;
}

double OrderItem::GetComponentPrice(int componentId, tm* date)
{
	Connection* myCon = new Connection();
	Command* myComm2 = new Command();
	myComm2->SetConnection(myCon->GetConnection());
	myComm2->SetCommandText("GetPrice");
	myComm2->SetCommandType(4);
	myComm2->AddParameter("@parentId", (long)componentId);
	myComm2->AddParameter("@parentType", 1);

	RecordSet* rs2 = myComm2->ExecuteStoredProcedure();

	double price = 0;

	if (rs2->EndOF())
	{
		myCon->Close();
		return 0;
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

Variation* OrderItem::OrderIsItemBased()
{
	Variation* lastVariation = new Variation();
	lastVariation->Fill(variationId, &date);

	while (lastVariation->GetId() != 1)
	{
		if (lastVariation->GetPoints() > 0)
			return lastVariation;

		int parentId = lastVariation->GetParentId();

		lastVariation = new Variation();
		lastVariation->Fill(parentId, &date);
	}

	return NULL;
}

int OrderItem::GetId()
{
	return id;
}

int OrderItem::GetOrderId()
{
	return orderId;
}

int OrderItem::GetVariationId()
{
	return variationId;
}

int OrderItem::GetInOutStatus()
{
	return inOutStatus;
}

int OrderItem::GetDiscount()
{
	return discount;
}

void OrderItem::FillOrderItemComponents(Connection* conn)
{
	Command* myComm = new Command();
	myComm->SetConnection(conn->GetConnection());
	myComm->SetCommandText("GetOrderItemComponents");
	myComm->SetCommandType(4);
	myComm->AddParameter("@orderItemId", (long)id);

	RecordSet* rs = myComm->ExecuteStoredProcedure();

	bool eof = rs->EndOF();

	while (!eof)
	{
		int itemComponentId = rs->Value(0).lVal;
		int variationId = rs->Value(2).lVal;
		int componentId = rs->Value(3).lVal;
		int portions = rs->Value(4).bVal;

		OrderItemComponent* itemComponent = new OrderItemComponent(itemComponentId, id, variationId, componentId, portions);
		itemComponent->FillOrderItemComponentComponents(conn);

		components.push_back(itemComponent);

		rs->MoveNext();
		eof = rs->EndOF();
	}
}