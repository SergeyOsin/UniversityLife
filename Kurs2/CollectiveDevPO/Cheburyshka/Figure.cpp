#include "Figure.h" 
Figure::Figure(Point t, int _r, COLORREF c1, COLORREF c2)
{
    hwnd = GetConsoleWindow();
    hdc = GetDC(hwnd);
    GetClientRect(hwnd, &rt);
    center = t;
    r = _r;
    color_1 = c1;
    color_2 = c2;
};

Figure::Figure() {
    hwnd = GetConsoleWindow();
    hdc = GetDC(hwnd);
    GetClientRect(hwnd, &rt);
    center = { 200,200 };
    r = 100;
    color_1 = RGB(255, 0, 0);
    color_2 = RGB(0, 0, 255);
}

Figure::~Figure() {
    ReleaseDC(hwnd, hdc);
}

COLORREF Figure::color_default = RGB(12, 12, 12);
