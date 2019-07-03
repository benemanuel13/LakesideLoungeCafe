#include <time.h>
#include "LakesideLoungeOffice_h.h"
#include "Orders.h"

class Database
{
public:
	void Init(IDispatch* application);
	void Terminate();

	void AllReport();
	void WeeklyReport(tm* startDate);
	void DatedReport(tm* startDate, tm* endDate);
	void Test();

	void SetCell(int r, int c, char*val);

	Database();
	virtual ~Database();
private:
	IDispatch* connection;
	IDispatch* appl;

	void SalesReport(Orders* orders, int reportType);
};