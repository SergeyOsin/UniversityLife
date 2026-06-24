#pragma once
#include "Figure.h"
class Square : public Figure {
public:
	Square(Point _t, int r, COLORREF c1, COLORREF c2);
	void draw() override;
};
