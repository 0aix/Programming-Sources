// BotBoy.cpp : Defines the class behaviors for the application.
//

#define WIN32_LEAN_AND_MEAN
#include "stdafx.h"
#include "BotBoy.h"
#include "BotBoyDlg.h"
#include "math.h"
#include <sstream>
#include "windows.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CBotBoyApp

BEGIN_MESSAGE_MAP(CBotBoyApp, CWinApp)
	ON_COMMAND(ID_HELP, &CWinApp::OnHelp)
END_MESSAGE_MAP()


// CBotBoyApp construction

CBotBoyApp::CBotBoyApp()
{
	// TODO: add construction code here,
	// Place all significant initialization in InitInstance

}


// The one and only CBotBoyApp object

CBotBoyApp theApp;


// CBotBoyApp initialization

BOOL CBotBoyApp::InitInstance()
{
	// InitCommonControlsEx() is required on Windows XP if an application
	// manifest specifies use of ComCtl32.dll version 6 or later to enable
	// visual styles.  Otherwise, any window creation will fail.
	INITCOMMONCONTROLSEX InitCtrls;
	InitCtrls.dwSize = sizeof(InitCtrls);
	// Set this to include all the common control classes you want to use
	// in your application.
	InitCtrls.dwICC = ICC_WIN95_CLASSES;
	InitCommonControlsEx(&InitCtrls);

	CWinApp::InitInstance();

	AfxEnableControlContainer();

	// Standard initialization
	// If you are not using these features and wish to reduce the size
	// of your final executable, you should remove from the following
	// the specific initialization routines you do not need
	// Change the registry key under which our settings are stored
	// TODO: You should modify this string to be something appropriate
	// such as the name of your company or organization
	SetRegistryKey(_T("Local AppWizard-Generated Applications"));

	CBotBoyDlg dlg;
	m_pMainWnd = &dlg;
	INT_PTR nResponse = dlg.DoModal();
	if (nResponse == IDOK)
	{
		// TODO: Place code here to handle when the dialog is
		//  dismissed with OK
	}
	else if (nResponse == IDCANCEL)
	{
		// TODO: Place code here to handle when the dialog is
		//  dismissed with Cancel
	}

	// Since the dialog has been closed, return FALSE so that we exit the
	//  application, rather than start the application's message pump.
	return FALSE;
}

//Variables
int nado = 0;
int mirror = 0;
int m_angle = 0;
int m_mangle = 90;
int m_wind = 0;
int m_wAngle = 0;
int m_x = 0;
int m_y = 0;
int m_tx = 0;
int m_ty = 0;
int m_sx = 0;
int m_sy = 0;
int m_shotpow = 0;
POINT mxy;
POINT nxy1;
POINT nxy2;
int gch = 120;
int backshot = 0;
int scantype = 3; //0 = SS, 1 = Forks, 2 = Shot
LPCSTR m_stype = "Armor/Turtle";
HDC gunbound = NULL;
HWND gbwnd = NULL;
INPUT Input={0};
DWORD pid = 0;
HANDLE proc = 0;
SIZE_T read;
//shotscan
BOOL innado = false;
BOOL hitmirror = false;
double nadox = 0;
double nadoy = 0;
int interval = 0;
double shot_Xforce;
double shot_Yforce;
double wind_Xforce;
double wind_Yforce;

double current_x;
double current_y;

LONG iPowerCounter = 401;
LONG iShootPower;
BOOL scandone = false;
int getangle = 0;

double targetx = m_tx;
double targety = m_ty;
double newx;
double newy;

double check;
int change;

const double DEGTORAD = 0.017453295199433;

int LineTo1 = 0;
int liney1 = 0;
double wolfwind = .843;
double wolfgrav = 83;

BOOL ctrl = false;
BOOL shift = false;
BOOL II = false;
BOOL update = false;

COLORREF col = RGB(180,234,131);
//

//----------------------------------------------------------
//extra
double RadToDeg(double Radians)
{
   return Radians * (180 / 3.1416);
}
//----------------------------------------------------------
//Bypassed APIs
//Rewrote at least the first 5 bytes of every hooked API and then had it jump to the original function
DWORD fwa = (DWORD)GetProcAddress(LoadLibraryA("user32.dll"), "FindWindowA") + 8;
//DWORD fwa = 2118288105;
__declspec(naked) HWND WINAPI FindWindowX(LPCSTR lpClassName, LPCSTR lpWindowName)
{
	__asm {
		mov edi,edi
		push ebp
		mov ebp,esp
		xor eax,eax
		push eax
		jmp dword ptr ds:[fwa]
	}
}

DWORD gwtpi = (DWORD)GetProcAddress(LoadLibraryA("user32.dll"), "GetWindowThreadProcessId") + 6;
//DWORD gwtpi = 2118224518;
__declspec(naked) DWORD WINAPI GetWindowThreadProcessIdX(HWND hWnd, LPDWORD lpdwProcessId)
{
	__asm {
		mov edi,edi
		push ebp
		mov ebp,esp
		push esi
		jmp dword ptr ds:[gwtpi]
	}
}

DWORD op = (DWORD)GetProcAddress(LoadLibraryA("kernel32.dll"), "OpenProcess") + 8;
//DWORD op = 2088962545;
__declspec(naked) HANDLE WINAPI OpenProcessX(DWORD dwDesiredAccess, BOOL bInheritHandle, DWORD dwProcessId)
{
	__asm {
		mov edi,edi
		push ebp
		mov ebp,esp
		sub esp,0x20
		jmp dword ptr ds:[op]
	}
}

