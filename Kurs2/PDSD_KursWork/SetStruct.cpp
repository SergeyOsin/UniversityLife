#include "SetStruct.h"
using namespace std;

//F1. Создание  пустого множества
SetStruct* createEmptySet() {
	return nullptr;
}

//F2. Пустое множество?
bool isEmptySet(SetStruct* headNode) {
	return headNode == nullptr;
}

//F3. Проверка принадлежности элемента множеству
bool isElementinSet(SetStruct* headNode, int element) {
	if (isEmptySet(headNode))
		return false;
	SetStruct* newSet = headNode;
	while (newSet != nullptr) {
		if (newSet->data == element)
			return true;
		newSet = newSet->next;
	}
	return false;
}

//F4. Добавление нового элемента в множество
SetStruct* addnewElement(SetStruct* head, int new_element) {
	if (isElementinSet(head, new_element))
		return head;
	return new SetStruct{ new_element,head };
}

//F5. Создание множества
SetStruct* createnewSet(int size, int min_element, int max_element) {
	if (size <= 0 || min_element >= max_element)
		return nullptr;
	SetStruct* newSet = createEmptySet();
	int count_elem = 0;
	while (count_elem < size) {
		int random_element = rand() % (max_element - min_element + 1) + min_element;
		SetStruct* set1 = newSet;
		newSet = addnewElement(newSet, random_element);
		if (newSet!=set1)
			count_elem++;
	}
	return newSet;
}

//F6. Мощность множества
int LengthSet(SetStruct* head) {
	if (isEmptySet(head))
		return 0;
	int size = 0;
	SetStruct* current = head;
	while (current != nullptr) {
		size++;
		current = current->next;
	}
	return size;
}

//F7. Вывод элементов множества
string printSet(SetStruct* head, char delimiter) {
	if (isEmptySet(head))
		return "";
	string result = "";
	SetStruct* current = head;
	while (current != nullptr) {
		result += std::to_string(current->data);
		result += delimiter;
		current = current->next;
	}
	result.pop_back();
	return result;
}

//F8. Удаление множества (очистка памяти, занимаемой списокм).
SetStruct* deleteSet(SetStruct* head) {
	SetStruct* current = head;
	while (current != nullptr) {
		SetStruct* next = current->next;
		delete current;
		current = next;
	}
	return current;
}

//F9. Множество A является подмножеством B?
bool isSubset(SetStruct* FirstSet, SetStruct* SecondSet) {
	if (isEmptySet(FirstSet))
		return true;
	SetStruct* SetA = FirstSet;
	while (SetA != nullptr) {
		if (!isElementinSet(SecondSet, SetA->data))
			return false;
		SetA = SetA->next;
	}
	return true;
}

//F10. Множество А равно Множеству B?
bool isEqual(SetStruct* FirstSet, SetStruct* SecondSet) {
	return isSubset(FirstSet, SecondSet) && isSubset(SecondSet, FirstSet);
}

//F11. Объединение Множества А и Множества B.
SetStruct* merge(SetStruct* FirstA, SetStruct* SecondB) {
	SetStruct* SetA = FirstA;
	SetStruct* SetC = nullptr;
	while (SetA) {
		SetC = addnewElement(SetC, SetA->data);
		SetA = SetA->next;
	}
	SetStruct* SetB = SecondB;
	while (SetB) {
		SetC = addnewElement(SetC, SetB->data);
		SetB = SetB->next;
	}
	return SetC;
}

//F12. Пересечение множеств А и B.
SetStruct* Intersection(SetStruct* FirstA, SetStruct* SecondB) {
	SetStruct* SetC = nullptr;
	if (isEmptySet(FirstA) || isEmptySet(SecondB))
		return SetC;
	SetStruct* SetA = FirstA;
	while (SetA) {
		if (isElementinSet(SecondB, SetA->data))
			SetC = addnewElement(SetC, SetA->data);
		SetA = SetA->next;
	}
	return SetC;
}

//F13. Разность множества А и B.
SetStruct* Difference(SetStruct* FirstA, SetStruct* SecondB) {
	SetStruct* C = nullptr;
	SetStruct* current = FirstA;
	while (current) {
		if (!isElementinSet(SecondB, current->data))
			C = addnewElement(C, current->data);
		current = current->next;
	}
	return C;
}

//F14. Симметричная разность множеств А и B.
SetStruct* SimmetricDif(SetStruct* FirstA, SetStruct* SecondB) {
	return merge(Difference(FirstA, SecondB), Difference(SecondB, FirstA));
}