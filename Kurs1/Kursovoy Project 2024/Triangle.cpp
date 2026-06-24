#include "Triangle.h"
#include "Figure.h"
Triangle::Triangle(int x, int y, int a, COLORREF b, COLORREF w) : Figure(x, y, a, b, w) {}
void Triangle::hide() {
    cout << "Triangle is hidden: " << x << " " << y << " " << a << '\n';
    HPEN p = CreatePen(PS_SOLID, 2, RGB(244, 244, 244));
    HBRUSH rus = CreateSolidBrush(RGB(244, 244, 244));
    SelectObject(hd, p);
    SelectObject(hd, rus);
    int x1 = x - a, y1 = y - a;
    int x2 = x + a, y2 = y - a;
    POINT points[3] = { {x, y}, {x1, y1}, {x2, y2} }; 
    Polygon(hd, points, 3); 
    DeleteObject(p);
    DeleteObject(rus);
}

void Triangle::draw() {
    if (x + a > rt.right || y + a > rt.bottom) {
        throw Border();
    }
    if (x < 0 || y < 0 || a <= 0) {
        throw InvalCoor();
    }
    cout << "Triangle: " << x << " " << y << " " << a << '\n';
    HPEN p = CreatePen(PS_SOLID, 2, RGB(0, 0, 225)); // Создаем синий перо
    HBRUSH b = CreateSolidBrush(RGB(255, 0, 0)); // Создаем красную кисть
    SelectObject(hd, p); // Выбираем перо
    SelectObject(hd, b); // Выбираем кисть
    int x1 = x + a, y1 = y;
    int x2 = x + a / 2, y2 = y - a;
    POINT points[3] = { {x, y}, {x1, y1}, {x2, y2} }; // Точки для построения треугольника
    Polygon(hd, points, 3); // Рисуем треугольник
    DeleteObject(p); // Удаляем перо
    DeleteObject(b); // Удаляем кисть
}

void Triangle::move(int с, int d) {
    this->hide();
    x = с;
    y = d;
    this->draw();
}