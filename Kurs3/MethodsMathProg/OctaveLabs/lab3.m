% Осин С.М. 23ВП2.
clc;
format short;

function PrintSolution(Matrix,Type)
    [rows, cols] = size(Matrix);
    Xsol = zeros(1, cols - rows);
    for i = 1:rows-1
        for j = 1:cols-rows
            if (Matrix(i, j) == 1)
                Xsol(j) = Matrix(i, cols);
                break;
            endif
        endfor
    endfor
    disp("Решение: ");
    for i = 1:cols-rows
        disp(["X", num2str(i), "= ", num2str(Xsol(i))]);
    endfor
    if (Type=="min")
      disp(["Z= ", num2str(-Matrix(rows, cols))]);
    else
      disp(["Z= ", num2str(Matrix(rows, cols))]);
    end
end

function Simp = SimpleSimplexSolve(Simp, minEl, min_ind)
    [rows, cols] = size(Simp);
    if (all(Simp(1:rows-1, min_ind) < 0))
        error("В разрешающем столбце нет чисел > 0.");
    end
    Ind = 0;
    minValue = inf;
    for j = 1:rows-1
        if Simp(j, min_ind) > 0
            Divis = Simp(j, cols) / Simp(j, min_ind);
            if minValue > Divis
                minValue = Divis;
                Ind = j;
            end
        end
    end
    Simp(Ind, :) = Simp(Ind, :) / Simp(Ind, min_ind);
    for i = 1:rows
        if i == Ind
            continue;
        end
        Simp(i, :) = Simp(i, :) - Simp(i, min_ind) * Simp(Ind, :);
    end
end

function Matrix = DoubleSimpleSolve(Matrix,minElem, minInd)
    [rows,cols]=size(Matrix);
    Dj = zeros(1, cols-1);
    for i = 1:cols-1
      if Matrix(minInd, i)<0
           Dj(i) = abs(Matrix(rows, i) / Matrix(minInd, i));
      end
    endfor
    minVal = Inf; IndMinVal = 0;
    for j = 1:cols-1
         if Dj(j) > 0 && Dj(j) < minVal
               minVal = Dj(j);
               IndMinVal = j;
         end
    endfor

    Matrix(minInd, :) = Matrix(minInd, :) / Matrix(minInd, IndMinVal);
    for i = 1:rows
         if i == minInd
              continue;
         end
    Matrix(i, :) = Matrix(i, :) - Matrix(i, IndMinVal) * Matrix(minInd, :);
    end
end

function main()
    Count = input("Количество переменных в Z: ", "s");
    Count = str2num(Count);
    disp("Введите в квадратных скобках: [1,2,3] например");
    ArrayZ = input(["Введите ", num2str(Count), " коэффициентов Z: "]);

    Type = input("Z->max/min (по умолчанию max):  ", "s");
    if isempty(Type)
        Type = "max";
    end

    NumberSystem = input("Введите количество ограничений: ", "s");
    NumberSystem = str2num(NumberSystem);

    rows = NumberSystem + 1;
    cols = Count + NumberSystem + 1;
    Matrix = zeros(rows, cols);
    ArrayZ=-ArrayZ;

    Matrix(rows, 1:Count) = ArrayZ;
    Matrix(rows, cols) = 0;

    disp("Введите коэффициенты системы (каждое ограничение):");
    for i = 1:NumberSystem
        disp(["Ограничение ", num2str(i), ":"]);

        znak = input("Знак: (0 - =, 1 - <=, 2 - >=): ", "s");
        znak = str2num(znak);

        coeffs = input("Коэффициенты [a1,a2,...,an,b]: ", "s");
        coeffs = str2num(coeffs);

        Matrix(i, 1:Count) = coeffs(1:end-1);

        if znak == 1
            Matrix(i, Count + i) = 1;
            Matrix(i, cols) = coeffs(end);
        elseif znak == 2
            Matrix(i,:)=-Matrix(i,:);
            Matrix(i, Count + i) = 1;
            Matrix(i, cols) = -coeffs(end);
        else
            cols = cols + 1;
            Matrix = [Matrix, zeros(rows, 1)];
            Matrix(i, cols-1) = 1;
            Matrix(i, cols) = coeffs(end);
        end
    end

    disp("Симплекс-матрица:");
    disp(Matrix);

    [rows, cols] = size(Matrix);
    LastCol = Matrix(:, cols);

    if all(LastCol >= 0)
        disp("Решение обычным симплекс-методом.");
        NumIter = 1;
        target_row = Matrix(rows, 1:cols-1);
        [minEl, min_ind] = min(target_row);
        while (minEl<0)
            disp(["Итерация ", num2str(NumIter)]);
            Matrix = SimpleSimplexSolve(Matrix, minEl, min_ind);
            disp(Matrix);
            NumIter = NumIter + 1;
            target_row = Matrix(rows, 1:cols-1);
            [minEl, min_ind] = min(target_row);
        end
    else
        disp("Решение двойственным симлпекс-методом");
        [minElem, minInd] = min(LastCol);
        % условие бесконечности Z функции
        if all(Matrix(minInd, 1:cols-1) >= 0)
            error("В ведущей строке нет чисел < 0! Задача нерешима");
        end

        NumIter = 1;
        while minElem < 0
            disp(["Итерация ", num2str(NumIter)]);
            Matrix=DoubleSimpleSolve(Matrix, minElem, minInd);
            disp(Matrix);
            LastCol = Matrix(:, cols);
            [minElem, minInd] = min(LastCol);
            NumIter = NumIter + 1;
        end
    end

    PrintSolution(Matrix,Type);
end

main();

