#!/usr/bin/env python
# -*- coding: utf-8 -*-
#
#  test_ini2.py
#  
#  Copyright 2020 mchmi <mchmi@DESKTOP-VBIAJ1G>
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
    config.read(path, encoding='utf-8')
    return config
 

def ConfigSectionMap(path,section):
    dict1 = {}
    Config = get_config(path)
    options = Config.options(section)
    for option in options:
        try:
            dict1[option] = Config.get(section, option)
            if dict1[option] == -1:
                DebugPrint("skip: %s" % option)
        except:
            print("exception on %s!" % option)
            dict1[option] = None
    return dict1

def ReturnListSection(path):
    
    Config = get_config(path)
    options = Config.sections()
    
    return options
    
if __name__ == "__main__":
    path = r"setting\totalInfo.ini"
    vid = ConfigSectionMap(path,"Input")
    options = ReturnListSection(path)
    print(options)
    print(vid)
    
