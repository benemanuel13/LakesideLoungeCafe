
#include "OrderItemComponentComponent.h"

OrderItemComponentComponent::OrderItemComponentComponent(int vOrderItemComponentId, int vComponentId, int vPortions)
{
	orderItemComponentId = vOrderItemComponentId;
	componentId = vComponentId;
	portions = vPortions;
}

int OrderItemComponentComponent::GetComponentId()
{
	return componentId;
}

int OrderItemComponentComponent::GetPortions()
{
	return portions;
}