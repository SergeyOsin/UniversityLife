#pragma once
#include "pch.h"
#include "Triangle.h"
#include "Triangle.h"
#include "Figure.h"
Triangle::Triangle(int x, int y, int a, COLORREF b, COLORREF w) : Figure(x, y, a, b, w), b1(b), w1(w) {
    x1 = x + a, y1 = y + a;
    x2 = x - a, y2 = y + a;
}
void Triangle::hide() {
    cout << "Triangle is hidden: " << x << " " << y << " " << a << '\n';
    GetClientRect(h, &rt);
    HPEN p = CreatePen(PS_SOLID, 2, RGB(255, 255, 255));
    HBRUSH rus = CreateSolidBrush(RGB(255, 255, 255));
    SelectObject(hd, p);
    SelectObject(hd, rus);
    POINT points[3] = { {x, y}, {x1, y1}, {x2, y2} };
    Polygon(hd, points, 3);
    DeleteObject(p);
    DeleteObject(rus);
}

void Triangle::draw() {
    cout << "Triangle: " << x << " " << y << " " << a << '\n';
    if (x + a > rt.right || y + a > rt.bottom) {
        throw Border();
    }
    if (x < 0 || y < 0 || a <= 0) {
        throw InvalCoor();
    }
    GetClientRect(h, &rt);
    HPEN p = CreatePen(PS_SOLID, 2, b1);
    HBRUSH b = CreateSolidBrush(w1);
    SelectObject(hd, p);
    SelectObject(hd, b);
    POINT points[] = { {x,y},{x1,y1},{x2,y2} };
    Polygon(hd, points, 3);
    DeleteObject(p);
    DeleteObject(b);
}

void Triangle::move(int n_x, int n_y) {
    this->hide();
    x = n_x;
    y = n_y;
    x1 = x + a, y1 = y + a;
    x2 = x - a, y2 = y + a;
    this->draw();
}