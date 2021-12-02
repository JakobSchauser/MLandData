#include <stdlib.h>
#include "numbers.h"

int main() {
  struct bits8 toprint = bits8_mul(bits8_from_int(1),bits8_from_int(10));
  printf("Result:\n");

  bits8_print(toprint);
  
}
