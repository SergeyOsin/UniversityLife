#include "Body.h"

Body::Body(Point _t, int a, COLORREF c11) {
	tr_left = new Triangle(_t, { _t.x,_t.y + a }, { _t.x - a,_t.y + a },  c11,  c11);
	sq_up = new Square( _t, a, c11, c11);
	tr_right = new Triangle({ _t.x + a,_t.y }, { _t.x + a,_t.y + a }, { _t.x + 2 * a,_t.y + a }, c11, c11);
	sq_todown = new Square({ _t.x,_t.y + a }, a, c11, c11);
	sq_leftleg = new Square({ _t.x - a,_t.y + 2 * a }, a, c11, c11);
	sq_rightleg = new Square({ _t.x + a,_t.y + 2 * a }, a, c11, c11);
	
}

void Body::draw_body() {
	tr_left->draw();
	tr_right->draw();
	sq_up->draw();
	sq_todown->draw();
	sq_leftleg->draw();
	sq_rightleg->draw();
}