#include "file.h"

void File::SetName(char* name)
{
	this->name = name;
}

char* File::GetName()
{
	return name;
}

void File::SetBytes(char* bytes)
{
	this->bytes = bytes;
}