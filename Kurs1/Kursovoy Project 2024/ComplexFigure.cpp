#include "ComplexFigure.h"
ComplexFigure::ComplexFigure(int x, int y, int a, COLORREF w, COLORREF b) {
	tr = new Triangle(x, y, a, w, b);
	rc = new rectangle(x, y, a, b, w);
}


void ComplexFigure::hide() {
	tr->hide();
	rc->hide();
}

void ComplexFigure::draw() {
	cout << "Composition:\n";
	tr->draw();
	rc->draw();
}

void ComplexFigure::move(int c, int d) {
	tr->move(c, d);
	rc->move(c, d);
}

ComplexFigure::~ComplexFigure() {
	delete tr;
	delete rc;
}



