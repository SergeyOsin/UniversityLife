#include "Square.h"

Square::Square(Point _t, int r, COLORREF c1, COLORREF c2) :
Figure(_t, r, c1, c2) {}

void Square::draw() {
    int x3 = center.x, y3 = center.y;
    int x4 = center.x + r, y4 = center.y + r;
    HPEN p = CreatePen(PS_SOLID, 2, color_1);
    HBRUSH b = CreateSolidBrush(color_2);
    SelectObject(hdc, p);
    SelectObject(hdc, b);
    Rectangle(hdc, x3, y3, x4, y4);
    DeleteObject(p);
    DeleteObject(b);
}




