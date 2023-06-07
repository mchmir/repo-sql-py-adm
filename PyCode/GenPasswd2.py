# -*- coding: utf-8 -*-
import random

def passw_generator(count_char = 8):
    arr = ['a','b','c','d','e','f','g','h','i','j','k','l','m',
	'n','o','p','q','r','s','t','u','v','w','x','y','z','A','B','C','D','E',
	'F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','W','X','Y','Z']
    arr2 = ['0','1','2','3','4','5','6','7','8','9']
    arr3 = ['_','!','@','#','$','%','^','&','*','(',')','-','+','=']
    passw = []

    for i in range(5):
        passw.append(random.choice(arr))
    for i in range(2):
        passw.append(random.choice(arr2))
    for i in range(1):
        passw.append(random.choice(arr3))
	# перемешиваем список
    random.shuffle(passw)
    return "".join(passw)

print( passw_generator())
input()
