#ifndef KDTREE_H
#define KDTREE_H

#include <stdio.h>

// An opaque struct representing a k-d tree.
struct kdtree;

// Construct a new k-d tree corresponding to points in a space.
//
// 'd' is the number of dimensions in the space.
//
// 'n' is the number of reference points in the space.
//
// 'points' is an 'n'-element array of 'd'-dimensional reference
// points.
struct kdtree *kdtree_create(int d, int n, const double *points);

// Free a k-d tree.  The pointer must not be used again.
void kdtree_free(struct kdtree* tree);

// k-d tree-accelerated k-nearest-neighbours.
//
// 'tree' is a k-d tree produced by kdtree_create().
//
// 'k' is the number of neighbours to find.
//
// 'query' is the query point that we are finding neighbours for.  It
// is assumed to have the same number of dimensions as the space that
// was used to construct 'tree'.
//
// Returns a freshly allocated 'k'-element array that contains the
// indexes of the nearest neighbours to 'query' in 'points'.  It is
// the responsibility of the caller to free this array.
int* kdtree_knn(const struct kdtree *tree, int k, const double* query);

// Print an SVG representation of the tree to the given file, scaling
// up point coordinates as indicated.
void kdtree_svg(double scale, FILE* f, const struct kdtree *tree);

#endif
