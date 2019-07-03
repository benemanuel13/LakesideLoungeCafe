#include "Connection.h"
#include "Global.h"

IDispatch* Connection::GetConnection()
{
	return connection;
}

void Connection::Close()
{
	VARIANT retOpen;
	HRESULT hr = CallMethod(connection, LPOLESTR("Close"), &retOpen);
}

Connection::Connection()
{
	CLSID clsid;
	HRESULT hr = CLSIDFromProgID(L"ADODB.Connection", &clsid);

	if (!SUCCEEDED(hr))
		MessageBox(0, "Could not get ADODB.Connection clsId.", "Error", 0);

	hr = CoCreateInstance(clsid, NULL, CLSCTX_INPROC_SERVER, IID_IDispatch, (LPVOID *)&connection);

	if (!SUCCEEDED(hr))
		MessageBox(0, "Could not Create Connection.", "Error", 0);

	//const char* conString = "LakesideLounge";
	const char* conString = "Data Source=LakesideLounge; User Id=LakesideLounge; Password=k^hUe9%2!jeIhj&^;";

	VARIANT retOpen;
	hr = CallMethod(connection, LPOLESTR("Open"), conString, &retOpen);

	if (!SUCCEEDED(hr))
		MessageBox(0, "Could not Open Connection.", "Error", 0);
}

Connection::~Connection()
{
}