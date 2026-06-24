#include <iostream>
#include "SetStruct.h"
#include "SetClass.h"
#include "SetLab4_Osin.h"
using namespace std;
const int min_size = 7, max_size = 9;
const int min_elem = 10, max_elem = 98;
int main() {
	setlocale(LC_ALL, "ru");
	cout << "Лабораторная работа 4\n";
	cout << "Осин Сергей. 23ВП2. Вариант-21. Множество A - нечётные цифры, Множество B - числа кратные 3.\n";
	srand(time(NULL));
	int sizeA = rand() % (max_size - min_size + 1) + min_size;
	int sizeB = rand() % (max_size - min_size + 1) + min_size;
	SetList* setlist = new SetList();
	setlist=setlist->createnewSet('A', sizeA, min_elem, max_elem);
	cout << "Множество A: " << setlist->printSet(',');
	cout << "\nМощность A: " << setlist->LengthSet();
	cout << "\nМножество B: ";
	SetList* setlist1 = new SetList();
	setlist1 = setlist1->createnewSet('B', sizeB, min_elem, max_elem);
	cout << setlist1->printSet(',') + '\n';
	cout << "Мощность B: " << setlist1->LengthSet()<<'\n';
	SetList* C = new SetList();
	setlist->isSubset(setlist1);
	string answ = (setlist->isSubset(setlist1)) ? "Да" : "Нет";
	cout << "Множество А подмножество В? " <<answ<< '\n';
	answ = (setlist->isSubset(setlist)) ? "Да" : "Нет";
	cout << "Множество А подмножество A? " << answ << '\n';
	answ = (setlist->isEqual(setlist1)) ? "Да" : "Нет";
	cout << "Множество А равно В? " + answ << '\n';
	answ = (setlist->isEqual(setlist)) ? "Да" : "Нет";
	cout << "Множество А равно A? " + answ + '\n';
	C = setlist->merge(setlist1);
	cout << "Объединение множеств А и В: " << C->printSet(',') + '\n';
	cout << "Мощность C: " << C->LengthSet() <<'\n';
	C = setlist->Intersection(setlist1);
	cout << "Пересечение множеств А и В: " << C->printSet(',') + '\n';
	cout << "Мощность C: " << C->LengthSet() << '\n';
	C = setlist->Difference(setlist1);
	cout << "Разность А и B: " << C->printSet(',') << '\n';
	cout << "Мощность C: " << C->LengthSet() << '\n';
	C = setlist1->Difference(setlist);
	cout << "Разность B и A: " << C->printSet(',') << '\n';
	cout << "Мощность C: " << C->LengthSet() << '\n';
	C = setlist->SimmetricDif(setlist1);
	cout << "Симметричная разность А и B: " << C->printSet(',') << '\n';
	cout << "Мощность C: " << C->LengthSet() << '\n';
	cout << "Очистка множеств A и B:\n";
	setlist->clearSet();
	setlist1->clearSet();
	cout << "Множество A: " << setlist->printSet(',') << '\n';
	cout << "Мощность A: " << setlist->LengthSet() << '\n';
	cout << "Множество B: " << setlist1->printSet(',') << '\n';
	cout << "Мощность B: " << setlist1->LengthSet() << '\n';
	return 0;
}
