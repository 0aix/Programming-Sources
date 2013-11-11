// Euler17.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <stdlib.h>
using namespace std;

int _tmain(int argc, _TCHAR* argv[])
{
	int letters = 11;//one thousand
	for(int x = 999; x > 0; x--)
	{
		bool last = false;
		char c[3];
		itoa(x, c, 10);
		if(c[2] != 0 && c[2] != -52) //Checks for 3 digits
		{
			switch(c[0])
			{
			case '1':
			case '2':
			case '6':
				letters += 10;//one hundred //two hundred //six hundred
				break;
			case '3':
			case '7':
			case '8':
				letters += 12;//three hundred //seven hundred //eight hundred
				break;
			case '4':
			case '5':
			case '9':
				letters += 11;//four hundred //five hundred // nine hundred
				break;
			}
			if(c[1] != '0' || c[2] != '0') //If not double zeros
				letters += 3; //Add 3 letters for 'and'
			c[0] = c[1];
			c[1] = c[2];
			c[2] = 0;
		}
		if(c[1] != 0 && c[1] != -52) //Checks for 2 digits
		{
			switch(c[0])
			{
			case '0':
				break;
			case '2':
			case '3':
			case '8':
			case '9':
				letters += 6;//twenty //thirty //eighty // ninety
				break;
			case '4':
			case '5':
			case '6':
				letters += 5;//forty //fifty //sixty
				break;
			case '7':
				letters += 7;//seventy
				break;
			default:
				switch(c[1])
				{
				case '1':
				case '2':
					letters += 6; //eleven //twelve
					break;
				case '3':
				case '4':
				case '8':
				case '9':
					letters += 8; //thirteen //fourteen //eighteen //nineteen
					break;
				case '5':
				case '6':
					letters += 7; //fifteen //sixteen
					break;
				case '7':
					letters += 9; //seventeen
					break;
				}
				last = true;
			}
			if(c[0] == '1' && c[1] == '0')
				letters += 3; //ten
			c[0] = c[1];
			c[1] = 0;
		}
		if(!last)
		{
			switch(c[0])
			{
			case '1':
			case '2':
			case '6':
				letters += 3;//one //two //six
				break;
			case '3':
			case '7':
			case '8':
				letters += 5;//three //seven //eight
				break;
			case '4':
			case '5':
			case '9':
				letters += 4;//four //five //nine
				break;
			}
		}
	}
	cout << letters;
	char a;
	cin >> a;
	return 0;
}

