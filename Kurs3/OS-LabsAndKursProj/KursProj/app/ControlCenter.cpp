#include <iostream>
#include <unistd.h>
#include <cmath>
#include "ControlCenter.h"

using namespace std;

bool CheckCount(const Message&msg){
    if (msg.count==0){
        cout<<"Маршрут полёта беспилотника не задан\n";
        return false;
    }
    return true;
}

void processMultirotor(const Message& msg) {
    cout << "Тип: Мультироторный БПЛА\n";
    if (!CheckCount(msg)) return;
    cout<<"Маршрут беспилотника задан " << msg.count<< " точками\n";
    cout << "Дрон начал маршрут\n";
    for (int i = 0; i < msg.count - 1; i++) {
        Waypoint a = msg.points[i];
        Waypoint b = msg.points[i + 1];

        cout<<"\nДрон летит на точку ("<< b.x<<","<<b.y<<")\n";
        sleep(1);

        double dy = b.y - a.y;

        if (dy != 0) {
            cout << "Изменение высоты до " << b.y << "...\n";
            cout << "Скорость по вертикали: " << abs(dy) << " м/с\n";
            sleep(1);
        }

        double dx = b.x - a.x;

        if (dx != 0) {
            cout << "Полёт по горизонтали к X = " << b.x << "\n";
            cout << "Скорость по горизонтали: " << abs(dx) << " м/с\n";
            cout<<"Дрон застыл в воздухе\n";
            sleep(3);
        }
    }

    Waypoint last = msg.points[msg.count - 1];

    if (last.y > 0) {
        cout << "Начало посадки\n";
        cout << "Снижение с высоты " << last.y << " до 0\n";
        cout << "Скорость снижения: " << last.y << " м/с\n";
        sleep(2);
    } 
    cout<<"Дрон приземлился\n";

    cout << "Маршрут завершён.\n";
}

void processFixedWing(const Message& msg) {
    cout << "Тип: Самолетный БПЛА\n";
    if (!CheckCount(msg)) return;
    cout<<"Маршрут беспилотника задан " << msg.count<< " точками\n";
    cout << "Дрон начал маршрут\n";
    for (int i = 0; i < msg.count - 1; i++) {
        Waypoint a = msg.points[i];
        Waypoint b = msg.points[i + 1];

        cout<<"\nДрон летит на точку ("<< b.x<<" "<<b.y<<")\n";
        sleep(1);

        double dx = b.x - a.x;
        double dy = b.y - a.y;

        double distance = sqrt(dx * dx + dy * dy);

        double speed = distance * 1.5;

        cout << "Прямой полёт к точке (" << b.x << ", " << b.y << ")\n";
        cout << "Скорость: " << speed << " м/с\n\n";

        sleep(1);
    }
    Waypoint last = msg.points[msg.count - 1];

    if (last.y > 0) {
        cout << "Начало снижения\n";

        double currentY = last.y;

        while (currentY > 0) {
            double step = min(2.0, currentY); 
            currentY -= step;

            cout << "Высота: " << currentY << " м\n";
            sleep(1);
        }

        cout << "Касание земли\n";
        cout << "Пробег по полосе\n";
        sleep(1);

    }
    cout<<"Беспилотник приземлился\n";

    cout << "Маршрут завершён.\n";
}

void processRoute(const Message& msg) {
    (msg.droneType==1)?processMultirotor(msg):processFixedWing(msg);
}