
#if !defined(OrderItemComponent_Class)
#define OrderItemComponent_Class

#include "OrderItemComponentComponent.h"
#include <vector>

using namespace std;

typedef vector<OrderItemComponentComponent*> OrderItemComponentComponentsVector;

class OrderItemComponent
{
public:
	int GetId();
	int GetOrderItemId();
	int GetVariationId();
	int GetComponentId();
	int GetPortions();


	int GetComponentComponentCount();
	OrderItemComponentComponentsVector GetComponentComponents();

	void FillOrderItemComponentComponents(Connection* conn);

	OrderItemComponent(int vId, int vOrderItemId, int vVariationId, int vComponentId, int vPortions);
private:
	int id;
	int orderItemId;
	int variationId;
	int componentId;
	int portions;

	OrderItemComponentComponentsVector components;
};

#endif