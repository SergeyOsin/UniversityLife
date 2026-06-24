#ifndef CLIENT_H
#define CLIENT_H


struct Waypoint {
    double x;
    double y;
};

struct Message {
    long mtype;
    Waypoint points[100];
    int count; 
    int droneType;
};

Message createMessage(int dronetype);

#endif