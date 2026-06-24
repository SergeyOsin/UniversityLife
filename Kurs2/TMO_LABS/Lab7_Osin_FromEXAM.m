%Осин С.М. 23ВП2. Лабораторная работа №7. Вариант-21.
clc;
format short g;

function main()
    disp('Входные параметры:');
    lambda = 3
    time_service = 1.2
    n = 4
    m = 3
    disp(' ');
    [P0, array_p] = calculate_probabilities(lambda, time_service, n, m);
    display_results(lambda, time_service, n, m, P0, array_p);
     pi = lambda *time_service;
     disp('Повышение количество каналов');
    n=5
    [P0,array_p]=calculate_probabilities(lambda,time_service,n,m);
    display_results(lambda,time_service,n,m,P0,array_p);
    disp('Повышение количества парковок');
    n=4
    m=5
    [P0,array_p]=calculate_probabilities(lambda,time_service,n,m);
    display_results(lambda,time_service,n,m,P0,array_p);
    disp('Понижение времени обслуживания');
    n=4
    m=3
    time_service=1
    [P0,array_p]=calculate_probabilities(lambda,time_service,n,m);
    display_results(lambda,time_service,n,m,P0,array_p);

end



function [P0, array_p] = calculate_probabilities(lambda, time_service, n, m)
    mu = 1/time_service
    pi = lambda/mu
    sum1 = sum(pi.^(0:n) ./ factorial(0:n));
    sum2 = sum((pi/n).^(1:m));
    P0 = (sum1 + (pi^n / factorial(n)) * sum2)^-1
    array_p = zeros(1, n+m);
    k = 1:n;
    array_p(k) = (pi.^k ./ factorial(k)) * P0;
    l = 1:m;
    array_p(n + l) = (pi^n / factorial(n)) * (pi/n).^l * P0;
end

function display_results(lambda, time_service, n, m, P0, array_p)
    mu = 1/time_service;
    pi = lambda/mu;
    disp('=== Предельные вероятности состояний ===');
    for i = 0:(n+m)
        if i == 0
            disp(['p0 = ', num2str(P0)]);
        else
            disp(['p', num2str(i), ' = ', num2str(array_p(i))]);
        end
    end
    disp(['Сумма вероятностей: ', num2str(sum(array_p) + P0)]);
    disp(' ');
    disp('=== Характеристики эффективности СМО ===');
    p_reject = array_p(n+m);
    disp(['Вероятность отказа: ', num2str(p_reject)]);
    disp(['Вероятность обслуживания: ', num2str(1 - p_reject)]);
    Q = lambda * (1 - p_reject);
    disp(['Абсолютная пропускная способность: ', num2str(Q)]);
    disp(['Относительная пропускная способность: ', num2str(1 - p_reject)]);
    Nqueue = pi^(n+1)/(n*factorial(n)) * ((1-(m+1)*(pi/n)^m + m*(pi/n)^(m+1))/(1-pi/n)^2) * P0;
    Nchan = pi * (1 - p_reject);
    Nsystem = Nqueue + Nchan;
    disp(['Среднее число заявок в очереди: ', num2str(Nqueue)]);
    disp(['Среднее число занятых каналов: ', num2str(Nchan)]);
    disp(['Среднее число заявок в СМО: ', num2str(Nsystem)]);
    disp(['Среднее время пребывания заявки в СМО: ', num2str(Nsystem/lambda)]);
    disp(['Среднее время пребывания заявки в очереди: ', num2str(Nqueue/lambda)]);
end

main();

