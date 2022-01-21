// This file contains skeleton code for benchmarking your solutions.
// You are not *required* to use it, but it may be very helpful.  Read
// the comments to understand how to use it.  The two important helper
// functions are bench_transpose() and bench_matmul().  See the main()
// function for (commented-out) example uses.
//
// The matmul benchmarking function assume that the "basic" version
// matmul() is correct.  Validation is by comparing the results of the
// given function with the results of matmul().  This means you should
// be absolutely sure that the matmul() is correct, or the validation
// will not work.

#include <stdint.h>
#include <stdio.h>

#include "matlib.h"
#include "timing.h"

// Pointer to a transposition function.  All transpose variants
// defined in matlib.h have this type.
typedef void (*transpose_fn)(int n, int m, double* B, const double* A);

// Pointer to a matrix multiplication function.  All matmul variants
// defined in matlib.h have this type.
typedef void (*matmul_fn)(int n, int m, int k, double* C, const double* A, const double* B);

// Create a random array of 'n' doubles.
double* random_array(int n) {
  double *p = malloc(n * sizeof(double));
  for (int i = 0; i < n; i++) {
    p[i] = ((double)rand())/RAND_MAX;
  }
  return p;
}

void bench_transpose(const char *desc, int runs, transpose_fn f,
                     int n, int m) {
  uint64_t bef = microseconds();
  printf("%30s n=%4d, m=%4d: ", desc, n, m);
  fflush(stdout);

  double *A = random_array(n*m);
  double *B = calloc(m*n, sizeof(double));

  for (int i = 0; i < runs; i++) {
    f(n, m, B, A);
  }

  double us = (microseconds()-bef)/runs;

  // Validate the result.
  for (int i = 0; i < n; i++) {
    for (int j = 0; j < m; j++) {
      if (A[i*m+j] != B[j*n+i]) {
        printf("\nFailed validation: mismatch at index [%d,%d]\n",
               i, j);
        return;
      }
    }
  }

  double s = us/1e6;
  double mib = ((double)n*m) * sizeof(double) / (1024*1024);
  printf("%4.fms (%5f MiB/s)\n", us/1e3, mib/s);

  free(A);
  free(B);
}

void bench_matmul(const char *desc, int runs, matmul_fn f,
                  int n, int m, int k) {
  uint64_t bef = microseconds();
  printf("%30s n=%4d, m=%4d, k=%4d: ", desc, n, m, k);
  fflush(stdout);

  double *A = random_array(n*m);
  double *B = random_array(m*k);
  double *C = calloc(n*k, sizeof(double));

  // Compute the "expected" output of matrix multiplication.
  double *golden = calloc(n*k, sizeof(double));
  matmul(n, m, k, golden, A, B);

  for (int i = 0; i < runs; i++) {
    f(n, m, k, C, A, B);
  }

  double us = (microseconds()-bef)/runs;

  for (int i = 0; i < n; i++) {
    for (int j = 0; j < k; j++) {
      double expected = golden[i*k+j];
      double got = C[i*k+j];
      if (expected != got) {
        printf("\nFailed validation: at index [%d,%d] expected %f, got %f\n",
               i, j, expected, got);
        return;
      }
    }
  }

  double s = us/1e6;
  double gflops = ((double)n*k*(2*m)) / 1e9;
  double mib = ((double)n*k*m) * sizeof(double) / (1024*1024);
  printf("%4.fms (%4f GFLOP/s, %5f MiB/s)\n", us/1e3, gflops/s, mib/s);

  free(A);
  free(B);
  free(C);
  free(golden);
}

int main() {
  // Pick your own sensible sizes.
  int n = 1;
  int m = 2;
  int k = 3;

  // Think about how many runs is proper for each case, and probably
  // don't use the same number for all tests.
  int runs = 10;

  // As you implement your functions, you can use the benchmarking
  // functions commented out below.

  // bench_transpose("transpose", runs, transpose, n, m);
  // bench_transpose("transpose_blocked", runs, transpose_blocked, n, m);
  // bench_transpose("transpose_parallel", runs, transpose_parallel, n, m);
  // bench_transpose("transpose_blocked_parallel", runs, transpose_blocked_parallel, n, m);

  // bench_matmul("matmul_parallel", runs, matmul_parallel, n, m, k);
  // bench_matmul("matmul_locality", runs, matmul_locality, n, m, k);
  // bench_matmul("matmul_transpose", runs, matmul_transpose, n, m, k);
  // bench_matmul("matmul_locality_parallel", runs, matmul_locality_parallel, n, m, k);
  // bench_matmul("matmul_transpose_parallel", runs, matmul_transpose_parallel, n, m, k);
}
