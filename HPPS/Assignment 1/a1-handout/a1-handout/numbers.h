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


// struct bits8 setBit(int x, int ind){
//   int ret = x|(1 << ind);
//   return bits8_from_int(ret);

// }

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


struct bit calc_overflow(struct bit a, struct bit b,struct bit c){

  return bit_or(bit_or(bit_and(a,b),bit_and(a,c)),bit_and(b,c));
}

struct bits8 bits8_fill(struct bit b){

  struct bits8 returner;
  returner.b0 = b;
  returner.b1 = b;
  returner.b2 = b;
  returner.b3 = b;
  returner.b4 = b;
  returner.b5 = b;
  returner.b6 = b;
  returner.b7 = b;

  return returner;
  
}

struct bits8 bits8_add(struct bits8 x, struct bits8 y){
  struct bit overflow;
  struct bits8 bits;

  bits.b0 = bit_xor(x.b0, y.b0);
  overflow = calc_overflow(x.b0,y.b0,bit_from_int(0));

  
  bits.b1 = bit_xor(bit_xor(x.b1, y.b1), overflow);
  overflow = calc_overflow(x.b1,y.b1,overflow);

  bits.b2 = bit_xor(bit_xor(x.b2, y.b2), overflow);
  overflow = calc_overflow(x.b2,y.b2,overflow);

  bits.b3 = bit_xor(bit_xor(x.b3, y.b3), overflow);
  overflow = calc_overflow(x.b3,y.b3,overflow);

  bits.b4 = bit_xor(bit_xor(x.b4, y.b4), overflow);
  overflow = calc_overflow(x.b4,y.b4,overflow);

  bits.b5 = bit_xor(bit_xor(x.b5, y.b5), overflow);
  overflow = calc_overflow(x.b5,y.b5,overflow);

  bits.b6 = bit_xor(bit_xor(x.b6, y.b6), overflow);
  overflow = calc_overflow(x.b6,y.b6,overflow);

  bits.b7 = bit_xor(bit_xor(x.b7, y.b7), overflow);

  return bits;
}


struct bits8 bits8_shiftleft(struct bits8 x, int num){
  if(num == 0){return x;};

  struct bits8 returner;
  returner.b0 = bit_from_int(0);
  returner.b1 = x.b0;
  returner.b2 = x.b1;
  returner.b3 = x.b2;
  returner.b4 = x.b3;
  returner.b5 = x.b4;
  returner.b6 = x.b5;
  returner.b7 = x.b6;

  return bits8_shiftleft(returner,num-1);
}



struct bits8 bits8_and(struct bits8 x,struct bits8 y){

  struct bits8 result;

  result.b0 = bit_and(x.b0,y.b0);
  result.b1 = bit_and(x.b1,y.b1);
  result.b2 = bit_and(x.b2,y.b2);
  result.b3 = bit_and(x.b3,y.b3);
  result.b4 = bit_and(x.b4,y.b4);
  result.b5 = bit_and(x.b5,y.b5);
  result.b6 = bit_and(x.b6,y.b6);
  result.b7 = bit_and(x.b7,y.b7);

  return result;
}


struct bits8 bits8_xor(struct bits8 x,struct bits8 y){
  struct bits8 result;

  result.b0 = bit_xor(x.b0,y.b0);
  result.b1 = bit_xor(x.b1,y.b1);
  result.b2 = bit_xor(x.b2,y.b2);
  result.b3 = bit_xor(x.b3,y.b3);
  result.b4 = bit_xor(x.b4,y.b4);
  result.b5 = bit_xor(x.b5,y.b5);
  result.b6 = bit_xor(x.b6,y.b6);
  result.b7 = bit_xor(x.b7,y.b7);

  return result;
}

struct bits8 bits8_negate(struct bits8 x){
  struct bits8 ones = bits8_fill(bit_from_int(1));
  struct bits8 one = bits8_from_int(1);

  // negate by xor'ing with a bits8 bits8_filled with 1's. Add one.
  struct bits8 new = bits8_add(bits8_xor(x,ones), one);
  return new;

}
struct bits8 bits8_mul(struct bits8 x, struct bits8 y){
  struct bits8 result = bits8_fill(bit_from_int(0));  // All zeros
  struct bits8 shifted; 


  shifted = bits8_shiftleft(bits8_and(bits8_fill(x.b0),y),0);
  result = bits8_add(result,shifted);

  shifted = bits8_shiftleft(bits8_and(bits8_fill(x.b1),y),1);
  result = bits8_add(result,shifted);

  shifted = bits8_shiftleft(bits8_and(bits8_fill(x.b2),y),2);
  result = bits8_add(result,shifted);

  shifted = bits8_shiftleft(bits8_and(bits8_fill(x.b3),y),3);
  result = bits8_add(result,shifted);

  shifted = bits8_shiftleft(bits8_and(bits8_fill(x.b4),y),4);
  result = bits8_add(result,shifted);

  shifted = bits8_shiftleft(bits8_and(bits8_fill(x.b5),y),5);
  result = bits8_add(result,shifted);

  shifted = bits8_shiftleft(bits8_and(bits8_fill(x.b6),y),6);
  result = bits8_add(result,shifted);

  shifted = bits8_shiftleft(bits8_and(bits8_fill(x.b7),y),7);
  result = bits8_add(result,shifted);

  
  return result;

}

struct bits8 bits8_sub(struct bits8 x, struct bits8 y){
  return bits8_add(x,bits8_negate(y));
}
