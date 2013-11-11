// Sieve.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <vector>
using namespace std;

vector<int> plist;
bool primes [100000000];

int _tmain(int argc, _TCHAR* argv[])
{
	while(true)
	{
		int c = 0;
		while(true)
		{
			cout << "Please enter the integer you'd like to search up to (max 100,000,000): \n";
			cout << "(Input -1 to exit this application.)\n";
			cin >> c;
			if(c == -1)
				return 0;
			if(c > 2)
				break;
		}
		for(int x = 2; x < c; x++)
		{
			if(!primes[x])
			{
				plist.push_back(x);
				for(int z = x + x; z < c; z+=x)
				{
					primes[z] = true;
				}
			}
		}
		cout << plist.size() << " primes below " << c << " generated.\n";
		while(true)
		{
			cout << "Please input an integer to represent which number prime you'd like to receive. (under " << plist.size() << ")\n";
			cout << "(Input -1 to exit this loop.)\n";
			int v = 0;
			cin >> v;
			if(v == -1)
				break;
			if(v > 0 && v <= plist.size())
			{
				cout << "The prime is " << plist[v - 1] << ".\n";
			}
		}
	}
	return 0;
}

