#include "Triangle.h"

Triangle::Triangle(Point t, Point _t1, Point _t2, COLORREF _brush, COLORREF _pen): 

	Figure(t, (_t1.y - t.y), _brush, _pen), t1(_t1), t2(_t2) {}

void Triangle::draw() {
	HPEN pen = CreatePen(PS_SOLID, 2, color_2);
	HBRUSH brush = CreateSolidBrush(color_1);
	SelectObject(hdc, pen);
	SelectObject(hdc, brush);
	POINT points[] = { {center.x, center.y}, {t1.x, t1.y}, {t2.x, t2.y} };
	Polygon(hdc, points, 3);
	DeleteObject(pen);
	DeleteObject(brush);
}