; Создать массив, содержащий 15 первых чисел Фибоначчи
.586
.model flat, stdcall
option casemap:none

include \masm32\include\windows.inc
include \masm32\include\kernel32.inc
include \masm32\include\masm32.inc
includelib \masm32\lib\kernel32.lib
includelib \masm32\lib\masm32.lib

.data
    Array dd 15 dup(?)           
    newline db 13, 10, 0       
    press_enter db 13, 10, "Press Enter to exit...", 0
    buffer db 128 dup(0)

.code
start:

    mov dword ptr [Array], 0
    mov dword ptr [Array + 4], 1

    mov ecx, 3                   
    mov ebx, 0                   
    mov edx, 1                    

calc_fib:
    add ebx, edx                 
    mov dword ptr [Array + ecx*4], ebx
    mov eax, edx
    mov edx, ebx
	inc ecx
    cmp ecx, 15
	jl calc_fib
	
    mov ecx, 0                 
print_all:
    lea esi, [Array + ecx*4]
    invoke StdOut, esi

    invoke StdOut, ADDR newline
    inc ecx
	jl print_all
	invoke StdOut, ADDR press_enter

    invoke ExitProcess, 0
end start