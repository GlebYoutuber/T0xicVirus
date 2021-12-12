@echo off 
title NASM 
@set path=C:\Users\admin\AppData\Local\bin\NASM;%path%
@%comspec%
nasm -f bin mbroverwrite.asm -o mbr.bin
exit
