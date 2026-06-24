#include "Rectangle.h"
#include "Figure.h"
rectangle::rectangle(int x, int y, int a, COLORREF b, COLORREF w) :
    Figure(x, y, a, b, w) {
    int x1 = x - a, y1 = y - a;
    int x2 = x + a, y2 = y - a;
    x3 = x - a / 2;
    y3 = y - a / 2;
    x4 = (3 * x2 + x1) / 4;
    y4 = (3 * y2 + y1) / 4;
}

void rectangle::hide() {
    cout << "Rectangle is hidden: " << x3 << " " << y3 << " " << x4 << " " << y4 << '\n';
    HPEN p = CreatePen(PS_SOLID, 2, RGB(0, 0, 255));
    HBRUSH b = CreateSolidBrush(RGB(0, 0, 0));
    SelectObject(hd, p);
    SelectObject(hd, b);
    Rectangle(hd, x3, y3, x4, y4);
    DeleteObject(p);
    DeleteObject(b);
}

void rectangle::draw() {
    if (x + a > rt.right || x - a < rt.left || y + a > rt.bottom || y - a < rt.top) {
        throw Border();
    }
    if (x < 0 || y < 0 || a <= 0) {
        throw InvalCoor();
    }
    cout << "Rectangle: " << x3 << " " << y3 << " " << x4 << " " << y4 << '\n';
    HPEN p = CreatePen(PS_SOLID, 2, RGB(0, 0, 255)); // Создаем синий перо
    HBRUSH b = CreateSolidBrush(RGB(255, 0, 0)); // Создаем красную кисть
    SelectObject(hd, p); // Выбираем перо
    SelectObject(hd, b); // Выбираем кисть
    Rectangle(hd, x3, y3, x4, y4);
    DeleteObject(p); // Удаляем перо
    DeleteObject(b); // Удаляем кисть
}

void rectangle::move(int c, int d) {
    this->hide();
    x = c;
    y = d;
    int x1 = x - a, y1 = y - a;
    int x2 = x + a, y2 = y - a;
    x3 = (x + x1) / 2;
    y3 = (y + y1) / 2;
    x4 = (3 * x2 + x1) / 4;
    y4 = (3 * y2 + y1) / 4;
    this->draw();
}


