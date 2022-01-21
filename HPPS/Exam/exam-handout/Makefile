CC?=cc
CFLAGS?=-std=c99 -Wall -Wextra -pedantic -O3 -fopenmp

all: benchmark

matlib.o: matlib.c matlib.h
	$(CC) -o matlib.o matlib.c -c $(CFLAGS)

benchmark.o: benchmark.c
	$(CC) -o benchmark.o benchmark.c -c $(CFLAGS)

benchmark: benchmark.o matlib.o
	$(CC) -o benchmark benchmark.o matlib.o $(CFLAGS)

clean:
	rm -f *.o benchmark
