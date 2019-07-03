
#include "Command.h"
#include "Global.h"

void Command::SetConnection(IDispatch* connection)
{
	VARIANT myRet;
	HRESULT hr = PutPropertyEx(command, LPOLESTR("ActiveConnection"), connection, &myRet);

	if(!SUCCEEDED(hr))
		MessageBox(0, "Could Not Set Connection", "Ok", 0);

	VARIANT connVar;
	hr = GetPropertyEx(command, LPOLESTR("ActiveConnection"), &connVar);

	if(!SUCCEEDED(hr))
		MessageBox(0, "Could Not Get Connection", "Error", 0);

	IDispatch* conn = connVar.pdispVal;

	hr = PutPropertyEx(conn, LPOLESTR("CursorLocation"), 3, &myRet);

	if(!SUCCEEDED(hr))
		MessageBox(0, "Could Not Set Cursor Location", "Error", 0);

	connectionEx = connection;
}

void Command::SetCommandText(const char* text)
{
	VARIANT myRet;
	HRESULT hr = PutPropertyEx(command, LPOLESTR("CommandText"), text, &myRet);

	if(!SUCCEEDED(hr))
		MessageBox(0, "Couldn't Set Command Text!", "ERROR", 0);
}

void Command::SetCommandType(int type)
{
	VARIANT myRet;
	HRESULT hr = PutPropertyEx(command, LPOLESTR("CommandType"), type, &myRet);

	if(!SUCCEEDED(hr))
		MessageBox(0, "Couldn't Set Command Type!", "ERROR", 0);
}

void Command::ResetParameters()
{
	VARIANT paramsVar;
	HRESULT hr = GetPropertyEx(command, LPOLESTR("Parameters"), &paramsVar);

	if(!SUCCEEDED(hr))
		MessageBox(0, "Could Not Get Parameters", "Ok", 0);

	IDispatch* params = paramsVar.pdispVal;

	VARIANT myRet;
	hr = CallMethod(params, LPOLESTR("Refresh"), &myRet);

	if(!SUCCEEDED(hr))
		MessageBox(0, "Could Not Refresh Parameters", "Ok", 0);

}

void Command::AddParameter(const char* paramName, int value)
{
	VARIANT paramsVar;
	HRESULT hr = GetPropertyEx(command, LPOLESTR("Parameters"), &paramsVar);

	if(!SUCCEEDED(hr))
		MessageBox(0, "Could Not Get Parameters", "Ok", 0);

	VARIANT paramVar;
	hr = CallMethod(command, LPOLESTR("CreateParameter"), paramName, 3, 1, &paramVar);

	if(!SUCCEEDED(hr))
		MessageBox(0, "Could Not Create Parameters", "Ok", 0);

	IDispatch* params = paramsVar.pdispVal;
	IDispatch* param = paramVar.pdispVal;

	VARIANT myRet;
	hr = CallMethod(params, LPOLESTR("Append"), param, &myRet);

	if(!SUCCEEDED(hr))
		MessageBox(0, "Could Not Append Parameter", "Ok", 0);
	
	hr = PutPropertyEx(param, LPOLESTR("Value"), value, &myRet);

	if(!SUCCEEDED(hr))
		MessageBox(0, "Could Not Set Parameter Value", "Ok", 0);
}

void Command::AddParameter(const char* paramName, long value)
{
	VARIANT paramsVar;
	HRESULT hr = GetPropertyEx(command, LPOLESTR("Parameters"), &paramsVar);

	if (!SUCCEEDED(hr))
		MessageBox(0, "Could Not Get Parameters", "Ok", 0);

	VARIANT paramVar;
	hr = CallMethod(command, LPOLESTR("CreateParameter"), paramName, 3, 1, &paramVar);

	if (!SUCCEEDED(hr))
		MessageBox(0, "Could Not Create Parameters", "Ok", 0);

	IDispatch* params = paramsVar.pdispVal;
	IDispatch* param = paramVar.pdispVal;

	VARIANT myRet;
	hr = CallMethod(params, LPOLESTR("Append"), param, &myRet);

	if (!SUCCEEDED(hr))
		MessageBox(0, "Could Not Append Parameter", "Ok", 0);

	hr = PutPropertyEx(param, LPOLESTR("Value"), value, &myRet);

	if (!SUCCEEDED(hr))
		MessageBox(0, "Could Not Set Parameter Value", "Ok", 0);
}

