import numpy as np 
import matplotlib.pyplot as plt
import seaborn as sns
sns.set_style("white")


def errorplot(x,y,yerr,label = None,ax = None):

    if not ax:
        fig, ax = plt.subplot(1,1,(12,8))

    ax.fill_between(x,np.maximum(0,y-yerr),y+yerr,alpha=0.4)
    ax.plot(x,y,'--',label = label)
    sns.despine()

def flotplot(x,y,label = None,ax = None):

    if not ax:
        fig, ax = plt.subplot(1,1,(12,8))

    ax.plot(x,y,'.',label = label)
    ax.plot(x,y,'-')
    sns.despine()

def jitter(values,amount = 0.3):
    return values + np.random.normal(0,0.3*values,values.shape)


def zoom_inset(x,y,ax, x_range = [], size = "30%",loc = "center left",label = "",ls = "-"):
    axins = inset_axes(ax, size, size ,loc=loc, borderpad=1)

    axins.plot(sizes,avg_times_normal,ls=ls,label = label)

    if len(x_range) > 0:
        axins.set_xlim(x_range[0],x_range[1])

    mark_inset(ax,axins,1,3,color = "black",fc="none", ec="0.5")

    return axins

def plot_and_inset(x,y,ax = None, x_range = [], size = "30%",loc = "center left",label = "",ls = "-"):
    flotplot(x,y,label = label,ax = ax)

    axins = inset_axes(ax, size, size ,loc=loc, borderpad=1)

    axins.plot(sizes,avg_times_normal,ls='-',label = label)

    if len(x_range) > 0:
        axins.set_xlim(x_range[0],x_range[1])

    mark_inset(ax,axins,1,3,color = "black",fc="none", ec="0.5")

    return axins

    

x = [i for i in range(100)]

plot_and_inset(x, x)


