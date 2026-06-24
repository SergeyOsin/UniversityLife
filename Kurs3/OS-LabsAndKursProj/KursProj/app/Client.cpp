#include "Client.h"
Message msg;

Message createMessage(int droneType) {
    msg.mtype=1;
    msg.droneType = droneType; 
    if (droneType==1){
        msg.count = 5;
        msg.points[0] = {0, 0};
        msg.points[1] = {0, 8};   
        msg.points[2] = {6, 9};   
        msg.points[3] = {3, 11};  
        msg.points[4] = {8,12};
    }
    else{
        msg.count = 6;
        msg.points[0] = {0, 0};
        msg.points[1] = {1,4};
        msg.points[2] = {3,9};
        msg.points[3] = {4,12};
        msg.points[4]={6,14};
        msg.points[5]={8,16};
    }
    return msg;
}