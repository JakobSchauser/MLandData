#include <stdio.h>
#include <stdlib.h>
#include <stdint.h>
#include <assert.h>
#include "knn-bruteforce.h"
#include "io.h"



int main(int argc, char *argv[]) {
    FILE * file = fopen("20_5.points","r");
    int n_out, d_out;
    double *data = read_points(file, &n_out, &d_out);

    b
}