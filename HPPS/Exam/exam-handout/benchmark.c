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
#include <math.h>


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

void display_array(int n, int m, double *A){
  for(int i = 0; i < n; i++) {
      for(int j = 0; j < m; j++) {
         printf("%f ", A[i*n + j]);
         if(j==m-1){
            printf("\n");
         }
      }
   }
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

double gettime_transpose(int runs, transpose_fn f, int n, int m){
  uint64_t bef = microseconds();
  fflush(stdout);
  double *A = random_array(n*m);
  double *B = calloc(m*n, sizeof(double));

  for (int i = 0; i < runs; i++) {
    f(n, m, B, A);
  }

  double us = (microseconds()-bef)/runs;

  free(A);
  free(B);

  return us/1e6;

}


void bench_transpose_csv(const char *filename, transpose_fn f, int n_sizes,int n_runs){
  printf("Starting\n");
  int sizes[n_sizes];
  double times[n_sizes*n_runs];

  for (int i = 0; i < n_sizes; i ++){

    int s = (i+1)*(i+1);
    printf("Now at size %d: %d/%d\n",s,i,n_sizes);
    sizes[i] = s;
    for (int r = 0; r < n_runs; r ++){
      times[i*n_runs + r] = gettime_transpose(5,f,s,s);
    }
  }

  // Find and open or create .csv file
  FILE *fpt;
  fpt = fopen(filename, "w+");

  // Make header
  fprintf(fpt,"size");
  for (int r = 0; r < n_runs; r ++){
    fprintf(fpt,",time %d",r);
  }
  fprintf(fpt,"\n");

  // Add data
  for (int i = 0; i < n_sizes; i ++){
    fprintf(fpt,"%d", sizes[i]);
    for (int r = 0; r < n_runs; r ++){
      fprintf(fpt,",%f", times[i*n_runs + r]);
    }
    fprintf(fpt,"\n");
  }
  fclose(fpt);
  printf("Finished!\n");

}


/////////////////  FINDING THE BEST T'S  ////////////////////

double gettime_transpose_T(int runs, int n, int m, int T){
  uint64_t bef = microseconds();
  fflush(stdout);
  double *A = random_array(n*m);
  double *B = calloc(m*n, sizeof(double));

  for (int i = 0; i < runs; i++) {
    transpose_blocked(n, m, B, A);
  }

  double us = (microseconds()-bef)/runs;

  free(A);
  free(B);

  return us/1e6;

}

void find_best_T(const char *filename, int n_sizes){

  printf("Starting\n");
  int sizes[n_sizes];
  int Ts[n_sizes];


  for (int i = 0; i < n_sizes; i ++){
    int s = (i+1)*(i+1);
    sizes[i] = s;

    double bt = 99999999.0;
    int bT = 1;
    printf("Now at %d\n", s);

    for(int T = 1; T <= sqrt(s); T++){
      if(s%T == 0){
        double t = gettime_transpose_T(5,s,s,T);
        if(t < bt){
          bt = t;
          bT = T;
        }

      }
    }
    Ts[i] = bT;
  }

  // Find and open or create .csv file
  FILE *fpt;
  fpt = fopen(filename, "w+");

  // Make header
  fprintf(fpt,"size");
  fprintf(fpt,", best T");
  fprintf(fpt,"\n");

  // Add data
  for (int i = 0; i < n_sizes; i ++){
    fprintf(fpt,"%d", sizes[i]);
    fprintf(fpt,",%d", Ts[i]);
    fprintf(fpt,"\n");
  }
  fclose(fpt);
  printf("Finished!\n");
  
}

int main() {
  // Pick your own sensible sizes.
  // int n = 2000;
  // int m = 2000;
  int k = 2;

  // Think about how many runs is proper for each case, and probably
  // don't use the same number for all tests.
  int runs = 10;

  // As you implement your functions, you can use the benchmarking
  // functions commented out below.

  // bench_transpose("transpose", runs, transpose, n, m);
  // bench_transpose("transpose_blocked", runs, transpose_blocked, n, m);
  // bench_transpose("transpose_parallel", runs, transpose_parallel, n, m);
  // bench_transpose("transpose_blocked_parallel", runs, transpose_blocked_parallel, n, m);
  
  // bench_matmul("matmul", runs, matmul, n, m, k);
  // bench_matmul("matmul_parallel", runs, matmul_parallel, n, m, k);
  // bench_matmul("matmul_locality", runs, matmul_locality, n, m, k);
  // bench_matmul("matmul_transpose", runs, matmul_transpose, n, m, k);
  // bench_matmul("matmul_locality_parallel", runs, matmul_locality_parallel, n, m, k);
  // bench_matmul("matmul_transpose_parallel", runs, matmul_transpose_parallel, n, m, k);
  int n_sizes = 65;
  int n_runs = 5;

  // bench_transpose_csv("transpose_blocked_sizes.csv",transpose_blocked,n_sizes,n_runs);
  find_best_T("finding_T.csv", n_sizes);

}
