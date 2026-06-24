#pragma once
#include"Octagon.h"
#include "Circle.h"

class Head {
public:
	Head(Point _t, int _r, COLORREF _Face, COLORREF _Ear);
	void draw();

private:
	Octagon* oct;
	Circle* cir1;
	Circle* cir2;
};