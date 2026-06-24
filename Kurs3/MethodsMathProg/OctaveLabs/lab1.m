% Осин С.М. 23ВП2. Вариант - 1 (16-15).
clc;
format short g;

function printMatrix(Matrix)
    disp("\t   x1 \t\tx2\t\tx3 \t  x4 \t\tx5 \t  bi");
    disp(Matrix);
end

function checkBi(Matrix)
    [rows, cols] = size(Matrix);
    for i = 1:rows-1
        if Matrix(i, cols) < 0
            error("Некоторые свободные члены отрицательные");
        end
    end
end

function Simp = solve(Simp, minEl, min_ind)
    [rows, cols] = size(Simp);
    if (all(Simp(1:rows-1,min_ind)<0))
     error("В разрешающем столбце нет чисел > 0.");
    end
    Ind = 0;
    minValue = inf;
    for j = 1:rows-1
        if Simp(j, min_ind) > 0
            Divis=Simp(j,cols)/Simp(j,min_ind);
            if  minValue>Divis
                minValue=Divis;
                Ind = j;
            end
        end
    end
    Simp(Ind, :) = Simp(Ind, :) / Simp(Ind, min_ind);
    for i = 1:rows
        if i == Ind
            continue;
        end
        Simp(i, :) = Simp(i, :) - Simp(i,min_ind) * Simp(Ind, :);
    end
end

function main()
    disp("Исходные данные: ");
    Simp = [
        1,1,1,0,0,3000;
        0.1,0.2,0,1,0,400;
        0.05,0.02,0,0,1,100;
        -4,-6,0,0,0,0];
    printMatrix(Simp);
    [rows, cols] = size(Simp);
    checkBi(Simp);
    SelectRow = Simp(rows, 1:cols-1);
    [minEl, min_ind] = min(SelectRow);
    NumbIteration=1;

    while minEl < 0
        disp(['Итерация ' num2str(NumbIteration)]);
        Simp = solve(Simp, minEl, min_ind);
        checkBi(Simp);
        printMatrix(Simp);
        SelectRow=Simp(rows,1:cols-1);
        [minEl, min_ind] = min(SelectRow);
        NumbIteration=NumbIteration+1;
    end

    disp('Решение:');
    for i=1:3
      for j=1:3
        if (Simp(i,j)==1)
          disp(['x' num2str(j) ' = ' num2str(Simp(i,cols))]);
          break;
        end
      end
    end
    disp(['Z = ' num2str(Simp(rows, cols))]);
end

main();


