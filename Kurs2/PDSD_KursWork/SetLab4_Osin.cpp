#include "SetLab4_Osin.h"
using namespace std;

//F1. Создание  пустого множества
SetList::SetList() {}

//F2. Пустое множество?
bool SetList::isEmptySet() {
	return list0.empty();
}

//F3. Проверка принадлежности элемента множеству
bool SetList::isElementinSet(int element) {
	if (isEmptySet())
		return false;
	for (auto iter = list0.begin(); iter != list0.end(); iter++)
		if (*iter == element)
			return true;
	return false;
}

//F4. Добавление нового элемента в множество
bool SetList::addnewElement(int new_element) {
	if (!isElementinSet(new_element)) {
		list0.push_front(new_element);
		return true;
	}
	return false;
}

//F5. Создание множества
SetList* SetList::createnewSet(char A, int size, int min_element, int max_element) {
	SetList* setlist = new SetList();
	if (size <= 0 || min_element >= max_element)
		return setlist;
	int count_elem = 0;
	while (count_elem < size) {
		int random_element = rand() % (max_element + 1 - min_element) + min_element;
		if (A == 'A') {
			if (random_element % 2 == 0)
				random_element++;
		}
		else if (random_element % 3 != 0)
			random_element += 3 - random_element % 3;
		if (setlist->addnewElement(random_element))
			count_elem++;
	}
	return setlist;
}

////F6. Мощность множества
int SetList::LengthSet() {
	return this->list0.size();
}

//F7. Вывод элементов множества
string SetList::printSet(char delimiter) {
	if (isEmptySet())
		return "";
	string result = "";
	for (auto iter = list0.begin(); iter != list0.end(); iter++)
		result += std::to_string(*iter) + delimiter;
	result.pop_back();
	return result;
}

//F8. Удаление множества (очистка памяти, занимаемой списокм).
void SetList::clearSet() {
	list0.clear();
}

//F9. Множество А является подможноством Множества B?
bool SetList::isSubset(SetList* SecondB) {
	if (isEmptySet())
		return true;
	for (auto iter = list0.begin(); iter != list0.end(); iter++) {
		if (!SecondB->isElementinSet(*iter))
			return false;
	}
	return true;
}

//F10. Множество А равно множеству B?
bool SetList::isEqual(SetList* SecondB) {
	return this->isSubset(SecondB) && SecondB->isSubset(this);
}

////F11. Объединение Множеств A и B.
SetList* SetList::merge(SetList* SecondB) {
	SetList* C = new SetList();
	if (isEmptySet() || SecondB->isEmptySet())
		return C;
	for (auto iter = list0.begin(); iter != list0.end(); iter++) {
		C->addnewElement(*iter);
	}
	for (auto iter = SecondB->list0.begin(); iter != SecondB->list0.end(); iter++) {
		C->addnewElement(*iter);
	}
	return C;
}

////F12. Пересечение множеств А и B.
SetList* SetList::Intersection(SetList* SecondB) {
	SetList* C = new SetList();
	if (isEmptySet() || SecondB->isEmptySet())
		return C;
	for (auto iteration = list0.begin(); iteration != list0.end(); iteration++) {
		if (SecondB->isElementinSet(*iteration))
			C->addnewElement(*iteration);
	}
	return C;
}

////F13. Разность между множествами A и B.
SetList* SetList::Difference(SetList* SecondB) {
	SetList* C = new SetList();
	if (isEmptySet())
		return C;
	for (auto iteration = list0.begin(); iteration != list0.end(); iteration++) {
		if (!SecondB->isElementinSet(*iteration))
			C->addnewElement(*iteration);
	}
	return C;
}

////F14. Симметричная разность множеств A и B.
SetList* SetList::SimmetricDif(SetList* SecondB) {
	return this->merge(SecondB)->Difference(this->Intersection(SecondB));
}
