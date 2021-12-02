#include <stdlib.h>
#include <stdio.h>

int main(int args, char** argv){
    FILE * pfile = fopen(argv[1],"r");

    char buffer[100];
    while(fread(&buffer,sizeof(char),1,pfile) != 0){
        printf("%x",buffer);
    }


    // printf("x: %d  y: %d",x,y);
    
    fclose(pfile);
}
