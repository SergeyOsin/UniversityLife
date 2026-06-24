#pragma once
#include "Figure.h"


class Circle : public Figure {
public:
	Circle(Point t, int _r, COLORREF c1, COLORREF c2) : Figure(t, _r, c1, c2) {};
	Circle() : Figure() {};
	void draw() override;
};