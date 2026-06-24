#pragma once
#include "Body.h";
#include "Head.h"

class Cheburyshka {
private:
	Body* body;
	Head* head;
public:
	Cheburyshka(Point _t, int a, COLORREF c1, COLORREF c2);
	void draw();
};