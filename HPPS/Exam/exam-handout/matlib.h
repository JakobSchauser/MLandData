#ifndef MATLIB_H
#define MATLIB_H

// Compute the transposition of A, storing it in B.
//
// A has size n x m and must be stored in row-major order.
//
// Memory for B must be allocated by the caller and have room for at
// least n*m*sizeof(double) bytes.
void transpose(int n, int m, double *B, const double *A);

// As 'transpose', but parallelised.
void transpose_parallel(int n, int m, double *B, const double *A);

// As 'transpose', but with the blocking optimisation.
void transpose_blocked(int n, int m, double *B, const double *A);

////////////////// REMEMBER TO CHANGE BACK //////////////////////////
void transpose_blocked_T(int n, int m, double *B, const double *A, int T);


// As 'transpose', but both parallelised and with blocking.
void transpose_blocked_parallel(int n, int m, double *B, const double *A);

// Compute C = A * B
//
// A has size n x m.
// B has size m x k.
// C has size n x k.
//
// A and B must be provided in row-major order.  Memory must be
// allocated for C, and it will be produced in row-major order.
void matmul(int n, int m, int k, double* C,
            const double* A, const double* B);

// As 'matmul', but with a locality transformation.
void matmul_locality(int n, int m, int k,
                     double* C, const double* A, const double* B);

// As 'matmul', but with a locality transformation and parallelised.
void matmul_locality_parallel(int n, int m, int k,
                              double* C, const double* A, const double* B);

// As 'matmul', but parallelised.
void matmul_parallel(int n, int m, int k,
                     double* C, const double* A, const double* B);

// As 'matmul', but first transposes B.
void matmul_transpose(int n, int m, int k,
                      double* C, const double* A, const double* B);

// As 'matmul', but parallelised and first transposes B.
void matmul_transpose_parallel(int n, int m, int k,
                               double* C, const double* A, const double* B);

#endif
