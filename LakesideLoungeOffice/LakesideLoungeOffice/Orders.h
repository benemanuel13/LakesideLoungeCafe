
#include <windows.h>
#include "Order.h"
//#include <ctime>
#include <vector>


class Orders
{
public:
	void FillAll();
	void FillWeekly(tm* startDay);
	void FillDated(tm* startDate, tm* endDate);

	void AddOrder(Order* order);
	Order* GetOrder(int index);
	int GetOrderCount();

	Order* GetCurrentOrder();
	void MoveNext();
	bool EndOfFile();

	Orders();
private:
	int orderIndex;
	std::vector<Order*> orders;

	int id;

};