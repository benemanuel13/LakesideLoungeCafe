
#include <map>
#include "Component.h"

#if !defined(Variation_Class)
#define Variation_Class

using namespace std;

typedef map<int, Component*> ComponentsMap;

class Variation
{
public:
	void Fill(int vId, tm* date);

	void SetId(int value);
	void SetParentId(int value);
	void SetPrice(double value);
	void SetPoints(float value);
	void SetPointPrice(double value);
	void SetVatStatus(int value);

	int GetId();
	int GetParentId();
	double GetPrice();
	float GetPoints();
	double GetPointPrice();
	int GetVatStatus();

	Component* GetComponent(int id);
private:
	void FillComponents(tm* date);
	double GetComponentPrice(int id, tm* date);

	int id;
	int parentId;
	double price;
	float points;
	double pointPrice;
	int vatStatus;

	ComponentsMap components;
};

#endif