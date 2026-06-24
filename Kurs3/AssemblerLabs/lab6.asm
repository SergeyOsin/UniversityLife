; Даны X, Y — беззнаковые переменные (WORD), а Z — переменная типа
; DWORD. Реализовать операцию Z = X + 8*Y, не используя команды
; умножения.

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
    press_enter db 13, 10, "Press Enter to exit...",0
	input_X db 13,10, "X= ",0
	input_y db 13,10, "Y= ",0
	outputZ db 13,10, "Z= ",0
	X WORD ?
	Y WORD ?
	Z DWORD ?

.code
main:
	invoke StdOut, ADDR input_X
	invoke StdIn, ADDR buffer, 128
    invoke atodw, ADDR buffer
	mov X, ax
	
	invoke StdOut,ADDR input_y
	invoke StdIn, ADDR buffer, 128
	invoke atodw, ADDR buffer
	mov Y,ax 
	
    mov ax, X
    mov bx, Y
    shl bx, 3       

    movzx eax, ax      
    movzx ebx, bx   

    add eax, ebx    

    mov Z, eax      

	invoke StdOut, ADDR outputZ
    invoke dwtoa, Z, ADDR buffer
    invoke StdOut, ADDR buffer
	
    invoke StdOut, ADDR press_enter
    invoke StdIn, ADDR buffer, 255
    invoke StdOut, ADDR buffer

    invoke ExitProcess, 0
end main