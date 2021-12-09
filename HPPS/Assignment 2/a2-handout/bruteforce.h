#ifndef KNN_BRUTEFORCE_H
#define KNN_BRUTEFORCE_H

// Brute-force k-nearest-neighbours.
//
// 'k' is the number of neighbours to find.
//
// 'd' is the number of dimensions in the space.
//
// 'n' is the number of reference points.
//
// 'query' is the query point that we are finding neighbours for.
//
// Returns a freshly allocated 'k'-element array that contains the
// indexes of the nearest neighbours to 'query' in 'points'.  It is
// the responsibility of the caller to free this array.
int* knn(int k, int d, int n, const double *points, const double* query);

#endif