DWORD gp = (DWORD)GetProcAddress(LoadLibraryA("gdi32.dll"), "GetPixel") + 6;
//DWORD gp = 2012329810;
__declspec(naked) COLORREF WINAPI GetPixelX(HDC hdc, int xXPos, int nYPos)
{
	__asm {
		mov edi,edi
		push ebp
		mov ebp,esp
		push ebx
		jmp dword ptr ds:[gp]
	}
}

DWORD rpm = (DWORD)GetProcAddress(LoadLibraryA("kernel32.dll"), "ReadProcessMemory") + 8;
//DWORD rpm = 2088772056;
__declspec(naked) BOOL WINAPI ReadProcessMemoryX(HANDLE hProcess, LPCVOID lpBaseAddress, LPVOID lpBuffer, SIZE_T nSize, SIZE_T *lpNumberOfBytesRead)
{
	__asm {
		mov edi,edi
		push ebp
		mov ebp,esp
		lea eax,dword ptr ss:[ebp+0x14]
		jmp dword ptr ds:[rpm]
	}
}

DWORD toa = (DWORD)GetProcAddress(LoadLibraryA("gdi32.dll"), "TextOutA") + 7;
//DWORD toa = 2012330582;
__declspec(naked) BOOL WINAPI TextOutX(HDC hdc, int nXStart, int nYStart, LPCSTR lpString, int cbString)
{
	__asm {
		mov edi,edi
		push ebp
		mov ebp,esp
		xor eax,eax
		jmp dword ptr ds:[toa]
	}
}

DWORD spx = (DWORD)GetProcAddress(LoadLibraryA("gdi32.dll"), "SetPixel") + 6;
//DWORD spx = 2012330065;
__declspec(naked) COLORREF WINAPI SetPixelX(HDC hdc, int X, int Y, COLORREF crColor)
{
	__asm {
		mov edi,edi
		push ebp
		mov ebp,esp
		push ecx
		jmp dword ptr ds:[spx]
	}
}
//
void lineSimple(int x0, int y0, int x1, int y1, COLORREF color)
{
    float dx = x1 - x0;
    float dy = y1 - y0;
	float t = (float) 0.5;
	COLORREF inv;

	SetPixelX(gunbound, x0, y0, color);
	
    if (fabs(dx) > fabs(dy)) 
	{          // slope < 1
        float m = (float) dy / (float) dx;      // compute slope
        t += y0;
        dx = (dx < 0) ? -1 : 1;
        m *= dx;
        while (x0 != x1) 
		{
            x0 += dx;                           // step to next x value
            t += m;                             // add slope to y value
            SetPixelX(gunbound, x0, (int) t, color);
        }
    } 
	else 
	{                                    // slope >= 1
        float m = (float) dx / (float) dy;      // compute slope
        t += x0;
        dy = (dy < 0) ? -1 : 1;
        m *= dy;
        while (y0 != y1) 
		{
            y0 += dy;                           // step to next y value
            t += m;                             // add slope to x value
            SetPixelX(gunbound, (int) t, y0, color);
        }
    }
}

void circlePoints(int cx, int cy, int x, int y, COLORREF color)
    {
        if (x == 0) 
		{
			SetPixelX(gunbound, cx, cy + y, color);
			SetPixelX(gunbound, cx, cy - y, color);
			SetPixelX(gunbound, cx + y, cy, color);
			SetPixelX(gunbound, cx - y, cy, color);
        } 
		else if (x == y) 
		{
			SetPixelX(gunbound,cx + x, cy + y, color);
			SetPixelX(gunbound,cx - x, cy + y, color);
			SetPixelX(gunbound,cx + x, cy - y, color);
			SetPixelX(gunbound,cx - x, cy - y, color);
        } 
		else if (x < y) 
		{
			SetPixelX(gunbound,cx + x, cy + y, color);
			SetPixelX(gunbound,cx - x, cy + y, color);
			SetPixelX(gunbound,cx + x, cy - y, color);
			SetPixelX(gunbound,cx - x, cy - y, color);
			SetPixelX(gunbound,cx + y, cy + x, color);
			SetPixelX(gunbound,cx - y, cy + x, color);
			SetPixelX(gunbound,cx + y, cy - x, color);
			SetPixelX(gunbound,cx - y, cy - x, color);
        }
    }


void circle(int xCenter, int yCenter, int radius, COLORREF color)
{
    int x = 0;
    int y = radius;
    int p = (5 - radius*4)/4;

    circlePoints(xCenter, yCenter, x, y, color);
    while (x < y) 
	{
        x++;
        if (p < 0) 
		{
            p += 2*x+1;
        } 
		else 
		{
            y--;
            p += 2*(x-y)+1;
        }
        circlePoints(xCenter, yCenter, x, y, color);
    }
}

void GetInfo ()
{
   //Memory Plugin
   int buffer = 0;
   ReadProcessMemoryX(proc, (LPVOID)0x008fc1bc, &buffer, sizeof(buffer), &read);
   buffer += 4485;
   WORD wAngle = 0;
   ReadProcessMemoryX(proc, (LPVOID)buffer, &wAngle, sizeof(wAngle), &read);
   m_wAngle = wAngle;
   buffer = 0;
   ReadProcessMemoryX(proc, (LPVOID)0x008fc1bc, &buffer, sizeof(buffer), &read);
   buffer += 4484;
   BYTE wind = 0;
   ReadProcessMemoryX(proc, (LPVOID)buffer, &wind, sizeof(wind), &read);
   m_wind = wind;
}

