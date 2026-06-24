#pragma once
#include <string>


class SetClass{
private:
	struct LinkedList {
		int data;
		LinkedList* next;
	};

	LinkedList* list;
public:
	SetClass();
	bool isEmptySet();
	bool isElementinSet(int element);
	LinkedList* addnewElement(int new_element);
	LinkedList* createnewSet(int size, int min_element, int max_element);
	int LengthSet();
	std::string printSet(char delimiter);
	~SetClass();
	bool isSubset(SetClass* SecondB);
	bool isEqual(SetClass* SecondB);
	SetClass* merge(SetClass* SecondB);
	SetClass* Intersection(SetClass* SecondB);
	SetClass* Difference(SetClass* SecondB);
	SetClass* SimmetricDif(SetClass* SecondB);
};
