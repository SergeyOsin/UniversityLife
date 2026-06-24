%Осин С.М. 23ВП2. Лабораторная работа №3. Вариант - 21.
clc;

global lambda;

function dpdt = markov_system(t, p)
  global lambda;
  dpdt = zeros(4, 1);
  dpdt(1) = -lambda(1, 2) * p(1) - lambda(1, 3) * p(1) + lambda(2, 1) * p(2) + lambda(3, 1) * p(3);
  dpdt(2) = -lambda(2, 1) * p(2) - lambda(2, 4) * p(2) + lambda(3, 2) * p(3) + lambda(4, 2) * p(4) + lambda(1, 2) * p(1);
  dpdt(3) = -lambda(3, 1) * p(3) - lambda(3, 2) * p(3) + lambda(1, 3) * p(1) + lambda(4, 3) * p(4);
  dpdt(4) = -lambda(4, 2) * p(4) - lambda(4, 3) * p(4) + lambda(2, 4) * p(2);
endfunction

function result = stat_probs()
  global lambda;
  A = [
       -(lambda(1, 2) + lambda(1, 3)), lambda(2, 1), lambda(3, 1), 0;
       lambda(1, 2), -(lambda(2, 1) + lambda(2, 4)), lambda(3, 2), lambda(4, 2);
       lambda(1, 3), 0, -(lambda(3, 1) + lambda(3, 2)), lambda(4, 3);
       1, 1, 1, 1
      ];
  B = [0; 0; 0; 1];
  result = A\B;
endfunction

N = 21;
lambda = [
          0, 3 + 0.02 * N, 4 + 0.01 * N, 0;
          1.5 + 0.03 * N, 0, 0, 2.5 + 0.02 * N;
          3.5 + 0.01 * N, 4.5 + 0.02 * N, 0, 0;
          0, 5 - 0.02 * N, 4 - 0.03 * N, 0
         ];

fprintf('Матрица интенсивностей\n');
disp(lambda);

P1_0 = [0.2, 0.25, 0.3, 0.25];
P2_0 = [1, 0, 0, 0];

t0 = 0;
L = 10;
delta_t = 1;
t = t0 : delta_t : L;

[t1, p1] = ode45(@markov_system, t, P1_0);
fprintf('     Время      p1        p2        p3        p4\n');
result_table_1 = [t1, p1];
disp(result_table_1);
[t2, p2] = ode45(@markov_system, t, P2_0);
fprintf('     Время      p1        p2        p3        p4\n');
result_table_2 = [t2, p2];
disp(result_table_2);
fprintf('Предельные вероятности:\n');
disp(stat_probs());

