CC?=gcc
CFLAGS?=-Wextra -Wall -pedantic -std=c99 -g
LDFLAGS?=-lm

all: knn-bruteforce knn-svg knn-kdtree knn-genpoints

knn-bruteforce: knn-bruteforce.o bruteforce.o io.o util.o
	$(CC) -o $@ $^ $(LDFLAGS)

knn-kdtree: knn-kdtree.o bruteforce.o io.o util.o kdtree.o sort.o
	$(CC) -o $@ $^ $(LDFLAGS)

knn-genpoints: knn-genpoints.o io.o
	$(CC) -o $@ $^ $(LDFLAGS)

knn-svg: knn-svg.o io.o util.o kdtree.o sort.o
	$(CC) -o $@ $^ $(LDFLAGS)

# A general rule that tells us how to generate an .o file from a .c
# file.  This cuts down on the boilerplate.
%.o: %.c
	$(CC) -c $< $(CFLAGS)

clean:
	rm -rf knn-genpoints knn-bruteforce knn-svg knn-kdtree *.o *.dSYM
	rm -rf points queries indexes points.svg

# Testing rules

NUM_POINTS=10000
NUM_QUERIES=1000
K=5

points: knn-genpoints
	./knn-genpoints $(NUM_POINTS) 2 > points

queries: knn-genpoints
	./knn-genpoints $(NUM_QUERIES) 2 > queries

indexes: points queries knn-bruteforce
	./knn-bruteforce points queries $(K) indexes

points.svg: points queries indexes knn-svg
	./knn-svg points queries indexes > points.svg
