import task1

from random import randint

N = 10
list_of_nums = []
list_of_nums_2 = []
for i in range(N):
    list_of_nums.append(randint(1, 99))
    list_of_nums_2 = list_of_nums.copy()

print(list_of_nums)
print(task1.bubble_sort(list_of_nums))
print(task1.bubble_sort_3(list_of_nums_2,0))