% Осин С.М. 23ВП2. Лабораторная работа №8. Вариант-21
clc;
clear all;
format short g;

N = randi([3, 10])
n = N + 1
m = 5 * (N + 1)
lambda = randi([N + 10, N + 20])
time_service = randi([N + 90, N + 100])
mu = 1 / time_service
pi_val = lambda / mu

function [P0, array_p] = calculate_limited_probabilities(pi_val, m, n)
    sum1 = 0;
    for k = 0:n
        sum1 = sum1 + (factorial(m) * pi_val^k) / (factorial(k) * factorial(m - k));
    end
    sum2 = 0;
    for k = n+1:m
        sum2 = sum2 + (factorial(m) * pi_val^k) / (factorial(n) * n^(k - n) * factorial(m - k));
    end
    P0 = (sum1 + sum2)^-1;
    array_p = zeros(1, m + 1);
    for k = 0:n
        array_p(k + 1) = (factorial(m) * pi_val^k) / (factorial(k) * factorial(m - k)) * P0;
    end
    for k = n+1:m
        array_p(k + 1) = (factorial(m) * pi_val^k) / (factorial(n) * n^(k - n) * factorial(m - k)) * P0;
    end
    disp('Вероятности состояний:');
    for k = 0:m
      disp(['p_', num2str(k), ' = ', num2str(array_p(k + 1))]);
    end
    disp(['Сумма вероятностей: ', num2str(sum(array_p))]);
    disp(' ');
end

function display_effect_characteristics(n, m, mu, lambda, array_p)
    Nqueue=0;
    for k=n:m
      Nqueue=Nqueue+(k-n)*array_p(k);
    endfor
    Nbusychan = sum(min((0:m), n) .* array_p);
    NqueueSMO = sum((0:m) .* array_p);
    Q = Nbusychan * mu;
    time_queue = Nqueue / (lambda * (1 - array_p(m+1)));
    Kpr1 = Nqueue / m;
    Kpr2 = Nbusychan/n;
    Kusing = 1 - NqueueSMO/m;
    disp('Характеристики СМО:');
    disp(['Среднее число заявок в очереди: ', num2str(Nqueue)]);
    disp(['Среднее число занятых каналов: ', num2str(Nbusychan)]);
    disp(['Среднее число заявок в СМО: ', num2str(NqueueSMO)]);
    disp(['Среднее время пребывания заявки в очереди: ', num2str(time_queue)]);
    disp(['Коэффициент простоя обслуживаемой заявки в очереди: ', num2str(Kpr1)]);
    disp(['Коэффициент простоя обслуживающих каналов: ', num2str(Kpr2)]);
    disp(['Коэффициент использования обслуживаемых объектов: ', num2str(Kusing)]);
end

[P0, array_p] = calculate_limited_probabilities(pi_val, m, n);
display_effect_characteristics(n, m, mu, lambda, array_p);

optimal_n=0;
K_optimal=0.9;
for n = 1:1000
    sum1 = sum((factorial(m) * pi_val.^(0:n)) ./ (factorial(0:n) .* factorial(m - (0:n))));
    sum2 = sum((factorial(m) * pi_val.^(n+1:m)) ./ (factorial(n) * n.^((n+1:m) - n) .* factorial(m - (n+1:m))));
    P0 = (sum1 + sum2)^-1;
    array_p = [(factorial(m) * pi_val.^(0:n)) ./ (factorial(0:n) .* factorial(m - (0:n))) * P0, ...
               (factorial(m) * pi_val.^(n+1:m)) ./ (factorial(n) * n.^((n+1:m) - n) .* factorial(m - (n+1:m))) * P0];

    N_queue = 0;
    for k = n:length(array_p)
        N_queue = N_queue + (k - n) * array_p(k);
    end
    K_idle = N_queue / m;
    if K_idle <= K_optimal
        optimal_n = n;
        break;
    end
end

m1=5*optimal_n;
disp(['Оптимальное число: ', num2str(optimal_n)]);
disp(' ');
[P1,array_p1]=calculate_limited_probabilities(lambda/mu,m1,optimal_n);
display_effect_characteristics(optimal_n,m1,mu,lambda,array_p1);

disp(' ');
disp(['Пусть каждый канал обрабатывает по ',num2str(m/n),' заявок']);
n=n*m/n;
[P2,array_p2]=calculate_limited_probabilities(lambda/mu,m,optimal_n);
display_effect_characteristics(n,m,mu,lambda,array_p2);
