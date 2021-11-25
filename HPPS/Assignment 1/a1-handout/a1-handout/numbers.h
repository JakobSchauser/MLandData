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
  bits.b1 = bit_from_int((x>>1)%2);
  bits.b2 = bit_from_int((x>>2)%2);
  bits.b3 = bit_from_int((x>>3)%2);
  bits.b4 = bit_from_int((x>>4)%2);
  bits.b5 = bit_from_int((x>>5)%2);
  bits.b6 = bit_from_int((x>>6)%2);
  bits.b7 = bit_from_int((x>>7)%2);
  return bits;
}

unsigned int bits8_to_int(struct bits8 x);
void bits8_print(struct bits8 v){
  bit_print(v.b7);
  bit_print(v.b6);
  bit_print(v.b5);
  bit_print(v.b4);
  bit_print(v.b3);
  bit_print(v.b2);
  bit_print(v.b1);
  bit_print(v.b0);

}

struct bit calc_overflow(struct bit x, struct bit y, struct bit z){
  // return bit_or(bit_and(x,y),bit_and(y,z));
  return true;
}

struct bits8 bits8_add(struct bits8 x, struct bits8 y){
  struct bit overflow;
  struct bits8 bits;

  bits.b0 = bit_xor(x.b0, y.b0);
  overflow = bit_and(x.b0,y.b0);
  
  bits.b1 = bit_xor(bit_xor(x.b1, y.b1), overflow);
  overflow = bit_and(bits.b1,y.b0);



}
struct bits8 bits8_negate(struct bits8 x);
struct bits8 bits8_mul(struct bits8 x, struct bits8 y);
