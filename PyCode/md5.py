# Python 3 code to demonstrate the 
# working of MD5 (string - hexadecimal) 

import hashlib 


# initializing string 
str = "Diablo666"

# encoding GeeksforGeeks using encode() 
# then sending to md5() 
result = hashlib.md5(str.encode('UTF-8')) 

# printing the equivalent hexadecimal value. 
print("Обычный MD5 хеш строки : ", end ="") 
print(result.hexdigest()) 


def getMD5(text):
    encoding = text.encode("UTF-16LE")
    md5 = hashlib.md5()
    md5.update(encoding)
    return md5.hexdigest()
    
    
strR = getMD5("Diablo666")
print("С# MD5 : ", end ="") 
print(strR)