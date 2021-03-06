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


  struct indexed_data *data = malloc(sizeof(struct indexed_data));
  data->irs = malloc(sizeof(struct index_record)*n);
  data->irs = sorted;
  data->n = n;
  return data;
}

void free_index(struct indexed_data* data) {
  for (int i = 0; i < data->n; i++){
      free(&data->irs[i]);
  }
  free(data);
}

const struct record* lookup_index(struct indexed_data *data, int64_t needle) {

  for (int i = 0; i<data->n; i++){
    struct index_record *rec = &(data->irs[i]);
    int64_t diff = rec->osm_id;
    if(diff == needle){
      return rec->record;
    }     
  }
  return NULL;

}

int main(int argc, char** argv) {
  return id_query_loop(argc, argv,
                    (mk_index_fn)mk_index,
                    (free_index_fn)free_index,
                    (lookup_fn)lookup_index);
}