POINT GetCamera ()
{
   //Memory Plugin
   POINT camera;
   int buffer = 0;
   ReadProcessMemoryX(proc, (LPVOID)0x98E644, &buffer, sizeof(buffer), &read);
   camera.x = buffer;
   ReadProcessMemoryX(proc, (LPVOID)0x98E648, &buffer, sizeof(buffer), &read);
   camera.y = buffer;
   camera.x -= 400;
   camera.x = 0;
   camera.y = 0;
   return camera;
}

//Pixel detection of player angle
void GetAngle ()
{
   COLORREF ref = RGB(0,0,0);
   if( GetPixelX( gunbound, 316, 541) != ref && GetPixelX( gunbound, 314, 544) != ref && GetPixelX( gunbound, 318, 544) != ref && GetPixelX( gunbound, 316, 545) != ref && GetPixelX( gunbound, 314, 547) != ref && GetPixelX( gunbound, 318, 547) != ref && GetPixelX( gunbound, 316, 550) != ref)
   {
   m_mangle = 80;
   }
   else if( GetPixelX( gunbound, 316, 541) != ref && GetPixelX( gunbound, 314, 544) != ref && GetPixelX( gunbound, 318, 544) != ref && GetPixelX( gunbound, 314, 547) != ref && GetPixelX( gunbound, 318, 547) != ref && GetPixelX( gunbound, 316, 550) != ref)
   {
   m_mangle = 00;
   }
   else if( GetPixelX( gunbound, 316, 541) != ref && GetPixelX( gunbound, 314, 544) != ref && GetPixelX( gunbound, 318, 544) != ref && GetPixelX( gunbound, 316, 545) != ref && GetPixelX( gunbound, 318, 547) != ref )
   {
   m_mangle = 90;
   }
   else if( GetPixelX( gunbound, 316, 541) != ref && GetPixelX( gunbound, 314, 544) != ref && GetPixelX( gunbound, 316, 545) != ref && GetPixelX( gunbound, 318, 547) != ref && GetPixelX( gunbound, 316, 550) != ref)
   {
   m_mangle = 50;
   }
   else if( GetPixelX( gunbound, 316, 541) != ref && GetPixelX( gunbound, 318, 544) != ref && GetPixelX( gunbound, 316, 545) != ref && GetPixelX( gunbound, 314, 547) != ref && GetPixelX( gunbound, 316, 550) != ref)
   {
   m_mangle = 20;
   }
   else if( GetPixelX( gunbound, 316, 541) != ref && GetPixelX( gunbound, 318, 544) != ref && GetPixelX( gunbound, 316, 545) != ref && GetPixelX( gunbound, 318, 547) != ref && GetPixelX( gunbound, 316, 550) != ref)
   {
   m_mangle = 30;
   }
   else if( GetPixelX( gunbound, 314, 544) != ref && GetPixelX( gunbound, 316, 545) != ref && GetPixelX( gunbound, 314, 547) != ref && GetPixelX( gunbound, 318, 547) != ref && GetPixelX( gunbound, 316, 550) != ref)
   {
   m_mangle = 60;
   }
   else if( GetPixelX( gunbound, 314, 544) != ref && GetPixelX( gunbound, 318, 544) != ref && GetPixelX( gunbound, 316, 545) != ref && GetPixelX( gunbound, 318, 547) != ref )
   {
   m_mangle = 40;
   }
   else if( GetPixelX( gunbound, 316, 541) != ref && GetPixelX( gunbound, 318, 544) != ref && GetPixelX( gunbound, 318, 547) != ref )
   {
   m_mangle = 70;
   }
   else
   {
   m_mangle = 10;
   }
   if( GetPixelX( gunbound, 323, 541) != ref && GetPixelX( gunbound, 321, 544) != ref && GetPixelX( gunbound, 325, 544) != ref && GetPixelX( gunbound, 323, 545) != ref && GetPixelX( gunbound, 321, 547) != ref && GetPixelX( gunbound, 325, 547) != ref && GetPixelX( gunbound, 323, 550) != ref)
   {
   m_mangle += 8;
   }
   else if( GetPixelX( gunbound, 323, 541) != ref && GetPixelX( gunbound, 321, 544) != ref && GetPixelX( gunbound, 325, 544) != ref && GetPixelX( gunbound, 321, 547) != ref && GetPixelX( gunbound, 325, 547) != ref && GetPixelX( gunbound, 323, 550) != ref)
   {
   m_mangle += 0;
   }
   else if( GetPixelX( gunbound, 323, 541) != ref && GetPixelX( gunbound, 321, 544) != ref && GetPixelX( gunbound, 325, 544) != ref && GetPixelX( gunbound, 323, 545) != ref && GetPixelX( gunbound, 325, 547) != ref )
   {
   m_mangle += 9;
   }
   else if( GetPixelX( gunbound, 323, 541) != ref && GetPixelX( gunbound, 321, 544) != ref && GetPixelX( gunbound, 323, 545) != ref && GetPixelX( gunbound, 325, 547) != ref && GetPixelX( gunbound, 323, 550) != ref)
   {
   m_mangle += 5;
   }
   else if( GetPixelX( gunbound, 323, 541) != ref && GetPixelX( gunbound, 325, 544) != ref && GetPixelX( gunbound, 323, 545) != ref && GetPixelX( gunbound, 321, 547) != ref && GetPixelX( gunbound, 323, 550) != ref)
   {
   m_mangle += 2;
   }
   else if( GetPixelX( gunbound, 323, 541) != ref && GetPixelX( gunbound, 325, 544) != ref && GetPixelX( gunbound, 323, 545) != ref && GetPixelX( gunbound, 325, 547) != ref && GetPixelX( gunbound, 323, 550) != ref)
   {
   m_mangle += 3;
   }
   else if( GetPixelX( gunbound, 321, 544) != ref && GetPixelX( gunbound, 323, 545) != ref && GetPixelX( gunbound, 321, 547) != ref && GetPixelX( gunbound, 325, 547) != ref && GetPixelX( gunbound, 323, 550) != ref)
   {
   m_mangle += 6;
   }
   else if( GetPixelX( gunbound, 321, 544) != ref && GetPixelX( gunbound, 325, 544) != ref && GetPixelX( gunbound, 323, 545) != ref && GetPixelX( gunbound, 325, 547) != ref )
   {
   m_mangle += 4;
   }
   else if( GetPixelX( gunbound, 323, 541) != ref && GetPixelX( gunbound, 325, 544) != ref && GetPixelX( gunbound, 325, 547) != ref )
   {
   m_mangle += 7;
   }
   else
   {
   m_mangle += 1;
   }
   if( GetPixelX( gunbound, 310, 546) != ref )
   {
      m_mangle *= -1;
   }
}

