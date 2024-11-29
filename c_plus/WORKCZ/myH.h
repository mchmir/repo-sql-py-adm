#pragma once
#include <iostream>
#include <string>
#include <vector>
#include <algorithm>
#include <cmath>
#include <sstream>

using namespace std;


inline void pauseU() {
    cout << "Press Enter to continue...";
    cin.ignore(numeric_limits<streamsize>::max(), '\n'); // Clear the buffer
    cin.get(); // Waiting for Enter
}

inline void pauseWin() {
    /*
     * Only works in Windows
     */
    system("pause"); // Stops the console and waits for any key to be pressed
}

inline void pause() {
// определен для всех версий Windows (32 и 64 бита).
#ifdef _WIN32
    pauseWin(); // Вызов pauseWin для Windows
#else
    pauseU();    // Вызов pause для других ОС
#endif
}


inline void keep_window_open()
{
    cin.clear();
    cout << "Please enter a character to exit\n";
    char ch;
    cin >> ch;
}

inline void keep_window_open(const string& s)
{
    if (s.empty()) return;
    cin.clear();
    cin.ignore(120,'\n');
    for (;;) {
        cout << "Please enter " << s << " to exit\n";
        string ss;
        while (cin >> ss && ss!=s)
            cout << "Please enter " << s << " to exit\n";
        return;
    }
}


// error() simply disguises throws:
inline void error(const string& s)
{
    throw runtime_error(s);
}

inline void error(const string& s, const string& s2)
{
    error(s+s2);
}

inline void error(const string& s, const int i)
{
    ostringstream os;
    os << s <<": " << i;
    error(os.str());
}



