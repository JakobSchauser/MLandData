


def GetkthKey(x, k):
    if k > x.size or k < 0:
        return nil
    else if k == x.size:
        return x

    if x['left'].size == k:
      return x['left']
    elif x['left'].size < k:
        return GetkthKey(x['right'], k - x['left'].size)
    return GetkthKey(x['left'],k)

