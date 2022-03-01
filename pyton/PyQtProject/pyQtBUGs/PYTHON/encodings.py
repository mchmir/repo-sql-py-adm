#!/usr/bin/env python
# -*- coding: utf-8 -*-
#
import encodings.aliases

arr = encodings.aliases.aliases
keys = list( arr.keys() )
keys.sort()
for key in keys:
	print("%s => %s" % (key, arr[key]))

# arr = sys.argv[:]  аргументы командной строки


def main(args):
    return 0

if __name__ == '__main__':
    import sys
    sys.exit(main(sys.argv))
