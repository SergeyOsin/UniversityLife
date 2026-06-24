#include <iostream>
#include <iomanip>
#include <chrono>
#include "SetStruct.h"
#include "SetClass.h"
#include "SetList.h"
#include "Setset.h"
#include "Setunorderedset.h"
#include "Setbitset.h"
#include "Tree_Lab7_Osin.h"
#include <Windows.h>
#include <vector>
#define nl '\n';
#define ver cout << '|';
using std::cout;
using std::vector;
using namespace std::chrono;

int main() {
    srand(time(NULL));
    setlocale(LC_ALL, "ru");
    SetConsoleTitle(L"Осин Сергей 23ВП2");
    cout << "Создание пустого дерева\n";
    BinarySearchTree* bst = new BinarySearchTree();
    cout << "Дерево пустое? ";
    string str = (bst->IsEmptyTree()) ? "Да" : "Нет";
    cout << str << '\n';
    cout << "Создали дерево: \n";
    bst->createTree(10,100,10);
    cout << "Обход сверху вниз: " << bst->AvoidfromToptoButtom() << '\n';
    cout << "Вставка элемента 58 в дерево: \n";
    bst->InsertNewData(58);
    cout << "Обход сверху вниз: " << bst->AvoidfromToptoButtom() << '\n';
    cout << "Обход слева направо: " << bst->AvoidLeftToRight() << '\n';
    cout << "Обход снизу вверх: " << bst->AvoidButtomtoTop() << '\n';
    cout << "Удаление дерева: ";
    bst->DeleteTree();
    system("pause");
    return 0;
}