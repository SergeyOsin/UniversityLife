% Осин С.М. 23ВП2. Вариант - 6.

clc;
clear;
format short;

function F=ForwardWay(w,p,W)
    rows=length(w)+1;
    Matrix=zeros(rows,W+1);
    disp(["Размер таблицы f = ", num2str(rows), "x",num2str(W+1)]);
    for i=2:rows
      for j=2:W+1
        if (w(i-1)>j-1)
          Matrix(i,j)=Matrix(i-1,j);
        else
          Matrix(i,j)=max(Matrix(i-1,j),Matrix(i-1,j-w(i-1))+p(i-1));
        endif
      endfor
    endfor
    disp(Matrix);
    F=Matrix;
    disp(["Оптимальное значение целевой функции = ", num2str(F(rows,W+1))]);
end

function answ = BackWay(F,w,i,j)
    answ=[];
    numbIter=1;
    while (F(i,j)>0)
      disp(["Итерация №", num2str(numbIter)]);
      disp(["Сравнение F(", num2str(i),",",num2str(j),") и F(", num2str(i-1),...
      ",",num2str(j),")"]);
      disp([num2str(F(i,j))," и ", num2str(F(i-1,j))]);
      if (F(i,j)!=F(i-1,j))
        disp(["Предмет №", num2str(i-1), " добавляется в ответ"]);
        answ(end+1)=i-1;
        j-=w(i-1);
      endif
      i--;
      numbIter++;
    endwhile
    disp(["F(", num2str(i),",",num2str(j),") = ", num2str(F(i,j)), ...
    " => Цикл завершен"]);
    disp("\nОптимальный набор предметов: ");
    disp(answ);
    disp("");
end

function PrintResults(answ,w,p,W)
   sumW=0; sumP=0;
   for k=1:length(answ)
         current=answ(k);
         disp(["Предмет №", num2str(current)]);
         disp(["Вес данного предмета = ", num2str(w(current))]);
         sumW+=w(current);
         disp(["Ценность данного предмета = ", num2str(p(current))]);
         sumP+=p(current);
   endfor
   disp(["\nСуммарный вес = ", num2str(sumW)]);
   disp([num2str(sumW) "<=", num2str(W)]);
   disp(["Суммарная ценность = ", num2str(sumP)]);
end

function main()
    disp("Варианты работы программы: \n1. Пример\n2. Собственный ввод");
    disp("Любой другой символ - завершение программы");
    choose=input("Введите символ: ");
    if (choose==1)
        W=13;
        printf("W = %d\n", W);
        disp("w = ");
        w=[3,4,5,8,9];
        disp(w);
        disp("p = ");
        p=[1,6,4,7,6];
        disp(p);
        n=length(w);
        printf("Количество предметов = %d\n", n);
    elseif (choose==2)
        W=(input("Грузоподъемность рюкзака = "));
        n=(input("Количество предметов = "));
        disp("Введите в квадратных скобках []. Например, [1,3,4,5]");
        w=input(["Введите веса ", num2str(n), " предметов = "]);
        p=input(["Введите ценности ", num2str(n), " предметов = "]);
    else return;
    endif
    rows=n + 1;
    disp("\nПрямой ход");
    F=ForwardWay(w,p,W);
    disp("\nЗадний ход");
    answ=BackWay(F,w,rows,W+1);
    disp("Результаты: ");
    PrintResults(answ,w,p,W);
endfunction

main();
