#ifndef CONTROL_CENTER_H
#define CONTROL_CENTER_H

#include "Client.h"

void processRoute(const Message& msg);

bool CheckCount(const Message&msg);

void processMultirotor(const Message& msg);

void processFixedWing(const Message& msg);

#endif