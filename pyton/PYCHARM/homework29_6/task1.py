import random


# Функция bubble_sort принимает список целых чисел
# data и сортирует его в порядке убывания элементов с
# помощью пузырьковой сортировки. Кроме того, функция
# должна посчитать количество операций (итераций цикла),
# которые выполняет алгоритм, и вернуть это число вызывающей
# стороне.
# делает n проходов по n итераций в каждом
def bubble_sort(data):

    iteration = 0
    for i in range(len(data)):
        for j in range(len(data) - 1):
            if data[j] < data[j + 1]:
                data[j], data[j + 1] = data[j + 1], data[j]
            iteration += 1
        iteration += 1
    print(f'Количество итераций: {iteration}')
    return data


# Итерация заканчивается, когда самый большой элемент списка оказывается в конце списка.
# В первый раз мы передаем самый большой элемент в конечную позицию, используя позицию n.
# Во второй раз мы проходим через позицию n-1, второй по величине элемент.
# На каждой последующей итерации мы можем сравнивать на один элемент меньше, чем раньше.
# Точнее, на k-й итерации нужно сравнить только первые n – k + 1 элементов:
def bubble_sort_2(data):
    iteration = 0
    for i in range(len(data)-1):
        for j in range(len(data) - i - 1):
            if data[j] < data[j + 1]:
                data[j], data[j + 1] = data[j + 1], data[j]
            iteration += 1

    print(f'Количество итераций: {iteration}')
    return data


def bubble_sort_3(data, flag: int) -> int:
    iteration = 0
    for i in range(len(data) - 1):
        for j in range(len(data) - i - 1):
            if data[j] < data[j + 1]:
                data[j], data[j + 1] = data[j + 1], data[j]
            iteration += 1
    return iteration


def test_sorted():
    data = [random.randint(0, 1000) for i in range(100)]
    data_to_sort = data.copy()
    bubble_sort(data_to_sort)
    if data_to_sort == sorted(data, reverse=True):
        print('OK')
    else:
        print('NOT OK')


def make_observations():
    size = 10
    results = []
    for i in range(100):
        data = [random.randint(0, 1000) for i in range(size)]
        results.append((size, bubble_sort(data)))
        size += 10
    return results


def main():
    test_sorted()
    with open('bubble.csv', 'w') as file:
        file.write(f'size, iterations\n')
        for row in make_observations():
            file.write(f'{row[0]}, {row[1]}\n')
    print('Done!')


if __name__ == '__main__':
    main()