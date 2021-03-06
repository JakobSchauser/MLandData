
┏━━━┓┏━┳┓╋╋╋╋╋╋╋╋╋╋╋╋╋╋╋╋╋╋╋┏━━━┓
┃┏━┓┃┃┏┫┃╋╋╋╋╋╋╋╋╋╋╋╋╋╋╋╋╋╋╋┃┏━━┛
┃┃╋┃┣┛┗┫┃┏━━┳┓┏┳━━┳━┳┳━┓┏━━┓┃┗━━┓
┃┗━┛┣┓┏┫┃┃┃━┫┗┛┃┃━┫┏╋┫┏┓┫┏┓┃┃┏━┓┃
┃┏━┓┃┃┃┃┗┫┃━╋┓┏┫┃━┫┃┃┃┃┃┃┗┛┃┃┗━┛┃
┗┛╋┗┛┗┛┗━┻━━┛┗┛┗━━┻┛┗┻┛┗┻━┓┃┗━━━┛
╋╋╋╋╋╋╋╋╋╋╋╋╋╋╋╋╋╋╋╋╋╋╋╋┏━┛┃╋╋╋╋╋
╋╋╋╋╋╋╋╋╋╋╋╋╋╋╋╋╋╋╋╋╋╋╋╋┗━━┛╋╋╋╋╋

There are multiple ways, but to run the f-sharp code, the modules must first be compiled down into a 
.dll file using "fsharpc -a continuedFraction.fsi continuedFraction.fs". These can then be combined with the main file "continuedFraction.fsx" and compiled into 
.exe files by the command "fsharpc -r continuedFraction.dll continuedFraction.fsx" and run by use of mono.




Whitebox:
+-----------------+-------+-----------+-------+-----------------+--------------------------+
| Unit            | Branx | Condition | Input | Expected Output | Comment                  |
+-----------------+-------+-----------+-------+-----------------+--------------------------+
| cfrac2float     | 1     | None      |       |                 |                          |
+-----------------+-------+-----------+-------+-----------------+--------------------------+
| List.length lst | 1a    | 0         | []    | 0.0             |                          |
+-----------------+-------+-----------+-------+-----------------+--------------------------+
|                 | 1b    | 1         | [2]   | 2.0             |                          |
+-----------------+-------+-----------+-------+-----------------+--------------------------+
|                 | 1c    | else      | [4;2] | 4.5             | + recursive call         |
+-----------------+-------+-----------+-------+-----------------+--------------------------+
| float2cfrac     | 2     |           |       |                 |                          |
+-----------------+-------+-----------+-------+-----------------+--------------------------+
| r < eps         | 2a    | false     | 3.245 | [3]             | + recursive call         |
+-----------------+-------+-----------+-------+-----------------+--------------------------+
|                 | 2b    | true      | 3.245 | [3; 4; 12; 4]   |                          |
+-----------------+-------+-----------+-------+-----------------+--------------------------+