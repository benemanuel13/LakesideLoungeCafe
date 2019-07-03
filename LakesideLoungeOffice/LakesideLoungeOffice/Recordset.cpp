
#include "RecordSet.h"
#include "Global.h"

RecordSet::RecordSet()
{}

RecordSet::~RecordSet()
{}

void RecordSet::SetRecordSet(IDispatch* recSet)
{
	recordset = recSet;
}

bool RecordSet::EndOF()
{
	VARIANT eofVal;
	GetPropertyEx(recordset, LPOLESTR("EOF"), &eofVal);

	return eofVal.boolVal;
}

void RecordSet::MoveNext()
{
	VARIANT myVal;
	CallMethod(recordset, LPOLESTR("MoveNext"), &myVal);
}

VARIANT RecordSet::Value(long column)
{
	VARIANT fieldsVal;
	HRESULT hr = GetPropertyEx(recordset, LPOLESTR("Fields"), &fieldsVal);

	if(!SUCCEEDED(hr))
		MessageBox(0, "Not Got Fields", "Ok", 0);

	IDispatch* fields = fieldsVal.pdispVal;

	VARIANT itemVal;
	hr = GetPropertyEx(fields, LPOLESTR("Item"), column, &itemVal, 1);

	if(!SUCCEEDED(hr))
		MessageBox(0, "Not Got Item X", "Ok", 0);

	IDispatch* item = itemVal.pdispVal;

	VARIANT value;
	hr = GetPropertyEx(item, LPOLESTR("Value"), &value);

	if(!SUCCEEDED(hr))
		MessageBox(0, "Not Got Value", "Ok", 0);

	return value;
}
