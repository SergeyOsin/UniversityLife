% Осин С.М. 23ВП2. Вариант - 6.

clc;
clear;
format short;

function [X,C,Xbas,basic]=SolveNorthEastAngle(A,B,C)
    i=1;j=1;
    Xbas=[];
    sizeA=length(A);
    sizeB=length(B);
    basic=zeros(sizeA,sizeB);
    X=zeros(sizeA,sizeB);
    NumIter=1;
    while (i<=sizeA && j<=sizeB)
      disp(["Итерация №", num2str(NumIter)]);
      if (A(i)>=B(j))
        X(i,j)=B(j);
        basic(i,j)=1;
        Xbas(end+1)=B(j);
        A(i)=A(i)-B(j);
        B(j)=0;
        j=j+1;
      else
        X(i,j)=A(i);
        basic(i,j)=1;
        Xbas(end+1)=A(i);
        B(j)=B(j)-A(i);
        A(i)=0;
        i=i+1;
      endif
      disp(X);
      NumIter=NumIter+1;
    endwhile

    if (any(B > 0))
      for k = 1:sizeB
        if (B(k) > 0)
          X(sizeA,k) = B(k);
          Xbas(end+1) = B(k);
        endif
      endfor
    endif
end

function PrintResults(A,B,C,X,Xbas)
  disp("\nРезультаты Северо-Западного угла:");
  disp(["Xбаз = ", num2str(Xbas)]);
  lenXbas=length(Xbas);
  disp(["Количество Xбаз = ", num2str(lenXbas)]);
  if (lenXbas==length(A)+length(B)-1)
   disp("Данное решение - базисное");
  endif
  if (all(Xbas>0))
   disp("Данное решение - невырожденное");
  else disp("Данное решение - вырожденное");
  endif
  disp("C = ");
  disp(C);
  disp("X = ");
  disp(X);
  sumRez = sum(sum(X .* C));
  disp(["Начальная стоимость Z = ", num2str(sumRez)]);
end


function SolvePotentional(A, B, C, X,basic)
    [lenA, lenB] = size(X);
    MaxIter = 100;
    Iter = 1;
    while Iter <= MaxIter
        disp(["\nИтерация №", num2str(Iter)]);
        U = nan(1, lenA);
        V = nan(1, lenB);
        U(1) = 0;

        nan_count = sum(isnan(U)) + sum(isnan(V));
        while nan_count > 0
            prev_nan_count = nan_count;
            for i = 1:lenA
                  for j = 1:lenB
                         if basic(i,j)==1
                             if ~isnan(U(i)) && isnan(V(j))
                                 V(j) = C(i,j) - U(i);
                             elseif isnan(U(i)) && ~isnan(V(j))
                                 U(i) = C(i,j) - V(j);
                             end
                         end
                  end
            end
            nan_count = sum(isnan(U)) + sum(isnan(V));
            if nan_count == prev_nan_count
                break;
            end
        end

        disp("Система потенциалов: ");
        disp(["U = ", num2str(U)]);
        disp(["V = ", num2str(V)]);

        Delta = zeros(lenA, lenB);
        maxDelta = -inf;
        rowIn = -1; colIn = -1;

        for i = 1:lenA
            for j = 1:lenB
                if basic(i,j) == 0
                    Delta(i,j) = U(i) + V(j) - C(i,j);
                    if Delta(i,j) > maxDelta
                        maxDelta = Delta(i,j);
                        rowIn = i;
                        colIn = j;
                    elseif Delta(i,j) == maxDelta
                        if C(i,j) < C(rowIn,colIn)
                            rowIn = i;
                            colIn = j;
                        endif
                    endif
                endif
            end
        end

        disp("Матрица оценок Delta:");
        disp(Delta);

        disp(["Максимальная оценка: ", num2str(maxDelta)]);
        if maxDelta <= 0
            disp("\nРешение: ");
            PrintFinal(X, C);
            return;
        end
        disp(["Базис в клетке: (", num2str(rowIn), ", ", num2str(colIn), ")"]);

        PathMask = basic;
        PathMask(rowIn, colIn) = 1;

        changed = true;
        while changed
            changed = false;
            for i = 1:size(PathMask, 1)
                if sum(PathMask(i, :)) == 1
                    PathMask(i, :) = 0;
                    changed = true;
                end
            end
            for j = 1:size(PathMask, 2)
                if sum(PathMask(:, j)) == 1
                    PathMask(:, j) = 0;
                    changed = true;
                end
            end
        end

        [X, basic] = UpdatePlan(X, basic, PathMask, rowIn, colIn);

        disp("Обновленный план X:"); disp(X);
        Iter = Iter + 1;
    end
end


