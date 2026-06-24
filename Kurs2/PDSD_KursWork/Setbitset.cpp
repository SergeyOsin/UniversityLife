#include "Setbitset.h"
using namespace std;

//F1. Создание пустого множества
Setbitset::Setbitset() {
	bitset1.reset();
}

//F2. Проверка множество на пустоту
bool Setbitset::isEmptySet() {
	return bitset1.none();
}

////F3. Проверка нахождения элемента во множестве
bool Setbitset::isElementinSet(int element) {
	if (element < 0 || element >= 100000) {
		return false; 
	}
	return bitset1.test(element); 
}

//F4. Добавление нового элемента во множество
bool Setbitset::addnewElement(int new_element) {
	if (!isElementinSet(new_element)) {
		bitset1.set(new_element, true);
		return true;
	}
	return false;
}

//F5. Создание нового множества
Setbitset* Setbitset::createnewSet(int size, int min_element, int max_element) {
	Setbitset* newset = new Setbitset();
	if (size <= 0 || min_element >= max_element)
		return newset;
	int count_elem = 0;
	while (count_elem < size) {
		int rand_elem = rand() % (max_element - min_element + 1) + min_element;
		if (newset->addnewElement(rand_elem))
			count_elem++;
	}
	return newset;
}


int Setbitset::LengthSet() {
	return bitset1.count();
}

//F7. Вывод множества
std::string Setbitset::printSet(char delimiter) {
	string result = " ";
	if (isEmptySet())
		return result;
	for (int i = 0; i < 100000; i++) 
		if (bitset1.test(i))
			result += std::to_string(i) + delimiter;
	result.pop_back();
	return result;
}

//F8. Очистка множества
void Setbitset::clearSet() {
	bitset1.reset();
}

//F9. Множество А является подмножеством B?
bool Setbitset::isSubset(Setbitset* SecondB) {
	return (bitset1 & ~SecondB->bitset1).none();
}

//F10. Множество А равно В?
bool Setbitset::isEqual(Setbitset* SecondB) {
	return this->isSubset(SecondB) && SecondB->isSubset(this);
}

//F11. Объединение множеств А и В.
Setbitset* Setbitset::merge(Setbitset* SecondB) {
	Setbitset* result = new Setbitset();
	result->bitset1 = bitset1 | SecondB->bitset1; 
	return result;
}

//F12. Пересечение множеств А и В.
Setbitset* Setbitset::Intersection(Setbitset* SecondB) {
	Setbitset* C = new Setbitset();
	C->bitset1 = bitset1 & SecondB->bitset1;
	return C;
}

//F13. Разность множеств А и В.
Setbitset* Setbitset::Difference(Setbitset* SecondB) {
	Setbitset* result = new Setbitset();
	result->bitset1 = bitset1 & ~SecondB->bitset1; 
	return result;
}

//F14. Симметричная разность А и В.
Setbitset* Setbitset::SimmetricDif(Setbitset* SecondB) {
	return this->merge(SecondB)->Difference(this->Intersection(SecondB));
}