clc
format longg

N = 21
n = 100
m = 24
lambda = 10*(N+1)/(N+4)
T1 = N+1
T2 = N+4

%Нахождение интервалов между последовательными моментами поступления
function l = getIntervals(n, lambda)
  for i = 1:n
    r = rand();
    l(i) = (-1/lambda)*log(r);
  endfor
endfunction

%Нахождение моментов поступления заявок
function t = getMoments(l, n)
  t = zeros(1,n);
  for i = 1:n
    sum = 0;
    for j = 1:i
      sum = sum + l(j);
    endfor
    t(i) = sum;
  endfor
endfunction

%Разделение временного промежутка на интервалы длины tau
function intervals = getTauIntervals(m, T1, T2, t, n)
  tau = (T2 - T1)/m %Шаг
  intervals = zeros(m, 3); %Интервалы записываются в виде матрицы 2x24
  startT = t(1); %Начало отсчёта
  for i = 1:m
    intervals(i,1) = startT; %Левая граница интервала
    intervals(i,2) = startT + tau; %Правая граница интервала
    startT = startT + tau;
  endfor

  for i = 1:m
    for j = 1:n
      %Если момент поступления заявки попадает в интервал, то в третьем столбце
      %матрицы напротив данного интервала увеличивается счётчик событий,
      %попавших в данный интервал
      if t(j) > intervals(i, 1) && t(j) < intervals(i, 2)
        intervals(i,3) = intervals(i,3) + 1;
      endif
    endfor
  endfor
endfunction

%Построение таблицы
function table = buildTable(intervals, n, m)
  table = zeros(3, n+1); %Таблица в виде матрицы
  k = 0;
  for i = 1:n + 1
    table(1, i) = k; %Первая строка - кол-во заявок от 0 до 100
    k = k + 1;
  endfor

  % Вторая строка - абсолютная частота
  for i = 1:n+1
    f = 0;
    for j = 1:m
      %Если данное кол-во заявок поступило на интервале, увеличиваем счётчик
      if intervals(j, 3) == i
        f = f + 1;
      endif
    endfor
    table(2, i+1) = f;
  endfor

  %Третья строка - относительная частота
  for i = 1:n + 1
    table(3,i) = table(2,i) / m;
  endfor

  % Для 0 заявок
  f0 = 0
  for j = 1:m
      if intervals(j, 3) == 0
        f0 = f0 + 1;
      endif
  endfor
  table(2, 1) = f0;
  table(3,1) = table(2,1) / m;
endfunction

% Вероятности
function p = getProbability(lambda, T1, T2, m)
  tau = (T2 - T1)/m;
  p = zeros(1, 6);
  for i = 0:5
    p(i+1) = (lambda*tau)^(i)/factorial(i) * exp(-1 * lambda * tau);
  endfor
end

%Интервалы времени между последовательными моментами поступления
l = getIntervals(n,lambda)

%Моменты поступления заявок
t = getMoments(l, n)

%Интервалы длины tau
intervals = getTauIntervals(m, T1, T2, t, n)

%Таблица
table = buildTable(intervals, n, m)

%Вероятности
p = getProbability(lambda, T1, T2, m)