BOOL HookGB()
{
	if( FindWindowX("Softnyx",NULL) != NULL )
	{
		gbwnd = FindWindowX("Softnyx",NULL);
		gunbound = GetDC(gbwnd);
		GetWindowThreadProcessIdX(gbwnd, &pid);
		proc = OpenProcessX(PROCESS_VM_READ, false, pid);//PROCESS_VM_READ
		SetBkColor(gunbound, RGB(0,0,0));
		SetTextColor(gunbound, RGB(255,255,255));
		col = RGB(180,234,131);
		return true;
	}
	else
	{
		return false;
	}
}

void ShotScan()
{
   nadox = 0;
   nadoy = 0;
   iPowerCounter = 400;
   iShootPower = 0;
   scandone = false;
   getangle = 0;

   targetx = m_tx;
   targety = m_ty;

   GetAngle();
   GetInfo();

   int min = 0;
   if( scantype > 0 )
   {
      min = m_mangle - 40;
   }
   m_angle = m_mangle;
   for (; m_angle > min; m_angle--)
   {
      if( scandone )
      {
         break;
      }
      for (iPowerCounter = 0; iPowerCounter <= 399; iPowerCounter++)
      {
         if( scandone )
         {         
            break;
         }
         wind_Xforce = (cos(m_wAngle * DEGTORAD) * m_wind) * wolfwind;//.685/.728
         wind_Yforce = (sin(m_wAngle * DEGTORAD) * m_wind) * wolfwind - wolfgrav;//72.6

         shot_Xforce = cos(m_angle * DEGTORAD) * iPowerCounter;
         shot_Yforce = sin(m_angle * DEGTORAD) * iPowerCounter;

         if (m_x > m_tx)             
            shot_Xforce = shot_Xforce * -1;
         if (backshot == 1)
            shot_Xforce = shot_Xforce * -1;

         current_x = m_x;
         current_y = m_y;
         if( scantype == 0 )
         {
            current_x = m_x + (0.5 * pow(4.95, 2) * wind_Xforce + shot_Xforce * 4.95);
            current_y = m_y - (0.5 * pow(4.95, 2) * wind_Yforce + shot_Yforce * 4.95);
            if( current_x - 20 < targetx && current_x + 20 > targetx && current_y - 5 < targety && current_y + 5 > targety)
            {
               m_sx = (int)current_x;
               m_sy = (int)current_y;
               scandone = true;
               getangle = m_angle;
               iShootPower = iPowerCounter;
               m_stype = "Turtle Timebomb";
               break;
            }
         }
         else if( scantype == 1 )
         {
            current_x = m_x + (0.5 * pow(4.2, 2) * wind_Xforce + shot_Xforce * 4.2);
            current_y = m_y - (0.5 * pow(4.2, 2) * wind_Yforce + shot_Yforce * 4.2);
            if( current_x - 5 < targetx && current_x + 5 > targetx && current_y - 3 < targety && current_y + 3 > targety)
            {
               m_sx = (int)current_x;
               m_sy = (int)current_y;
               scandone = true;
               getangle = m_angle;
               iShootPower = iPowerCounter;
               m_stype = "Turtle Fork 6";
               break;
            }
            current_x = m_x + (0.5 * pow(3.45, 2) * wind_Xforce + shot_Xforce * 3.45);
            current_y = m_y - (0.5 * pow(3.45, 2) * wind_Yforce + shot_Yforce * 3.45);
            if( current_x - 5 < targetx && current_x + 5 > targetx && current_y - 3 < targety && current_y + 3 > targety)
            {
               m_sx = (int)current_x;
               m_sy = (int)current_y;
               scandone = true;
               getangle = m_angle;
               iShootPower = iPowerCounter;
               m_stype = "Turtle Fork 5";
               break;
            }
            current_x = m_x + (0.5 * pow(2.8, 2) * wind_Xforce + shot_Xforce * 2.8);
            current_y = m_y - (0.5 * pow(2.8, 2) * wind_Yforce + shot_Yforce * 2.8);
            if( current_x - 5 < targetx && current_x + 5 > targetx && current_y - 3 < targety && current_y + 3 > targety)
            {
               m_sx = (int)current_x;
               m_sy = (int)current_y;
               scandone = true;
               getangle = m_angle;
               iShootPower = iPowerCounter;
               m_stype = "Turtle Fork 4";
               break;
            }
            current_x = m_x + (0.5 * pow(2.15, 2) * wind_Xforce + shot_Xforce * 2.15);
            current_y = m_y - (0.5 * pow(2.15, 2) * wind_Yforce + shot_Yforce * 2.15);
            if( current_x - 5 < targetx && current_x + 5 > targetx && current_y - 3 < targety && current_y + 3 > targety)
            {
               m_sx = (int)current_x;
               m_sy = (int)current_y;
               scandone = true;
               getangle = m_angle;
               iShootPower = iPowerCounter;
               m_stype = "Turtle Fork 3";
            }
            current_x = m_x + (0.5 * pow(1.6, 2) * wind_Xforce + shot_Xforce * 1.6);
            current_y = m_y - (0.5 * pow(1.6, 2) * wind_Yforce + shot_Yforce * 1.6);
            if( current_x - 5 < targetx && current_x + 5 > targetx && current_y - 3 < targety && current_y + 3 > targety)
            {
               m_sx = (int)current_x;
               m_sy = (int)current_y;
               scandone = true;
               getangle = m_angle;
               iShootPower = iPowerCounter;
               m_stype = "Turtle Fork 2";
               break;
            }
            current_x = m_x + (0.5 * pow(1.15, 2) * wind_Xforce + shot_Xforce * 1.15);
            current_y = m_y - (0.5 * pow(1.15, 2) * wind_Yforce + shot_Yforce * 1.15);
            if( current_x - 5 < targetx && current_x + 5 > targetx && current_y - 3 < targety && current_y + 3 > targety)
            {
               m_sx = (int)current_x;
               m_sy = (int)current_y;
               scandone = true;
               getangle = m_angle;
               iShootPower = iPowerCounter;
               m_stype = "Turtle Fork 1";
               break;
            }
         }
         /*else if( scantype == 18 )//Phoenix
         {
            current_x = m_x + (0.5 * pow(3.45, 2) * wind_Xforce + shot_Xforce * 3.45);
            current_y = m_y - (0.5 * pow(3.45, 2) * wind_Yforce + shot_Yforce * 3.45);
            if( current_x - 2 < targetx && current_x + 2 > targetx && current_y < targety )
            {
               m_sx = (int)current_x;
               m_sy = (int)current_y;
               scandone = true;
               getangle = m_angle;
               iShootPower = iPowerCounter;
               break;
            }
         }*/
         else
         {
            double time = 0;
            innado = false;
            nadox = 0;
            nadoy = 0;

            while( !scandone )
            {
               if( current_x > 3000 )
               {
                  break;
               }
               if( current_x < -3000 )
               {
                  break;
               }
               if( current_y > 3000 )
               {
                  break;
               }
               if( current_y < -3000 )
               {
                  break;
               }

               current_x = m_x + (0.5 * pow(time, 2) * wind_Xforce + shot_Xforce * time);
               current_y = m_y - (0.5 * pow(time, 2) * wind_Yforce + shot_Yforce * time);

               if( current_x - 5 < targetx && current_x + 5 > targetx && current_y - 5 < targety && current_y + 5 > targety)
               {
                  m_sx = (int)current_x;
                  m_sy = (int)current_y;
                  scandone = true;
                  getangle = m_angle;
                  iShootPower = iPowerCounter;
                  if( scantype == 2 )
                  {
                     m_stype = "Armor/Turtle";
                  }
                  break;
               }
               time += .05;
            }
         }
      }
   }
   iShootPower = iPowerCounter;
   m_shotpow = iShootPower;
   m_angle = getangle;
   interval = 250;
   gch = 250;
   update = true;
   if( scantype == 1 || scantype == 0 )
   {
      scantype = 2;
   }
   if( scantype == 100 )
   {
      scantype = 8;
   }
}

