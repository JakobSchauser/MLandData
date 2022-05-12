import numpy as np
from numpy.random import randint
from itertools import permutations

base_2 = ['a','b']
base_3 = ['a','b','c']

# base_2 = [0,1]
# base_3 = [0,1,2]

bases = [base_2,base_3]

all_possible_short = np.linspace(1,10,10).astype(int)
all_possible_long = np.linspace(11,50,40).astype(int)

def make_true(N, is_short, type = 2):
    base = bases[type-2]
    if is_short:
        if type == 2:
            n = randint(1,11,N)
        elif type == 3:
            n = randint(1,7,N)
    else:
        if type == 2:
            n = randint(11,26,N)
        elif type == 3:
            n = randint(7,17,N)

    alls = []

    for nn in n:
        alls.append(np.repeat(base,nn))

    return alls

def make_false(N,is_short, type = 2):
    base = bases[type-2]
    if is_short:
        if type == 2:
            n = [np.random.choice(all_possible_short,2) for _ in range(N)]
        elif type == 3:
            n = [np.random.choice(all_possible_short,3) for _ in range(N)]
    else:
        if type == 2:
            n = [np.random.choice(all_possible_long,2) for _ in range(N)]
        elif type == 3:
            n = [np.random.choice(all_possible_long,3) for _ in range(N)]


    alls = []

    for nn in n:
        e = np.hstack([np.repeat(base[i], nn[i]) for i in range(len(nn))])
        alls.append(e)

    return alls


def generate_data(N, is_short, type = 2):
    """
    returns N/2 true and N/2 false shuffled data points and labels
    """


    true = make_true(N//2, is_short, type)
    false = make_false(N//2, is_short, type)

    alls = np.array([*true,*false])

    labels = np.array([*np.ones(N//2).astype(int),*np.zeros(N//2).astype(int)])

    

    return alls[p], labels[p]

