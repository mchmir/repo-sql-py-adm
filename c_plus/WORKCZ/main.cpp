#define UNICODE
// используем  Unicode-версии функций Windows API
// (например, CreateProcessW вместо CreateProcessA).
// Это позволяет использовать строки wstring (тип wchar_t*) для функций API.
#include <iostream>
#include <windows.h>
#include <string>
#include <filesystem>
#include <cstdlib> // Для system()
#include "myH.h"

using namespace std;

// Connection data
const string NETWORKPATH = R"(\\10.0.4.64\mikhailchm)";
const string DRIVELETTER = "Y:";


void lineGreen() {
    cout <<endl;
    cout << "\033[1;32m"; // Установить зеленый цвет текста
    cout << "----------------------------------\n";
    cout << "\033[0m"; // Сброс цвета текста
}


void displayMenu() {
    system("cls"); // Очистка экрана (для Windows)
    cout << "\033[1;32m"; // Установить зеленый цвет текста
    cout << "Setting up a workstation:\n";
    cout << "----------------------------------\n";
    cout << "0 - JUST GO OUT. I've changed my mind...\n";
    cout << "----------------------------------\n";
    cout << "1 - Connect only a network drive\n";
    cout << "2 - Activate the workstation\n";
    cout << "3 - Deactivate the workstation\n";
    cout << "----------------------------------\n";
    cout << "4 - Disconnect the network drive\n";
    cout << "5 - Reconnect the network drive (the drive will be disabled and enabled)\n";
    cout << "----------------------------------\n";
    cout << "6 - Connect CZ locale\n";
    cout << "7 - Connect the KZ locale\n";
    cout << "----------------------------------\n";
    cout << "Select the option (1, 2, 3, 4 [special points 5, 6, 7, 8 ]): ";
    cout << "\033[0m"; // Сброс цвета текста
}

/**
 * Присоединение сетевого диска Y
 */
void connectNetworkDrive() {
    cout << "Connecting a network drive...\n";

    const string command = "net use " + DRIVELETTER + " " + NETWORKPATH;
    if (system(command.c_str()) == 0) {
        cout << "Network drive connected to " << DRIVELETTER << endl;
    } else {
        cerr << "Connection error!" << endl;
    }
}

/**
 * Отсоединение сетевого диска Y
 */
void disconnectNetworkDrive() {
    // Disconnect the network drive
    cout << "Disconnecting a network drive...\n";
    const string command = "net use " + DRIVELETTER + " /delete /y";
    if (system(command.c_str()) == 0) {
        cout << "Network drive " << DRIVELETTER << " disconnected." << endl;
    } else {
        cerr << "Disconnection error!" << endl;
    }
}

/**
 * Загрузка REG файла
 */
void LoadRegistryFile(const string& filePath) {
    // Convert file path to wstring for Unicode commands
    const wstring wFilePath(filePath.begin(), filePath.end());
    wstring command = L"cmd.exe /C reg import \"" + wFilePath + L"\"";

    // Check if the file exists
    if (!filesystem::exists(wFilePath)) {
        cerr << "Error: registry file not found: " << filePath << endl;
        return;
    }

    // Setup for CreateProcess
    STARTUPINFO si = { sizeof(si) };
    PROCESS_INFORMATION pi;
    ZeroMemory(&pi, sizeof(pi));

    // Start the command using CreateProcess
    if (CreateProcess(nullptr, &command[0], nullptr, nullptr, FALSE, CREATE_NO_WINDOW, nullptr, nullptr, &si, &pi)) {
        // Waiting for the process to complete
        WaitForSingleObject(pi.hProcess, INFINITE);

        DWORD exitCode;
        GetExitCodeProcess(pi.hProcess, &exitCode);
        CloseHandle(pi.hProcess);
        CloseHandle(pi.hThread);

        // Проверяем, равен ли код завершения 0, что указывает на успешное выполнение команды.
        // Если успешно, выводим сообщение и отправляем системе сообщение WM_SETTINGCHANGE через SendMessageTimeout,
        // чтобы уведомить другие приложения об изменениях в системных настройках (в данном случае — настройках региональных форматов).
        if (exitCode == 0) {
            cout << "The registry file has been successfully uploaded: " << filePath << endl;
            // Notify the system about setting changes
            SendMessageTimeout(HWND_BROADCAST, WM_SETTINGCHANGE, 0, reinterpret_cast<LPARAM>(L"intl"), SMTO_ABORTIFHUNG, 100, nullptr);
        } else {
            cerr << "Failed to load the registry file. Exit code: " << exitCode << endl;
        }
    } else {
        // Getting the error code for CreateProcess
        const DWORD error = GetLastError();
        cerr << "CreateProcess failed with error code: " << error << endl;
    }
}

/**
 * Загрузка REG файла files\cs-CZ.reg
 */
void configureLocaleCZ() {
    cout << "Setting the language CZ-locale...\n";
    LoadRegistryFile("files\\cs-CZ.reg");
}

/**
 * Загрузка REG файла files\ru-KZ.reg
 */
void configureLocaleKZ() {
    cout << "Setting the language KZ-locale...\n";
    LoadRegistryFile("files\\ru-KZ.reg");
}


bool askToContinue() {
    cout << "\nDo you want to continue? (press 1 to continue, Enter to exit): ";
    string input;
    getline(cin, input); // Считываем строку

    return input == "1"; // Возвращаем true, если введено "1"
}


int main() {

    while (true) {
        system("cls"); // Очистить экран
        cout << "[DEBUG] Displaying menu...\n";
        displayMenu();

        int choice;
        cin >> choice;

        lineGreen();

        // Проверка некорректного ввода
        if (cin.fail()) {
            cin.clear(); // Сбросить ошибку
            cin.ignore(numeric_limits<streamsize>::max(), '\n'); // Очистить буфер ввода
            choice = -1; // Установить choice в некорректное значение
            cout << "[DEBUG] Incorrect input, choice = -1\n";
        }

        // Выполнение действий
        switch (choice) {
            case 1: // Connect only a network drive
                connectNetworkDrive();
                break;
            case 2: // Activate the workstation
                connectNetworkDrive();
                configureLocaleCZ();
                break;
            case 3: // Deactivate the workstation
                disconnectNetworkDrive();
                configureLocaleKZ();
                break;
            case 4: // Disconnect the network drive
                disconnectNetworkDrive();
                break;
            case 5: // Reconnect the network drive
                disconnectNetworkDrive();
                connectNetworkDrive();
                break;
            case 6: // Connect CZ locale
                configureLocaleCZ();
                break;
            case 7: // Connect KZ locale
                configureLocaleKZ();
                break;
            case 0: // Exit the program
                cout << "The program is over. Goodbye!\n";
                return 0;
            default:
                cout << "\033[1;31mInvalid selection. Try again.\033[0m\n";
                continue; // Возврат в меню
        }

        // Очистка буфера ввода после использования cin
        cin.ignore(numeric_limits<streamsize>::max(), '\n');

        // Запрос на продолжение
        if (!askToContinue()) {
            cout << "The program is over. Goodbye!\n";
            break;
        }
    }

    return 0;
}

