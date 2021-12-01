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
  int b = abs((x>>ind)%2);
  return bit_from_int(b);
}

struct bits8 bits8_from_int(unsigned int x){
  struct bits8 bits;

  bits.b0 = getBit(x,0);
  bits.b1 = getBit(x,1);
  bits.b2 = getBit(x,2);
  bits.b3 = getBit(x,3);
  bits.b4 = getBit(x,4);
  bits.b5 = getBit(x,5);
  bits.b6 = getBit(x,6);
  bits.b7 = getBit(x,7);
  return bits;
}


struct bits8 setBit(int x, int ind){
  int ret = x|(1 << ind);
  return bits8_from_int(ret);

}

unsigned int bits8_to_int(struct bits8 x){
  int num = 0;
  num = num|(bit_to_int(x.b0)<<0);
  num = num|(bit_to_int(x.b1)<<1);
  num = num|(bit_to_int(x.b2)<<2);
  num = num|(bit_to_int(x.b3)<<3);
  num = num|(bit_to_int(x.b4)<<4);
  num = num|(bit_to_int(x.b5)<<5);
  num = num|(bit_to_int(x.b6)<<6);
  num = num|(bit_to_int(x.b7)<<7);

  return num;
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
  int i = bits8_to_int(x);
  int ichanged = (i^(-1)) + 1;
  return bits8_from_int(ichanged);

}
struct bits8 bits8_mul(struct bits8 x, struct bits8 y){
  int num1 = bits8_to_int(y);

  int result = 0;
  result += num1*bit_to_int(x.b0)*(1<<0);
  result += num1*bit_to_int(x.b1)*(1<<1);
  result += num1*bit_to_int(x.b2)*(1<<2);
  result += num1*bit_to_int(x.b3)*(1<<3);
  result += num1*bit_to_int(x.b4)*(1<<4);
  result += num1*bit_to_int(x.b5)*(1<<5);
  result += num1*bit_to_int(x.b6)*(1<<6);
  result += num1*bit_to_int(x.b7)*(1<<7);

  return bits8_from_int(result);
}


