% Осин С.М. 23ВП2. Вариант - 6.

clc;
clear;
format short;

function n = gradNorm(x,y)
  n = sqrt((fDX(x))^2 + (fDY(y))^2);
end

function f = f(x,y)
  f = 3*x*x-4*x+y*y;
end

function DX = fDX(x)
  DX=6*x-4;
end

function DY=fDY(y)
  DY=2*y;
end

function printFirstWay(x,y,x_new,y_new)
  disp(["V f(",num2str(x),", ",num2str(y),") = (", num2str(fDX(x)), ", ", ...
  num2str(fDY(y)),")"]);
  disp(["(new_x, new_y) = (", num2str(x_new), ",", ...
   num2str(y_new),")"]);
end

function [x,x_new, y, y_new] = Iter1(x,x_new, y, y_new,h)
  x=x_new;
  y=y_new;
  x_new = x - h * fDX(x);
  y_new=y-h*fDY(y);
  printFirstWay(x,y,x_new,y_new);
end

function [x,x_new,y,y_new]=Iter2(x,x_new,y,y_new,h)
  x=x_new;
  y=y_new;
  x_new=x-h*fDX(x)/gradNorm(x,y);
  y_new=y-h*fDY(y)/gradNorm(x,y);
  printFirstWay(x,y,x_new,y_new);
end

function main()
  disp("Задача минимизации: f(x,y)=3x^2-4x+y^2");
  disp("x принадлежит R");
  disp("V f(x,y)=(6x-4, 2y)");
  h=input("h (<0.33!)= ");
  while (h>0.33 || h<0)
   disp("Введите положительное h<0.33!");
   h=input("h (<0.33)!= ");
  endwhile
  e=input("e=");
  disp(f(0.67,0));

  x=0;
  y=0;
  arr=[x,y];
  resFunc=zeros(1,6);
  resIter=zeros(1,6);
  lastIter=zeros(1,12);

  disp("Первый вариант");
  NumbIter=1;
  disp(["Итерация №", num2str(NumbIter)]);
  x_new=x-h*fDX(x);
  y_new=y-h*fDY(y);
  printFirstWay(x,y,x_new,y_new);
  while (abs(x_new-x)>e || abs(y_new-y)>e)
    NumbIter=NumbIter + 1;
    disp(["Итерация №",num2str(NumbIter)]);
    [x,x_new,y,y_new]=Iter1(x,x_new,y,y_new,h);
  endwhile
  resFunc(1)=f(x_new,y_new);
  resIter(1)=NumbIter;
  lastIter(1)=x_new;
  lastIter(2)=y_new;

  disp("\nВторой вариант\n");
  NumbIter=1;
  x=arr(1);
  y=arr(2);
  disp(["Итерация №", num2str(NumbIter)]);
  x_new=x-h*fDX(x);
  y_new=y-h*fDY(y);
  printFirstWay(x,y,x_new,y_new);;
  while(abs(f(x_new,y_new)-f(x,y))>e)
    NumbIter=NumbIter + 1;
    disp(["Итерация №",num2str(NumbIter)]);
    [x,x_new,y,y_new]=Iter1(x,x_new,y,y_new,h);
  end
  resFunc(2)=f(x_new,y_new);
  resIter(2)=NumbIter;
  lastIter(3)=x_new;
  lastIter(4)=y_new;

  disp("\nТретий вариант\n");
  NumbIter=1;
  x=arr(1);
  y=arr(2);
  disp(["Итерация №", num2str(NumbIter)]);
  x_new=x-h*fDX(x);
  y_new=y-h*fDY(y);
  printFirstWay(x,y,x_new,y_new);
  while (gradNorm(x,y) > e)
    NumbIter=NumbIter + 1;
    disp(["Итерация №",num2str(NumbIter)]);
    [x,x_new,y,y_new]=Iter1(x,x_new,y,y_new,h);
  end
  resFunc(3)=f(x_new,y_new);
  resIter(3)=NumbIter;
  lastIter(5)=x_new;
  lastIter(6)=y_new;

  disp("\nЧетвертый вариант\n");
  NumbIter = 1;
  x = arr(1);
  y = arr(2);
  h1=h;
  disp(["Итерация №", num2str(NumbIter)]);
  x_new = x - h1 * fDX(x) / gradNorm(x,y);
  y_new = y - h1 * fDY(y) / gradNorm(x,y);
  printFirstWay(x, y, x_new, y_new);
  h1=h1*0.9;
  while (abs(x_new-x)>e || abs(y_new-y)>e)
     NumbIter=NumbIter+1;
     disp(["Итерация №",num2str(NumbIter)]);
     [x,x_new,y,y_new]=Iter2(x,x_new,y,y_new,h1);
     h1=h1*0.9;
  end
  resFunc(4)=f(x_new,y_new);
  resIter(4)=NumbIter;
  lastIter(7)=x_new;
  lastIter(8)=y_new;

  disp("\nПятый вариант\n");
  NumbIter=1;
  x=arr(1);
  y=arr(2);
  h1=h;
  disp(["Итерация №", num2str(NumbIter)]);
  normG = gradNorm(x,y);
  x_new = x - h1 * fDX(x) / normG;
  y_new = y - h1 * fDY(y) / normG;
  printFirstWay(x, y, x_new, y_new);
  h1=h1*0.9;

  while (abs(f(x_new,y_new)-f(x,y))>e)
       NumbIter=NumbIter+1;
       disp(["Итерация №",num2str(NumbIter)]);
       [x,x_new,y,y_new]=Iter2(x,x_new,y,y_new,h1);
       h1=h1*0.9;
  end
  resFunc(5)=f(x_new,y_new);
  resIter(5)=NumbIter;
  lastIter(9)=x_new;
  lastIter(10)=y_new;

  disp("\nШестой вариант\n");
  NumbIter=1;
  x=arr(1);
  y=arr(2);

  disp(["Итерация №", num2str(NumbIter)]);
  normG = gradNorm(x,y);
  h1=h;
  x_new = x - h1 * fDX(x) / normG;
  y_new = y - h1 * fDY(y) / normG;
  printFirstWay(x, y, x_new, y_new);
  h1=h1*0.9;

  while (gradNorm(x,y) > e)
       NumbIter=NumbIter+1;
       disp(["Итерация №",num2str(NumbIter)]);
       [x,x_new,y,y_new]=Iter2(x,x_new,y,y_new,h1);
       h1=h1*0.9;
  end
  resFunc(6)=f(x_new,y_new);
  resIter(6)=NumbIter;
  lastIter(11)=x_new;
  lastIter(12)=y_new;

  disp("\n\nРезультаты функций и количество итераций: ");
  for i=1:6
   disp(["\nВариант ", num2str(i)]);
   fprintf("Значение функции = %.2f\n", resFunc(i));
   fprintf("(x,y) = (%.2f, %.2f)\n", lastIter(i*2-1), lastIter(i*2));
   disp(["Количество итераций = ", num2str(resIter(i))]);
  endfor
end

main();
