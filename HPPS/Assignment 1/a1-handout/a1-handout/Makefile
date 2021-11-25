CFLAGS=-Wall -Wextra -pedantic -std=c99

test_bits: test_bits.c bits.h
	gcc -o test_bits test_bits.c $(CFLAGS)

test_numbers: test_numbers.c numbers.h bits.h
	gcc -o test_numbers test_numbers.c $(CFLAGS)
