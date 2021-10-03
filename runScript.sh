#!/bin/bash

./Microsoft.Net.Compilers.3.11.0/tools/csc.exe ./Server/Server/Program.cs 
mv Program.exe Server.exe

./Microsoft.Net.Compilers.3.11.0/tools/csc.exe ./Client/Client/Program.cs
mv Program.exe ClientOne.exe

./Microsoft.Net.Compilers.3.11.0/tools/csc.exe ./Client/Client/Program.cs
mv Program.exe ClientTwo.exe

start Server.exe
start ClientOne.exe
start ClientTwo.exe