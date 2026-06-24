#include "Circle.h" 

void Circle::draw() {
    HPEN pen = CreatePen(PS_SOLID, 4, color_1);
    HBRUSH brush = CreateSolidBrush(color_2);
    SelectObject(hdc, pen);
    SelectObject(hdc, brush);
    Ellipse(hdc, center.x - r, center.y + r, center.x + r, center.y - r);
    DeleteObject(pen);
    DeleteObject(brush);
}