#include "bruteforce.h"
#include "util.h"
#include <stdlib.h>
#include <assert.h>


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
int* knn(int k, int d, int n, const double *points, const double* query) {

  int* closest = malloc(k * sizeof(int));

  for(int i = 0; i < k; i++){closest[i] = -1;}

  for(int i = 0; i < n; i++){
    int worked = insert_if_closer(k,d,&points,closest,query,i);
  }

  return closest;
}

