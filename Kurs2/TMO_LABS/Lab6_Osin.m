%Осин С.М. 23ВП2. Вариант -21.
clc;

function main()
  lambda=6.33
  time_service=0.26
  time_waiting=0.24
  number=6
  mu=1/time_service
  v=1/time_waiting
  alpha=lambda/mu
  pi=v/mu
  SMO_with_limited_on_time(lambda,time_service,time_waiting,alpha,pi,mu,v,number);
  disp(' ');
  disp('Для чистой СМО');
  Clean_SMO(lambda,alpha,pi,number,mu,time_waiting);
  SMO_with_limited_on_time(lambda,time_service,time_waiting,alpha,pi,mu,v,2);
  disp('Для чистой СМО: ');
  Clean_SMO(lambda,alpha,pi,2,mu,time_waiting);
endfunction

function SMO_with_limited_on_time(lambda,time_service,time_waiting,alpha,pi,mu,v,n)
  disp(' ')
  disp('Для СМО с ограничением на время ожидания: ')
  max_l=70;
  k = 0:n;
  sum1 = sum(alpha.^k ./ factorial(k));
  l = 1:max_l;
  denominators = arrayfun(@(l) prod(n + (1:l)*pi), l);
  sum3 = sum(alpha.^l ./ denominators);
  P0 = (sum1 + (alpha^n/factorial(n)) * (1 + sum3))^-1
  array_p = zeros(1,10);
  for i=1:n
    array_p(i)=alpha^i/factorial(i)*P0;
  endfor
  l = 1:(10-n);
  prods = arrayfun(@(m) prod(n + (1:m)*pi), l);
  array_p(n+1:10) = (alpha.^(n+l) * P0 ./ factorial(n)) ./ prods;
  disp('Предельные вероятности:')
  disp(array_p);
  disp(['s10=',num2str(sum(array_p)+P0)])
  disp('Характеристики эффективности СМО:');
  Charact_Effect_SMO_with_limtime(alpha,pi,6,P0,lambda,mu,v);
endfunction

function Charact_Effect_SMO_with_limtime(alpha,pi,n,P0,lambda,mu,v)
    max_l=70;
    Potk=pi/alpha * alpha^n/factorial(n);
    sum1=0;
    l = 1:max_l;
    prods = arrayfun(@(l) prod(n + (1:l)*pi), l);
    sum1 = sum(l .* alpha.^l ./ prods);
    k = 0:n;
    denom1 = sum(alpha.^k ./ factorial(k));
    denom2 = alpha^n / factorial(n);
    sum2 = sum(alpha.^l ./ prods);
    Potk = (pi/alpha) * (alpha^n/factorial(n)) * (1 + sum1/(denom1 + denom2*sum2));
    disp(['Вероятность отказа:',num2str(Potk)]);
    disp(['Вероятность обслуживания: ',num2str(1-Potk)]);
    Nqueue = (alpha^n/factorial(n)) * P0 * sum(l .* alpha.^l ./ prods);
    Q=lambda-v*Nqueue;
    disp(['Среднее число заявок в очереди:',num2str(Nqueue)]);
    disp(['Абсолютная пропускная способность СМО: ',num2str(Q)]);
    disp(['Отосительна пропускная способность СМО: ',num2str(Q/lambda)]);
    disp(['Средняя число занятых каналов: ',num2str(Q/mu)]);
endfunction

function Clean_SMO(lambda,alpha,pi,n,mu,time_waiting)
  sum0=0;
  for k=0:n
    sum0=sum0+alpha^k/factorial(k);
  endfor
  sum0=sum0+alpha^(n+1)/(factorial(n)*(n-alpha));
  P0=sum0^-1
  P=zeros(1,n);
  for i=1:n
    P(i)=alpha^i/factorial(i)*P0;
  endfor
  l=10-n;
  for j=1:l
    P(n+j)=alpha^(n+l)/(factorial(n)*n^l) *P0;
  endfor
  disp(['Предельные вероятности:',num2str(P)]);
  disp(['s10=',num2str(sum(P)+P0)]);
  CharaForCleanSMO(lambda,alpha,pi,6,mu,P0,time_waiting);
endfunction

function optimal_chan=FindCount(lambda,pi,P0)
  max_channels=70;
  optimal_chan = 1;
    time_in_queue = Inf;
    while (time_in_queue>3/60 && optimal_chan <= max_channels)
        rho = pi;
        n = optimal_chan;
        numerator = rho^(n+1);
        denominator = n * factorial(n) * (1 - rho/n)^2;
        N_queue = (numerator / denominator) * P0;
        time_in_queue = N_queue / lambda;
        if time_in_queue > 3/60
            optimal_chan = optimal_chan + 1;
        end
    end
endfunction

function CharaForCleanSMO(lambda,alpha,pi,n,mu,P0,time_waiting)
  Q=lambda;
  Nqueue1=alpha^(n+1)/(n*factorial(n))*(1-alpha/n)^-2 * P0;
  disp(['Средняя число заявок: ', num2str(Nqueue1)]);
  Nbusychan=Q/mu;
  disp(['Среднее число занятых каналов: ', num2str(Nbusychan)]);
  NcountinSMO=Nbusychan+alpha;
  disp(['Среднее число заявок в СМО: ', num2str(NcountinSMO)]);
  disp(['Среднее время пребывания заявки в СМО: ', num2str(NcountinSMO/lambda)]);
  disp(['Среднее время пребывания заявки в очереди: ', num2str(Nqueue1/lambda)]);
  chan=FindCount(lambda,pi,P0);
  disp(['Оптимальное число каналов: ', num2str(chan)]);

endfunction

main();







