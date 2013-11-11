// Euler21.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <vector>
using namespace std;

int pdiv[10000] = {0};

int Divisors(int i)
{
	int sum = 1;
	int z = i;
	for(int x = 2; x < z; x++)
	{
		if(i % x == 0)
		{
			sum += x;
			if(i / x != x)
			{
				sum += i / x;
			}
			z = i / x;
		}
	}
	return sum;
}

int IndexOf(int v[], int i, int r)
{
	int z = 0;
	for(int x = 0; x < 10000; x++)
	{
		if(v[x] == i)
		{
			z++;
			if(z == r)
				return x;
		}
	}
	return -1;
}

int _tmain(int argc, _TCHAR* argv[])
{
	_int64 sum = 0;
	for(int x = 1; x < 10000; x++)
	{
		for(int y = 1; y < 5; y++) //Should be dynamic
		{
			int i = IndexOf(pdiv, x, y);
			if(i > -1 && Divisors(x) == i + 1) 
			{
				sum += x;
				sum += i + 1;
			}
			pdiv[x - 1] = Divisors(x);
		}
	}
	cout << sum;
	char c;
	cin >> c;
	return 0;
}

