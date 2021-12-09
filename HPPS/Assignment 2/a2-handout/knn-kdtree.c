#include "io.h"
#include "kdtree.h"
#include <stdio.h>
#include <stdlib.h>
#include <assert.h>
#include <stdint.h>
#include <string.h>

int main(int argc, char** argv) {
  if (argc != 4 && argc != 5) {
    fprintf(stderr, "Usage: %s <points> <queries> <k> [output-file]\n", argv[0]);
    exit(1);
  }

  FILE * points_f = fopen(argv[1], "r");
  assert(points_f != NULL);
  FILE * queries_f = fopen(argv[2], "r");
  assert(queries_f != NULL);
  int32_t k = atoi(argv[3]);

  int n_points = -1;
  int d;
  double* points = read_points(points_f, &n_points, &d);
  if (points == NULL) {
    fprintf(stderr, "Failed reading data from %s\n", argv[1]);
    exit(1);
  }
  fclose(points_f);

  int n_queries = -1;
  int d_queries;
  double* queries = read_points(queries_f, &n_queries, &d_queries);
  if (queries == NULL) {
    fprintf(stderr, "Failed reading data from %s\n", argv[2]);
    exit(1);
  }
  fclose(queries_f);

  if (d != d_queries) {
    fprintf(stderr, "Reference points have dimensionality %d, but query points have dimensionality %d\n",
            (int)d, (int)d_queries);
    exit(1);
  }

  printf("Dimensions: %d\n", (int)d);
  printf("Points: %d\n", n_points);
  printf("Queries: %d\n", n_queries);
  printf("Finding indexes of %d nearest neighbours\n", k);

  struct kdtree *kdtree = kdtree_create(d, n_points, points);
  int* indexes = malloc(n_queries*k*sizeof(int));

  for (int q = 0; q < n_queries; q++) {
    int *closest = kdtree_knn(kdtree, k, &queries[q*d]);

    printf("Query %d: ", q);
    for (int i = 0; i < k; i++) {
      printf("%d ", closest[i]);
    }
    printf("\n");

    memcpy(&indexes[q*k], closest, k*sizeof(int));
    free(closest);
  }

  if (argc == 5) {
    FILE *output_f = fopen(argv[4], "w");
    assert(output_f != NULL);

    int err = write_indexes(output_f, n_queries, k, indexes);
    assert(err == 0);

    fclose(output_f);
  }

  kdtree_free(kdtree);

  free(indexes);
  free(points);
  free(queries);

  return 0;
}
