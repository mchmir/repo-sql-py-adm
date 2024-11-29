cmake_minimum_required(VERSION 3.29)
project(WORKCZ)

set(CMAKE_CXX_STANDARD 20)

add_executable(WORKCZ main.cpp myH.h)

# Устанавливаем другое имя для создаваемого исполняемого файла
set_target_properties(WORKCZ PROPERTIES OUTPUT_NAME "workcz")