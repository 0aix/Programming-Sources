// Euler28.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
using namespace std;

int _tmain(int argc, _TCHAR* argv[])
{
	int sum = 0; 
	int change = 0; 
	int add = 2; 
	for(int x = 1; x <= 1002001; ) 
	{ 
		sum += x; 
		if(change < 4) 
		{
			change++; 
		} 
		else 
		{ 
			add += 2; 
			change = 1; 
		} 
		x += add; 
	} 
	cout << sum;
	char c;
	cin >> c;
	return 0;
}

