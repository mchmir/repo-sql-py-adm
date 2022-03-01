#!/usr/bin/env python
# -*- coding: utf-8 -*-
#
#  test_ini1.py
#  
#  Copyright 2020 mchmi <mchmi@DESKTOP-VBIAJ1G>
#  
#
import configparser
import os, sys

def get_config(path):
    """
    Returns the config object
    """
    if not os.path.exists(path):
        print("нет такого файла:" + str(path))
    
    config = configparser.ConfigParser()
    config.read(path)
    return config
 
 
def get_setting(path, section, setting):
    """
    Print out a setting
    """
    config = get_config(path)
    value = config.get(section, setting)
    msg = "{section} {setting} is {value}".format(
        section=section, setting=setting, value=value
    )
    
    print(msg)
    return value



if __name__ == "__main__":
    path = r"setting\totalInfo.ini"
    vid = get_setting(path, 'Input', 'id')
    print(vid)
