#pragma once
#include <bitset>
#include <string>

class Setbitset {
private:
	std::bitset<100000>bitset1;
public:
	Setbitset();
	bool isEmptySet();
	bool isElementinSet(int element);
	bool addnewElement(int new_element);
	Setbitset* createnewSet(int size, int min_element, int max_element);
	int LengthSet();
	std::string printSet(char delimiter);
	void clearSet();
	bool isSubset(Setbitset* SecondB);
	bool isEqual(Setbitset* SecondB);
	Setbitset* merge(Setbitset* SecondB);
	Setbitset* Intersection(Setbitset* SecondB);
	Setbitset* Difference(Setbitset* SecondB);
	Setbitset* SimmetricDif(Setbitset* SecondB);
};