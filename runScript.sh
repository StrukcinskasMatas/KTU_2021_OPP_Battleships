#!/bin/bash
./Microsoft.Net.Compilers.3.11.0/tools/csc.exe ./Server/Server/Program.cs 
mv Program.exe Server.exe
./Microsoft.Net.Compilers.3.11.0/tools/csc.exe ./Client/Client/Program.cs
mv Program.exe Client.exe
./Microsoft.Net.Compilers.3.11.0/tools/csc.exe ./Client/Client/Program.cs
mv Program.exe Client1.exe
start Server.exe
start Client.exe
start Client1.exe