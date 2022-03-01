# -*- coding: utf-8 -*-

favorite_language = '   python 123   '
favorite_language.rstrip() # no whitespace exists at the right end of a string
print(favorite_language)
favorite_language.lstrip() # no whitespace exists at the left end of a string
print(favorite_language)
favorite_language.strip() # strip whitespace from both sides at once
print(favorite_language)

age=23
print(str(age)+' года') # by wrapping the variable int to str


motorcycles = ['honda', 'yamaha', 'suzuki']
print(motorcycles)
del motorcycles[0]
print(motorcycles)

motorcycles.pop() # The pop() method removes the last item in a list
print(motorcycles)
last_owned = motorcycles.pop()
print(last_owned.title())

motorcycles = ['honda', 'yamaha', 'ducati', 'suzuki', 'ducati']
motorcycles.remove('ducati')  # удаляет только первое вхождение в список, если такие есть еще, то нужно воспользоваться циклом
print(motorcycles)

motorcycles.insert(0, 'ducati200')
print(motorcycles)

motorcycles.append('yamaha300')
print(motorcycles)

print(sorted(motorcycles)) # просто вывод отсортированного списка
print(motorcycles)

motorcycles.sort() #сортирует список изменяя порядок
print(motorcycles)

motorcycles.reverse()
print(motorcycles)

print(str(len(motorcycles)))

for moto in motorcycles:
	print(moto) 

for value in range(1,6):
	print(value)
	
	
numbers = list(range(1,6))
print(numbers)

even_numbers = list(range(2,11,2)) # от 2 до 10 с шагом 2
print(even_numbers)

# squares = []
# for value in range(1,11):
	# square = value**2       # exponenta 
	# squares.append(square)
# print(squares)         или как указано ниже
squares = [value**2 for value in range(1,11)]
print(squares)

print(squares[1:3])
print(squares[:5])
print(squares[3:6])
print(squares[7:])

squares_copy = squares[:]  # скопировать значения одного списка в другой, только значения не сам список!!
print(squares_copy)

#-----------------------------------------------------------------
metrik = []
metrik.append(min(squares))
metrik.append(max(squares))
metrik.append(sum(squares))
print(metrik)

print("\n")
#-----------------------------------------------------------------
cars = ['audi', 'bmw', 'subaru', 'toyota']
for car in cars:
	if car == 'bmw':
		print(car.upper())
	else:
		print(car.title())
#-----------------------------------------------------------------
auto = 'lada'
if auto not in cars:
	print(auto.title() + " - not in the list of cars!")		

#-----------------------------------------------------------------
print("\n")
age = 12
if age < 4:
	price = 0
elif age < 18:
	price = 5
else:
	price = 10
print("Your admission cost is $" + str(price) + ".")
#-----------------------------------------------------------------
print("\n")                             # Словари
alien_0 = {'color': 'green'}
print("The alien is " + alien_0['color'] + ".")
alien_0['color'] = 'yellow'
print("The alien is now " + alien_0['color'] + ".")

favorite_languages = {
'jen': 'python',
'sarah': 'c',
'edward': 'ruby',
'phil': 'python',
}

print("\n")  
print("Sarah's favorite language is " +
	favorite_languages['sarah'].title() +
	".")

for key, value in favorite_languages.items():
	print("\nKey: " + key)
	print("Value: " + value.title())
	
print("\n") 
	
if 'erin' not in favorite_languages.keys():
	print("Erin, please take our poll!")	

print("\n") 

for name in sorted(favorite_languages.keys()):
	print(name.title() + ", thank you for taking the poll.")

for language in favorite_languages.values():
	print(language.title())

print("\n") 
for language in set(favorite_languages.values()): # identifies the unique items in the list and builds a set from those items 
	                                              #определяет уникальные элементы в списке и создает набор из этих элементов
	print(language.title())

#-----------------------------------------------------------------
print("\n")                             # Словари

favorite_languages = {
'jen': ['python', 'ruby'],
'sarah': ['c'],
'edward': ['ruby', 'go'],
'phil': ['python', 'haskell'],
}

for name, languages in favorite_languages.items():
	print("\n" + name.title() + "'s favorite languages are:")
	for language in languages:
		print("\t" + language.title())



print(list("ЭтоСтрока"))  # превращение строки в список




#-----------------------------------------------------------------
print("\n")                             


users = {
	'aeinstein': {
		'first': 'albert',
		'last': 'einstein',
		'location': 'princeton',
		},
	'mcurie': {
		'first': 'marie',
		'last': 'curie',
		'location': 'paris',
		},
	}

for username, user_info in users.items():
	print("\nUsername: " + username)
	full_name = user_info['first'] + " " + user_info['last']
	location = user_info['location']
	print("\tFull name: " + full_name.title())
	print("\tLocation: " + location.title())


#-----------------------------------------------------------------
print("\n")                             
input()
















