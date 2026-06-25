program :-
    write("Введите логическое выражение, используя следующие операторы:\n"),
    write("v (ИЛИ), & (И), - (НЕ), -> (ИМПЛИКАЦИЯ)\n"),
    write("Пример: A & (B v C)\n"),
    read_line_to_codes(user_input, Codes),
    string_codes(Input, Codes),
    process_input(Input).

process_input(Input) :-
    tokenize(Input, Tokens), !,
    process_tokens(Tokens).

process_tokens(Tokens) :-
    validate_tokens(Tokens), !,
    process_valid_tokens(Tokens).
process_tokens(_) :-
    writeln('Ошибка со скобками').

process_valid_tokens(Tokens) :-
    parse_expression(Tokens, Expr), !,
    process_parsed_expression(Expr).
process_valid_tokens(_) :-
    writeln('Синтаксическая ошибка в выражении').

process_parsed_expression(Expr) :-
    convert_to_dnf(Expr, _DNF).

tokenize(Input, Tokens) :-
    string_chars(Input, Chars),
    tokenize_chars(Chars, Tokens).

tokenize_chars([], []).

tokenize_chars([' '|T], Tokens) :- tokenize_chars(T, Tokens).

tokenize_chars(['('|T], ['('|Tokens]) :- tokenize_chars(T, Tokens).
tokenize_chars([')'|T], [')'|Tokens]) :- tokenize_chars(T, Tokens).
tokenize_chars(['&'|T], ['&'|Tokens]) :- tokenize_chars(T, Tokens).
tokenize_chars(['v'|T], ['v'|Tokens]) :- tokenize_chars(T, Tokens).

tokenize_chars(['-','>'|T], ['->'|Tokens]) :-
    tokenize_chars(T, Tokens).

tokenize_chars(['-'|T], ['-'|Tokens]) :-
    tokenize_chars(T, Tokens).

tokenize_chars([Ch|Chars], [Token|Tokens]) :-
    char_type(Ch, alpha),
    take_while(alnum, [Ch|Chars], TokenChars, Rest),
    atom_chars(Token, TokenChars),
    tokenize_chars(Rest, Tokens).

take_while(_, [], [], []).
take_while(alnum, [Ch|Rest], [Ch|R], Tail) :-
    char_type(Ch, alnum), !,
    take_while(alnum, Rest, R, Tail).
take_while(alnum, ['_'|Rest], ['_'|R], Tail) :- !,
    take_while(alnum, Rest, R, Tail).
take_while(_, List, [], List).


validate_tokens(Tokens) :-
    check_balanced_parentheses(Tokens, 0),
    check_valid_tokens(Tokens).

check_balanced_parentheses([], 0).
check_balanced_parentheses(['('|T], N) :-
    N1 is N + 1,
    check_balanced_parentheses(T, N1).
check_balanced_parentheses([')'|T], N) :-
    N > 0, N1 is N - 1,
    check_balanced_parentheses(T, N1).
check_balanced_parentheses([H|T], N) :-
    H \= '(',
    H \= ')',
    check_balanced_parentheses(T, N).

check_valid_tokens([]).
check_valid_tokens([Token|T]) :-
    valid_token(Token),
    check_valid_tokens(T).

valid_token('(').
valid_token(')').
valid_token('&').
valid_token('v').
valid_token('-').
valid_token('->').
valid_token(Token) :- atom(Token), \+ keyword(Token).

keyword('&').
keyword('v').
keyword('-').
keyword('->').


parse_expression(Tokens, Expr) :-
    expression(Expr, Tokens, []).


expression(E, Tin, Tout) :-
    impl_expr(E, Tin, Tout).


impl_expr('->'(L, R), Tin, Tout) :-
    or_expr(L, Tin, T1),
    T1 = ['->'|T2],
    impl_expr(R, T2, Tout).

impl_expr(E, Tin, Tout) :-
    or_expr(E, Tin, Tout).

or_expr(E, Tin, Tout) :-
    and_expr(L, Tin, T1),
    or_tail(L, E, T1, Tout).

or_tail(L, E, ['v'|T1], Tout) :-
    and_expr(R, T1, T2),
    or_tail(v(L, R), E, T2, Tout).

or_tail(E, E, Tin, Tin).


and_expr(E, Tin, Tout) :-
    factor(L, Tin, T1),
    and_tail(L, E, T1, Tout).

and_tail(L, E, ['&'|T1], Tout) :-
    factor(R, T1, T2),
    and_tail(&(L,R), E, T2, Tout).

and_tail(E, E, Tin, Tin).


