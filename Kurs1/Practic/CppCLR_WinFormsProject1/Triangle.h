#pragma once
#include "pch.h"
#include "Figure.h"

using namespace System;
using namespace System::Drawing;

public ref class Triangle : public Figure {
public:
    Triangle(int x, int y, int size, Color borderColor)
        : Figure(x, y, size, borderColor, Color::Transparent) {
        if (x <= 0 || y <= 0 || size < 0) {
            throw gcnew InvalidCoordinatesException();
        }

    }

    virtual void Draw(Graphics^ g) override  {
        // –исуем треугольник использу€ объект Graphics
        array<Point>^ points = gcnew array<Point>{
            Point(x, y),
                Point(x + size, y+size),
                Point(x -size, y + size)
        };
        if (x + size > g->VisibleClipBounds.Right-25 || y + size > g->VisibleClipBounds.Bottom-25)
            throw gcnew BorderException();
        if (y + size > g->VisibleClipBounds.Height)
            throw gcnew BorderException();
        if (y <= 0 || x <= 0 || size <= 0)
            throw gcnew InvalidCoordinatesException();
        g->DrawPolygon(gcnew Pen(borderColor), points);
    }

    virtual void Hide(Graphics^ g) override {
        // —крываем треугольник, перерисовыва€ его цветом фона
        array<Point>^ points = gcnew array<Point>{
            Point(x, y),
                Point(x + size, y+size),
                Point(x -size, y + size)
        };
        g->DrawPolygon(gcnew Pen(SystemColors::Control), points);
    }

    virtual void Move(int n_x, int n_y, Graphics^ g)override {
        // ѕеремещаем треугольник
        Hide(g);
        x = n_x;
        y = n_y;
        Draw(g);
    }
};


