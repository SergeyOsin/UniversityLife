%Осин Сергей. 23ВП2. Вариант - 21
clc;

N=21
c=0.0001*N

matrix=[0.963+c,0.025+c,0.008-c,0.003-c,0.001;
0.54+c,0.25+c,0.07-c,0.05-c,0.1;
0.032,0.06-c,0.69-c,0.083+c,0.135+c;
0.035-c,0.014,0.021-c,0.28+c,0.65+c;
0.068-c,0.012,0.045-c,0.105+c,0.77+c]



function [P1, matrix6, P6, matrix12, P12, matrix24, P24,matrix60,P60] = calculatefn(P0, matrix)
    % Вычисление P(1)
    P1 = P0 * matrix;

    % Вычисление matrix^6 и соответствующих вероятностей
    matrix6 = matrix^6;
    P6 = P0 * matrix6;
    % Вычисление matrix^12 и соответствующих вероятностей
    matrix12 = matrix^12;
    P12 = P0 * matrix12;

    % Вычисление matrix^24 и соответствующих вероятностей
    matrix24 = matrix^24;
    P24 = P0 * matrix24;

    % Вычисление matrix^60 и соответстующих вероятностей
    matrix60=matrix^60;
    P60=P0*matrix60;
endfunction

function probs = LimitProbs(_pi)
  probs = [_pi' - eye(size(_pi)); ones(1, length(_pi))] \ [zeros(length(_pi), 1); 1];
endfunction

%Пункт А.
disp('Пункт А');
P0=[1,0,0,0,0]
[P1, matrix6, P6, matrix12, P12, matrix24, P24,matrix60,P60] = calculatefn(P0, matrix)
%Пункт Б.
disp('Пункт Б');
P0_1=zeros(1,5);
random=randi(5);
P0_1(random)=1
[P1, matrix6, P6, matrix12, P12, matrix24, P24,matrix60,P60] = calculatefn(P0_1, matrix)

limit_probs = LimitProbs(matrix)


