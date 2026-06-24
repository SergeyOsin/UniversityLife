#pragma once 
#include<windows.h> 
#include<windowsx.h> 
#include <iostream> 
#include <string> 
using namespace std;

struct Point {
    int x;
    int y;
};
class Figure {
protected:
    int r;
    Point center;
    COLORREF color_1;
    COLORREF color_2;
    HWND hwnd;
    HDC hdc;
    RECT rt;
public:
    static COLORREF color_default;
    Figure(Point t, int _r, COLORREF c1, COLORREF c2);
    Figure();
    ~Figure();
    virtual void draw() = 0;
};