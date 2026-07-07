reverse_and_len_list(L1, L2, Len) :-
    createReverseList(L1, [], L2, Len).

createReverseList([Head|L1], Acc, Rev, Len) :-
    createReverseList(L1, [Head|Acc], Rev, Len1),
    Len is Len1 + 1.

createReverseList([], Acc, Acc, 0).

selectElems(X, [X|T], T).
selectElems(X, [H|T], [H|R]) :-
    selectElems(X, T, R).

permutation(L, LenL, [X|P], StartCount, FinalCount) :-
    selectElems(X, L, R),
    NewCount is StartCount * LenL,
    LenR is LenL - 1,
    permutation(R, LenR, P, NewCount, FinalCount).

permutation([], _, [], Count, Count).

write_permutations(List, Len, RevL, StartCount, FinalCount) :-
    permutation(List, Len, P, StartCount, FinalCount),
    writeln(P),
    P=RevL.

program:-
    L=[1,2,3,4],
    reverse_and_len_list(L, RevL,Len),
    writeln("Перестановки: "),
    write_permutations(L, Len, RevL, 1, Count),
    write("Количество перестановок: "),
    write(Count), !.
