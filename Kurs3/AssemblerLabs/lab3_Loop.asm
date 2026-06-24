; Задать первый член арифметической прогрессии и ее разность. Вывести 10 элементов этой прогрессии.
.586
.model flat, stdcall
option casemap:none

include \masm32\include\windows.inc
include \masm32\include\kernel32.inc
include \masm32\include\masm32.inc
includelib \masm32\lib\kernel32.lib
includelib \masm32\lib\masm32.lib

.data
    prompt_first db "First Member: ", 0
    prompt_diff  db "Dif:  ", 0
    press_enter  db 13, 10, "Press Enter to exit...", 0
	newline db 13,10,0
	     
    difference   dd ?     
    current      dd ?    
    counter      dd 10
    buffer       dw 128 dup(0)

.code
start:	
    invoke StdOut, ADDR prompt_first
    invoke StdIn, ADDR buffer, 128
    invoke atodw, ADDR buffer
    mov current, eax
	
    invoke StdOut, ADDR prompt_diff
    invoke StdIn, ADDR buffer, 128
    invoke atodw, ADDR buffer
    mov difference, eax
	mov ecx, counter
 
labwork:
	mov counter, ecx
	
	mov eax, current
    invoke dwtoa, eax, ADDR buffer
    invoke StdOut, ADDR buffer
    invoke StdOut, ADDR newline
	
    mov eax, current
    add eax, difference
    mov current, eax
	
	mov ecx, counter
	
    loop labwork

	invoke StdOut, ADDR newline
    invoke StdOut, ADDR press_enter
    invoke StdIn, ADDR buffer, 128

    invoke ExitProcess, 0
end start