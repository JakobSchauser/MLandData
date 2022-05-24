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

def make_true(train_test_validation, type = 2):
    base = bases[type-2]

    lo = 1 if train_test_validation == "train" else 20 if train_test_validation == "test" else 50
    hi = 20 if train_test_validation == "train" else 50 if train_test_validation == "test" else 70

    prods = list(product(*[range(1, hi) for _ in range(type)]))

    prods = [p for p in prods if (sum(p) <= hi and sum(p) > lo and np.all([k == p[0] for k in p]))]

    alls = []
    for nn in prods:
        e = np.hstack([np.repeat(base[i], nn[i]) for i in range(len(nn))])
        alls.append(e)

    return alls


def make_false(train_test_validation, type = 2):
    base = bases[type-2]

    lo = 1 if train_test_validation == "train" else 20 if train_test_validation == "test" else 50
    hi = 20 if train_test_validation == "train" else 50 if train_test_validation == "test" else 70

    if train_test_validation[0] == "n":
        hi = int(train_test_validation[1:])
        lo = int(train_test_validation[1:])-1

    prods = product(*[range(1, hi) for _ in range(type)])

    prods = [p for p in prods if (sum(p) <= hi and sum(p) > lo and np.any([k != p[0] for k in p]))]

    alls = []
    for nn in prods:
        e = np.hstack([np.repeat(base[i], nn[i]) for i in range(len(nn))])
        alls.append(e)

    return alls


def encode_letter(l):
    return np.array([tensor(onehot[d].values) for d in l])

letterToIndex = lambda l: ord(l) - 97


def encode_language(l, min_length = 50):
    tensor = torch.zeros(min_length, 1, 3)
    for li, letter in enumerate(l):
        tensor[li][0][letterToIndex(letter)] = 1
    return tensor

def onehot_encode(data, min_length = 50):
    if len(data) == 1:
        return encode_language(data[0],min_length = len(data[0]))
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
        if type(enc) is list:
            batch = torch.cat(enc,axis = 1)
        else:
            batch = enc
        
        truth = onehot_labels(_labels[i*batch_size:(i+1)*batch_size])
        yield (batch, truth)


def generate_data(train_test_validation, type = 2):
    """
    returns N/2 true and N/2 false shuffled data points and labels
    """

    true = np.array(make_true(train_test_validation, type))
    false = np.array(make_false(train_test_validation, type))


    shuffle = np.random.permutation(len(false))
    false = false[shuffle][:len(true)]

    shuffle = np.random.permutation(len(true))
    true = true[shuffle]


    alls = np.array([*true,*false])

    labels = np.array([*np.ones(len(true)).astype(int),*np.zeros(len(false)).astype(int)])
    
    return alls, labels


def get_x_generators(batch_size, type = 2):
    loaders = []
    for i in range(30):
        data, labels = generate_data("n"+str(i+20),2)
        l = data_loader(data, labels, batch_size, min_length = 50)
        loaders.append(l)

    return loaders

