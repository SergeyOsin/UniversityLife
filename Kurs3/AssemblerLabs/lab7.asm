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
countDigits PROC
    push ebp
    mov ebp, esp
    
    mov esi, [ebp+8]    
    mov ecx, 0 
    
countLoop:	
    mov al, [esi]
    test al, al
    jz finish
    
    cmp al, '0'
    jl nextChar
    cmp al, '9'
    jg nextChar
    
    inc ecx
    
nextChar:
    inc esi
    jmp countLoop
    
finish:
    mov [ebp+12], ecx    
    
    pop ebp
    ret         
countDigits ENDP

main PROC
    invoke StdOut, ADDR msg_input
    invoke StdIn, ADDR buffer, 256
    
    push OFFSET buffer
    call countDigits
    add esp, 4          
    
    mov eax, [esp]
    mov count, eax
    
    invoke dwtoa, count, ADDR buffer
    invoke StdOut, ADDR msg_digit
    invoke StdOut, ADDR buffer
	
    invoke StdOut, ADDR press_enter
    invoke StdIn, ADDR buffer, 128
    invoke ExitProcess, 0
main ENDP

END main
