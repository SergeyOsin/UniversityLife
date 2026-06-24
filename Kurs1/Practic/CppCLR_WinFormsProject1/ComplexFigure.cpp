#include "pch.h"
#include "ComplexFigure.h"

ComplexFigure::ComplexFigure(int x, int y, int a, COLORREF c, COLORREF w, COLORREF w1, COLORREF b1) {
	tr = new Triangle(x, y, a, c, w);
	rc = new rectangle(x, y, a, b1, w1);
}


void ComplexFigure::hide() {
	tr->hide();
	rc->hide();
}

void ComplexFigure::draw() {
	tr->draw();
	rc->draw();
}

void ComplexFigure::move(int new_x, int new_y) {
	tr->move(new_x, new_y);
	rc->move(new_x, new_y);
}

ComplexFigure::~ComplexFigure() {
	delete tr;
	delete rc;
}



