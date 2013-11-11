// Euler14.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
using namespace std;

int _tmain(int argc, _TCHAR* argv[])
{
	_int64 number = 0;
	int total = 0;
	int numx = 0;
	for(int x = 2; x < 1000000; x++)
	{
		number = x;
		int steps = 0;
		for(; number != 1; )
		{
			if(number % 2 == 0)
				number /= 2;
			else
				number = 3 * number + 1;
			steps++;
		}
		if(steps > total)
		{
			numx = x;
			total = steps;
		}
	}
	cout << numx;
	char c[1];
	cin.getline(c, 1);
	return 0;
}

