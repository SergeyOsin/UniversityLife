#include <iostream>
#include <unistd.h>
#include <sys/wait.h>
#include <sys/ipc.h>
#include <sys/msg.h>
#include <sys/types.h>
#include "Client.h"
#include "ControlCenter.h"

using namespace std;

void displayMenu() {
    string symb = "1";
    cout<<"Беспилотники включены\n";
    while (symb == "1" || symb == "2") {
        cout << "Типы беспилотника (1-2):\n";
        cout << "1. Мультироторный\n";
        cout << "2. Самолетный\n";
        cout << "Любой другой символ - завершение работы программы\n";
        cout << "Введите тип беспилотника: ";
        cin >> symb;
        if (symb == "1" || symb == "2") {

            int type = symb[0] - '0';

            int msqid = msgget(IPC_PRIVATE, 0666 | IPC_CREAT);
            if (msqid == -1) {
                perror("Ошибка получения сообщения");
                return;
            }

            pid_t pid = fork();

            if (pid < 0) {
                cerr << "Ошибка fork\n";
                msgctl(msqid, IPC_RMID, NULL);
                return;
            }

            if (pid > 0) {
                Message msg=createMessage(type);
                msg.mtype=1;
                if (msgsnd(msqid, &msg, sizeof(Message)-sizeof(long), 0) == -1) {
                    perror("Ошибка отправки сообщения");
                }

                wait(NULL);

                msgctl(msqid, IPC_RMID, NULL);
                cout << "Центр управления завершил работу\n\n";
            }
            else {
                Message msg;

               if (msgrcv(msqid, &msg, sizeof(Message) - sizeof(long), 1, 0) == -1) {
                    perror("Ошибка получения сообщения");
                    exit(1);
                }
                processRoute(msg);
                exit(0);
            }
        }
    }
    cout<<"Беспилотники отключены\n";
    cout << "Программа завершает работу\n";
}

int main() {
    displayMenu();
    return 0;
}