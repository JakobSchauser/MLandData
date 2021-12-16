#include "io.h"
#include "util.h"
#include "kdtree.h"
#include <stdio.h>
#include <stdlib.h>
#include <assert.h>
#include <time.h>
#include <stdint.h>

void draw_points(int size, int n_points, double* points) {
  // Draw a small circle for each reference points.
  double point_radius = 2;
  const char *point_colour = "black";
  for (int i = 0; i < n_points; i++) {
    // Assuming points are in (0,1)
    double x = points[i*2] * size;
    double y = points[i*2+1] * size;
    printf("<circle cx=\"%f\" cy=\"%f\" r=\"%f\" fill=\"%s\" />\n", x, y, point_radius, point_colour);
  }
}

void draw_queries(int size, int d, double *points,
                  const char *queries_fname, const char *indexes_fname) {
  FILE *queries_f = fopen(queries_fname, "r");
  assert(queries_f != NULL);

  int n_queries;
  int d_queries;
  double *queries = read_points(queries_f, &n_queries, &d_queries);

  if (d != d_queries) {
    fprintf(stderr, "Reference points have dimensionality %d, but query points have dimensionality %d\n",
            (int)d, (int)d_queries);
    exit(1);
  }

  if (queries == NULL) {
    fprintf(stderr, "Failed reading data from %s\n",
            queries_fname);
    exit(1);
  }
  fclose(queries_f);

  FILE *indexes_f = fopen(indexes_fname, "r");
  assert(indexes_f != NULL);

  int n_indexes;
  int k;
  int *indexes = read_indexes(indexes_f, &n_indexes, &k);
  if (indexes == NULL) {
    fprintf(stderr, "Failed reading data from %s\n",
            indexes_fname);
    exit(1);
  }
  fclose(indexes_f);

  if (n_queries != n_indexes) {
    fprintf(stderr, "Found %d queries, but %d indexes\n",
            n_queries, n_indexes);
    exit(1);
  }

  double query_radius = 4;
  for (int q = 0; q < n_queries; q++) {
    double x = queries[q*d] * size;
    double y = queries[q*d+1] * size;

    // Draw each query in a randomly generated colour.
    int r = rand() % 128;
    int g = rand() % 128;
    int b = rand() % 128;

    printf("<circle cx=\"%f\" cy=\"%f\" r=\"%f\" fill=\"#%.2x%.2x%.2x\" />\n",
           x, y, query_radius, r, g, b);

    // Find the distance to the most distant neighbour.
    double most_distant = 0;
    for (int j = 0; j < k; j++) {
      double j_dist = distance(d,
                               &queries[q*d],
                               &points[indexes[q*k+j]*d]);
      if (j_dist > most_distant) {
        most_distant = j_dist;
      }
    }

    // Then draw a non-filled circle with that radius around the
    // query point.
    double circle_thickness = 1;
    printf("<circle cx=\"%f\" cy=\"%f\" r=\"%f\" stroke=\"#%.2x%.2x%.2x\" stroke-width=\"%f\" fill-opacity=\"0\" />\n",
           x, y, most_distant*size, r, g, b, circle_thickness);
  }

  free(queries);
  free(indexes);
}

int main(int argc, char** argv) {
  if (argc != 2 && argc != 4) {
    fprintf(stderr, "Usage: %s <points> [<queries> <indexes>]\n", argv[0]);
    exit(1);
  }

  srand(time(NULL));

  FILE *points_f = fopen(argv[1], "r");
  assert(points_f != NULL);

  int n_points;
  int d;
  double *points = read_points(points_f, &n_points, &d);
  if (points == NULL) {
    fprintf(stderr, "Failed reading data from %s\n", argv[1]);
    exit(1);
  }
  fclose(points_f);

  if (d != 2) {
    fprintf(stderr, "Can only visualise 2-dimensional spaces, and input is %d-dimensional\n", d);
    exit(1);
  }

  // We produce a square image with this edge length.
  int size = 1000;

  // SVG header.
  printf("<svg xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" width=\"%d\" height=\"%d\" viewBox=\"0 0 %d %d\">\n",
         size, size, size, size);
  // Box around the canvas.
  printf("<rect x=\"0\" y=\"0\" width=\"%d\" height=\"%d\" fill= \"#ffffff\" stroke=\"black\" stroke-width=\"1\" />\n",
         size, size);

  draw_points(size, n_points, points);

  // Maybe draw queries and circles indicating the distance of their
  // k'th neighbour.
  if (argc == 4) {
    draw_queries(size, d, points, argv[2], argv[3]);
  }

  // Maybe compute KD-tree and draw it.
  int draw_kdtree = 1;
  if (draw_kdtree) {
    struct kdtree *kdtree = kdtree_create(d, n_points, points);
    kdtree_svg(size, stdout, kdtree);
    kdtree_free(kdtree);
  }

  printf("</svg>\n");

  free(points);
}
