#pragma once
#include <string>

struct SetStruct {
	int data;
	SetStruct* next;
};
SetStruct* createEmptySet();
bool isEmptySet(SetStruct* headNode);
bool isElementinSet(SetStruct* headNode, int element);
SetStruct* addnewElement(SetStruct* head, int new_element);
SetStruct* createnewSet(int size, int min_element, int max_element);
int LengthSet(SetStruct* head);
std::string printSet(SetStruct* head, char delimiter);
SetStruct* deleteSet(SetStruct* head);
bool isSubset(SetStruct* FirstA, SetStruct* SecondB);
bool isEqual(SetStruct* FirstA, SetStruct* SecondB);
SetStruct* merge(SetStruct* FirstA, SetStruct* SecondB);
SetStruct* Intersection(SetStruct* FirstA, SetStruct* SecondB);
SetStruct* Difference(SetStruct* FirstA, SetStruct* SecondB);
SetStruct* SimmetricDif(SetStruct* FirstA, SetStruct* SecondB);