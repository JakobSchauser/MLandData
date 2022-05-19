import numpy as np
from numpy.random import randint
from itertools import permutations, product
import pandas as pd
import torch




base_2 = ['a','b']
base_3 = ['a','b','c']

onehot = pd.Series(data = [[1,0,0],[0,1,0],[0,0,1]], index = ['a','b','c'])


# base_2 = [0,1]
# base_3 = [0,1,2]

bases = [base_2,base_3]

all_possible_short = np.linspace(1,20,20).astype(int)
all_possible_long = np.linspace(21,50,40).astype(int)

def make_true(is_short, type = 2):
    base = bases[type-2]
    lo = 0 if is_short else 20
    hi = 20 if is_short else 50

    prods = product(*[range(lo, hi) for _ in range(type)])

    prods = [p for p in prods if (sum(p) <= hi and all(p == p[0]))]

    alls = []
    for nn in prods:
        e = np.hstack([np.repeat(base[i], nn[i]) for i in range(len(nn))])
        alls.append(e)

    return prods


def make_false(is_short, type = 2):
    base = bases[type-2]
    lo = 0 if is_short else 20
    hi = 20 if is_short else 50

    prods = product(*[range(lo, hi) for _ in range(type)])

    prods = [p for p in prods if (sum(p) <= hi and all(p != p[0]))]

    alls = []
    for nn in prods:
        e = np.hstack([np.repeat(base[i], nn[i]) for i in range(len(nn))])
        alls.append(e)

    return prods


# def make_true(N, is_short, type = 2):
#     base = bases[type-2]
#     if is_short:
#         if type == 2:
#             n = randint(1,11,N)
#         elif type == 3:
#             n = randint(1,7,N)
#     else:
#         if type == 2:
#             n = randint(11,26,N)
#         elif type == 3:
#             n = randint(7,17,N)

#     alls = []

#     for nn in n:
#         alls.append(np.repeat(base,nn))

#     return alls

# def make_false(N,is_short, type = 2):
#     base = bases[type-2]
#     if is_short:
#         size = 20
#     else:
#         size = 50

#     n = []
#     for _ in range(N):
#         k = np.empty(type)
#         s = size
#         for i in range(type):
#             r = np.random.randint(s)
#             k[i] = r
#             s -= r
#         n.append(k)


#     alls = []

#     for nn in n:
#         e = np.hstack([np.repeat(base[i], nn[i]) for i in range(len(nn))])
#         alls.append(e)

#     return alls


def encode_letter(l):
    return np.array([tensor(onehot[d].values) for d in l])

letterToIndex = lambda l: ord(l) - 97

def encode_language(l, min_length = 50):
    tensor = torch.zeros(min_length, 1, 3)

    for li, letter in enumerate(l):
        tensor[li][0][letterToIndex(letter)] = 1
    return tensor

def onehot_encode(data, min_length = 50):
    return [encode_language(l,min_length = min_length) for l in data]

def onehot_labels(labels):
    tensor = torch.zeros(len(labels), 2)

    for i, l in enumerate(labels):
        tensor[i][l] = 1
    return tensor


def data_loader(data, labels, batch_size, min_length = 50):
    shuffle = np.random.permutation(len(data))

    _data = data[shuffle]
    _labels = labels[shuffle]
    for i in range(len(_data)//batch_size):
        enc = onehot_encode(_data[i*batch_size:(i+1)*batch_size], min_length = min_length)
        batch = torch.cat(enc,axis = 1)
        
        truth = onehot_labels(_labels[i*batch_size:(i+1)*batch_size])
        yield (batch, truth)


def generate_data(train_test_split, is_short, type = 2):
    """
    returns N/2 true and N/2 false shuffled data points and labels
    """

    true = make_true(is_short, type)
    false = make_false(is_short, type)

    N = len(true)

    shuffle = np.random.permutation(N)

    false = false[shuffle][:N]

    alls_train = np.array([*true[:(train_test_split*N)],*false[:(train_test_split*N)]])
    alls_test = np.array([*true[(train_test_split*N):],*false[(train_test_split*N):]])

    labels_train = np.array([*np.ones((train_test_split*N)).astype(int),*np.zeros((train_test_split*N)).astype(int)])

    labels_test = np.array([*np.ones(((1-train_test_split)*N)).astype(int),*np.zeros(((1-train_test_split)*N)).astype(int)])

    
    return alls_train, labels_train, alls_test, labels_test

