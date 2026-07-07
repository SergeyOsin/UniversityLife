main:-
    L=[1,2,3],
    write(L),
    recursive(L).

recursive([]).
recursive([Head|Tail], Sum):-
    newSum is Sum + Head,
    recursive(Tail).

