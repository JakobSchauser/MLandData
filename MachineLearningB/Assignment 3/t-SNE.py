import numpy as np
import matplotlib.pyplot as plt

points = np.genfromtxt("dataset1.txt",delimiter = ",")

print(points.shape)


N = len(points)
affinities = np.zeros((N,N))

sigma = np.ones(N)


def lensqr(a,b):
    return np.sum((a-b)*(a-b))**2

def exp(a,b):
    return np.exp(-lensqr(a, b)/(2*sigma[i]**2))

def perp(i,j):
    return exp(points[i],points[j])/np.sum([exp(points[i],k)*(k!=j) for k in range(N)])

for i in range(N):
    for j in range(N):
        if i == j:
            affinities[i,j] = 0
        else:
            affinities[i,j] = (perp(i,j) + perp(j,i))/(2*N)
        
def low_dim(i,j,Ys):
    return (1/(1+lensqr(Ys[i], Ys[j])))/(np.sum([1/(1+lensqr(Ys[i], Ys[k])) for k in range(N)]))


def get_low_dim(Ys):
    new_low_dim_affinities = np.zeros((N,N))

    for i in range(n):
        for j in range(n):
            if i == j:
                new_low_dim_affinities[i,j] = 0
            else:
                new_low_dim_affinities[i,j] = low_dim(i, j, Ys)

    return new_low_dim_affinities

# Number of iterations
T = 10

# Learning rate
eta = 0.1

# Momentum
alpha = lambda t: 0.5


print("Here")

# Begin
def do_reg():
    Ys = np.random.normal(0.0, 1e-4, (N,len(points[0])))
    last_Ys = Ys.copy()
    second_Ys = Ys.copy()

    low_dim_affinities = np.zeros((N,N))
    print("Starting now")
    for t in range(T):
        print("Running iteration",t)
        low_dim_affinities = get_low_dim(Ys)

        # Calculate gradients
        dCdy[i] = 4 * np.sum((affinities[i,j] - low_dim_affinities[i,j])*(Ys[i] - Ys[j])/(1+lensqr(Ys[i], Ys[j])))


        Ys += eta*dCdy + alpha(t)*(last_Ys-second_Ys)

        second_Ys = last_Ys.copy()
        last_Ys = Ys.copy()
        pass

    return Ys

Ys = do_reg()
