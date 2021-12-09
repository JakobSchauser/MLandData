#ifndef KNN_UTIL_H
#define KNN_UTIL_H

// Compute the Euclidean distance between two d-dimensional points 'x'
// and 'y'.  Usual formula:
//
// 
// √( ∑ (x[i]-y[i])² )
// 
double distance(int d, const double *x, const double *y);

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
                     int candidate);

#endif
