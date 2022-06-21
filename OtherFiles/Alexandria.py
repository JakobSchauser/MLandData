import numpy as np 
import matplotlib.pyplot as plt
import seaborn as sns
sns.set_style("white")
from itertools import product



standard_figsize = (10,5)
despine = True

class plot:
    def __init__(self):
        pass

    def points(y,x = [], ax = None, **kwargs):

        for key, value in kwargs.items():
            if key == "label":
                print("yes")

        if not ax:
            fig, ax = plt.subplots(1,1,figsize = standard_figsize)

        if len(x) == 0:
            x = [i for i in range(len(y))]
        ax.plot(x,y,'.',label = label)

        if despine:
            sns.despine()

    
    def line(y,x = [],label = None,ax = None):

        if not ax:
            fig, ax = plt.subplots(1,1,figsize = standard_figsize)
        if len(x) == 0:
            x = [i for i in range(len(y))]
        ax.plot(x,y,'-',label = label)

        if despine:
            sns.despine()

    def flot(y,x = [],label = None,ax = None):

        if not ax:
            fig, ax = plt.subplots(1,1,figsize = standard_figsize)
        if len(x) == 0:
            x = [i for i in range(len(y))]
        ax.plot(x,y,'.',label = label)
        ax.plot(x,y,'-')

        if despine:
            sns.despine()

    def error(y, yerr,x = [],label = None,ax = None):

        if not ax:
            fig, ax = plt.subplots(1,1,figsize = standard_figsize)

        if len(x) == 0:
            x = [i for i in range(len(y))]
        ax.fill_between(x,np.maximum(0,y-yerr),y+yerr,alpha=0.4)
        ax.plot(x,y,'--',label = label)

        if despine:
            sns.despine()


    def hist(y,x = [],n = 19,label = None,ax = None):
        if ax == None:
            fig, ax = plt.subplots(1,1,figsize = standard_figsize)

        h, xx = np.histogram(y,bins = n)
        print(h)
        if len(x) == 0:
            x = (xx[1:]+xx[:-1])/2

            
        ax.hist(y, bins = n,histtype = "step")
        ax.errorbar(x, h, yerr = np.sqrt(h), fmt = '.',capsize = 3, label = label)

        if despine:
            sns.despine()

    def plot_loss_and_acc(loss, acc, title, runtype, log = False, ax1 = None, text = ""):
        if ax1 is None:
            fig, ax1 = plt.subplots()

        # print(loss.shape,acc.shape)

        b, g = "royalblue", "seagreen"
        ax1.set_xlabel('Epochs')
        ax1.set_ylabel('Loss', color = b)
        # ax1.plot(loss, label = "Loss", color = b)
        ax1.fill_between([i for i in range(len(loss[0]))], np.min(loss,axis = 0), np.max(loss,axis = 0), color = b, alpha = 0.6)
        ax1.tick_params(axis='y')
        ax1.set_ylim(0,np.max(loss))
        ax1.plot(np.mean(loss,axis = 0), '--', color = b)
        if log:
            ax1.set_xscale("log")

        ax2 = ax1.twinx()  # instantiate a second axes that shares the same x--axis

        ax2.set_ylabel('Validation accuracy', color = g)
        # ax2.plot(acc, label = "Accuracy", color = g)
        ax2.fill_between([i for i in range(len(acc[0]))], np.min(acc,axis = 0), np.max(acc,axis = 0), color = g, alpha = 0.6)
        ax2.set_ylim(0.5,1)
        ax2.plot(np.mean(acc,axis = 0), '--', color = g)
        ax2.tick_params(axis='y')

        ax1.set_title(title)
        ax1.text(np.log(len(loss[0]))/6,0.05, text)

def printshape(*args):
    try:
        print([a.shape for a in args])
    except:
        try:
            print(len(arr))
        except:
            print("Input is neither array nor list")

    

def jitter(values,amount = 0.3):
    return values + np.random.normal(0,amount*values,values.shape)


def kdn(k,n = 6, n_repeats = 1):
    """
    Simulates rolling k n-sided dice n_repeat times.
    Returns a numpy array of shape (n_repeats, k)
    """
    return np.random.randint(1,n,(n_repeats, k))


def coords(n):
    return product(range(n), repeat = 2)
# def zoom_inset(x,y,ax, x_range = [], size = "30%",loc = "center left",label = "",ls = "-"):
#     axins = inset_axes(ax, size, size ,loc=loc, borderpad=1)

#     axins.plot(sizes,avg_times_normal,ls=ls,label = label)

#     if len(x_range) > 0:
#         axins.set_xlim(x_range[0],x_range[1])

#     mark_inset(ax,axins,1,3,color = "black",fc="none", ec="0.5")

#     return axins

# def plot_and_inset(x,y,ax = None, x_range = [], size = "30%",loc = "center left",label = "",ls = "-"):
#     flotplot(x,y,label = label,ax = ax)

#     axins = inset_axes(ax, size, size ,loc=loc, borderpad=1)

#     axins.plot(sizes,avg_times_normal,ls='-',label = label)

#     if len(x_range) > 0:
#         axins.set_xlim(x_range[0],x_range[1])

#     mark_inset(ax,axins,1,3,color = "black",fc="none", ec="0.5")

#     return axins

    