void BotBoyShot()
{
   ctrl = false;
   shift = false;
   II = false;
   POINT txy;
   POINT camera = GetCamera();
   //gunbound = GetDC(gbwnd);
  // SetBkColor(gunbound, RGB(0,0,0));
   //SetTextColor(gunbound, RGB(255,255,255));
   if( GetAsyncKeyState(VK_CONTROL))
   {
      ctrl = true;
   }
   if( GetAsyncKeyState(VK_SHIFT))
   {
      shift = true;
   }
   if( GetAsyncKeyState('I'))
   {
      II = true;
   }
   if( GetAsyncKeyState( VK_LBUTTON ) && ctrl || ctrl && shift)
   {
      GetCursorPos(&txy);
      ScreenToClient(gbwnd, &txy);
      txy.x += camera.x;
      txy.y += camera.y;
      m_x = txy.x;
      m_y = txy.y;
      gch = 150;
   }
   if( GetAsyncKeyState( VK_MBUTTON ) || GetAsyncKeyState( VK_MENU ))
   {
      if( ctrl && scantype == 2 )
      {
         GetCursorPos(&txy);
         ScreenToClient(gbwnd, &txy);
         txy.x += camera.x;
         txy.y += camera.y;
         m_tx = txy.x;
         m_ty = txy.y;
         scantype = 1;
         ShotScan();
      }
      else
      {
         GetCursorPos(&txy);
         ScreenToClient(gbwnd, &txy);
         txy.x += camera.x;
         txy.y += camera.y;
         m_tx = txy.x;
         m_ty = txy.y;
         ShotScan();
      }
   }
   if( GetAsyncKeyState( VK_RBUTTON ) && ctrl)
   {
      if( scantype == 2 )
      {
         GetCursorPos(&txy);
         ScreenToClient(gbwnd, &txy);
         txy.x += camera.x;
         txy.y += camera.y;
         m_tx = txy.x;
         m_ty = txy.y;
         scantype = 0;
         ShotScan();
      }
   }
   if( GetAsyncKeyState( VK_SCROLL)&1 )
   {
      if(update)
      {
         ShotScan();
      }
   }
   if( ctrl && II )
   {
      COLORREF invert = GetPixelX(gunbound, 1, 1);
      col = RGB(255 - GetRValue(invert), 255 - GetGValue(invert), 255 - GetBValue(invert));
   }
   if( GetAsyncKeyState( VK_DELETE )&1 )
   {
      if( backshot == 0 )
      {
         backshot = 1;
      }
      else
      {
         backshot = 0;
      }
      gch = 150;
   }
   if( GetAsyncKeyState( VK_PRIOR )&1 )
   {
      if( nado == 0 )
      {
         GetCursorPos(&nxy1);
         ScreenToClient(gbwnd, &nxy1);
         nxy1.x += camera.x;
         nxy1.y += camera.y;
         nado = 1;
         mirror = 0;
      }
      else if( nado == 1 )
      {
         GetCursorPos(&nxy2);
         ScreenToClient(gbwnd, &nxy2);
         nxy2.x += camera.x;
         nxy2.y += camera.y;
         if( nxy2.x < nxy1.x )
         {
            POINT rep;
            rep.x = nxy1.x;
            nxy1.x = nxy2.x;
            nxy2.x = rep.x;
         }
         nado = 2;
         mirror = 0;
      }
      else
      {
         nado = 0;
      }
      gch = 150;
   }
   if( GetAsyncKeyState(VK_NEXT)&1 )
   {
      if( mirror == 0 )
      {
         GetCursorPos(&mxy);
         ScreenToClient(gbwnd, &mxy);
         mxy.x += camera.x;
         mxy.y += camera.y;
         mirror = 1;
         nado = 0;
      }
      else
      {
         mirror = 0;
      }
      gch = 150;
   }
   if( GetAsyncKeyState(VK_HOME)&1 )
   {
	   interval = 0;
   }
   if( GetAsyncKeyState(VK_ADD)&1)
   {
      if( ctrl )
      {
         wolfgrav += .1;
      }
      else
      {
         wolfwind += .001;
      }
      gch = 150;
   }
   if( GetAsyncKeyState(VK_SUBTRACT)&1)
   {
      if( ctrl )
      {
         wolfgrav -= .1;
      }
      else
      {
         wolfwind -= .001;
      }
      gch = 150;
   }
   if( interval > 0 )
   {
      char buffer [255];
      LineTo1 = 100000;
      liney1 = 100000;
      int nixx = 0;
      int nixy = 0;
      innado = false;
      hitmirror = false;
      BOOL arc = false;
      double time = .001;
      double hittime = 0;
      BOOL a = true;
      while( a )
      {
         if( current_x - camera.x > 800 )
            a = false;
         if( current_x - camera.x < 0 )
            a = false;
         if( current_y - camera.y > 600 )
            a = false;
         if( current_y - camera.y < 0 )
            a = false;

         current_x = m_x + (0.5 * pow(time, 2) * wind_Xforce + shot_Xforce * time);
         current_y = m_y - (0.5 * pow(time, 2) * wind_Yforce + shot_Yforce * time);

         if( innado )
         {
            if( current_x > newx)
            {
               current_y -= nadoy;
            }
            else
            {
               current_y += nadoy;
            }
            if( current_x > newx )
            {
               current_x += ( nxy2.x - newx );
            }
            else
            {
               current_x += ( nxy1.x - newx );
            }
         }
         else if( hitmirror )
         {
            current_x = m_x + (0.5 * pow(hittime, 2) * wind_Xforce + shot_Xforce * time);
            current_x = mxy.x - ( current_x - mxy.x );
            current_x = current_x + (0.5 * pow(time - hittime, 2) * wind_Xforce);
         }

         if( nado == 2 && current_x > nxy1.x && current_x < nxy2.x && !innado )
         {
            newx = m_x + (0.5 * pow(time + .01, 2) * wind_Xforce + shot_Xforce * ( time + .01 ));
            newy = m_y - (0.5 * pow(time + .01, 2) * wind_Yforce + shot_Yforce * ( time + .01 ));
            change = (int)( RadToDeg( atan2(double(newy - current_y), double(newx - current_x)) ) );
            if (change < 0)
            {
               change += 360;
            }
            change = 360 - change;
            nadox = cos(change * DEGTORAD);
            check = (2 * (nxy2.x - nxy1.x));
            if( current_x < newx )
            {
               check += ( nxy2.x - newx );
            }
            else
            {
               check += ( nxy1.x - newx ) * -1;
            }
            nadox = check / nadox;
            nadoy = sin((double)change * DEGTORAD);
            nadoy = nadoy * nadox;
            innado = true;
            double nadoyy = 0;
            double nadoxx = 0;
            if( current_x < newx )
            {
               nadoyy = sin(change * DEGTORAD) * ((nxy2.x - newx) / cos(change * DEGTORAD));
               nadoxx = sin(change * DEGTORAD) * ((nxy2.x - nxy1.x) / cos(change * DEGTORAD));
			   lineSimple(current_x - camera.x, current_y - camera.y, nxy2.x - camera.x, current_y - nadoyy - camera.y, col);
			   lineSimple(nxy2.x - camera.x, current_y - nadoyy - camera.y, nxy1.x - camera.x, current_y - (nadoy - nadoxx) - camera.y, col);
			   lineSimple(nxy1.x - camera.x, current_y - (nadoy - nadoxx) - camera.y, nxy2.x - camera.x, current_y - nadoy - camera.y, col);
               current_x = nxy2.x;
               current_y -= nadoy;
               LineTo1 = current_x;
               liney1 = current_y;
            }
            else if( current_x > newx )
            {
               nadoyy = sin(change * DEGTORAD) * (((nxy1.x - newx)*-1) / cos(change * DEGTORAD));
               nadoxx = sin(change * DEGTORAD) * ((nxy2.x - nxy1.x) / cos(change * DEGTORAD));
			   lineSimple(current_x -camera.x, current_y - camera.y, nxy1.x - camera.x, current_y + nadoyy - camera.y, col);
			   lineSimple(nxy1.x - camera.x, current_y + nadoyy - camera.y,  nxy2.x - camera.x, current_y + (nadoy - nadoxx) - camera.y, col);
			   lineSimple(nxy2.x - camera.x, current_y + (nadoy - nadoxx) - camera.y, nxy1.x - camera.x, current_y + nadoy - camera.y, col);
               current_x = nxy1.x;
               current_y += nadoy;
               LineTo1 = current_x;
               liney1 = current_y;
            }
         }
         else if( mirror == 1 && !hitmirror )
         {
            if( m_x < m_tx && current_x >= mxy.x || m_x > m_tx && current_x <= mxy.x )
            {
               hittime = time;
               current_x = m_x + (0.5 * pow(hittime, 2) * wind_Xforce + shot_Xforce * time);
               current_x = mxy.x - ( current_x - mxy.x );
               hitmirror = true;
            }
         }
         
         if( LineTo1 == 100000 && liney1 == 100000 )
         {
            LineTo1 = current_x;
            liney1 = current_y;
         }
		 if( scantype <= 2 )
		 {
			if((int)(time * 100) == 495)
				circle(current_x - camera.x, current_y - camera.y, 10, col);
			if((int)(time * 100) == 420)
				circle(current_x - camera.x, current_y - camera.y, 5, col);
			if((int)(time * 100) == 345)
				circle(current_x - camera.x, current_y - camera.y, 5, col);
			if((int)(time * 100) == 280)
				circle(current_x - camera.x, current_y - camera.y, 5, col);
			if((int)(time * 100) == 215)
				circle(current_x - camera.x, current_y - camera.y, 5, col);
			if((int)(time * 100) == 160)
				circle(current_x - camera.x, current_y - camera.y, 5, col);
			if((int)(time * 100) == 115)
				circle(current_x - camera.x, current_y - camera.y, 5, col);
		 }
		 if( scantype == 3)
		 {
			double cx = (cos(((180 - m_angle) + (time * 100)) * DEGTORAD) * 45);
			double cy = (sin(((180 - m_angle) + (time * 100)) * DEGTORAD) * 45);
			double dotx = current_x + cx;
			double doty = current_y - cy; 
			double dotx2 = current_x - cx;
			double doty2 = current_y + cy;
			circle(dotx - camera.x, doty - camera.y,2,col);
			circle(dotx2 - camera.x, doty2 - camera.y,2,col);
		 }
         if(LineTo1 - camera.x < 800 && LineTo1 - camera.x > 0 && liney1 - camera.y > 0 && liney1 - camera. y < 600);
			lineSimple(LineTo1 - camera.x, liney1 - camera.y, current_x - camera.x, current_y - camera.y, col);
         nixx = LineTo1;
         nixy = liney1;
         LineTo1 = current_x;
         liney1 = current_y;
         time += .05;
         if( scantype == 18 && time >= 3.45 )
         {
            a = false;
         }
      }
	  lineSimple(m_shotpow + 388, 562, m_shotpow + 388, 586, col);
      sprintf(buffer, "%d", m_angle);
      TextOutX(gunbound, m_shotpow + 388, 586, buffer, strlen(buffer));
      //interval--;
   }
   circle(m_x - camera.x, m_y - camera.y, 18, col);
   circle(m_x - camera.x, m_y - camera.y, 43, col);
   circle(m_sx - camera.x, m_sy - camera.y, 12, col);
   lineSimple(m_x - 18 - camera.x, m_y - camera.y, m_x + 18 - camera.x, m_y - camera.y, col);
   lineSimple(m_x - camera.x, m_y - 18 - camera.y, m_x - camera.x, m_y + 18 - camera.y, col);
   lineSimple(m_sx - 12 - camera.x, m_sy - camera.y, m_sx + 12 - camera.x, m_sy - camera.y, col);
   lineSimple(m_sx - camera.x, m_sy - 12 - camera.y, m_sx - camera.x, m_sy + 12 - camera.y, col);
   if( nado == 1 )
   {
 	  lineSimple(nxy1.x - camera.x, 0, nxy1.x - camera.x, 600, col);
   }
   else if( nado == 2 )
   {
      lineSimple(nxy1.x - camera.x, 0, nxy1.x - camera.x, 600, col);
      lineSimple(nxy2.x - camera.x, 0, nxy2.x - camera.x, 600, col);
   }
   if( mirror == 1 )
   {
      lineSimple(mxy.x - camera.x, 0, mxy.x - camera.x, 600, col);
   }
   if( gch > 0 )
   {
	  char buffer [255];
      if( backshot == 1 )
      {
         TextOutX(gunbound, 340, 5, "Backshot", 8 );
      }
      TextOutX(gunbound, 5, 5, m_stype, strlen(m_stype));
      sprintf(buffer, "Gravity: %f", wolfgrav);
      TextOutX(gunbound, 5, 25, buffer, strlen(buffer));
      sprintf(buffer, "WindEffect: %f", wolfwind);
      TextOutX(gunbound, 5, 45, buffer, strlen(buffer));
      gch--;
   }
}
/*Ignore below
//Memory Scanner Section
DWORD* address = new DWORD[1000];
DWORD* value = new DWORD[1000];
DWORD limit = 1000;

int search = 400;

void Expand()
{
	DWORD count = sizeof(address) / sizeof(DWORD);
	limit += 1000;

	DWORD* temp = new DWORD[limit];
	for (DWORD i = 0; i < count - 1; i++) 
	{
		temp[i] = address[i];      
	}
	delete [] address;
	address = temp;

	delete [] temp;
	temp = new DWORD[limit];
	for (DWORD i = 0; i < count - 1; i++) 
	{
		temp[i] = value[i];      
	}
	delete [] value;
	value = temp;
}

void Clear()
{
	DWORD n = 0;
	DWORD count = sizeof(address) / sizeof(DWORD);

	DWORD* temp = new DWORD[limit];
	DWORD* tempv = new DWORD[limit];
	for (DWORD i = 0; i < count - 1; i++) 
	{
		if(address[i] != 0)
		{
			temp[n] = address[i];
			tempv[n] = value[i];
			n++;
		}
	}
	delete [] address;
	delete [] value;
	address = temp;
	value = tempv;
}

void Empty()
{
	delete [] address;
	delete [] value;
	address = new DWORD[1000];
	value = new DWORD[1000];
}

void ScanMemory()
{
	if( GetAsyncKeyState( VK_END )&1 )
	{
		DWORD i = 0;
		DWORD buffer = 0;
		if(address[0] != 0)
		{
			DWORD count = sizeof(address) / sizeof(DWORD);
			for (DWORD x = 0; x < count - 1; x++) 
			{
				ReadProcessMemoryX(proc, (LPVOID)address[x], &buffer, sizeof(buffer), &read);
				if(buffer != search)
				{
					address[x] = 0;
				}
			}
			Clear();
		}
		else
		{
			Empty();
			for(DWORD x = 0; 0x00400000 + x < 0x01800000; x++)
			{
				ReadProcessMemoryX(proc, (LPVOID)(0x00400000 + x), &buffer, sizeof(buffer), &read);
				if(buffer == search)
				{
					address[i] = (DWORD)(0x00400000 + x);
					i++;
					if(i == limit)
						Expand();
				}
			}
		}
	}
	if( GetAsyncKeyState( VK_INSERT )&1 )
	{
		DWORD i = 0;
		DWORD buffer = 0;
		if(address[0] != 0)
		{
			DWORD count = sizeof(address) / sizeof(DWORD);
			for (DWORD x = 0; x < count - 1; x++) 
			{
				ReadProcessMemoryX(proc, (LPVOID)address[x], &buffer, sizeof(buffer), &read);
				if(value[x] >= buffer|| buffer > 5000)
				{
					address[x] = 0;
				}
			}
			Clear();
		}
		else
		{
			Empty();
			for(DWORD x = 0; 0x00400000 + x < 0x01800000; x++)
			{
				ReadProcessMemoryX(proc, (LPVOID)(0x00400000 + x), &buffer, sizeof(buffer), &read);
				if(buffer > search)
				{
					address[i] = (DWORD)(0x00400000 + x);
					i++;
					if(i == limit)
						Expand();
				}
			}
		}
	}
	if( GetAsyncKeyState( VK_DELETE )&1 )
	{
		DWORD i = 0;
		DWORD buffer = 0;
		if(address[0] != 0)
		{
			DWORD count = sizeof(address) / sizeof(DWORD);
			for (DWORD x = 0; x < count - 1; x++) 
			{
				ReadProcessMemoryX(proc, (LPVOID)address[x], &buffer, sizeof(buffer), &read);
				if(value[x] <= buffer|| buffer > 5000)
				{
					address[x] = 0;
				}
			}
			Clear();
		}
		else
		{
			Empty();
			for(DWORD x = 0; 0x00400000 + x < 0x01800000; x++)
			{
				ReadProcessMemoryX(proc, (LPVOID)(0x00400000 + x), &buffer, sizeof(buffer), &read);
				if(buffer < search)
				{
					address[i] = (DWORD)(0x00400000 + x);
					i++;
					if(i == limit)
						Expand();
				}
			}
		}
	}
	if( GetAsyncKeyState( VK_PRIOR )&1 )
	{
		search += 100;
	}
	if( GetAsyncKeyState( VK_NEXT )&1 )
	{
		search -= 100;
	}
	char buff [255];
	sprintf(buff, "%d", search);
	TextOutX(gunbound, 5, 5, buff, strlen(buff));
}

void BotBoyShot()
{
	ScanMemory();
}*/