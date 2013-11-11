// Euler16.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <stdlib.h>
#include <vector>
#include <iostream>
using namespace std;

int parse(char c)
{
	char buffer[1];
	buffer[0] = c;
	return atoi(buffer);
}

vector<int> Number(string str)
{
	vector<int> v;
	for(int x = strlen(str.c_str()) - 1; x >= 0; x--)
		v.insert(v.begin(), parse(str[x]));
	return v;
}

vector<int> Add(vector<int> op1, vector<int> op2)
{
	vector<int> sum;
	vector<int> carry;
	int i = 0;
	if(op1.size() < op2.size())
	{
		i = op2.size();
		for(; op1.size() < op2.size();)
			op1.insert(op1.begin(), 0);
	}
	else
	{
		i = op1.size();
		for(; op2.size() < op1.size();)
			op2.insert(op2.begin(), 0);
	}
	int tcarry = 0;
	for(int x = i - 1; x >= 0; x--)
	{
		int tsum = op1.at(x) + op2.at(x) + tcarry;
		tcarry = 0;
		for(; tsum > 9; tsum -= 10)
			tcarry++;
		if(x != 0)
			sum.insert(sum.begin(), tsum);
		else
		{
			sum.insert(sum.begin(), tsum);
			if(tcarry > 0)
				sum.insert(sum.begin(), tcarry);
		}
	}
	return sum;
}

vector<int> Multi(vector<int> op1, vector<int> op2)
{
	vector<int> product;
	int i = op2.size();
	for(int x = i - 1; x >= 0; x--)
	{
		vector<int> tproduct;
		int tcarry = 0;
		for(int y = 1; y < i - x; y++)
			tproduct.insert(tproduct.begin(), 0);
		for(int y = op1.size() - 1; y >= 0; y--)
		{
			int tsum = op2.at(x) * op1.at(y) + tcarry;
			tcarry = 0;
			for(; tsum > 9; tsum -= 10)
				tcarry++;
			if(y != 0)
				tproduct.insert(tproduct.begin(), tsum);
			else
			{
				tproduct.insert(tproduct.begin(), tsum);
				if(tcarry > 0)
					tproduct.insert(tproduct.begin(), tcarry);
			}
		}
		product = Add(product, tproduct);
	}
	return product;
}


int _tmain(int argc, _TCHAR* argv[])
{
	vector<int> total = Number("2");
	for(int x = 1; x < 1000; x++)
	{
		total = Multi(total, Number("2"));
	}
	int i = total.size();
	int sum = 0;
	for(int x = 0; x < i; x++)
	{
		sum += total.at(x);
	}
	cout << sum;
	cin >> i;
	return 0;
}

