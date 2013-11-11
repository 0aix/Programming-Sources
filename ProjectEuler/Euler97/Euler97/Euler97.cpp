// Euler97.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
using namespace std;

int _tmain(int argc, _TCHAR* argv[])
{
	_int64 product = 1;
	for(int x = 0; x < 7830457; x++)
	{
		product *= 2;
		for(; product > 9999999999; )
			product -= 10000000000;
	}
	product *= 28433;
	product++;
	for(; product > 9999999999; )
			product -= 10000000000;
	cout << product;
	char c[1];
	cin.getline(c, 1);
	return 0;
}

