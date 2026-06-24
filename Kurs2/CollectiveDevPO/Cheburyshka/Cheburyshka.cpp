#include "Cheburyshka.h"

Cheburyshka::Cheburyshka(Point _t, int a, COLORREF c1, COLORREF c2)
{
	body = new Body({ _t.x - a/2, _t.y + a }, a, c1);
	head = new Head(_t, a, c2, c1);
}

void Cheburyshka::draw()
{
	body->draw_body();
	head->draw();
}
