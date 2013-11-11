// Euler18.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <vector>
using namespace std;

int netpyramid[] = {75,
					95,64,
					17,47,82,
					18,35,87,10,
					20,04,82,47,65,
					19,01,23,75,03,34,
					88,02,77,73,07,63,67,
					99,65,04,28,06,16,70,92,
					41,41,26,56,83,40,80,70,33,
					41,48,72,33,47,32,37,16,94,29,
					53,71,44,65,25,43,91,52,97,51,14,
					70,11,33,28,77,73,17,78,39,68,17,57,
					91,71,52,38,17,14,91,43,58,50,27,29,48,
					63,66,04,68,89,53,67,30,73,16,69,87,40,31,
					04,62,98,27,23,9,70,98,73,93,38,53,60,04,23};

int Row(int i)
{
	int x = 0;
	for(int y = 0; y < i; y++)
		x += y;
	return x;
}

int _tmain(int argc, _TCHAR* argv[])
{
	for(int x = 14; x > 0; x--)
	{
		for(int y = x - 1; y >= 0; y--)
		{
			if(netpyramid[Row(x + 1) + y] > netpyramid[Row(x + 1) + y + 1])
			{
				netpyramid[Row(x) + y] += netpyramid[Row(x + 1) + y];
			}
			else
			{
				netpyramid[Row(x) + y] += netpyramid[Row(x + 1) + y + 1];
			}
		}
	}
	cout << netpyramid[0];
	char c;
	cin >> c;
	return 0;
}