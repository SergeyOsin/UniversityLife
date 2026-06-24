; Определить количество цифр в строке.
.586
.model flat, stdcall
option casemap:none

include \masm32\include\windows.inc
include \masm32\include\kernel32.inc
include \masm32\include\masm32.inc
includelib \masm32\lib\kernel32.lib
includelib \masm32\lib\masm32.lib

.data
    buffer      db 256 dup(0)            
    press_enter db 13, 10, "Press Enter to exit...", 0
    count       dd 0                     
    msg_input   db "Write string: ", 0
    msg_digit   db "Count numbers in string: ", 0

.code
main:
    invoke StdOut, ADDR msg_input
    invoke StdIn, ADDR buffer, 256
    mov ecx, 0            
    mov esi, OFFSET buffer 

count_digits:
    mov al, [esi]
    test al, al
    je finish

    cmp al, '0'
    jl not_digit
    cmp al, '9'
    jg not_digit
    inc ecx

not_digit:
    inc esi
    jmp count_digits

finish:
    mov eax, ecx
    invoke dwtoa, eax, ADDR buffer
    invoke StdOut, ADDR msg_digit
    invoke StdOut, ADDR buffer

    invoke StdOut, ADDR press_enter
    invoke StdIn, ADDR buffer, 128

    invoke ExitProcess, 0
end main