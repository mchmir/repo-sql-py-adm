
class MyContextManager:
    def __enter__(self):
        # Логика инициализации или предварительных операций
        print("Вход в контекст")
        # Возвращаем объект, который будет связан с as-переменной
        return self

    def __exit__(self, exc_type, exc_value, traceback):
        # Логика освобождения ресурсов или завершающих операций
        print("Выход из контекста")
        # Можно обработать исключение, если оно было вызвано внутри блока контекста
        if exc_type is not None:
            print(f"Произошло исключение: {exc_type}, {exc_value}")

    def __hash__(self) -> int:
        return super().__hash__()

    def __init__(self) -> None:
        super().__init__()


# Использование
with MyContextManager() as cm:
    print("Внутри контекста")

'''
Вход в контекст
Внутри контекста
Выход из контекста
'''