function [X, basic] = UpdatePlan(X, basic, PathMask, r0, c0)
    Cycle = [r0, c0];
    curr_r = r0; curr_c = c0;
    dir = 1;

    for k = 1:100
        if dir == 1
            c_cands = find(PathMask(curr_r, :));
            curr_c = c_cands(c_cands ~= curr_c);
            if isempty(curr_c), break; end
            curr_c = curr_c(1);
        else
            r_cands = find(PathMask(:, curr_c));
            curr_r = r_cands(r_cands ~= curr_r);
            if isempty(curr_r), break; end
            curr_r = curr_r(1);
        end

        if curr_r == r0 && curr_c == c0
            break;
        end
        Cycle = [Cycle; curr_r, curr_c];
        dir = 1 - dir;
    end

    PlusCells = Cycle(1:2:end, :);
    MinusCells = Cycle(2:2:end, :);

    vals = [];
    for i = 1:size(MinusCells, 1)
        vals(end+1) = X(MinusCells(i,1), MinusCells(i,2));
    end
    theta = min(vals);
    for i = 1:size(PlusCells, 1)
        X(PlusCells(i,1), PlusCells(i,2)) = X(PlusCells(i,1), PlusCells(i,2)) + theta;
    end

    zero_found = false;
    r_out = -1; c_out = -1;
    for i = 1:size(MinusCells, 1)
        X(MinusCells(i,1), MinusCells(i,2)) = X(MinusCells(i,1), MinusCells(i,2)) - theta;
        if X(MinusCells(i,1), MinusCells(i,2)) == 0 && ~zero_found
            r_out = MinusCells(i,1);
            c_out = MinusCells(i,2);
            zero_found = true;
        end
    end

    basic(r0, c0) = 1;
    if r_out ~= -1 && c_out ~= -1
        basic(r_out, c_out) = 0;
        disp(["Клетка (", num2str(r_out), ", ", num2str(c_out), ") покинула базис."]);
    end
end

function PrintFinal(X, C)
    disp("X ="); disp(X);
    disp("C ="); disp(C);

    sumRez = sum(sum(X .* C));
    disp(["Zmin = ", num2str(sumRez)]);
end


function main()
  disp("Вариант работы программы: ");
  disp("1. Пример");
  disp("2. Вариант - 6");
  disp("3. Вариант с открытой моделью (Сумма потребителей больше)");
  disp("4. Вариант с открытой моделью (Сумма поставщиков больше)");
  disp("5. Вариант с вырожденным решением");
  disp("Любой другой символ - завершение программы");
  choose=input("Вариант работы: ");

  switch(choose)
      case 1
         A=[100,250,200,300];
         B=[200,200,100,100,250];
         C=[
            10,7,4,1,4;
            2,7,10,6,11;
            8,5,3,2,2;
            11,8,12,16,13
        ];
      case 2
        A=[350,200,300];
        B=[170,140,200,195,145];
        C=[
          22,14,16,28,30;
          19,17,26,36,36;
          37,30,31,39,41
         ];
      case 3
         A=[400,200,300];
         B=[170,140,200,195,145];
         C=[
           22,14,16,28,30;
           19,17,26,36,36;
           37,30,31,39,41
          ];
      case 4
         A=[90,200,300];
         B=[100,140,200,195,100];
         C=[
           22,14,16,28,30;
           19,17,26,36,36;
           37,30,31,39,41
          ];
       case 5
         A=[170,200,300];
         B=[170,140,200,195,145];
         C=[
           22,14,16,28,30;
           19,17,26,36,36;
           37,30,31,39,41
          ];
       otherwise
           return;
  endswitch
  disp("\nЧасть 1. Северо-западный угол \n");

  disp(["Ai|Bj ", num2str(B)]);
  [str,cols]=size(C);
  for i=1:str
   disp([num2str(A(i)),"     ", num2str(C(i,:))]);
  endfor
  sumA=sum(A);
  sumB=sum(B);
  disp(["\nСумма A = ", num2str(sumA)]);
  disp(["Сумма B = ", num2str(sumB)]);

  X=zeros(str,cols);
  if (sumA>sumB)
    disp("Открытая модель\n");
    B(end+1)=sum(A)-sum(B);
    disp(["B = ",num2str(B)]);
    C(:,end+1)=0;
    X(:,end+1)=0;
   elseif (sumA<sumB)
    disp("Открытая модель\n");
    A(end+1)=sum(B)-sum(A);
    disp(["A = ",num2str(A)]);
    C(end+1,:)=0;
    X(end+1,:)=0;
  disp("C = "), disp(C);
  disp("Модель закрытая");
  endif

   [X,C,Xbas,basic]=SolveNorthEastAngle(A,B,C);
   PrintResults(A,B,C,X,Xbas);

   disp("\nЧасть 2. Метод потенциалов");
   SolvePotentional(A,B,C,X,basic);
end

main();