void Command::AddCharParameter(const char* paramName, char* value)
{
	VARIANT paramsVar;
	HRESULT hr = GetPropertyEx(command, LPOLESTR("Parameters"), &paramsVar);

	if (!SUCCEEDED(hr))
		MessageBox(0, "Could Not Get Parameters", "Ok", 0);

	VARIANT paramVar;
	hr = CallMethod(command, LPOLESTR("CreateParameter"), paramName, 3, 1, &paramVar);

	if (!SUCCEEDED(hr))
		MessageBox(0, "Could Not Create Parameters", "Ok", 0);

	IDispatch* params = paramsVar.pdispVal;
	IDispatch* param = paramVar.pdispVal;

	VARIANT myRet;

	hr = PutPropertyEx(param, LPOLESTR("Type"), 8, &myRet);

	if (!SUCCEEDED(hr))
		MessageBox(0, "Could Not Set Parameter Type ", "Ok", 0);

	hr = PutPropertyCharEx(param, LPOLESTR("Value"), value, &myRet);

	if (!SUCCEEDED(hr))
		MessageBox(0, "Could Not Set Parameter Value", "Ok", 0);

	hr = CallMethod(params, LPOLESTR("Append"), param, &myRet);

	if (!SUCCEEDED(hr))
		MessageBox(0, "Could Not Append Parameter", "Ok", 0);
}

void Command::AddParameter(const char* paramName, DATE value)
{
	VARIANT paramsVar;
	HRESULT hr = GetPropertyEx(command, LPOLESTR("Parameters"), &paramsVar);

	if (!SUCCEEDED(hr))
		MessageBox(0, "Could Not Get Parameters", "Ok", 0);

	VARIANT paramVar;
	hr = CallMethod(command, LPOLESTR("CreateParameter"), paramName, 3, 1, &paramVar);

	if (!SUCCEEDED(hr))
		MessageBox(0, "Could Not Create Parameters", "Ok", 0);

	IDispatch* params = paramsVar.pdispVal;
	IDispatch* param = paramVar.pdispVal;

	VARIANT myRet;

	hr = PutPropertyEx(param, LPOLESTR("Type"), 7, &myRet);

	if (!SUCCEEDED(hr))
		MessageBox(0, "Could Not Set Parameter Type ", "Ok", 0);

	hr = PutPropertyEx(param, LPOLESTR("Value"), value, &myRet);

	if (!SUCCEEDED(hr))
		MessageBox(0, "Could Not Set Parameter Value", "Ok", 0);

	hr = CallMethod(params, LPOLESTR("Append"), param, &myRet);

	if (!SUCCEEDED(hr))
		MessageBox(0, "Could Not Append Parameter", "Ok", 0);
}

void Command::ClearParameters()
{
	VARIANT paramsVar;
	HRESULT hr = GetPropertyEx(command, LPOLESTR("Parameters"), &paramsVar);

	if(!SUCCEEDED(hr))
		MessageBox(0, "Could Not Get Parameters", "Ok", 0);

	IDispatch* params = paramsVar.pdispVal;

	VARIANT countVar;
	hr = GetPropertyEx(params, LPOLESTR("Count"), &countVar);
	
	int count = countVar.bVal;

	for(int i = count - 1; i > 0; i--)
	{
		VARIANT myRet;
		hr = CallMethod(params, LPOLESTR("Delete"), i, &myRet);
	}

	if(!SUCCEEDED(hr))
		MessageBox(0, "Could Not Clear Parameters", "Ok", 0);
}

RecordSet* Command::ExecuteStoredProcedure()
{
	VARIANT recsVar;
	HRESULT hr = CallMethod(command, LPOLESTR("Execute"), &recsVar);

	if (SUCCEEDED(hr))
	{
		RecordSet* recSet = new RecordSet();
		recSet->SetRecordSet(recsVar.pdispVal);

		return recSet;
	}
	else
	{
		//ShowMessage(hr);
		MessageBox(0, "Could not execute.", "Error", 0);
	}

	return NULL;
}

RecordSet* Command::ExecuteNonQuery(const char* query)
{
	CLSID clsid;
	HRESULT hr = CLSIDFromProgID(L"ADODB.RecordSet", &clsid);

	IDispatch* rs;

	hr = CoCreateInstance(clsid, NULL, CLSCTX_INPROC_SERVER, IID_IDispatch, (LPVOID *)&rs);

	VARIANT connVar;
	hr = GetPropertyEx(command, LPOLESTR("ActiveConnection"), &connVar);

	IDispatch* conn = connVar.pdispVal;

	VARIANT myRet;
	hr = CallMethod(rs, LPOLESTR("Open"), query, connectionEx, 3, 1, 1, &myRet);

	RecordSet* newRec = new RecordSet();
	newRec->SetRecordSet(rs);

	return newRec;
}

Command::Command()
{
	CLSID clsid;
	HRESULT hr = CLSIDFromProgID(L"ADODB.Command", &clsid);

	hr = CoCreateInstance(clsid, NULL, CLSCTX_INPROC_SERVER, IID_IDispatch, (LPVOID *)&command);
}

Command::~Command()
{
}