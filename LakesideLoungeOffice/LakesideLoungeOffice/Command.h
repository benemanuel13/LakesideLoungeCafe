
#include "LakesideLoungeOffice_h.h"
#include "RecordSet.h"

class Command
{
public:
	void SetConnection(IDispatch* connection);
	void SetCommandText(const char* text);
	void SetCommandType(int type);
	void ResetParameters();
	void ClearParameters();
	void AddParameter(const char*, int);
	void AddCharParameter(const char*, char* value);
	void AddParameter(const char*, DATE value);
	void AddParameter(const char* paramName, long value);

	RecordSet* ExecuteNonQuery(const char* query);
	RecordSet* ExecuteStoredProcedure();

	Command();
	virtual ~Command();
private:
	IDispatch* connectionEx;
	IDispatch* command;
};