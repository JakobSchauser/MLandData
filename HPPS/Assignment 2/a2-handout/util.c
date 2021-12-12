#include "util.h"
#include <math.h>
#include <stdio.h>
#include <assert.h>

double distance(int d, const double *x, const double *y) {
    double sum = 0;

    for (int i = 0; i < d; i++){
        sum += (x[i] - y[i])*(x[i] - y[i])
    }
    return sqrt(sum);
}


// Maintain a sorted sequence of indexes to the 'k' closest point seen
// so far.
//
// 'd' is the number of dimensions in the space.
//
// 'points' is the array of all reference points.
//
// 'closest' is an array of length 'k' that contains valid indexes
// into 'points', or -1 to indicate the absence of an element.
//
// 'query' is the query point from which distances are computed.
//
// 'candidate' is the index of a point in 'points'.
//
// Updates 'closest' to contain 'candidate' if 'candidate' is closer
// to 'query' than any point in 'closest'.
//
// Returns 1 if 'closest' was updated, and otherwise '0'.

int insert_if_closer(int k, int d,
                     const double *points, int *closest, const double *query,
                     int candidate) {

  int len = sizeof(*closest)/sizeof(int);


  double new_dist = distance(d, *points[candidate], *points[*query]);

  for(int i = 0; i < len; i++){
    if(distance(d, *points[query]), *points[*closest[i]] < new_dist){
      return 0;
    }

  }

  closest[len] = candidate;
  return 1;

  
}
