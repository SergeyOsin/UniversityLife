#include "SetClass.h"
using namespace std;

//F1. Создание пустого множества
SetClass::SetClass() {
    list = nullptr;
}

//F2. Пустое множество?
bool SetClass::isEmptySet() {
    return list == nullptr;
}

//F3. Проверка принадлежности элемента множеству
bool SetClass::isElementinSet(int element) {
    if (isEmptySet())
        return false;
    LinkedList* current = this->list;
    while (current != nullptr) {
        if (current->data == element)
            return true;
        current = current->next;
    }
    return false;
}

//F4. Добавление нового элемента в множество
SetClass::LinkedList* SetClass::addnewElement(int new_element) {
    if (isElementinSet(new_element))
        return list;
    LinkedList* newNode = new LinkedList();
    newNode->data = new_element;
    newNode->next = list;
    list = newNode;
    return list;
}

//F5. Создание множества
SetClass::LinkedList* SetClass::createnewSet(int size, int min_element, int max_element) {
    list = nullptr;
    if (size <= 0 || min_element >= max_element)
        return list;
    int count_elem = 0;
    while (count_elem < size) {
        int random_element = rand() % (max_element + 1 - min_element) + min_element;
        LinkedList* list1 = list;
        list = addnewElement(random_element);
        if (list != list1)
            count_elem++;
    }
    return list;
}

//F6. Мощность множества
int SetClass::LengthSet() {
    if (isEmptySet())
        return 0;
    int size = 0;
    LinkedList* current = list;
    while (current != nullptr) {
        size++;
        current = current->next;
    }
    return size;
}

//F7. Вывод элементов множества
string SetClass::printSet(char delimiter) {
    if (isEmptySet())
        return "";
    string result = "";
    LinkedList* current = list;
    while (current != nullptr) {
        result += std::to_string(current->data);
        result += delimiter;
        current = current->next;
    }
    result.pop_back();
    return result;
}

//F8. Удаление множества (очистка памяти, занимаемой списком)
SetClass::~SetClass() {
    LinkedList* current = list;
    while (current != nullptr) {
        LinkedList* next = current->next;
        delete current;
        current = next;
    }
}

//F9. Множество А является подмножеством множества B?
bool SetClass::isSubset(SetClass* SecondB) {
    if (isEmptySet())
        return true;
    LinkedList* current = list;
    while (current) {
        if (!SecondB->isElementinSet(current->data))
            return false;
        current = current->next;
    }
    return true;
}

//F10. Множество А равно множеству B?
bool SetClass::isEqual(SetClass* SecondB) {
    return isSubset(SecondB) && SecondB->isSubset(this);
}

//F11. Объединение множеств A и B
SetClass* SetClass::merge(SetClass* SecondB) {
    SetClass* C = new SetClass();
    if (this->isEmptySet())
        return SecondB;
    LinkedList* current = this->list;
    while (current) {
        C->addnewElement(current->data);
        current = current->next;
    }
    current = SecondB->list;
    while (current) {
        C->addnewElement(current->data);
        current = current->next;
    }
    return C;
}

//F12. Пересечение множеств А и B
SetClass* SetClass::Intersection(SetClass* SecondB) {
    SetClass* C = new SetClass();
    if (SecondB->isEmptySet())
        return C;
    LinkedList* current = this->list;
    while (current) {
        if (SecondB->isElementinSet(current->data))
            C->addnewElement(current->data);
        current = current->next;
    }
    return C;
}

//F13. Разность между множествами A и B
SetClass* SetClass::Difference(SetClass* SecondB) {
    SetClass* C = new SetClass();
    if (isEmptySet())
        return C;
    LinkedList* current = list;
    while (current) {
        if (!SecondB->isElementinSet(current->data))
            C->addnewElement(current->data);
        current = current->next;
    }
    return C;
}

//F14. Симметричная разность множеств A и B
SetClass* SetClass::SimmetricDif(SetClass* SecondB) {
    return merge(SecondB)->Difference(Intersection(SecondB));
}