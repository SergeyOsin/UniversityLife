#include <iostream>
#include "Cheburyshka.h"
using namespace std;
int main()
{
    Cheburyshka cheba = Cheburyshka({300, 100}, 50, RGB(139, 69, 19), RGB(255, 255, 0));
    cheba.draw();
    Sleep(10000);
    system("pause");
    return 0;
}


