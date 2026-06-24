#include "deque.h"

void Deque::add(Figure* f) {
	fig.push_back(f);
}
void Deque::write() {
	if (fig.empty()) {
		throw "Deque is empty";
	}
	while (!fig.empty()) {
		fig.front()->draw();
		fig.pop_front();
	}
}
void Deque::erase() {
	for (auto i : fig)
		delete i;
}
Deque::~Deque() {
	fig.clear();
}