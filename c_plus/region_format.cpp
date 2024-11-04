#define UNICODE
// используем  Unicode-версии функций Windows API
// (например, CreateProcessW вместо CreateProcessA).
// Это позволяет использовать строки wstring (тип wchar_t*) для функций API.
#include <iostream>
#include <windows.h>
#include <string>
#include <limits>
#include <filesystem>

using namespace std;


void pause() {
    cout << "Press Enter to continue...";
    cin.ignore(numeric_limits<streamsize>::max(), '\n'); // Clear the buffer
    cin.get(); // Waiting for Enter
}


void LoadRegistryFile(const string& filePath) {
    // Convert file path to wstring for Unicode commands
    const wstring wFilePath(filePath.begin(), filePath.end());
    wstring command = L"cmd.exe /C reg import \"" + wFilePath + L"\"";

    // Check if the file exists
    if (!filesystem::exists(wFilePath)) {
        cerr << "Error: registry file not found: " << filePath << endl;
        pause();
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
    pause();
}

int main() {
    string input;
    cout << "Enter 'work' for Czech format or any other word for Russian format: ";
    cin >> input;

    if (input == "work") {
        LoadRegistryFile("files\\cs-CZ.reg");
    } else {
        LoadRegistryFile("files\\ru-KZ.reg");
    }

    return 0;
}
