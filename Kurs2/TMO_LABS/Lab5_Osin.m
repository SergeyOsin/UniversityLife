clc;
clear all;
format short g;

lambda = 2.37
service_time = 0.27
time_step = 1
end_time = 20
channels_case1 = 3
channels_case2 = 4
mu = 1/service_time


initial_state1 = [1, zeros(1, channels_case1)];
initial_state2 = [1, zeros(1, channels_case2)];
time_points = 0:time_step:end_time;



function dpdt = erlang_ode(t, p, lambda, n, mu)
    dpdt = zeros(1,n+1);
    dpdt(1) = -lambda * p(1) + mu * p(2);

    for i = 2:n
        dpdt(i) = lambda * p(i-1) - (lambda + (i-1)*mu) * p(i) + i*mu * p(i+1);
    end
    dpdt(n+1) = lambda * p(n) - n*mu * p(n+1);
end

function [t, p] = solve_erlang_system(t_span, p0, lambda, n, mu)
    [t, p] = ode45(@(t,p) erlang_ode(t, p, lambda, n, mu), t_span, p0');
end

function plot_state_probabilities(p, t, n_channels)
    figure;
    hold on;
    for i = 1:size(p, 2)
        plot(t, p(:, i), 'LineWidth', 1.5, ...
            'DisplayName', sprintf('%d заявок', i-1));
    end
    xlabel('Время');
    ylabel('Вероятность');
    title(sprintf('Вероятности состояний (%d каналов)', n_channels));
    legend('Location', 'best');
    grid on;
    hold off;
end


function steady_probs = calculate_steady_state(lambda, mu, n)
    rho = lambda / mu;
    sum_terms = 0;
    for k = 0:n
        sum_terms = sum_terms + (rho^k / factorial(k));
    end

    p0 = 1 / sum_terms;
    steady_probs = zeros(1, n+1);
    steady_probs(1) = p0;

    for k = 1:n
        steady_probs(k+1) = (rho^k / factorial(k)) * p0;
    end
end


function print_system_stats(n, steady_probs, lambda, mu)
    fprintf('\n=== Система с %d каналами ===\n', n);
    p_reject = steady_probs(end);
    p_serve = 1 - p_reject;
    %P отклонения = p(n)
    fprintf('Вероятность отказа: %.4f\n', p_reject);
    fprintf('Вероятность обслуживания: %.4f\n', p_serve);
    fprintf('Абсолютная пропускная способность: %.4f\n', lambda * p_serve);
    fprintf('Среднее число занятых каналов: %.4f\n', (lambda/mu) * p_serve);
end


function n = find_optimal_channels(lambda, mu, target_serve_prob)
    n = 1;
    probs = calculate_steady_state(lambda, mu, n);
    serve_prob = 1 - probs(end);
    while serve_prob < target_serve_prob
        n = n + 1;
        probs = calculate_steady_state(lambda, mu, n);
        serve_prob = 1 - probs(end);
    end
end

[t1, p1] = solve_erlang_system(time_points, initial_state1, lambda, channels_case1, mu);
plot_state_probabilities(p1, t1, channels_case1);
steady_probs1 = calculate_steady_state(lambda, mu, channels_case1);

[t2, p2] = solve_erlang_system(time_points, initial_state2, lambda, channels_case2, mu)
plot_state_probabilities(p2, t2, channels_case2);
steady_probs2 = calculate_steady_state(lambda, mu, channels_case2);


print_system_stats(channels_case1, steady_probs1, lambda, mu);
print_system_stats(channels_case2, steady_probs2, lambda, mu);

optimal_channels = find_optimal_channels(lambda, mu, 0.95);
fprintf('\nМинимальное число каналов для 95%% обслуживания: %d\n', optimal_channels);




