// Euler12.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <vector>
#include <iostream>
#include <math.h>
using namespace std;

bool contains(vector<int> a, int b)
{
	for(int x = 0; x < a.size(); x++)
	{
		if(a[x] == b)
			return true;
	}
	return false;
}

int _tmain(int argc, _TCHAR* argv[])
{
	int num = 0;
	for(int x = 4; true; x++)
	{
		double iy = x * 0.5;
		double ix = (x + 1) * iy;
		int i = (int)ix;
		int limit = (int)floor(sqrt((double)i));
		bool primes [1000000];
		vector<int> plist;
		for(int y = 2; y <= limit; y++)
		{
			if(primes[y])
			{
				plist.push_back(y);
				for(int z = y + y; z <= limit; z+=y)
				{
					primes[z] = false;
				}
			}
		}
		int fx = 1;
		int fy = 1;
		int t = i;
		for (int z = 0; z < plist.size(); z++)
        {
            if (t % plist[z] == 0)
            {
                for (; t % plist[z] == 0; )
                {
                    t /= plist[z];
					fy++;
                }
            }
			fx *= fy;
			fy = 1;
		}
		if(fx + 2 > 500)
		{
			num = i;
			break;
		}
	}
	cout << num;
	int v = 0;
	cin >> v;
	return 0;
}

