#include <iostream>
#include <iomanip>
#include "SetStruct.h"
#include "SetClass.h"
#include "SetLab5_Osin.h"
#include "Setunorderedset.h"
#include "Setbitset.h"
#include <Windows.h>
#include <time.h>
#define nl '\n'
using namespace std;
const int min_size = 7, max_size = 9;
const int min_elem = 10, max_elem = 98;
const string horizontLine = "\n---------------------------------------------------------------------------------------------------------------------\n";
int setSize;
void lineWithParam(string text) {
	horizontLine;
	while (text.size() < 23) text += " ";
	string emptyCell1(20, ' ');
	string emptyCell2(14, ' ');
	string emptyCell3(12, ' ');
	string emptyCell4(13, ' ');
	string emptyCell5(10, ' ');
	string emptyCell6(15, ' ');

	cout << horizontLine + ' ' + text + '|' + emptyCell1 + '|' + emptyCell2 +
		'|' + emptyCell3 + '|' + emptyCell4 + '|' + emptyCell5 +
		'|' + emptyCell6 + '|';
}
void emptycell(int num,string text) {
	string empty(num, ' ');
	cout << empty << text;
}
void buildSetStruct() {
	cout << horizontLine;
	emptycell(6, "Создание множества|");
	SetStruct* set0 = new SetStruct();
	clock_t clock0 = clock();
	set0 = createnewSet('A', setSize, 0, setSize * 10);
	clock_t finish = clock();
	double rez =(double) (finish - clock0) / CLOCKS_PER_SEC;
	cout << rez;
}
void addTitle() {
	const string horizontLine = "\n---------------------------------------------------------------------------------------------------------------------\n";
	const string title = horizontLine + "\t\t\t| Односвязный список | Класс список |    List    |     Set     |  Bitset  | Unordered_set |";
	cout << title;
}

void addAllLineWithParam() {
	addTitle();
	lineWithParam("Создание множества");
	lineWithParam("Мощность");
	lineWithParam("Подмножество А-А");
	lineWithParam("Подмножество В-А");
	lineWithParam("Равенство А-А");
	lineWithParam("Равенство В-А");
	lineWithParam("Объединение");
	lineWithParam("Пересечение");
	lineWithParam("Разность А-В");
	lineWithParam("Разность В-А");
	lineWithParam("Симметричная разность");
	const string horizontLine = "\n---------------------------------------------------------------------------------------------------------------------\n";
	cout << horizontLine;
}

void createTable() {
	cout << "Введите размер множества: ";
	cin>>setSize;
	cout << '\n';
	addTitle();
	buildSetStruct();
}

void Lab5() {
	cout << "Лабораторная работа 5.\nОсин Сергей. 23ВП2. Вариант-21. Множество A - нечётные цифры, Множество B - числа кратные 3.\n";
	srand(time(NULL));
	int sizeA = rand() % (max_size - min_size + 1) + min_size;
	int sizeB = rand() % (max_size - min_size + 1) + min_size;
	cout << "Множество A: ";
	Setset* setsetA = new Setset();
	setsetA = setsetA->createnewSet('A', sizeA, min_elem, max_elem);
	cout << setsetA->printSet(',') + '\n';
	cout << "Мощность A: " << setsetA->LengthSet() << nl;
	cout << "Множество B: ";
	Setset* setsetB = new Setset();
	setsetB = setsetB->createnewSet('B', sizeB, min_elem, max_elem);
	cout << setsetB->printSet(',') + nl;
	cout << "Мощность B: " << setsetB->LengthSet() << nl;
	string state = setsetA->isSubset(setsetB) ? "Да" : "Нет";
	cout << "Множество A подмножество B? " + state + nl;
	state = setsetA->isSubset(setsetA) ? "Да" : "Нет";
	cout << "Множество А подмножество В? " + state + nl;
	state = setsetA->isEqual(setsetB) ? "Да" : "Нет";
	cout << "Множество А равно B? " + state + nl;
	state = setsetA->isEqual(setsetA) ? "Да" : "Нет";
	cout << "Множество А равно A? " + state + nl;
	Setset* setsetC = new Setset();
	setsetC = setsetA->merge(setsetB);
	cout << "Объединение А и В: " << setsetC->printSet(',') + nl;
	cout << "Мощность C: " << setsetC->LengthSet() << nl;
	setsetC = setsetA->Intersection(setsetB);
	cout << "Пересечение А и В: " << setsetC->printSet(',') + nl;
	cout << "Мощность С: " << setsetC->LengthSet() << nl;
	setsetC = setsetA->Difference(setsetB);
	cout << "Разность А и В: " << setsetC->printSet(',') + nl;
	cout << "Мощность С: " << setsetC->LengthSet() << nl;
	setsetC = setsetB->Difference(setsetA);
	cout << "Разность В и А: " << setsetC->printSet(',') + nl;
	cout << "Мощность C: " << setsetC->LengthSet() << nl;
	setsetC = setsetA->SimmetricDif(setsetB);
	cout << "Симметричная разность A и B: " << setsetC->printSet(',') + nl;
	cout << "Мощность С: " << setsetC->LengthSet() << nl;
	cout << "Очистка множеств A и B: " << nl;
	setsetA->clearSet();
	setsetB->clearSet();
	cout << "Множество А: " << setsetA->printSet(',') + nl;
	cout << "Мощность А: " << setsetA->LengthSet() << nl;
	cout << "Множество B: " << setsetB->printSet(',') + nl;
	cout << "Мощность B: " << setsetB->LengthSet() << nl;
}
int main() {
	setlocale(LC_ALL, "ru");
	SetConsoleTitle(L"Осин Сергей 23ВП2");
	Lab5();
	/*createTable();*/
	system("pause");
	return 0;
}

