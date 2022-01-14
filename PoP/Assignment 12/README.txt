                /|  /|  ---------------------------
                ||__||  |                         |
               /   O O\__      PoP Aflevering     |
              /          \           12i          |
             /      \     \                       |
            /   _    \     \ ----------------------
           /    |\____\     \      ||
          /     | | | |\____/      ||
         /       \| | | |/ |     __||
        /  /  \   -------  |_____| ||
       /   |   |           |       --|
       |   |   |           |_____  --|
       |  |_|_|_|          |     \----
       /\                  |
      / /\        |        /
     / /  |       |       |
 ___/ /   |       |       |
|____/    c_c_c_C/ \C_c_c_c


HOW TO RUN THE CODE:

The /src-folder contains the following file:
 * chessApp.fsx
 * chess.fs
 * pieces.fs


They are run in the following manner:

First, compile chess.fs and pieces.fs into .dll files and place them in the /src-folder.

Afterwards, the rest of the files can be compiled and run in the following manner
"fsharpc -r pieces.dll chess.dll chessApp.fsx"
"mono chessApp.exe"