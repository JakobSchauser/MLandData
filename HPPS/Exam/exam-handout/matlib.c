#include <stdlib.h>
#include <assert.h>
#include <omp.h>

void transpose(int n, int m, double *B, const double *A) {
  for (int i = 0; i < n; i++){
    for (int j = 0; j < m; j++){
      B[j*n + i] = A[i*m + j]; 
    }
  }
}

void transpose_parallel(int n, int m, double *B, const double *A) {
  #pragma omp parallel for
  for (int i = 0; i < n; i++){
    for (int j = 0; j < m; j++){
      B[j*n + i] = A[i*m + j]; 
    }
  }
}

void transpose_blocked(int n, int m, double *B, const double *A) {
  int T = 2;

  for (int ii = 0; ii < n; ii += T){
    for (int jj = 0; jj < m; jj += T){

      for (int i = ii; i < ii+T; i++){
        for (int j = jj; j < jj+T; j++){
          B[j*n + i] = A[i*m + j]; 
        }
      }

    }
  }
}

void transpose_blocked_parallel(int n, int m, double *B, const double *A) {
  int T = 2;

  for (int ii = 0; ii < n; ii += T){
    for (int jj = 0; jj < m; jj += T){
      #pragma omp parallel for
      for (int i = ii; i < ii+T; i++){
        for (int j = jj; j < jj+T; j++){
          B[j*n + i] = A[i*m + j]; 
        }
      }

    }
  }
}


void matmul(int n, int m, int k,
            double* C, const double* A, const double* B) {

  for (int i = 0; i < n; i++){
    for (int j = 0; j < k; j++){
      double acc = 0;
      for (int p = 0; p < m; p++){
        acc += A[p*n + i]*B[j*m + p];
      }
      C[j*n + i] = acc;
    }
  }
}

void matmul_locality(int n, int m, int k,
                     double* C, const double* A, const double* B) {

  //Checks if C is initialized purely with zeros
  double s = 0.0;
  for(int i = 0; i < n*k; i ++){ s += C[i] }
  assert(s = 0.0);

  for (int i = 0; i < n; i++){
    for (int p = 0; p < m; p++){
      double a = A[p*n + i];
      for (int j = 0; j < k; j++){
        C[j*n + i] += a * B[j*m + p];
      }
    }
  }
}

void matmul_transpose(int n, int m, int k,
                      double* C, const double* A, const double* B) {
//   1. Transpose B to obtain BT
// .
// 2. Perform a conventional matrix multiplication (as in matmul()), but
// where you are traversing the rows of BT
// instead of the columns of B.
// Use your fastest non-parallel transposition function.

  // remember to use fastest here!!
  double * BT = malloc(m*k*sizeof(double));
  transpose(m,k,BT,B);


}

void matmul_parallel(int n, int m, int k,
                     double* C, const double* A, const double* B) {
  assert(0);
}

void matmul_locality_parallel(int n, int m, int k,
                              double* C, const double* A, const double* B) {
  assert(0);
}

void matmul_transpose_parallel(int n, int m, int k,
                               double* C, const double* A, const double* B) {
  assert(0);
}
