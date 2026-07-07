graph(1,2,5).
graph(1,4,6).
graph(1,5,4).

graph(2,3,7).

path(X,Y,W) :- graph(X,Y,W).
path(X,Y,W) :- graph(Y,X,W).

path_simple(Start, End, TotalWeight) :-
    graph(Start, Next, W1),
    Start \= End,
    path_simple(Next, End, W2),
    TotalWeight is W1 + W2.

path_simple(Start, End, Weight) :-
    graph(Start, End, Weight).


program :-
    path_simple(2,4,Sum),
        write(Sum).
