#pragma once
#include "Figure.h"

class Triangle : public Figure {
public:
	Triangle(Point t, Point _t1, Point _t2, COLORREF brush, COLORREF pen);
	void draw() override;
private:
	Point t1, t2;
};