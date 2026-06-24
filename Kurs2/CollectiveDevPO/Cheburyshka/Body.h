#pragma once
#include "Square.h"
#include "Triangle.h"
class Body {
private:
	Triangle* tr_left;
	Square* sq_up;
	Triangle* tr_right;
	Square* sq_todown;
	Square* sq_leftleg;
	Square* sq_rightleg;
public:
	Body(Point _t, int a, COLORREF c11);
	void draw_body();
};