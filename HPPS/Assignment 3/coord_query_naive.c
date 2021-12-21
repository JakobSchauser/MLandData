#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <sys/time.h>
#include <stdint.h>
#include <errno.h>
#include <assert.h>

#include "record.h"
#include "coord_query.h"

struct naive_data {
  struct record *rs;
  int n;
};

struct naive_data* mk_naive(struct record* rs, int n) {
  struct naive_data *data = malloc(sizeof(struct naive_data));
  data->rs = malloc(sizeof(struct record)*n);
  data->rs = rs;
  data->n = n;
  return data;
}

void free_naive(struct naive_data* data) {
  free(data);
}

double distance(struct record *rec, double lon, double lat){
  // I choose to not sqr for optimiation. It does not matter since they are monotonic functions.
  return ((lon - rec->lon)*(lon - rec->lon) + (lat - rec->lat)*(lat - rec->lat));
}

const struct record* lookup_naive(struct naive_data *data, double lon, double lat) {
  double lowest_dist = (90*90)+(360*360);
  struct record *lowest = NULL;

  for (int i = 0; i<data->n; i++){
    struct record *rec = &(data->rs[i]);
    double dist = distance(rec,lon,lat);
    if(dist < lowest_dist){
      lowest_dist = dist;
      lowest = rec;
    }     
  }
  return lowest;

  
}

int main(int argc, char** argv) {
  return coord_query_loop(argc, argv,
                          (mk_index_fn)mk_naive,
                          (free_index_fn)free_naive,
                          (lookup_fn)lookup_naive);
}
