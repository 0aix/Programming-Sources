// stdafx.h : include file for standard system include files,
// or project specific include files that are used frequently, but
// are changed infrequently
//

#pragma once

#include "targetver.h"

#define WIN32_LEAN_AND_MEAN             // Exclude rarely-used stuff from Windows headers
#include <windows.h>
#include <vector>
#include <random>
#include <cstdlib>
#include <iostream>
#include <fstream>
using namespace std;

extern vector<DWORD> FilterList;
extern int Mesos;
extern bool itemfilter;
extern bool tubi;
extern bool mesofilter;
extern bool mining;
extern bool kamiloot;
extern bool pgm;
extern bool freeze;
extern bool deaggro;
extern void TogglePacket();
extern void ToggleMining();
extern void ToggleGND();
extern void ToggleMC();
extern void ToggleCPU();
extern void NoDelay();
extern int Main();
extern void UpdateFilter();