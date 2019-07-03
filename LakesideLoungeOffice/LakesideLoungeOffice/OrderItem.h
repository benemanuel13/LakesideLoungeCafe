
#include "Variation.h"
//#include <windows.h>
#include <vector>

#include "Order.h"
#include "OrderItemComponent.h"
#include "Connection.h"

using namespace std;

typedef vector<OrderItemComponent*> OrderItemComponentsVector;

#if !defined(OrderItem_Class)
#define OrderItem_Class

class OrderItem
{
public:
	int GetId();
	int GetOrderId();
	int GetVariationId();
	int GetInOutStatus();
	int GetDiscount();

	void FillOrderItemComponents(Connection* conn);

	double GetPrice();
	bool GetVatAble();

	OrderItem(int vId, int vOrderId, int vVariationID, int vInOutStatus, int vDiscount, tm* vDate);
private:
	int id;
	int orderId;
	int variationId;
	int inOutStatus;
	int discount;

	struct tm date;

	Variation* OrderIsItemBased();
	double GetComponentPrice(int componentId, tm* date);

	OrderItemComponentsVector components;
};

#endif