#include <stdlib.h>
#include <assert.h>
#include <omp.h>
#include <math.h>

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



////////////////////////////// REMEMBER TO CHANGE BACK //////////////////////////////////////
void transpose_blocked_T(int n, int m, double *B, const double *A, int T){

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

void transpose_blocked(int n, int m, double *B, const double *A) {
  ////////////////////////////// REMEMBER TO CHANGE BACK //////////////////////////////////////
  // transpose_blocked_T(n, m, B, A, 2);
  if(n < 2 || m < 2){
    transpose(n ,m, B, A);
    return;
  }
  int T = sqrt(n);

  // if (T%n != 0 || T%n != 0){
  //   transpose(n ,m, B, A);
  //   return;

  // }


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
  if(n < 2 || m < 2){
    transpose(n ,m, B, A);
    return;
  }
  int T = sqrt(n);
  
  #pragma omp parallel for
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
        acc += A[i*m + p] * B[p*k + j];
      }
      C[j*n + i] = acc;
    }
  }
}

void matmul_locality(int n, int m, int k,
                     double* C, const double* A, const double* B) {

  //Checks if C is initialized purely with zeros
  // double s = 0.0;
  // for(int i = 0; i < n*k; i ++){ s += C[i]; }
  // assert(s == 0.0);

  for (int i = 0; i < n; i++){
    for (int p = 0; p < m; p++){
      double a = A[i*m + p];
      for (int j = 0; j < k; j++){
        C[j*n + i] += a * B[p*k + j];
      }
    }
  }
}

void matmul_transpose(int n, int m, int k,
                      double* C, const double* A, const double* B) {
  // remember to use fastest here!!
  double * BT = malloc(m*k*sizeof(double));
  transpose(m,k,BT,B);

  // BT = k x m = j x p

  for (int i = 0; i < n; i++){
    for (int j = 0; j < k; j++){
      double acc = 0;
      for (int p = 0; p < m; p++){ 
        acc += A[i*m + p] * BT[j*m + p];
      }
      C[j*n + i] = acc;
    }
  }
}

void matmul_parallel(int n, int m, int k,
                     double* C, const double* A, const double* B) {

  #pragma omp parallel for
  for (int i = 0; i < n; i++){
    for (int j = 0; j < k; j++){
      
      double acc = 0;
      for (int p = 0; p < m; p++){
        acc += A[i*m + p] * B[p*k + j];
      }
      C[j*n + i] = acc;
    }
  }
}

void matmul_locality_parallel(int n, int m, int k,
                              double* C, const double* A, const double* B) {
  //Checks if C is initialized purely with zeros
  // double s = 0.0;
  // for(int i = 0; i < n*k; i ++){ s += C[i]; }
  // assert(s == 0.0);

  // Here stuff might go wrong with the 'a'-definition
  #pragma omp parallel for
  for (int i = 0; i < n; i++){
    for (int p = 0; p < m; p++){
      double a = A[i*m + p];
      for (int j = 0; j < k; j++){
        C[j*n + i] += a * B[p*k + j];
      }
    }
  }
}

void matmul_transpose_parallel(int n, int m, int k,
                               double* C, const double* A, const double* B) {
  // remember to use fastest here!!
  double * BT = malloc(m*k*sizeof(double));
  transpose_parallel(m,k,BT,B);

  #pragma omp parallel for
  for (int i = 0; i < n; i++){
    for (int j = 0; j < k; j++){
      double acc = 0;
      for (int p = 0; p < m; p++){
        acc += A[i*m + p] * BT[j*m + p];
      }
      C[j*n + i] = acc;
    }
  }
}
