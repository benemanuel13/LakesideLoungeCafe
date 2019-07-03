
#include "LakesideLoungeOffice_h.h"

#if !defined(RSET)
#define RSET

class RecordSet
{
public:
	void SetRecordSet(IDispatch* recSet);
	bool EndOF();
	void MoveNext();
	VARIANT Value(long column);

	RecordSet();
	virtual ~RecordSet();
private:
	IDispatch* recordset;
};

#endif