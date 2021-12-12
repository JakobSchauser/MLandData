#include "io.h"
#include <stdio.h>
#include <stdlib.h>
#include <stdint.h>
#include <assert.h>


int main(int argc, char *argv[]) {
  FILE * file = fopen("20_5.points","r");
  int n_out, d_out;
  double *data = read_points(file, &n_out, &d_out);

  fclose(file);
  for (int i = 0; i < (n_out) * (d_out); i++){
    if(i % d_out == 0){
      printf("\nPoint %d: ",(int)floor(i/d_out));
    }
    printf("%f ", data[i]);
    
  }

  printf("Here");

  // FILE * filecopy;// = (FILE *) malloc(n_out*d_out*sizeof(double));
  FILE * filecopy = fopen("20_5_copy.points","wb");
  // assert(filecopy);
  if(filecopy == NULL){printf("EHHEYEYHE");}else{printf("Happy");}
  // int a = write_points(filecopy, n_out, d_out, data);
  
  fclose(filecopy);
  

}