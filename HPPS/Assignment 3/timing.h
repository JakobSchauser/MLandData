#ifndef TIMING_H
#define TIMING_H

#include <sys/time.h>

static uint64_t microseconds() {
  static struct timeval t;
  gettimeofday(&t, NULL);
  return ((uint64_t)t.tv_sec*1000000)+t.tv_usec;
}

#endif
