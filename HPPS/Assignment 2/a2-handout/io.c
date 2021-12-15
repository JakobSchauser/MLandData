#include "io.h"
#include <stdio.h>
#include <stdlib.h>
#include <stdint.h>
#include <assert.h>


// MANGLER: Fejltjek over alt
///
double* read_points(FILE *f, int* n_out, int *d_out) {
  assert(f);
  fread (n_out, sizeof(int32_t), 1, f);
  fread (d_out, sizeof(int32_t), 1, f);

  int size;
  size = (*n_out) * (*d_out);

  // printf("%d %d %d\n",*n_out,*d_out, size);

  double *datapointer = malloc(size*sizeof(double));

  fread (datapointer, sizeof(double), size, f);
  return datapointer;
}


// Read indexes from an indexes data file.  Returns a pointer to the
// data, and writes the size to the n_out and k_out arguments.
// Returns a NULL pointer if reading fails.  It is the caller's
// responsibility to eventually free the returned pointer with free().
int* read_indexes(FILE *f, int *n_out, int *k_out) {
  assert(f);
  fread (n_out, sizeof(int32_t), 1, f);

  int size;
  size = (*n_out) * (*k_out);

  // printf("%d %d %d\n",*n_out,*d_out, size);

  int *datapointer = malloc(size*sizeof(int32_t));

  fread (datapointer, sizeof(int), size, f);
  return datapointer;
}

int write_points(FILE *f, int32_t n, int32_t d, double *data) {
  assert(f);

  fwrite(data, sizeof(double), n*d, f);

  return 0;
}

int write_indexes(FILE *f, int32_t n, int32_t k, int *data) {
  assert(f);

  fwrite(data, sizeof(int), n*k, f);

  return 0;
}
