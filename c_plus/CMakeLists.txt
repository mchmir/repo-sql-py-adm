cmake_minimum_required(VERSION 3.29)
project(c_plus)

set(CMAKE_CXX_STANDARD 20)

add_executable(region_format region_format.cpp
        ExampleWindowsAPI.cpp)
add_executable(ExampleWindowsAPI ExampleWindowsAPI.cpp)
