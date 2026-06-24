#pragma once
#include <string>
#include <set>

class Setset {
private:
	std::set<int>set0;
public:
	Setset();
	bool isEmptySet();
	bool isElementinSet(int element);
	bool addnewElement(int new_element);
	Setset* createnewSet(int size, int min_element, int max_element);
	int LengthSet();
	std::string printSet(char delimiter);
	void clearSet();
	bool isSubset(Setset* SecondB);
	bool isEqual(Setset* SecondB);
	Setset* merge(Setset* SecondB);
	Setset* Intersection(Setset* SecondB);
	Setset* Difference(Setset* SecondB);
	Setset* SimmetricDif(Setset* SecondB);
};