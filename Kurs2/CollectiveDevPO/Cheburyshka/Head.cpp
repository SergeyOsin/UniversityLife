#include "Head.h"

Head::Head(Point _t, int _r, COLORREF _Face, COLORREF _Ear) {
	oct = new Octagon(_t, _r, _Face, _Face);
	cir1 = new Circle({(_t.x - _r*2), (_t.y - _r/2)}, _r, _Ear, _Ear);
	cir2 = new Circle({ (_t.x + _r*2), (_t.y - _r/2) }, _r, _Ear, _Ear);
}

void Head::draw() {
	cir1->draw();
	cir2->draw();
	oct->draw();
}