import numpy as np 
import matplotlib.pyplot as plt
import seaborn as sns
sns.set_style("white")



"""
    IDEAS:

    
    Webscraping?






"""
standard_figsize = (10,5)
despine = True

class plot:
    def __init__(self):
        pass

    def points(x,y,label = None,ax = None):

        if not ax:
            fig, ax = plt.subplots(1,1,figsize = standard_figsize)

        ax.plot(x,y,'.',label = label)

        if despine:
            sns.despine()

    
    def line(x,y,label = None,ax = None):

        if not ax:
            fig, ax = plt.subplots(1,1,figsize = standard_figsize)

        ax.plot(x,y,'-',label = label)

        if despine:
            sns.despine()

    def flot(x,y,label = None,ax = None):

        if not ax:
            fig, ax = plt.subplots(1,1,figsize = standard_figsize)

        ax.plot(x,y,'.',label = label)
        ax.plot(x,y,'-')

        if despine:
            sns.despine()

    def error(x,y,yerr,label = None,ax = None):

        if not ax:
            fig, axs = plt.subplots(1,1,figsize = standard_figsize)


        ax.fill_between(x,np.maximum(0,y-yerr),y+yerr,alpha=0.4)
        ax.plot(x,y,'--',label = label)

        if despine:
            sns.despine()


    def hist(y,x = [],n = 19,label = None,ax = None):
        if not ax:
            fig, ax = plt.subplots(1,1,figsize = standard_figsize)

        h, xx = np.histogram(y,bins = n)
        print(h)
        if len(x) == 0:
            x = (xx[1:]+xx[:-1])/2

            
        ax.hist(y, bins = n,histtype = "step")
        ax.errorbar(x, h, yerr = np.sqrt(h), fmt = '.',capsize = 3, label = label)

        if despine:
            sns.despine()


def jitter(values,amount = 0.3):
    return values + np.random.normal(0,0.3*values,values.shape)


def kdn(k,n = 6, n_repeats = 1):
    """
    Simulates rolling k n-sided dice n_repeat times.
    Returns a numpy array of shape (n_repeats, k)
    """
    return np.random.randint(1,n,(n_repeats, k))

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

    
