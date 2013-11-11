// Euler48.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
using namespace std;

int _tmain(int argc, _TCHAR* argv[])
{
	_int64 sum = 0;
	_int64 prod = 1;
	for(int x = 1; x <= 1000; x++)
	{
		prod = 1;
		for(int i = 0; i < x; i++)
		{
			prod *= x;
			for(; prod > 9999999999; )
				prod -= 10000000000;
		}
		sum += prod;
		for(; sum > 9999999999; )
				sum -= 10000000000;
	}
	cout << sum;
	char c;
	cin >> c;
	return 0;
}