factor(-(E), ['-'|Tin], Tout) :-
    factor(E, Tin, Tout).

factor(E, ['('|Tin], Tout) :-
    expression(E, Tin, T1),
    T1 = [')'|Tout].

factor(V, [V|T], T) :-
    atom(V),
    \+ keyword(V).


convert_to_dnf(Expr, DNF) :-
    remove_implications(Expr, E1),
    write('После удаления импликаций: '),
    write_expression(E1), nl,
    apply_demorgan(E1, E2),
    write('После применения законов де Моргана: '),
    write_expression(E2), nl,
    normalize_or(E2, E3),
    normalize_and(E3, E4),
    write('После дистрибутивности: '),
    write_expression(E4), nl,
    normalize_or(E4, DNF),
    write('\nДизъюнктивная нормальная форма (ДНФ): '),
    write_expression(DNF), nl.


remove_implications('->'(A,B), v(-(A1), B1)) :- !,
    remove_implications(A, A1),
    remove_implications(B, B1).

remove_implications(-(A), -(R)) :- !,
    remove_implications(A, R).

remove_implications(&(A,B), &(A1,B1)) :- !,
    remove_implications(A, A1),
    remove_implications(B, B1).

remove_implications(v(A,B), v(A1,B1)) :- !,
    remove_implications(A, A1),
    remove_implications(B, B1).

remove_implications(A, A).


apply_demorgan(-(&(A,B)), v(NA, NB)) :- !,
    apply_demorgan(-(A), NA),
    apply_demorgan(-(B), NB).

apply_demorgan(-(v(A,B)), &(NA, NB)) :- !,
    apply_demorgan(-(A), NA),
    apply_demorgan(-(B), NB).

apply_demorgan(--(A), R) :- !,
    apply_demorgan(A, R).

apply_demorgan(&(A,B), &(A1,B1)) :- !,
    apply_demorgan(A, A1),
    apply_demorgan(B, B1).

apply_demorgan(v(A,B), v(A1,B1)) :- !,
    apply_demorgan(A, A1),
    apply_demorgan(B, B1).

apply_demorgan(-(A), -(R)) :- !,
    apply_demorgan(A, R).

apply_demorgan(A, A).


normalize_or(Expr, Expr) :-
    flat_or(Expr, Temp),
    Temp == Expr, !.

normalize_or(Expr, Result) :-
    flat_or(Expr, Temp),
    normalize_or(Temp, Result).

flat_or(v(A,v(B,C)), v(A1,R)) :- !,
    flat_or(A,A1),
    flat_or(v(B,C),R).

flat_or(v(v(A,B),C), v(A1,R)) :- !,
    flat_or(A,A1),
    flat_or(v(B,C),R).

flat_or(&(A,B), &(A1,B1)) :- !,
    flat_or(A,A1),
    flat_or(B,B1).

flat_or(v(A,B), v(A1,B1)) :- !,
    flat_or(A,A1),
    flat_or(B,B1).

flat_or(-(A), -(A1)) :- !,
    flat_or(A,A1).

flat_or(A, A).


normalize_and(Expr, Result) :-
    dist_and(Expr, Temp),
    Temp == Expr,
    Result = Temp.

normalize_and(Expr, Result) :-
    dist_and(Expr, Temp),
    normalize_and(Temp, Result).

dist_and(&(A, v(B,C)), v(&(A1,B1), &(A1,C1))) :- !,
    dist_and(A,A1), dist_and(B,B1), dist_and(C,C1).

dist_and(&(v(A,B), C), v(&(A1,C1), &(B1,C1))) :- !,
    dist_and(A,A1), dist_and(B,B1), dist_and(C,C1).

dist_and(&(A,B), &(A1,B1)) :- !,
    dist_and(A,A1), dist_and(B,B1).

dist_and(v(A,B), v(A1,B1)) :- !,
    dist_and(A,A1), dist_and(B,B1).

dist_and(-(A), -(A1)) :- !,
    dist_and(A,A1).

dist_and(A, A).


write_expression(-(E)) :-
    write('-'), write_expression(E).

write_expression(&(A,B)) :-
    write('('), write_expression(A),
    write(' & '),
    write_expression(B),
    write(')').

write_expression(v(A,B)) :-
    write('('), write_expression(A),
    write(' v '),
    write_expression(B),
    write(')').

write_expression('->'(A,B)) :-
    write('('), write_expression(A),
    write(' -> '),
    write_expression(B),
    write(')').

write_expression(Atom) :-
    atom(Atom),
    write(Atom).
