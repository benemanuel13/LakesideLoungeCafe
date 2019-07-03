
#if !defined(Order_Class)
#define Order_Class

#include <vector>
#include <time.h>
#include "OrderItem.h"
#include "Connection.h"

using namespace std;

typedef vector<OrderItem*> OrderItemsVector;

class Order
{
public:
	int GetId();
	char* GetName();
	int GetCustomerType();
	tm GetDate();

	void SetId(int vId);
	void SetName(char* vName);
	void SetCustomerType(int vCustomerType);
	void SetDate(tm* date);

	OrderItem* GetCurrentOrderItem();
	void MoveNext();
	bool EndOfFile();

	Order();
	Order(int vId, char* vName, int vCustomerType, tm* date, bool deep);
private:
	int id;
	char* name;
	int customerType;
	struct tm date;

	void FillOrderItems();

	int orderItemIndex;
	OrderItemsVector orderItems;
};

#endif