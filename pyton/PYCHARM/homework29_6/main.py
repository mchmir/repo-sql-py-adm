# This is a sample Python script.

# Press Shift+F10 to execute it or replace it with your code.
# Press Double Shift to search everywhere for classes, files, tool windows, actions, and settings.
from random import randint
import csv

import task1


def print_hi(name):
    # Use a breakpoint in the code line below to debug your script.
    print(f'Hi, {name}')  # Press Ctrl+F8 to toggle the breakpoint.


# Press the green button in the gutter to run the script.
if __name__ == '__main__':
    # print(task1.bubble_sort(list_of_nums))
    # print(task1.bubble_sort_2(list_of_nums_2))
    list_of_nums = []

    with open("bubble.csv", mode="w", encoding='utf-8') as wf_csv:
        file_wr = csv.writer(wf_csv, delimiter=",", lineterminator="\r")
        file_wr.writerow(["nn", "size", "iterations"])
        nn = 0
        for i in range(1, 101):
            for j in range(i*10):
                list_of_nums.append(randint(1, 99))
            file_wr.writerow([nn, i*10, task1.bubble_sort_3(list_of_nums, 0)])
            nn += 1
            list_of_nums = []


# See PyCharm help at https://www.jetbrains.com/help/pycharm/
