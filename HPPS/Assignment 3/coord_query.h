// Similar to id_query.h.  See the comments there.

#ifndef COORD_QUERY_LOOP_H
#define COORD_QUERY_LOOP_H

#include "record.h"

typedef void* (*mk_index_fn)(const struct record*, int);

typedef void (*free_index_fn)(void*);

typedef const struct record* (*lookup_fn)(void*, double, double);

int coord_query_loop(int argc, char** argv, mk_index_fn, free_index_fn, lookup_fn);

#endif
