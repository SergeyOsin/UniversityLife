; Вариант - 9. X=a+b*c-d/4
.586
.model flat, stdcall
option casemap:none

include \masm32\include\windows.inc
include \masm32\include\kernel32.inc
include \masm32\include\masm32.inc
includelib \masm32\lib\kernel32.lib
includelib \masm32\lib\masm32.lib

.data
    prompt_a    db "Введите a: ", 0
    prompt_b    db "Введите b: ", 0
    prompt_c    db "Введите c: ", 0
    prompt_d    db "Введите d: ", 0
    result_msg  db "X = ", 0
    press_enter db 13, 10, "Нажмите Enter для закрытия окна", 0
    newline     db 13, 10, 0
	
    a           dd ?
    b           dd ?
    c1          dd ?
    d           dd ?
    X           dd ?

    buffer      db 128 dup(0)

.code
start:
    invoke SetConsoleOutputCP, 1251 
    invoke SetConsoleCP, 1251
	
    invoke StdOut, ADDR prompt_a
    invoke StdIn, ADDR buffer, 128
    invoke atodw, ADDR buffer
    mov [a], eax

    invoke StdOut, ADDR prompt_b
    invoke StdIn, ADDR buffer, 128
    invoke atodw, ADDR buffer
    mov [b], eax

    invoke StdOut, ADDR prompt_c
    invoke StdIn, ADDR buffer, 128
    invoke atodw, ADDR buffer
    mov [c1], eax

    invoke StdOut, ADDR prompt_d
    invoke StdIn, ADDR buffer, 128
    invoke atodw, ADDR buffer
    mov [d], eax

	mov eax, [b]
	mov ebx, [c1]
	imul ebx        

	push eax         

	mov eax, [d]
	cdq
	mov ecx, 4
	idiv ecx         


	pop ebx         
	add ebx, [a]     
	sub ebx, eax     

	mov [X], ebx    


    invoke StdOut, ADDR result_msg
    invoke dwtoa, X, ADDR buffer  
    invoke StdOut, ADDR buffer
    invoke StdOut, ADDR newline

    invoke StdOut, ADDR press_enter
    invoke StdIn, ADDR buffer, 128  

    invoke ExitProcess, 0
end start