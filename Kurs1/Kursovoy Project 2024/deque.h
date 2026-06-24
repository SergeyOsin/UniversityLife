#pragma once
#include "Figure.h"
#include <deque>
class Deque {
private:
	deque<Figure*>fig;
public:
	Deque() {}
	void write();
	void add(Figure* f);
	void erase();
	~Deque();
};