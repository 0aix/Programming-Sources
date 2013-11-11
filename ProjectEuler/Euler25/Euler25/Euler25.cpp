// Euler25.cpp : Defines the entry point for the console application.
//
#include "stdafx.h"
#include <iostream>
using namespace std;

int main()
{
	int num1[220];
	int num2[220];
	int num3[220];
	
	for(int x = 0; x < 220; x++)
	{
		num1[x] = 0;
		num2[x] = 0;
		num3[x] = 0;
	}

	int counter = 2;
	num1[0] = 1;
	num2[0] = 1;
	while(true)
	{
		for(int x = 0; x < 220; x++)
		{
			num3[x] = num1[x] + num2[x];
			num1[x] = num2[x];
			num2[x] = num3[x];
		}
		for(int y = 0; y < 219; y++)
		{
			for(; num1[y] > 99999; )
			{
				num1[y] -= 100000;
				num1[y + 1]++;
			}
			for(; num2[y] > 99999; )
			{
				num2[y] -= 100000;
				num2[y + 1]++;
			}
			for(; num3[y] > 99999; )
			{
				num3[y] -= 100000;
				num3[y + 1]++;
			}
		}
		counter++;
		if(num3[199] > 9999)
			break;
	}
	cout << counter;
	char c[1];
	cin >> c;
	return 0;
}