CC=gcc
CFLAGS=-Wall -Wextra -pedantic -std=gnu99 -g -ggdb3
LDFLAGS=-lm
PROGRAMS=random_ids id_query_naive coord_query_naive id_query_indexed
TESTS=..

.PHONY: all test clean ../src.zip

all: $(PROGRAMS)

random_ids: random_ids.o record.o
	gcc -o $@ $^ $(LDFLAGS)

id_query_%: id_query_%.o record.o id_query.o
	gcc -o $@ $^ $(LDFLAGS)

coord_query_%: coord_query_%.o record.o coord_query.o
	gcc -o $@ $^ $(LDFLAGS)

id_query.o: id_query.c
	$(CC) -c $< $(CFLAGS)

coord_query.o: coord_query.c
	$(CC) -c $< $(CFLAGS)

record.o: record.c
	$(CC) -c $< $(CFLAGS)

sort.o: sort.c
	$(CC) -c $< $(CFLAGS)

test: $(TESTS)
	@set e; for test in $(TESTS); do echo ./$$test; ./$$test; done

clean:
	rm -rf core *.o $(PROGRAMS)

planet-latest-geonames.tsv:
	wget https://github.com/OSMNames/OSMNames/releases/download/v2.0.4/planet-latest_geonames.tsv.gz
	gunzip planet-latest_geonames.tsv.gz

../src.zip:
	make clean
	cd .. && zip src.zip -r src

.SECONDARY:
