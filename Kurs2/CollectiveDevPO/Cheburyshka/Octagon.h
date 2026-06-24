#pragma once
#include "Figure.h"
#include "cmath"

class Octagon : public Figure {
protected:
    int a;
public:
    Octagon(Point t, int r, COLORREF c1, COLORREF c2) : Figure(t, r, c1, c2), a(r /
        (1 + sqrt(2))) {};
    Octagon() : Figure(), a(100 / (1 + sqrt(2))) {};
    void draw() override;
};