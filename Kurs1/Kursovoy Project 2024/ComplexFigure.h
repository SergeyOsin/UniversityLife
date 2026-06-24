#pragma once
#include "Figure.h"
#include "Triangle.h"
#include "Rectangle.h"
class ComplexFigure : public Figure {
private:
	Triangle* tr;
	rectangle* rc;
public:
	ComplexFigure(int x, int y, int a, COLORREF b, COLORREF w);
	~ComplexFigure();
	void draw() override;
	void hide() override;
	void move(int a, int b) override;
};