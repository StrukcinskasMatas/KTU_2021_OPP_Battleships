#!/bin/bash

# Prerequisites:
# 1: 'wget https://dist.nuget.org/win-x86-commandline/latest/nuget.exe -OutFile nuget.exe'
# 2: '.\nuget.exe install Microsoft.Net.Compilers'

./Microsoft.Net.Compilers.3.11.0/tools/csc.exe ./Server/Server/Program.cs 
mv Program.exe Server.exe

./Microsoft.Net.Compilers.3.11.0/tools/csc.exe ./Client/Client/Program.cs
mv Program.exe ClientOne.exe

./Microsoft.Net.Compilers.3.11.0/tools/csc.exe ./Client/Client/Program.cs
mv Program.exe ClientTwo.exe

start Server.exe
start ClientOne.exe
start ClientTwo.exe