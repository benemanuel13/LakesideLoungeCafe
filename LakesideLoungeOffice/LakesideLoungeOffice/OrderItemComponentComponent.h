
#include "Connection.h"

class OrderItemComponentComponent
{
public:
	OrderItemComponentComponent(int vOrderItemComponentId, int vComponentId, int vPortions);

	int GetComponentId();
	int GetPortions();
private:
	int orderItemComponentId;
	int componentId;
	int portions;
};
