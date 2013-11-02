// Snake.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <windows.h>
#include <iostream>
#include <vector>
#include <math.h>
using namespace std;

COLORREF back = RGB(255, 255, 255);
COLORREF fore = RGB(80, 80, 80);
COLORREF red = RGB(255, 0, 0);
HWND hwnd;
HDC hdc;
int width;
int height;
bool isAlive = true;
POINT head;
POINT dot;
DWORD lasttick = 0;
DWORD tick = 0;
int dx = 0;
int dy = 0;
int dir = -1;
bool change = false;
int size = 0;
vector<int> bodyx;
vector<int> bodyy;

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
    switch(msg)
    {
		case WM_KEYDOWN:
			if (wParam >= VK_LEFT && wParam <= VK_DOWN)
			{
				if (!change)
				{
					switch (wParam)
					{
						case VK_LEFT:
							if (dir != 1)
							{
								dx = -1;
								dy = 0;
								dir = 0;
								change = true;
							}
							break;
						case VK_RIGHT:
							if (dir != 0)
							{
								dx = 1;
								dy = 0;
								dir = 1;
								change = true;
							}
							break;
						case VK_UP:
							if (dir != 3)
							{
								dx = 0;
								dy = -1;
								dir = 2;
								change = true;
							}
							break;
						case VK_DOWN:
							if (dir != 2)
							{
								dx = 0;
								dy = 1;
								dir = 3;
								change = true;
							}
							break;
					}
				}
			}
			break;
        case WM_CLOSE:
            DestroyWindow(hwnd);
        break;
        case WM_DESTROY:
            PostQuitMessage(0);
			isAlive = false;
        break;
        default:
            return DefWindowProc(hwnd, msg, wParam, lParam);
    }
    return 0;
}

void DrawSquare(POINT p)
{
	for (int x = 1; x < 9; x++)
		for (int y = 1; y < 9; y++)
			SetPixel(hdc, p.x * 10 + x, p.y * 10 + y, fore);
}

void EraseSquare(int xh, int yh)
{
	for (int x = 1; x < 9; x++)
		for (int y = 1; y < 9; y++)
			SetPixel(hdc, xh * 10 + x, yh * 10 + y, back);
}

void DrawDot()
{
	for (int x = 1; x < 9; x++)
		for (int y = 1; y < 9; y++)
			SetPixel(hdc, dot.x * 10 + x, dot.y * 10 + y, red);
}

int _tmain(int argc, _TCHAR* argv[])
{
	ShowWindow(GetForegroundWindow(), SW_HIDE); //Cheap fix
	WNDCLASSEX wcx = {};
	wcx.cbSize = sizeof(WNDCLASSEX);
	wcx.lpfnWndProc = WndProc;
	wcx.hInstance = GetModuleHandle(0);
	wcx.lpszClassName = L"Snake";
	wcx.hIcon = LoadIcon(0, IDI_APPLICATION);
	wcx.hIconSm = LoadIcon(0, IDI_APPLICATION);
	wcx.hCursor = LoadCursor(NULL, IDC_ARROW);
	if (RegisterClassEx(&wcx)) 
	{
		hwnd = CreateWindowEx(WS_EX_TOPMOST | WS_EX_LAYERED, L"Snake", L"Snake", WS_POPUP, 0, 0, 0, 0, 0, 0, GetModuleHandle(0), 0);
		if(hwnd)
        {
			SetLayeredWindowAttributes(hwnd, back, 0, LWA_COLORKEY);
            ShowWindow(hwnd, SW_SHOWMAXIMIZED);
			UpdateWindow(hwnd);
			RECT rect;
			GetWindowRect(hwnd, &rect);
			width = rect.right / 10;
			height = rect.bottom / 10;
			hdc = GetDC(hwnd);
			head.x = width / 2;
			head.y = height / 2;
			lasttick = GetTickCount();
			dot.x = lasttick % rand() % width;
			dot.y = lasttick % rand() % height;
			MSG msg;
			while (isAlive)
			{
				//Movement
				tick = GetTickCount();
				if (tick - lasttick > 40)
				{
					//Erase tail
					bodyx.insert(bodyx.begin(), head.x);
					bodyy.insert(bodyy.begin(), head.y);
					EraseSquare(bodyx.back(), bodyy.back());
					bodyx.pop_back();
					bodyy.pop_back();

					//Calculate new positions
					head.x += dx;
					head.y += dy;
					change = false;
					lasttick += 40;
					
					for (int i = 0; i < size; i++)
					{
						if (head.x == bodyx.at(i) && head.y == bodyy.at(i))
							return 0;
					}
					if (head.x == dot.x && head.y == dot.y)
					{
						for (int i = 0; i < 5; i++)
						{
							bodyx.push_back(0);
							bodyy.push_back(0);
						}
						size += 5;
						dot.x = lasttick % rand() % width;
						dot.y = lasttick % rand() % height;
					}
					else if (head.x < 0 || head.x > width - 1 || head.y < 0 || head.y > height - 1)
						return 0;
					

					//Render
					DrawSquare(head);
					DrawDot();
				}

				//Message Queue
				if (PeekMessage(&msg, hwnd, 0, 0, 1))
				{
					TranslateMessage(&msg);
					DispatchMessage(&msg);
				}
			}
        }
	}
	return 0;
}

