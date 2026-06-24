#pragma once
#include <unordered_set>
#include <string>

class Setunorderedset {
private:
	std::unordered_set<int>set1;
public:
	Setunorderedset();
	bool isEmptySet();
	bool isElementinSet(int element);
	bool addnewElement(int new_element);
	Setunorderedset* createnewSet(int size, int min_element, int max_element);
	int LengthSet();
	std::string printSet(char delimiter);
	void clearSet();
	bool isSubset(Setunorderedset* SecondB);
	bool isEqual(Setunorderedset* SecondB);
	Setunorderedset* merge(Setunorderedset* SecondB);
	Setunorderedset* Intersection(Setunorderedset* SecondB);
	Setunorderedset* Difference(Setunorderedset* SecondB);
	Setunorderedset* SimmetricDif(Setunorderedset* SecondB);
};