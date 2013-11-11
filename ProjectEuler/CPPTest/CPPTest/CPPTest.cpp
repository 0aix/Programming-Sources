// CPPTest.cpp : Defines the entry point for the console application.
//
#include "stdafx.h"
#include <iostream>
using namespace std;

int _tmain(int argc, _TCHAR* argv[])
{
	double month;
	while(true)
	{
		try
		{
			cout << "Enter a month (use a 1 for Jan, etc.):\n";
			cin >> month;
			if (month - (int)month != 0)
				throw 0;
			if (month < 1 || month > 12)
				throw 0;
			break;
		}
		catch (...) 
		{ 
			cout << "Invalid month\n"; 
			cin.clear();
			cin.ignore(INT_MAX, '\n');
		}
	}
	int months[] = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
	double day;
	while(true)
	{
		try
		{
			cout << "Enter a day of the month:\n";
			cin >> day;
			if (day - (int)day != 0)
				throw 0;
			if (day < 1 || day > months[(int)month])
				throw 0;
			break;
		}
		catch (...) 
		{ 
			cout << "Invalid day\n"; 
			cin.clear();
			cin.ignore(INT_MAX, '\n');
		}
	}
	system("pause");
	return 0;
}

