#pragma once
using namespace System;
using namespace System::Drawing;

public ref class Figure {
protected:
    int x, y;
    int size;
    Color borderColor;
    Color fillColor;
    Rectangle rt;
public:
    Figure() : size(0), x(0), y(0), borderColor(Color::Black), fillColor(Color::White) {}
    Figure(int _x, int _y, int _size, Color _borderColor, Color _fillColor)
        : x(_x), y(_y), size(_size), borderColor(_borderColor), fillColor(_fillColor) {
    }

    virtual void Draw(Graphics^ g) abstract;
    virtual void Hide(Graphics^ g) abstract;
    virtual void Move(int n_x, int n_y, Graphics^ g) {
        Hide(g);
        x = n_x;
        y = n_y;
        Draw(g);
    }
};

public ref class BorderException : public System::Exception {
public:
    BorderException() : System::Exception("Breaking Window border") {}
};

public ref class InvalidCoordinatesException : public System::Exception {
public:
    InvalidCoordinatesException() : System::Exception("Coordinates or sides are less than 0") {}
};
