#!/usr/bin/env python
# -*- coding: utf-8 -*-
#
import test4
print(test4.__doc__)
print(test4.func.__doc__)

input()


def main(args):
    return 0

if __name__ == '__main__':
    import sys
    sys.exit(main(sys.argv))
