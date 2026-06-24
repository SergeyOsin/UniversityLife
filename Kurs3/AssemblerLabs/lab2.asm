; Вариант - 9. Даны числа a, b, c. Проверить выполняется ли неравенство a<b<c.
.586
.model flat, stdcall
option casemap:none

include \masm32\include\windows.inc
include \masm32\include\kernel32.inc
include \masm32\include\masm32.inc
includelib \masm32\lib\kernel32.lib
includelib \masm32\lib\masm32.lib

.data
    prompt_a    db "a=", 0
    prompt_b    db "b=", 0
    prompt_c    db "c=", 0
	true_msg    db "a < b < c is true", 0
    false_msg   db "a<b<c is false", 0
    press_enter db 13, 10, "Press Enter to Exit", 0
    newline     db 13, 10, 0

    a           dw ?
    b           dw ?
    c1          dw ?

    buffer      db 128 dup(0)

.code
start:
    invoke StdOut, ADDR prompt_a
    invoke StdIn, ADDR buffer, 128
    invoke atodw, ADDR buffer
    mov [a], ax

    invoke StdOut, ADDR prompt_b
    invoke StdIn, ADDR buffer, 128
    invoke atodw, ADDR buffer
    mov [b], ax

    invoke StdOut, ADDR prompt_c
    invoke StdIn, ADDR buffer, 128
    invoke atodw, ADDR buffer
    mov [c1], ax

    mov ax, [a]
    cmp ax, [b]        
    jge false_result     

    mov ax, [b]
    cmp ax, [c1]         
    jge false_result    

true_result:
    invoke StdOut, ADDR true_msg
    jmp wait_exit

false_result:
    invoke StdOut, ADDR false_msg

wait_exit:
    invoke StdOut, ADDR newline
    invoke StdOut, ADDR press_enter
    invoke StdIn, ADDR buffer, 128 

    invoke ExitProcess, 0
end start