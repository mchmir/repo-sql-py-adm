
def xor_cipher( str, key ):
    encript_str = ""
    for letter in str:
        encript_str += chr( ord(letter) ^ key )
    return  encript_str


key  = 87
line = 'Server=ИВАН-ПК\SQLEXPRESS'   
print (xor_cipher(line, key))