#include <stdint.h>
#include "bits.h"

struct bits8 {
  struct bit b0; // Least significant bit
  struct bit b1;
  struct bit b2;
  struct bit b3;
  struct bit b4;
  struct bit b5;
  struct bit b6;
  struct bit b7;
};

struct bits8 bits8_from_int(unsigned int x){
  struct bits8 bits;

  bits.b0 = bit_from_int(x%2);
  bits.b1 = bit_from_int(x%4);
  bits.b2 = bit_from_int(x%2);
  bits.b3 = bit_from_int(x%2);
  bits.b4 = bit_from_int(x%2);
  bits.b5 = bit_from_int(x%2);
  bits.b6 = bit_from_int(x%2);
  bits.b7 = bit_from_int(x%2);

};
unsigned int bits8_to_int(struct bits8 x);
void bits8_print(struct bits8 v);
struct bits8 bits8_add(struct bits8 x, struct bits8 y);
struct bits8 bits8_negate(struct bits8 x);
struct bits8 bits8_mul(struct bits8 x, struct bits8 y);
