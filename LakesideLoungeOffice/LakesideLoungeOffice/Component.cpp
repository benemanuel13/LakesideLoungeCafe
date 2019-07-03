
#include "Component.h"

Component::Component(int vId, double vPoints)
{
	id = vId;
	points = vPoints;
}

float Component::GetPoints()
{
	return points;
};