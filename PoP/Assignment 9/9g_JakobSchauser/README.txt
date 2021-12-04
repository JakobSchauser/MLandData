 _______  _______  _        _______           _______  _______ _________ _        _______     _____  
(  ___  )(  ____ \( \      (  ____ \|\     /|(  ____ \(  ____ )\__   __/( (    /|(  ____ \   / ___ \ 
| (   ) || (    \/| (      | (    \/| )   ( || (    \/| (    )|   ) (   |  \  ( || (    \/  ( (   ) )
| (___) || (__    | |      | (__    | |   | || (__    | (____)|   | |   |   \ | || |        ( (___) |
|  ___  ||  __)   | |      |  __)   ( (   ) )|  __)   |     __)   | |   | (\ \) || | ____    \____  |
| (   ) || (      | |      | (       \ \_/ / | (      | (\ (      | |   | | \   || | \_  )        ) |
| )   ( || )      | (____/\| (____/\  \   /  | (____/\| ) \ \_____) (___| )  \  || (___) |  /\____) )
|/     \||/       (_______/(_______/   \_/   (_______/|/   \__/\_______/|/    )_)(_______)  \______/ 
_____________________________________________________________________________________________________

HOW TO COMPILE THE CODE

This src folder contains the following files:
    * readNWrite.fs
    * cat.fsx
    * tac.fsx
    * countLinks.fsx

They are compiled in the following way:

1. Compile the library by the following command "fsharpc -a readNWrite.fs"
2. The .fsx files can then be compiled by the command "fsharpc -r readNWrite.dll <filename>"

HOW TO RUN THE .EXE FILES: 

Each .exe file is run with with mono and by the following command "mono <filename> args"
where the args are specific to each file.

CAT:
cat.exe takes one or more filenames and concatenates their concatent
and finally prints the concatenated content to the screen if possible.

Example:
We introduce two test files "abcdef.txt" and "kradsbørstig.txt" which respectively contain
the following text: "abc\ndef" and "krads\nbørstig".
Running cat.exe with these files we get:

Input: mono cat.exe abcdef.txt kradsbørstig.txt
Output: abc\ndefkrads\nbørstig

TAC:
We use the same example files as for cat and get

Input: mono tac.exe abcdef.txt kradsbørstig.txt
Output: gitsrøb\nsdarkfed\ncba

COUNTLINKS:
For testing this function we use the url "http://goodfreshshinypathway.neverssl.com/online"

Input: mono countlinks.exe http://goodfreshshinypathway.neverssl.com/online
Output: 2