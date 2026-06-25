:- dynamic known_yes/1.
:- dynamic known_no/1.
:- dynamic known_any/1.
:- dynamic known_value/2.
:- dynamic known_brand/1.
:- dynamic cpu_is/1.

menu :-
    nl, write('1. Подбор процессора'), nl,
    write('2. Выход'), nl,
    write('Ваш выбор -> '),
    read_line_to_string(user_input, ChoiceStr),
    number_string(Ch, ChoiceStr),
    process_choice(Ch).

process_choice(1) :- clear_db, consultation, !, menu.
process_choice(2) :- !.
process_choice(_) :- write('Ошибка ввода.'), nl, menu.

consultation :-
    findall(Name, cpu_is(Name), CPUs),
    print_results(CPUs).

print_results([]) :-
    nl, write('Подходящих процессоров не найдено.'), nl.

print_results(CPUs) :-
    CPUs = [_|_],
    nl, write('Подходящие варианты'), nl,
    print_cpu_list(CPUs).

print_cpu_list([]).

print_cpu_list([Name | Rest]) :-
    nl, write('Модель: '), write(Name), nl,
    write('Характеристики:'), nl,
    clause(cpu_is(Name), Body),
    print_specs_from_body(Body),
    print_cpu_list(Rest).

cpu_is('AMD Ryzen 9 7950X3D') :-
    is_brand(amd), check_price(72000), check_cores(16), check_threads(32), check_freq(5.7),
    pos(has, heavy_mt), pos(has, v_cache), pos(has, igpu), pos(has, no_e_cores).

cpu_is('Intel Core i9-14900K') :-
    is_brand(intel), check_price(65000), check_cores(24), check_threads(32), check_freq(6.0),
    pos(has, water_cooling), pos(has, igpu), pos(has, manual_oc), pos(has, e_cores).

cpu_is('Intel Core i7-14700K') :-
    is_brand(intel), check_price(46000), check_cores(20), check_threads(28), check_freq(5.6),
    pos(has, water_cooling), pos(has, igpu), pos(has, manual_oc), pos(has, e_cores).

cpu_is('AMD Ryzen 7 7800X3D') :-
    is_brand(amd), check_price(42000), check_cores(8), check_threads(16), check_freq(5.0),
    pos(has, v_cache), pos(has, igpu), pos(has, no_e_cores).

cpu_is('Intel Core i5-13600K') :-
    is_brand(intel), check_price(33000), check_cores(14), check_threads(20), check_freq(5.1),
    pos(has, manual_oc), pos(has, igpu), pos(has, e_cores).

cpu_is('AMD Ryzen 7 5700X3D') :-
    is_brand(amd), check_price(25000), check_cores(8), check_threads(16), check_freq(4.1),
    pos(has, v_cache), pos(has, old_platform), pos(has, no_igpu), pos(has, no_e_cores).

cpu_is('AMD Ryzen 5 7600X') :-
    is_brand(amd), check_price(22000), check_cores(6), check_threads(12), check_freq(5.3),
    pos(has, igpu), pos(has, manual_oc), pos(has, no_e_cores).

cpu_is('AMD Ryzen 5 8600G') :-
    is_brand(amd), check_price(21000), check_cores(6), check_threads(12), check_freq(5.0),
    pos(has, igpu), pos(has, npu), pos(has, no_e_cores).

cpu_is('Intel Core i5-13400F') :-
    is_brand(intel), check_price(19000), check_cores(10), check_threads(16), check_freq(4.6),
    pos(has, no_igpu), pos(has, e_cores).

cpu_is('Intel Core i5-12400F') :-
    is_brand(intel), check_price(13000), check_cores(6), check_threads(12), check_freq(4.4),
    pos(has, no_e_cores), pos(has, no_igpu).

cpu_is('AMD Ryzen 5 5600') :-
    is_brand(amd), check_price(11000), check_cores(6), check_threads(12), check_freq(4.4),
    pos(has, old_platform), pos(has, no_igpu), pos(has, no_e_cores).

cpu_is('Intel Core i3-12100F') :-
    is_brand(intel), check_price(8000), check_cores(4), check_threads(8), check_freq(4.3),
    pos(has, no_e_cores), pos(has, no_igpu).

cpu_is('AMD Athlon 3000G') :-
    is_brand(amd), check_price(4500), check_cores(2), check_threads(4), check_freq(3.5),
    pos(has, igpu), pos(has, old_platform), pos(has, no_e_cores).

print_specs_from_body(true) :- !.
print_specs_from_body((A, B)) :-
    !,
    print_specs_from_body(A),
    print_specs_from_body(B).
print_specs_from_body(is_brand(B)) :-
    !, upcase_atom(B, BUpper), format('  - Производитель: ~w~n', [BUpper]).
print_specs_from_body(check_price(P)) :-
    !, format('  - Цена: ~w руб.~n', [P]).
print_specs_from_body(check_cores(C)) :-
    !, format('  - Количество ядер: ~w~n', [C]).
print_specs_from_body(check_threads(T)) :-
    !, format('  - Количество потоков: ~w~n', [T]).
print_specs_from_body(check_freq(F)) :-
    !, format('  - Тактовая частота: ~w ГГц~n', [F]).
print_specs_from_body(pos(_, F)) :-
    feature_desc(F, Desc), !, format('  - Особенность: ~w~n', [Desc]).
print_specs_from_body(_) :- !.

feature_desc(heavy_mt, 'Упор на тяжелые многопоточные вычисления').
feature_desc(v_cache, 'Наличие технологии 3D V-Cache').
feature_desc(igpu, 'Наличие встроенной графики').
feature_desc(no_igpu, 'Отсутствие встроенной графики').
feature_desc(water_cooling, 'Использование мощных систем охлаждения').
feature_desc(manual_oc, 'Поддержка ручного разгона').
feature_desc(old_platform, 'Использование платформы прошлого поколения').
feature_desc(npu, 'Наличие нейронного сопроцессора (NPU)').
feature_desc(no_e_cores, 'Отсутствие энергоэффективных ядер').
feature_desc(e_cores, 'Наличие энергоэффективных ядер').

