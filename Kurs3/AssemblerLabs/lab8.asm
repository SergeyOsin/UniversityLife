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


INPUT_NUMBER MACRO varName, prompt
    invoke StdOut, ADDR prompt
    invoke StdIn, ADDR buffer, 128
    invoke atodw, ADDR buffer
    mov varName, ax
ENDM

OUTPUT_NUMBER MACRO varName, prompt
    invoke StdOut, ADDR prompt
    invoke dwtoa, varName, ADDR buffer
    invoke StdOut, ADDR buffer
ENDM

.data
    buffer      db 256 dup(0)            
    press_enter db 13, 10, "Press Enter to exit...",0
    input_X     db 13,10, "X= ",0
    input_Y     db 13,10, "Y= ",0
    outputZ     db 13,10, "Z= ",0
    X          WORD ?
    Y          WORD ?
    Z          DWORD ?

.code
main:
    INPUT_NUMBER X, input_X
    
    INPUT_NUMBER Y, input_Y
   
    mov ax, X
    mov bx, Y
    shl bx, 3       
    
    movzx eax, ax
    movzx ebx, bx
    
    add eax, ebx    
    mov Z, eax      
    
    OUTPUT_NUMBER Z, outputZ
    
    invoke StdOut, ADDR press_enter
    invoke StdIn, ADDR buffer, 255
    invoke StdOut, ADDR buffer
    
    invoke ExitProcess, 0
end main
