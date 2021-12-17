#include "io.h"
#include <stdio.h>
#include <stdlib.h>
#include <stdint.h>
#include <assert.h>

double* read_points(FILE *f, int* n_out, int *d_out) {
  int read;

  read = fread(n_out, sizeof(int) , 1 , f);
  if (read != 1) {
    return NULL;
  }


  read = fread(d_out, sizeof(int) , 1 , f);
  if (read != 1) {
    return NULL;
  }

  int size = (*n_out) * (*d_out);

  double *points = malloc(sizeof(double) * size);

  read = fread(points, sizeof(double), size, f);
  if (read != size) {
    free(points);
    return NULL;
  }

  return points;
}

int* read_indexes(FILE *f, int *n_out, int *k_out) {
  int read;

  read = fread(n_out, sizeof(int) , 1 , f);
  if (read != 1) {
    return NULL;
  }


  read = fread(k_out, sizeof(int) , 1 , f);
  if (read != 1) {
    return NULL;
  }

  int size = (*n_out) * (*k_out);

  int *indexes = malloc(sizeof(int) * size);

  read = fread(indexes, sizeof(int), size, f);
  if (read != size) {
    free(indexes);
    return NULL;
  }

  return indexes;
}

int write_points(FILE *f, int32_t n, int32_t d, double *data) {

  if (fwrite(&n, sizeof(int32_t), 1, f) != 1 ) {
    return 1;
  }

  if (fwrite(&d, sizeof(int32_t), 1, f) != 1) {
    return 1;
  }

  int size = n * d;

  if ((int)fwrite(data, sizeof(double), size, f) != size) {
    return 1;
  }

return 0;

}

int write_indexes(FILE *f, int32_t n, int32_t k, int *data) {
  
  // Write number of points.
  if (fwrite(&n, sizeof(int32_t), 1, f) != 1) {
    return 1;
  }

  // Write number of indexes for each point.
  if (fwrite(&k, sizeof(int32_t), 1, f) != 1) {
    return 1;
  }

  // Write the raw point data.
  if ((int)fwrite(data, k*sizeof(int), n, f) != n) {
    return 1;
  }

  return 0;
}