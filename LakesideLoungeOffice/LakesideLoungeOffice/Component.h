
#if !defined(Component_Class)
#define Component_Class

class Component
{
public:
	Component(int vId, double vPoints);

	float GetPoints();
private:
	int id;
	float points;
};

#endif