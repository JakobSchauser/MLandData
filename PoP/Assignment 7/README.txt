                                                                                                                          _________  
                   .---.                                                                                                 /         | 
                   |   |      __.....__   .----.     .----.   __.....__             .--.   _..._                        '-----.   .' 
              _.._ |   |  .-''         '.  \    \   /    /.-''         '.           |__| .'     '.   .--./)                 .'  .'   
            .' .._||   | /     .-''"'-.  `. '   '. /'   //     .-''"'-.  `. .-,.--. .--..   .-.   . /.''\\                .'  .'     
    __      | '    |   |/     /________\   \|    |'    //     /________\   \|  .-. ||  ||  '   '  || |  | |             .'  .'       
 .:--.'.  __| |__  |   ||                  ||    ||    ||                  || |  | ||  ||  |   |  | \`-' /             '---'         
/ |   \ ||__   __| |   |\    .-------------''.   `'   .'\    .-------------'| |  | ||  ||  |   |  | /("'`                            
`" __ | |   | |    |   | \    '-.____...---. \        /  \    '-.____...---.| |  '- |  ||  |   |  | \ '---.                          
 .'.''| |   | |    |   |  `.             .'   \      /    `.             .' | |     |__||  |   |  |  /'""'.\                         
/ /   | |_  | |    '---'    `''-...... -'      '----'       `''-...... -'   | |         |  |   |  | ||     ||                        
\ \._,\ '/  | |                                                             |_|         |  |   |  | \'. __//                         
 `--'  `"   |_|                                                                         '--'   '--'  `'---'                          
_______________________________________________________________________________________________________________________________________

HOW TO RUN THE CODE:

This folder contains the following files
 * rotate.fs
 * rotate.fsi
 * game.fsx
 * whiteboxtests.fsx
 * blackboxtests.fsx

They are run in the following manner:

The rotate library can be compiled in mono by the following command:
"fsharpc -a rotate.fsi rotate.fs"

Afterwards the rest of the files can be compiled and run in the following manner
"fsharpc -r rotate.dll <filename>"
"mono <filename>"