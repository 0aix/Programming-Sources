// Euler15.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
using namespace std;

_int64 paths = 0;

void Move(int x, int y)
{
	int n = x + y;
	int k = y;
	_int64 nx = 1;
	_int64 ny = 1;
	if(n != 0)
	{
		int i2 = 1;
		for(int i = y + 1; i <= n; i++)
		{
			nx *= i;
			if(i2 <= n - k)
			{
				ny *= i2;
				if(nx % i2 == 0)
				{
					nx /= i2;
					ny /= i2;
				}
				if(nx % ny == 0)
				{
					nx /= ny;
					ny /= ny;
				}
				i2++;
			}
		}
	}
	paths = nx / ny;
}

int _tmain(int argc, _TCHAR* argv[])
{
	cout << "Generate paths from x = 0, y = 0 to: " << endl;
	int x = 0;
	int y = 0;
	cout << "X: ";
	cin >> x;
	cout << "Y: ";
	cin >> y;
	Move(x, y);
	cout << paths;
	char c[1];
	cin >> c;
	return 0;
}

