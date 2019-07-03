
#include "LakesideLoungeOffice_h.h"

#if !defined(Connection_Class)
#define Connection_Class

class Connection
{
public:
	IDispatch* GetConnection();
	void Close();

	Connection();
	virtual ~Connection();

private:
	IDispatch* connection;
};

#endif