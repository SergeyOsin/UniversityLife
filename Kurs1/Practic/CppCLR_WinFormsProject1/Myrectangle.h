#pragma once
#include "Figure.h"

using namespace System;
using namespace System::Drawing;

public ref class MyRectangle : public Figure {
public:
    MyRectangle(int x,int y,int a, Color borderColor, Color fillColor)
        : Figure(x, y, a, borderColor, fillColor) {
        int x1 = x + a, y1 = y + a;
    int x2 = x - a, y2 = y + a;
    x3 = x - a / 2;
    y3 = y + a / 2;
    x4 = x + a / 2;
    y4 = y + a;

    }

    virtual void Draw(Graphics^ g) override {
        if (x + size > g->VisibleClipBounds.Right || x - size < g->VisibleClipBounds.Left) {
            throw gcnew BorderException();
        }
        if (y + size > g->VisibleClipBounds.Height)
            throw gcnew BorderException();
        if (x3 <= 0 || y3 <= 0)
            throw gcnew InvalidCoordinatesException();
        if (x <= 0 || y <= 0 || size <= 0) {
            throw gcnew InvalidCoordinatesException();
        }
        g->DrawRectangle(gcnew Pen(borderColor), x3,y3,size,size/2);
        g->FillRectangle(gcnew SolidBrush(borderColor), x3,y3, size, size / 2);
    }

    virtual void Hide(Graphics^ g) override {
        // Скрываем прямоугольник, перерисовывая его цветом фона
        g->DrawRectangle(gcnew Pen(SystemColors::Control), x3, y3, size, size / 2);
        g->FillRectangle(gcnew SolidBrush(SystemColors::Control),x3,y3, size, size / 2);
    }

    virtual void Move(int n_x, int n_y, Graphics^ g) override {
        // Перемещаем прямоугольник
        Hide(g);
        int deltaX = n_x - x;
        int deltaY = n_y - y;
        x = n_x;
        y = n_y;
        x3 += deltaX;
        y3 += deltaY;
        Draw(g);
    }

private:
    int x3, y3;
    int x4, y4;
};
