% Осин С.М. 23ВП2.

clc;
format short;

function checkBi(Matrix)
    [rows, cols] = size(Matrix);
    for i = 1:rows-1
        if Matrix(i, cols) < 0
            error("Некоторые свободные члены отрицательные");
        end
    end
end

function [Matrix,minElem,minInd]= Solve(Matrix,minInd,Type)
     [rows,cols]=size(Matrix);
     if (all(Matrix(1:rows-1,minInd)<0))
         error("В разрешающем столбце нет чисел > 0.");
      end
     SI=zeros(1,rows-2);
     for i=1:rows-2
        if(Matrix(i,cols)/Matrix(i,minInd)>0)
          SI(i)=Matrix(i,cols)/Matrix(i,minInd);
        end
     endfor
     MElem=Inf; MInd=0;
     for i=1:rows-2
        if (SI(i)>0 && SI(i)<MElem)
         MElem=SI(i);
         MInd=i;
        endif
     endfor
     Matrix(MInd,:)=Matrix(MInd,:)/Matrix(MInd,minInd);
     for i=1:rows
        if i==MInd
         continue;
        endif
        Matrix(i,:)=Matrix(i,:)-Matrix(i,minInd)* Matrix(MInd,:);
     endfor
     if (Type==1)
        Matrix(:,cols-1-(rows-2)+MInd)=[];
     endif
     eps_val = 1e-5;
     Matrix(abs(Matrix) < eps_val) = 0;
     disp(Matrix);
     [newRow,newCols]=size(Matrix);
     LastStr=Matrix(newRow,1:newCols-1);
     [minElem,minInd]=min(LastStr);
end

function printResult(Matrix,Type)
   disp("Результаты: ");
   [Rows,Cols]=size(Matrix);
   AnswX=zeros(1,Cols-1);
   for i=1:Rows-1
    for j=1:Cols-1
      if (Matrix(i,j)==1)
         AnswX(j)=(Matrix(i,Cols));
        break;
      endif
    endfor
   endfor
   for k=1:Cols-1
     disp(["X",num2str(k),"= ", num2str(AnswX(k))]);
   endfor
   if (Type=="min")
     disp(["Z= ",num2str(-Matrix(Rows,Cols))]);
   endif
   if (Type=="max")
    disp(["Z= ",num2str(Matrix(Rows,Cols))]);
   end
end

function main(Type)
 disp("Исходная матрица: ");
    Matrix = [
        1, -3, 2, 2, 1, 0,3;
        2, -2, 1, 1, 0, 1, 3;
        -5, -3, -4, 1, 0, 0, 0
    ];
    disp(Matrix);
    checkBi(Matrix);
    [rows, cols] = size(Matrix);
    M_row = zeros(1, cols);
    for i = 1:cols-1
        M_row(i) = -(Matrix(1, i) + Matrix(2, i));
    endfor
    M_row=zeros(1,cols-3);
    for j=1:cols-3
     M_row(j)=-(Matrix(1,j)+Matrix(2,j));
    endfor
    M_row(cols) = -(Matrix(1, cols) + Matrix(2, cols));
    Matrix = [Matrix; M_row];
    disp("M-строка:");
    disp(Matrix(end, :));

    if all(Matrix(end, 1:cols-3) >= 0)
        error("Система несовместна.");
    endif

   LastStr=Matrix(rows,1:cols-1);
   [minElem,minInd]=min(LastStr);
   NumberIter=1;
   TypeSolve=1;
   while (minElem<0)
     disp(["Итерация ", num2str(NumberIter)]);
    [Matrix, minElem, minInd] = Solve(Matrix, minInd,TypeSolve);
    NumberIter = NumberIter + 1;
   endwhile
   TypeSolve=TypeSolve+1;
   [Rows,Cols]=size(Matrix);
   Matrix(Rows,:)=[];
   [minElem,minInd]=min(Matrix(Rows-1,:));
   while (minElem<0)
     [rows,cols]=size(Matrix);
     disp(["Итерация ",num2str(NumberIter)]);
     [Matrix,minElem,minInd]=Solve(Matrix,minInd,TypeSolve);
     NumberIter=NumberIter+1;
   endwhile
   printResult(Matrix,Type);
end;

main("max");
