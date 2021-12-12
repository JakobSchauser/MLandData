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
  free(data);
   

}