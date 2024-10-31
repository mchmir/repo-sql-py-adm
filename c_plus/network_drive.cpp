#include <iostream>
#include <cstdlib>
#include <limits> // Connect header for numeric_limits

using namespace std;

void pauseWin() {
    /*
     * Only works in Windows
     */
    system("pause"); // Stops the console and waits for any key to be pressed
}

void pause() {
    cout << "Press Enter to continue...";
    cin.ignore(numeric_limits<streamsize>::max(), '\n'); // Clear the buffer
    cin.get(); // Waiting for Enter
}

void manageNetworkDrive() {
    string action;
    cout << "Enter 'SWITCH' to connect the network drive or 'OFF' to disconnect: ";
    cin >> action;

    // Connection data
    const string networkPath = R"(\\10.0.4.64\mikhailchm)";
    const string driveLetter = "Y:";

    if (action == "SWITCH") {
        // Connect a network drive
        const string command = "net use " + driveLetter + " " + networkPath;
        if (system(command.c_str()) == 0) {
            cout << "Network drive connected to " << driveLetter << endl;
            pause();
        } else {
            cerr << "Connection error!" << endl;
            pause();
        }
    } else {
        // Disconnect the network drive
        const string command = "net use " + driveLetter + " /delete /y";
        if (system(command.c_str()) == 0) {
            cout << "Network drive " << driveLetter << " disconnected." << endl;
            pause();
        } else {
            cerr << "Disconnection error!" << endl;
            pause();
        }
    }
}

int main() {
    manageNetworkDrive();
    return 0;
}
