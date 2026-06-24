#pragma once
#include "Figure.h"
class Triangle : public Figure {
public:
    Triangle(int x, int y, int a, COLORREF b, COLORREF w);
    void draw() override;
    void hide() override;
    void move(int a1, int b1) override;
protected:
    int x1, y1;
    int x2, y2;
};
