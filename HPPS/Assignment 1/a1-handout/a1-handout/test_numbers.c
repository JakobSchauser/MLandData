#include <stdlib.h>
#include "numbers.h"

int main() {
  struct bits8 toprint = bits8_mul(bits8_from_int(2),bits8_from_int(5));
  bits8_print(toprint);
}
