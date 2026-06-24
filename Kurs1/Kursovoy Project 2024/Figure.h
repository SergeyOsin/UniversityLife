#pragma once
#include <iostream>
#include <Windows.h>
#include <windowsx.h>
#include <string>
using namespace std;
class Figure {
protected:
    int x, y;
    int a;
    HWND h;
    HDC hd;
    RECT rt;
    COLORREF black;
    COLORREF white;
public:
    Figure(int _x, int _y, int sh, COLORREF b, COLORREF w); 
    Figure(): a(0), x(0), y(0), h(NULL), hd(NULL), black(0), white(0) {
        h = GetConsoleWindow();
        hd = GetDC(h);
        GetClientRect(h, &rt);
    };
    ~Figure() {
        ReleaseDC(h, hd);
    }
    virtual void print() const {
        cout << x << "," << y << '\n';
    }
    virtual void draw();
    virtual void hide();
    virtual void move(int a1, int b1) {
        hide();
        x = a1;
        y = b1;
        draw();
    }
    class Border {
    private:
        string error;
    public:
        Border() : error("\nBreaking Window border\n") {};
        void err() {
            cout << error << '\n';
        }
    };
    class InvalCoor {
    private:
        string coor;
    public:
        InvalCoor() : coor("\nCoordinates or sides are less than 0\n") {};
        void cr() {
            cout << coor << '\n';
        }
    };
};