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


struct bit getBit(int x, int ind){
  return bit_from_int((x>>ind)%2);
}

struct bits8 setBit(int x, int ind){
  // struct bits8 bits;
  // bits.b0 = getBit(x,0);
  // bits.b1 = getBit(x,1);
  // bits.b2 = getBit(x,2);
  // bits.b3 = getBit(x,3);
  // bits.b4 = getBit(x,4);
  // bits.b5 = getBit(x,5);
  // bits.b6 = getBit(x,6);
  // bits.b7 = getBit(x,7);

  x^(1 << ind)
  return bits;

}
struct bits8 bits8_from_int(unsigned int x){
  struct bits8 bits;

  bits.b0 = getBit(x,0);
  bits.b1 = getBit(x,1);
  bits.b2 = getBit(x,2);
  bits.b7 = getBit(x,3);
  bits.b3 = getBit(x,4);
  bits.b4 = getBit(x,5);
  bits.b5 = getBit(x,6);
  bits.b6 = getBit(x,7);
  return bits;
}

unsigned int bits8_to_int(struct bits8 x){



}

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
  return bit_from_int(1);
}

struct bits8 bits8_add(struct bits8 x, struct bits8 y){
  struct bit overflow;
  struct bits8 bits;

  bits.b0 = bit_xor(x.b0, y.b0);
  overflow = bit_and(x.b0,y.b0);
  
  bits.b1 = bit_xor(bit_xor(x.b1, y.b1), overflow);
  overflow = bit_and(bit_or(x.b1,y.b1),bit_or(y.b1,overflow));

  bits.b2 = bit_xor(bit_xor(x.b2, y.b2), overflow);
  overflow = bit_and(bit_or(x.b2,y.b2),bit_or(y.b2,overflow));

  bits.b3 = bit_xor(bit_xor(x.b3, y.b3), overflow);
  overflow = bit_and(bit_or(x.b3,y.b3),bit_or(y.b3,overflow));

  bits.b4 = bit_xor(bit_xor(x.b4, y.b4), overflow);
  overflow = bit_and(bit_or(x.b4,y.b4),bit_or(y.b4,overflow));

  bits.b5 = bit_xor(bit_xor(x.b5, y.b5), overflow);
  overflow = bit_and(bit_or(x.b5,y.b5),bit_or(y.b5,overflow));

  bits.b6 = bit_xor(bit_xor(x.b6, y.b6), overflow);
  overflow = bit_and(bit_or(x.b6,y.b6),bit_or(y.b6,overflow));

  bits.b7 = bit_xor(bit_xor(x.b7, y.b7), overflow);

  return bits;
}
struct bits8 bits8_negate(struct bits8 x){


}
struct bits8 bits8_mul(struct bits8 x, struct bits8 y);
