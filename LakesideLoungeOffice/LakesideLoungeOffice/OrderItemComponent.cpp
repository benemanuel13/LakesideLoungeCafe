
#include "OrderItemComponent.h"
#include "Command.h"

OrderItemComponent::OrderItemComponent(int vId, int vOrderItemId, int vVariationId, int vComponentId, int vPortions)
{
	id = vId;
	orderItemId = vOrderItemId;
	variationId = vVariationId;
	componentId = vComponentId;
	portions = vPortions;
}

int OrderItemComponent::GetId()
{
	return id;
}

int OrderItemComponent::GetOrderItemId()
{
	return orderItemId;
}

int OrderItemComponent::GetVariationId()
{
	return variationId;
}

int OrderItemComponent::GetComponentId()
{
	return componentId;
}

int OrderItemComponent::GetPortions()
{
	return portions;
}

void OrderItemComponent::FillOrderItemComponentComponents(Connection* conn)
{
	Command* myComm = new Command();
	myComm->SetConnection(conn->GetConnection());
	myComm->SetCommandText("GetOrderItemComponentComponents");
	myComm->SetCommandType(4);
	myComm->AddParameter("@orderItemComponentId", (long)id);

	RecordSet* rs = myComm->ExecuteStoredProcedure();

	bool eof = rs->EndOF();

	while (!eof)
	{
		int componentId = rs->Value(1).lVal;
		int portions = rs->Value(2).bVal;

		OrderItemComponentComponent* component = new OrderItemComponentComponent(id, componentId, portions);
		components.push_back(component);

		rs->MoveNext();
		eof = rs->EndOF();
	}
};

int OrderItemComponent::GetComponentComponentCount()
{
	return components.size();
}

OrderItemComponentComponentsVector OrderItemComponent::GetComponentComponents()
{
	return components;
}