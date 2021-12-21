#include "util.h"
// #include "util.c"
// #include "io.c"
#include <stdio.h>
#include <stdlib.h>
#include <assert.h>
#include <stdint.h>
#include <time.h>
#include <math.h>


int check_if_lower(int k, int d,
                     const double *points, int *q_indexes, const double *query,
                     int candidate)  {


    double *point_candidate = &points[d * candidate];
    double candidate_dist = distance(d, query, point_candidate);

    if(candidate_dist == 0) {
        return 1;
    }

    for(int i=0; i<k; i++) {
        if(candidate == q_indexes[i]) {
            return 1;
        }
    }

    // printf("candidate dist: %f \n", candidate_dist);   
    for(int i=0; i<k; i++) {
        double index_dist = distance(d, query, &points[d * q_indexes[i]]);
        if(candidate_dist < index_dist ) {
            return 0;
        }  
    }
    return 1;
} 


int main() {
    FILE *f_points = fopen("200_5.points", "r");
    FILE *f_indexes = fopen("test_kdtree.indexes", "r");
    FILE *f_queries = fopen("200_5.points", "r");

    int d,n_points;
    int n_queries;
    int k;

    double *points = read_points(f_points, &n_points, &d);
    double *queries = read_points(f_queries, &n_queries, &d);
    int *indexes = read_indexes(f_indexes, &n_queries, &k);

    fclose(f_points);
    fclose(f_indexes);
    fclose(f_queries);

    // for (int i = 0; i < (n_points) * (d); i++){
    //     if(i % d == 0){
    //         printf("\nPoint %d: ",(int)floor(i/d));
    //     }
    //     printf("%f ", points[d*i]);
    
    // }

    printf("k: %i \n", k);
    int is_lower;

    for (int j=0; j<n_queries; j++) {
        double *query = &queries[d * j];
        for(int i=0; i<n_points; i++) {
            is_lower = check_if_lower(k,d,points, &indexes[k * j],query, i);
            if (is_lower == 0) {
                printf("Found a fucky-wucky \n");
                return 0;
            }
    }
    }
    printf("Found no fucky-wuckies \n");
    free(points);
    free(queries);
    free(indexes);
    return 1;
}
