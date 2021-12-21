#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <sys/time.h>
#include <stdint.h>
#include <errno.h>
#include <assert.h>

#include "record.h"
#include "id_query.h"

struct index_record {
  int64_t osm_id;
  const struct record *record; 
};

struct indexed_data {
    struct index_record *irs;
    int n;
};


int comparefunc(struct index_record *a, struct index_record *b){
    return (int) (a->osm_id - b->osm_id);
}

struct indexed_data* mk_index(struct record* rs, int n) {
  struct index_record *sorted = malloc(sizeof(struct index_record)*n);

  struct index_record *nxt = malloc(sizeof(struct index_record));

  for (int i = 0; i < n; i++){
    struct record *rc = &rs[i];
    nxt->osm_id = rc->osm_id;
    nxt->record = rc;
    sorted[i] = *nxt;
  }
  
  free(nxt);

  qsort(sorted, n, sizeof(struct index_record), comparefunc);

  struct indexed_data *data = malloc(sizeof(struct indexed_data));
  data->irs = malloc(sizeof(struct index_record)*n);
  data->irs = sorted;
  data->n = n;
  free(sorted);
  return data;
}

void free_index(struct indexed_data* data) {
  for (int i = 0; i < data->n; i++){
      free(&data->irs[i]);
  }
  free(data);
}

const struct record* lookup_index(struct indexed_data *data, int64_t needle) {
  int max = data->n;
  int min = 0;
  int index = max/2;

  while (1) {
    struct index_record *rec = &(data->irs[index]);
    int64_t diff = rec->osm_id - needle;

    if(diff == 0){
      return rec->record;
    } else if (diff < 0){
        min = index;
    } else {
        max = index;
    }

    index = (max + min)/2;

    if(abs(max-min)<=1){
        return NULL;
    }
  }

}

int main(int argc, char** argv) {
  return id_query_loop(argc, argv,
                    (mk_index_fn)mk_index,
                    (free_index_fn)free_index,
                    (lookup_fn)lookup_index);
}
