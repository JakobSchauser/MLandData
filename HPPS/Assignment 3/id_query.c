#include <stdio.h>
#include <errno.h>
#include <stdint.h>
#include <string.h>
#include <stdlib.h>

#include "id_query.h"
#include "timing.h"

int id_query_loop(int argc, char** argv, mk_index_fn mk_index, free_index_fn free_index, lookup_fn lookup) {
  if (argc != 2) {
    fprintf(stderr, "Usage: %s FILE\n", argv[0]);
    exit(1);
  }

  uint64_t start, runtime;
  int n;

  start = microseconds();
  struct record *rs = read_records(argv[1], &n);
  runtime = microseconds()-start;

  if (rs) {
    printf("Reading records: %dms\n", (int)runtime/1000);

    start = microseconds();
    void *index = mk_index(rs, n);
    runtime = microseconds()-start;
    printf("Building index: %dms\n", (int)runtime/1000);

    char *line = NULL;
    size_t line_len;

    uint64_t runtime_sum = 0;
    while (getline(&line, &line_len, stdin) != -1) {
      int64_t needle = atol(line);

      start = microseconds();
      const struct record *r = lookup(index, needle);
      runtime = microseconds()-start;

      if (r) {
        printf("%ld: %s %f %f\n", (long)needle, r->name, r->lon, r->lat);
      } else {
        printf("%ld: not found\n", (long)needle);
      }

      printf("Query time: %dus\n", (int)runtime);
      runtime_sum += runtime;
    }

    printf("Total query runtime: %dus\n", (int)runtime_sum);

    free(line);
    free_index(index);
    free_records(rs, n);
    return 0;
  } else {
    fprintf(stderr, "Failed to read input from %s (errno: %s)\n",
            argv[1], strerror(errno));
    return 1;
  }
}
