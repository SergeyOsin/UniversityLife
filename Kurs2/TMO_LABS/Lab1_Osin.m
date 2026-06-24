% Осин Сергей. 23ВП2. Вариант - 21.
clc
a = 3.0;
b = 5.0;
m = 5;
t1 = 9;
n = 15;

x = zeros(m, n);
disp('Матрица');
for i = 1:m
  for j = 1:n
    r = rand();
    x(i, j) = a + b * exp(-r);
  endfor
end
disp(x);


m_hat = mean(x);
D_hat = var(x, 0, 1);
sigma_hat = sqrt(D_hat);


K_hat = zeros(n, n);

for j = 1:n
  for l = 1:n
    K_hat(j, l) = sum((x(:, j) - m_hat(j)) .* (x(:, l) - m_hat(l))) / (m - 1);
  endfor
end

% Нормированная КФ
r_hat = K_hat ./ (sigma_hat' * sigma_hat);

% Построение графиков
figure;
hold on;


for i = 1:m
  plot(1:n, x(i, :), 'DisplayName', ['День ' num2str(i)]);
end


plot(1:n, m_hat, 'k-', 'LineWidth', 2, 'DisplayName', 'Математическое ожидание');

xlabel('Часы');
ylabel('Конверсия');
title('Реализации СП и МО');
legend show;
grid on;
hold off;

% Вывод результатов
disp('Математическое ожидание');
disp(m_hat);

disp('Дисперсия');
disp(D_hat);

disp('Среднее квадратическое отклонение');
disp(sigma_hat);

disp('Корреляционная функция');
disp(K_hat);

disp('Нормированная КФ');
disp(r_hat);
