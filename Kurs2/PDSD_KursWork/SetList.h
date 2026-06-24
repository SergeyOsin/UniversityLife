#pragma once
#include <string>
#include <list>

class SetList {
private:
	std::list<int> list0;
public:
	SetList();
	bool isEmptySet();
	bool isElementinSet(int element);
	bool addnewElement(int new_element);
	SetList* createnewSet(int size, int min_element, int max_element);
	int LengthSet();
	std::string printSet(char delimiter);
	void clearSet();
	bool isSubset(SetList* SecondB);
	bool isEqual(SetList* SecondB);
	SetList* merge(SetList* SecondB);
	SetList* Intersection(SetList* SecondB);
	SetList* Difference(SetList* SecondB);
	SetList* SimmetricDif(SetList* SecondB);
};

