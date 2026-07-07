
% Базовый случай для завершения создания списка
createList([]).

% Предикат для разворота N элементов списка
reverseNElems([Head|Tail], N, List, Rev):-
    N\==0,
    N1 is N-1,
    reverseNElems(Tail, N1, [Head|List], Rev).

% Завершение разворота элементов
reverseNElems(_, _, List, List).

% Предикат для создания палиндрома из списка
createPalindrome(List, N ,List2):-
    reverseNElems(List, N, [], List1),
    addList(List, List1, List2),
    palindrome(List2).

% Рекурсивный вариант создания палиндрома с увеличением N
createPalindrome(List, N, Res):-
    N1 is N + 1,
    createPalindrome(List, N1, Res).

% Объединение списков
addList([], L, L).
addList([E|List1], List2, [E|List3]):-
    addList(List1, List2, List3).

% Предикат для разворота списка
reverse_list([],[]).
reverse_list(L1,L2):-
    createReverseList(L1,[],L2).

% Вспомогательный предикат для разворота списка
createReverseList([Head|L1],L2,L3):-
    createReverseList(L1,[Head|L2],L3).
createReverseList([],L,L).

% Предикат для проверки списка на палиндром
palindrome(List):-
    reverse_list(List,ResList),
    List=ResList.

% Основной предикат программы
program:-
    List=[1,2,3,4],
    write("Введенный список: "), writeln(List),
    createPalindrome(List,0,ResList),
    write("Итоговый список: "), write(ResList).
