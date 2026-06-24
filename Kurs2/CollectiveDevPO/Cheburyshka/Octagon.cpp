#include "Octagon.h"

void Octagon::draw()
{
    POINT coordinates_mass[8] = { {center.x - r,center.y + a},
    {center.x - a,center.y + r}, {center.x + a,center.y + r}, {center.x + r
    ,center.y + a},
        {center.x + r, center.y - a}, {center.x + a, center.y - r},
        {center.x - a, center.y - r}, {center.x - r, center.y - a} };
    HPEN pen = CreatePen(PS_SOLID, 4, color_1);
    HBRUSH brush = CreateSolidBrush(color_2);
    SelectObject(hdc, pen);
    SelectObject(hdc, brush);
    Polygon(hdc, coordinates_mass, 8);
    DeleteObject(pen);
    DeleteObject(brush);
}
