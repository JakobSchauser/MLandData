#include "io.h"
#include <stdio.h>
#include <stdlib.h>
#include <stdint.h>
#include <assert.h>


// MANGLER: Fejltjek over alt

double* read_points(FILE *f, int* n_out, int *d_out) {

  fread (n_out, sizeof(int32_t), 1, f);
  fread (d_out, sizeof(int32_t), 1, f);

  int size;
  size = (*n_out) * (*d_out);

  // printf("%d %d %d\n",*n_out,*d_out, size);

  double *datapointer = (double *) malloc(size);

  fread (datapointer, sizeof(double), size, f);
  return datapointer;
}


int* read_indexes(FILE *f, int *n_out, int *k_out) {
  assert(0);
}

int write_points(FILE *f, int32_t n, int32_t d, double *data) {
  return fwrite(data, sizeof(double), n*d, f);
}

int write_indexes(FILE *f, int32_t n, int32_t k, int *data) {
  assert(0);
}
