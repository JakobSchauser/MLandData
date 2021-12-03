#include <stdlib.h>
#include "numbers.h"

int main() {
  struct bits8 neg10 = bits8_from_int(-10);
  struct bits8 pos10 =  bits8_from_int(10);
  struct bits8 neg2 = bits8_from_int(-2);
  struct bits8 pos2 = bits8_from_int(2);

  printf("Visual tests. Any two should be the same:\n")
  printf("Test addition:\n");
  bits8_print(bits8_add(pos10,neg2));
  printf("\n");
  bits8_print(bits8_from_int(8));
  printf("\n");
  printf("\n");
  bits8_print(bits8_add(neg10,neg2));
  printf("\n");
  bits8_print(bits8_from_int(-12));
  printf("\n");
  printf("\n");
  bits8_print(bits8_add(pos10,pos2));
  printf("\n");
  bits8_print(bits8_from_int(12));
  printf("\n");
  printf("\n");

  printf("Test multiplication:\n");
  bits8_print(bits8_mul(pos10,neg2));
  printf("\n");
  bits8_print(bits8_from_int(-20));
  printf("\n");
  printf("\n");
  bits8_print(bits8_mul(neg10,neg2));
  printf("\n");
  bits8_print(bits8_from_int(20));
  printf("\n");
  printf("\n");
  bits8_print(bits8_mul(pos10,pos2));
  printf("\n");
  bits8_print(bits8_from_int(20));
  printf("\n");
  printf("\n");

}
