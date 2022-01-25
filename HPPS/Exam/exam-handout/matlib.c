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




void transpose_blocked(int n, int m, double *B, const double *A) {
  if(n < 2 || m < 2){
    // printf("Too small matrix!");
    transpose(n ,m, B, A);
    return;
  }
  // int T = sqrt(n);
  int T = 50;

  if (n%T != 0){
    // printf("Illegal! n = %d", n);
    transpose(n ,m, B, A);
    return;

  }


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
    // printf("Too small matrix!");
    transpose(n ,m, B, A);
    return;
  }
  // int T = sqrt(n);
  int T = 100;
  if(n%T != 0){
    printf("Illegal!");
    transpose(n ,m, B, A);
    return;
  }

  #pragma omp parallel for
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


void matmul(int n, int m, int k,
            double* C, const double* A, const double* B) {

  for (int i = 0; i < n; i++){
    for (int j = 0; j < k; j++){

      double acc = 0;
      for (int p = 0; p < m; p++){
        acc += A[i*m + p] * B[p*k + j];
      }
      C[i*k + j] = acc;
    }
  }
}

void matmul_locality(int n, int m, int k,
                     double* C, const double* A, const double* B) {

  //Checks if C is initialized purely with zeros
  // double s = 0.0;
  // for(int i = 0; i < n*k; i ++){ s += C[i]; }
  // assert(s == 0.0);


  // A has size n x m.
  // B has size m x k.
  // C has size n x k.
  for (int i = 0; i < n; i++){
    for (int p = 0; p < m; p++){
      double a = A[i*m + p];
      for (int j = 0; j < k; j++){
        C[i*k + j] += a * B[p*k + j];
      }
    }
  }
}

void matmul_transpose(int n, int m, int k,
                      double* C, const double* A, const double* B) {
  double * BT = malloc(m*k*sizeof(double));
  transpose_blocked(m,k,BT,B);

  // BT = k x m = j x p

  for (int i = 0; i < n; i++){
    for (int j = 0; j < k; j++){
      double acc = 0;
      for (int p = 0; p < m; p++){ 
        acc += A[i*m + p] * BT[j*m + p];
      }
      C[i*k + j] = acc;
    }
  }

  free(BT);

  
}

void matmul_parallel(int n, int m, int k,
                     double* C, const double* A, const double* B) {

  #pragma omp parallel for
  for (int i = 0; i < n; i++){
    for (int j = 0; j < k; j++){
      
      double acc = 0;
      // #pragma omp parallel for reduction (+: acc)
      for (int p = 0; p < m; p++){
        acc += A[i*m + p] * B[p*k + j];
      }
      C[i*k + j] = acc;
    }
  }
}

void matmul_locality_parallel(int n, int m, int k,
                              double* C, const double* A, const double* B) {
  //Checks if C is initialized purely with zeros
  // double s = 0.0;
  // for(int i = 0; i < n*k; i ++){ s += C[i]; }
  // assert(s == 0.0);

  #pragma omp parallel for
  for (int i = 0; i < n; i++){
    for (int p = 0; p < m; p++){
      double a = A[i*m + p];
      for (int j = 0; j < k; j++){
        C[i*k + j] += a * B[p*k + j];
      }
    }
  }
}

void matmul_transpose_parallel(int n, int m, int k,
                               double* C, const double* A, const double* B) {

  double * BT = malloc(m*k*sizeof(double));

  transpose_blocked_parallel(m,k,BT,B);

  #pragma omp parallel for
  for (int i = 0; i < n; i++){
    for (int j = 0; j < k; j++){
      double acc = 0;
      for (int p = 0; p < m; p++){
        acc += A[i*m + p] * BT[j*m + p];
      }
      C[i*k + j] = acc;
    }
  }
  free(BT);

}