opposite_feature(igpu, no_igpu).
opposite_feature(no_igpu, igpu).
opposite_feature(e_cores, no_e_cores).
opposite_feature(no_e_cores, e_cores).

is_brand(CpuBrand) :-
    get_brand(UserBrand),
    (UserBrand == any ; UserBrand == CpuBrand).

pos(has, none) :- !.
pos(has, Feature) :- known_yes(Feature), !.
pos(has, Feature) :- known_any(Feature), !.
pos(has, Feature) :- known_no(Feature), !, fail.
pos(has, Feature) :- ask_feature(Feature).

ask_feature(Feature) :-
    feature_desc(Feature, Desc),
    format('~w? (д/н/неважно): ', [Desc]),
    flush_output,
    read_line_to_string(user_input, ReplyStr),
    string_lower(ReplyStr, CleanStr),
    normalize_space(string(Reply), CleanStr),
    process_feature_reply(Reply, Feature).

process_feature_reply(Reply, Feature) :-
    (Reply == "д" ; Reply == "да"), !,
    assertz(known_yes(Feature)),
    (opposite_feature(Feature, Opp) -> assertz(known_no(Opp)) ; true).
process_feature_reply(Reply, Feature) :-
    (Reply == "н" ; Reply == "нет"), !,
    assertz(known_no(Feature)),
    (opposite_feature(Feature, Opp) -> assertz(known_yes(Opp)) ; true),
    fail.
process_feature_reply(Reply, Feature) :-
    (Reply == "неважно" ; Reply == "any"), !,
    assertz(known_any(Feature)),
    (opposite_feature(Feature, Opp) -> assertz(known_any(Opp)) ; true).
process_feature_reply(_, Feature) :-
    write('Ошибка: введите "д", "н" или "неважно".'), nl,
    ask_feature(Feature).

check_price(CpuPrice) :-
    get_budget(UserBudget),
    UserBudget >= CpuPrice.

check_cores(CpuCores) :-
    get_range('Кол-во ядер', cores, Min, Max),
    CpuCores >= Min, CpuCores =< Max.

check_threads(CpuThreads) :-
    get_range('Кол-во потоков', threads, Min, Max),
    CpuThreads >= Min, CpuThreads =< Max.

check_freq(CpuFreq) :-
    get_range('Макс. частота в ГГц', freq, Min, Max),
    CpuFreq >= Min, CpuFreq =< Max.

get_brand(Brand) :- known_brand(Brand), !.
get_brand(Brand) :-
    write('Производитель? (amd/intel/неважно): '),
    flush_output,
    read_line_to_string(user_input, ReplyStr),
    string_lower(ReplyStr, CleanStr),
    normalize_space(string(Reply), CleanStr),
    (   Reply == "amd" -> assertz(known_brand(amd)), Brand = amd
    ;   Reply == "intel" -> assertz(known_brand(intel)), Brand = intel
    ;   (Reply == "неважно" ; Reply == "any") -> assertz(known_brand(any)), Brand = any
    ;   write('Ошибка! Введите "amd", "intel" или "неважно".'), nl,
        get_brand(Brand)
    ).

get_budget(Value) :- known_value(budget, Value), !.
get_budget(Value) :-
    write('Бюджет в рублях? (число/неважно): '),
    flush_output,
    read_line_to_string(user_input, ReplyStr),
    string_lower(ReplyStr, CleanStr),
    normalize_space(string(Reply), CleanStr),
    (   (Reply == "неважно" ; Reply == "any") ->
        Value = 9999999,
        assertz(known_value(budget, Value))
    ;   number_string(Num, Reply) ->
        Value = Num,
        assertz(known_value(budget, Value))
    ;   write('Ошибка: введите число или "неважно".'), nl,
        get_budget(Value)
    ).

get_range(_, Property, Min, Max) :- known_value(Property, range(Min, Max)), !.
get_range(Prompt, Property, Min, Max) :-
    format('~w? (число/диапазон/неважно): ', [Prompt]),
    flush_output,
    read_line_to_string(user_input, ReplyStr),
    string_lower(ReplyStr, CleanStr),
    normalize_space(string(Reply), CleanStr),
    (   (Reply == "неважно" ; Reply == "any") ->
        Min = 0.0, Max = 9999.0,
        assertz(known_value(Property, range(Min, Max)))
    ;   parse_range(Reply, ParsedMin, ParsedMax) ->
        Min = ParsedMin, Max = ParsedMax,
        assertz(known_value(Property, range(Min, Max)))
    ;   write('Ошибка: неверный формат. Попробуйте еще раз.'), nl,
        get_range(Prompt, Property, Min, Max)
    ).

parse_range(Str, Min, Max) :-
    split_string(Str, "-", " ", [MinStr, MaxStr]),
    MinStr \= "", MaxStr \= "",
    number_string(Min, MinStr),
    number_string(Max, MaxStr),
    Min =< Max, !.

parse_range(Str, Min, Max) :-
    sub_string(Str, 0, _, 1, MinStr),
    sub_string(Str, _, _, 0, "+"),
    number_string(Min, MinStr),
    Max = 9999.0, !.

parse_range(Str, Min, Max) :-
    number_string(Val, Str),
    Min = Val, Max = Val, !.

clear_db :-
    retractall(known_yes(_)),
    retractall(known_no(_)),
    retractall(known_any(_)),
    retractall(known_value(_, _)),
    retractall(known_brand(_)).
