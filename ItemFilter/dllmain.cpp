// dllmain.cpp : Defines the entry point for the DLL application.
#include "stdafx.h"
using namespace std;

vector<DWORD> FilterList;
int Mesos = 0;
bool itemfilter = false;
bool tubi = false;
bool mesofilter = false;
bool mining = false;
bool kamiloot = false;
bool fgm = false;
bool freeze = false;
bool deaggro = false;
bool gnd = false;
/*
//Packet Structure
struct COutPacket
{
    BOOL fLoopback;
    PVOID pData;
    DWORD dwSize;
    UINT uOffset;
    BOOL fEncrypted;
};

struct CInPacket
{
	BOOL fLoopback;
	int iState;
	PVOID pData;
	DWORD dwTotalLength;
	DWORD dwUnknown;
	DWORD dwValidLength;
	UINT uOffset;
};

//Change to DWORD* if working
typedef void (__fastcall *pfnSendPacket)(PVOID pCClientSocket, PVOID pEDX, COutPacket *pPacket);
typedef void (__fastcall *pfnRecvPacket)(PVOID pCClientSocket, PVOID pEDX, CInPacket *pPacket);
typedef void (__fastcall *pfnFreePacket)(PVOID pPacket);
typedef DWORD (__fastcall *pfnDecryptData)(PVOID pData);

const PVOID *ppCClientSocket = reinterpret_cast<const PVOID*>(0x016A134C);
const pfnSendPacket MSSendPacket = (pfnSendPacket)0x0051C2A0;
const pfnRecvPacket MSRecvPacket = reinterpret_cast<pfnRecvPacket>(0x0051D680);
const pfnFreePacket MSFreePacket = reinterpret_cast<pfnFreePacket>(0x0043C680);
const pfnDecryptData MSDecryptData = reinterpret_cast<pfnDecryptData>(0x004A00F0);

const PVOID pReturnAddress = reinterpret_cast<const PVOID>(0x0040CE08);
const PVOID pLastThreadId = reinterpret_cast<const PVOID>(0x01B35307);
const DWORD dwRealThreadIdOffset = 0x06B8;
DWORD ThreadID = 0;
bool Inject = false;

void InjectPacket(COutPacket *pPacket)
{
    //__writefsdword(0x06B8, MSDecryptData(pLastThreadId));
	__writefsdword(0, ThreadID);
    __asm
    {
        push Next
        push ecx
        mov ecx, dword ptr [ppCClientSocket]
        mov ecx, dword ptr [ecx]
        push dword ptr [pPacket]
        push dword ptr [pReturnAddress]
        jmp dword ptr [MSSendPacket]
        Next:
    }
    MSFreePacket(reinterpret_cast<PVOID>(pPacket));
}

bool SendPacket(vector<BYTE> vData)
{
    COutPacket *pPacket = new COutPacket();
    SecureZeroMemory(pPacket, sizeof(COutPacket));
    
    pPacket->dwSize = vData.size();
    pPacket->pData = vData.data();

    try
    {
        InjectPacket(pPacket);
    }
    catch (...)
    {
        return false;
    }
    return true;
}

void InjectPacket(CInPacket *pPacket)
{
	MSRecvPacket(*ppCClientSocket, NULL, pPacket);
	MSFreePacket(reinterpret_cast<PVOID>(pPacket));
}

bool RecvPacket(vector<BYTE> vData)
{
	static tr1::uniform_int<DWORD> gen;
	static tr1::mt19937 engine;

	DWORD dwHeader = gen(engine);

	for (int i = 0; i < sizeof(DWORD); ++i)
	{
		vData.insert(vData.begin(), static_cast<BYTE>(dwHeader >> (i * 8)));
	}

	CInPacket *pPacket = new CInPacket();
	pPacket->fLoopback = 0;
	pPacket->iState = 2;
	pPacket->pData = vData.data();
	pPacket->dwTotalLength = vData.size();
	pPacket->dwUnknown = 0;
	pPacket->dwValidLength = pPacket->dwValidLength - 4;
	pPacket->uOffset = 4;

	try
	{
		InjectPacket(pPacket);
	}
	catch (...)
	{
		return false;
	}
	return true;
}*/

//8B 44 24 ? 3D ? ? ? ? 0F 8F ? ? ? ? 0F 84 ? ? ? ? 8D 90 ? ? ? ? 83 FA
DWORD PacketEntry = 0x018929A8;
DWORD PacketHookRet = *(DWORD*)(PacketEntry);
DWORD FilterArray = 0;
DWORD FilterArrayEnd = 0;
//DWORD ParseBYTE = 0x0040B760;
//DWORD ParseWORD = 0x0043BFD0;
//DWORD ParseDWORD = 0x0040B800;

__declspec(naked) void ParseBYTE()//PARAMS ecx = packet, eax = index //RETURNS al
{
	__asm
	{
		push esi
		push edx
		mov esi, dword ptr [ecx+0x08] //Packet Base Address
		mov edx, dword ptr [ecx+0x0C] //Packet Length
		add eax, dword ptr [ecx+0x18] //add Packet Base Index
		sub edx, eax
		cmp edx, 0x01
		jae Continue
		xor eax, eax
		pop edx
		pop esi
		ret

Continue:
		movzx eax, byte ptr [esi+eax]
		pop edx
		pop esi
		ret
	}
}

__declspec(naked) void WriteBYTE()//PARAMS ecx = packet, eax = index, push value = [esp+0x10]
{
	__asm
	{
		push esi
		push edx
		mov esi, dword ptr [ecx+0x08] //Packet Base Address
		mov edx, dword ptr [ecx+0x0C] //Packet Length
		add eax, dword ptr [ecx+0x18] //add Packet Base Index
		sub edx, eax
		cmp edx, 0x01
		jae Continue
		xor eax, eax
		pop edx
		pop esi
		ret 0x0004

Continue:
		push ebx
		mov bl, byte ptr [esp+0x10]
		mov byte ptr [esi+eax], bl
		pop ebx
		pop edx
		pop esi
		ret 0x0004
	}
}

__declspec(naked) void ParseWORD()//PARAMS ecx = packet, eax = index //RETURNS ax
{
	__asm
	{
		push esi
		push edx
		mov esi, dword ptr [ecx+0x08] //Packet Base Address
		mov edx, dword ptr [ecx+0x0C] //Packet Length
		add eax, dword ptr [ecx+0x18] //add Packet Base Index
		sub edx, eax
		cmp edx, 0x02
		jae Continue
		xor eax, eax
		pop edx
		pop esi
		ret

Continue:
		movzx eax, word ptr [esi+eax]
		pop edx
		pop esi
		ret
	}
}

__declspec(naked) void WriteWORD()//PARAMS ecx = packet, eax = index, push value = [esp+0x10]
{
	__asm
	{
		push esi
		push edx
		mov esi, dword ptr [ecx+0x08] //Packet Base Address
		mov edx, dword ptr [ecx+0x0C] //Packet Length
		add eax, dword ptr [ecx+0x18] //add Packet Base Index
		sub edx, eax
		cmp edx, 0x02
		jae Continue
		xor eax, eax
		pop edx
		pop esi
		ret 0x0004

Continue:
		push ebx
		mov bx, word ptr [esp+0x10]
		mov word ptr [esi+eax], bx
		pop ebx
		pop edx
		pop esi
		ret 0x0004
	}
}

__declspec(naked) void ParseDWORD()//PARAMS ecx = packet, eax = index //RETURNS eax
{
	__asm
	{
		push esi
		push edx
		mov esi, dword ptr [ecx+0x08] //Packet Base Address
		mov edx, dword ptr [ecx+0x0C] //Packet Length
		add eax, dword ptr [ecx+0x18] //add Packet Base Index
		sub edx, eax
		cmp edx, 0x04
		jae Continue
		xor eax, eax
		pop edx
		pop esi
		ret

Continue:
		mov eax, dword ptr [esi+eax]
		pop edx
		pop esi
		ret
	}
}

__declspec(naked) void WriteDWORD()//PARAMS ecx = packet, eax = index, push value = [esp+0x10]
{
	__asm
	{
		push esi
		push edx
		mov esi, dword ptr [ecx+0x08] //Packet Base Address
		mov edx, dword ptr [ecx+0x0C] //Packet Length
		add eax, dword ptr [ecx+0x18] //add Packet Base Index
		sub edx, eax
		cmp edx, 0x04
		jae Continue
		xor eax, eax
		pop edx
		pop esi
		ret 0x0004

Continue:
		push ebx
		mov ebx, dword ptr [esp+0x10]
		mov dword ptr [esi+eax], ebx
		pop ebx
		pop edx
		pop esi
		ret 0x0004
	}
}

__declspec(naked) void PacketHook()
{
	__asm
	{
		mov eax, dword ptr [esp+0x04]
		cmp eax, 0x0000021F
		je Mining
		cmp eax, 0x000002B4
		je Filter

Return:
		jmp dword ptr [PacketHookRet]

Mining:
		cmp [mining], 0x01
		jne Return
		mov ecx, dword ptr [esp+0x08]
		mov eax, 0x00000004
		push 0x0000000D
		call WriteDWORD
		jmp dword ptr [PacketHookRet]

Filter:
		movzx eax, [itemfilter]
		movzx edx, [mesofilter]
		or eax, edx
		test eax, eax
		je Return
		mov ecx, dword ptr [esp+0x08]
		mov eax, 0x00000005
		call ParseBYTE
		test eax, eax
		jne MesoFilter

ItemFilter:
		cmp [itemfilter], 0x01
		jne Return
		mov eax, 0x00000006
		call ParseDWORD
		mov ecx, dword ptr [FilterArray]
		mov edx, dword ptr [FilterArrayEnd]

ItemFilterLoop:
		cmp ecx, edx
		je Return
		cmp dword ptr [ecx], eax
		je ExitCode
		add ecx, 0x04
		jmp ItemFilterLoop

MesoFilter:
		cmp [mesofilter], 0x01
		jne Return
		mov eax, 0x00000006
		call ParseDWORD
		cmp eax, dword ptr [Mesos]
		jl ExitCode
		jmp dword ptr [PacketHookRet]

ExitCode:
		ret 0x0008
	}
}

void TogglePacket()
{
	unsigned long protect;
	VirtualProtect((void*)(PacketEntry), 4, PAGE_EXECUTE_READWRITE, &protect);
	*(DWORD*)(PacketEntry) = (itemfilter || mesofilter || mining) ? (DWORD)&PacketHook : PacketHookRet;
	VirtualProtect((void*)(PacketEntry), 4, protect, new DWORD);
}

DWORD Entry = 0x01B5D534;
DWORD HookRet = *(DWORD*)(Entry);
DWORD Exit1 = 0x00693222;
DWORD Exit2 = 0x00693077;
DWORD Call1 = 0x00899710;
DWORD Call2 = 0x013251C0;
DWORD ExitCall = 0x00691700;
DWORD TubiExit = 0x0069320A;
DWORD Item = 0;
DWORD ItemX = 0;
DWORD ItemY = 0;
DWORD PickUp = 0;
bool Kami = false;
bool KamiLock = false;

//Hook Address - 85 C0 75 4E 8D 4C ? ? C7 44 24 44 ? ? ? ? E8
__declspec(naked) void Hook()
{
	__asm
	{
		cmp dword ptr [esp], 0x0069318C //Item Return Hook Address
		jne Return
		cmp [kamiloot], 0x00
		je HookEnd
		cmp [Kami], 0x01
		je HookEnd
		mov eax, dword ptr [esi+0x34]
		test eax, eax
		je HookXY
		mov eax, dword ptr [esi+0x38]
		test eax, eax
		jne HookXY
		ret 0x000C

HookXY:
		mov eax, dword ptr [esp+0x08]
		mov dword ptr [ItemX], eax
		mov eax, dword ptr [esp+0x0C]
		add eax, 0x0A
		mov dword ptr [ItemY], eax
		mov [Kami], 0x01

HookEnd:
		mov dword ptr [esp], offset ReturnHook

Return:
		jmp dword ptr [HookRet]

ReturnHook:
		mov [PickUp], eax
		cmp [kamiloot], 0x01
		je Continue
		test eax, eax
		je ExitCode

Continue:
		cmp [PickUp], 0x00000001
		jne ExitCode
		cmp dword ptr [esi+0x34], 0x00
		jne JMP1
		mov eax, dword ptr [esi+0x38]
		mov ecx, 0x01B44CB0
		mov ecx, dword ptr [ecx]
		push eax
		call dword ptr [Call1]
		jmp JMP2

JMP1:
		xor eax, eax

JMP2:
		mov ecx, dword ptr [esi+0x24]
		mov edx, dword ptr [esp+0x4C]
		push eax
		push ecx
		mov ecx, dword ptr [esp+0x20]
		push edx
		call dword ptr [Call2]
		cmp [tubi], 0x00
		je TubiExitCode
		mov eax, 0x01B450AC //Server Base - 8B 2D ? ? ? ? A1 ? ? ? ? 8D ? 24 ? ? 8B
		mov eax, dword ptr [eax]
		mov dword ptr [eax+0x00002148], 0x00 //Tubi Offset - 83 ? ? ? ? ? 00 75 ? 83 7C ? ? 00 75 ? 8B ? ? ? ? ? 8B ? ? 51 83 C0 ? 50
		mov dword ptr [eax+0x0000214C], 0x00 //Global Delay Offset - Tubi Offset + 4

TubiExitCode:
		mov [Kami], 0x00
		jmp dword ptr [TubiExit]

ExitCode:
		lea ecx, [esp+0x24]
		mov dword ptr [esp+0x44], 0xFFFFFFFF
		call dword ptr [ExitCall]
		cmp dword ptr [esp+0x14], 0x00
		jne ExitJMP2
		cmp [kamiloot], 0x00
		je ExitJMP1
		cmp [Kami], 0x00
		je ExitJMP1
		mov [KamiLock], 0x01

ExitJMP1:
		jmp dword ptr [Exit1]

ExitJMP2:
		jmp dword ptr [Exit2]
	}
}

void UpdateFilter()
{
	FilterArray = *(DWORD*)((DWORD)&FilterList);
	FilterArrayEnd = *(DWORD*)((DWORD)&FilterList + 0x04);
	int size = FilterList.size();
	ofstream filter("filter.txt");
	if (filter.is_open())
	{
		for (int i = 0; i < size - 1; i++)
		{
			filter << FilterList.at(i) << endl;
		}
		filter << FilterList.at(size - 1) << flush;
		filter.close();
	}
}

DWORD MovementEntry = 0x01B5D590; //GetFocus
DWORD MovementHookRet = *(DWORD*)(MovementEntry);
DWORD MovementExit = 0x0129A061;
DWORD* CharInfoBase = (DWORD*)0x01B450B4; //8B 3D ? ? ? ? 8B 40
DWORD SetData = 0x005B9500; //56 8B ? 8B ? ? ? ? ? 41 [4th Result]
//Attack Count  - 89 ? ? ? 00 00 C7 ? ? ? 00 00 ? 00 00 00 89 ? ? ? 00 00 89 ? ? ? 00 00 89 ? ? ? 00 00 C7 ? ? ? 00 00 ? 00 00 00 89 ? ? ? 00 00 8D

__declspec(naked) void MovementHook()
{
	__asm
	{
		cmp dword ptr [esp], 0x0129A061
		jne Return
		mov dword ptr [esp], offset ReturnHook

Return:
		jmp dword ptr [MovementHookRet]

ReturnHook:
		cmp [KamiLock], 0x01
		jne UA
		push eax
		mov eax, dword ptr [ItemX]
		push eax
		mov ecx, dword ptr [CharInfoBase]
		mov ecx, dword ptr [ecx]
		lea ecx, [ecx+0x0000A344] //8D 8E ? ? ? ? C7 44 24 14 0A 00 00 00 E8 ? ? ? ? 68
		call dword ptr [SetData]
		mov eax, dword ptr [ItemY]
		push eax
		mov ecx, dword ptr [CharInfoBase]
		mov ecx, dword ptr [ecx]
		lea ecx, [ecx+0x0000A338] //TeleportX - 0x0C
		call dword ptr [SetData]
		push 0x01
		mov ecx, dword ptr [CharInfoBase]
		mov ecx, dword ptr [ecx]
		lea ecx, [ecx+0x0000A320] //TeleportY - 0x18 or TeleportX - 0x24
		call dword ptr [SetData]
		mov [KamiLock], 0x00
		pop eax

UA:
		push ecx
		mov ecx, 0x01B450AC
		mov ecx, dword ptr [ecx]
		mov dword ptr [ecx+0x0000214C], 0x00
		mov ecx, dword ptr [CharInfoBase]
		mov ecx, dword ptr [ecx]
		add ecx, 0x0000A560
		cmp dword ptr [ecx], 0x50
		jl ExitCode
		mov dword ptr [ecx], 0x00000000

ExitCode:
		pop ecx
		jmp dword ptr [MovementExit]
	}
}

DWORD GNDEntry = 0x01B2BA64;
DWORD GNDHookRet = *(DWORD*)(GNDEntry);
DWORD GNDExit = 0x0119BCAD;
DWORD GNDCall1 = 0x0056AF70;
DWORD GNDCall2 = 0x0068AC80;
DWORD GNDCall3 = 0x011DB9D0;
DWORD GNDCall4 = 0x012288C0;

//89 45 ? 8B ? ? ? FF FF 8B ? 8B 8D ? ? FF FF 8B 42 ? ? ? ? E8 ? ? ? ? ? ? ? 85 ? ? ? 8B
__declspec(naked) void GNDHook()
{
	__asm
	{
		cmp dword ptr [esp+0x2C], 0x0119BBC7
		jne Return
		mov dword ptr [esp+0x2C], offset ReturnHook
Return:
		jmp dword ptr [GNDHookRet]

ReturnHook:
		mov dword ptr [ebp-0x28],eax
		mov eax, dword ptr [ebp-0x00002D78]
		mov edx, dword ptr [eax]
		mov ecx, dword ptr [ebp-0x00002D78]
		mov eax, dword ptr [edx+0x68]
		call eax
		push eax
		call dword ptr [GNDCall1]
		add esp, 0x04
		test eax, eax
		je JMP1
		mov ecx, dword ptr [ebp-0x00000234]
		push ecx
		mov ecx, dword ptr [ebp-0x70]
		call dword ptr [GNDCall2]
		test eax, eax
		je JMP1
		mov dword ptr [ebp-0x00002D8C], 0x00000001
		jmp JMP2

JMP1:
		mov [ebp-0x00002D8C], 0x00000000

JMP2:
		mov edx, dword ptr [ebp-0x00002D8C]
		mov dword ptr [ebp-0x50], edx
		mov eax, dword ptr [ebp-0x00000234]
		push eax
		mov ecx, dword ptr [ebp-0x00002D78]
		call dword ptr [GNDCall3]
		mov dword ptr [ebp-0x00000248], eax
		cmp dword ptr [ebp+0x10], 0x00
		je JMP3
		mov ecx, dword ptr [ebp+0x10]
		mov dword ptr [ecx], 0x00000041

JMP3:
		call dword ptr [GNDCall4]
		mov dword ptr [ebp-0x00000150], eax
		mov ecx, dword ptr [ebp-0x00002D78]
		add ecx, 0x04
		mov edx, dword ptr [ebp-0x00002D78]
		mov eax, dword ptr [edx+0x04]
		mov edx, dword ptr [eax+0x20]
		call edx
		mov dword ptr [ebp-0x00000270], eax
		cmp dword ptr [ebp-0x00000234], 0x00
		je JMP4
		mov eax, dword ptr [ebp-0x00002D78]
		mov ecx, dword ptr [ebp-0x00000234]
		cmp ecx, dword ptr [eax+0x0000A2AC]
		jne JMP4
		mov dword ptr [ebp-0x00002D90], 0x00000001
		jmp JMP5

JMP4:
		mov dword ptr [ebp-0x00002D90], 0x00000000

JMP5:
		mov edx, dword ptr [ebp-0x00002D90]
		mov dword ptr [ebp-0x68], edx
		mov eax, dword ptr [ebp-0x00000234]
		push eax
		movzx eax, byte ptr [ebp-0x00002D90]
		neg eax
		sbb eax, eax
		add eax, 0x01
		mov byte ptr [ebp-0x00002D90], al
		mov byte ptr [ebp-0x68], al
		pop eax
		jmp dword ptr [GNDExit]
	}
}

void ToggleGND()
{
	unsigned long protect;
	VirtualProtect((void*)(GNDEntry), 4, PAGE_EXECUTE_READWRITE, &protect);
	*(DWORD*)(GNDEntry) = (*(DWORD*)(GNDEntry) == GNDHookRet) ? (DWORD)&GNDHook : GNDHookRet;
	VirtualProtect((void*)(GNDEntry), 4, protect, new DWORD);
}

DWORD MCEntry = 0x0161A0B0;
DWORD MCHookRet = *(DWORD*)(MCEntry);
DWORD MCExit = 0x01292D46;
DWORD PGMExit1 = 0x011CEAB0; 
DWORD PGMExit2 = 0x011CF8D9;
DWORD PGMCall1 = 0x0161A0AC;
DWORD PGMCall2 = 0x00490F70;
DWORD UMPExit = 0x1181170;
DWORD UMPExit1 = 0x01181158;
DWORD UMPCall1 = 0x004014D0;
DWORD UMPCall2 = 0x00486E20;
DWORD UMPCall3 = 0x0056AF40;
DWORD UMPCall4 = 0x00657740;
DWORD UMPCall5 = 0x00656180;
DWORD UMPCall6 = 0x006603B0;
DWORD UMPCall7 = 0x00660460;

//UMP - 8B 44 24 64 8B 88 ? ? ? ? 51 05 ? ? ? ? 50 E8 ? ? ? ? 83 C4 ? 85 C0
//Godmode - 85 C0 ? ? 55 FF 15 ? ? ? ? 85 DB ? ? 8B 13 8B 02 6A 01 8B CB FF D0 C7 ? ? ? ? ? ? ? ? ? ? 8D
//Mob Control - E8 ? ? ? ? 8B 86 ? ? ? ? C7 00 01 00 00 00
//Movement Offset - 83 BE ? ? ? ? 04 0F 85 ? ? ? ? 8B
//Aggro Offset - Movement Offset + 0x08
__declspec(naked) void MCHook()
{
	__asm
	{
		cmp dword ptr [esp], 0x011CEA6F
		je PGM
		cmp dword ptr [esp+0x14], 0x01292D46
		jne Continue
		mov dword ptr [esp+0x14], offset Freeze

Continue:
		cmp dword ptr [esp+0x14], 0x01181045
		jne Return
		mov dword ptr [esp+0x14], offset Unlimited

Return:
		jmp dword ptr [MCHookRet]

PGM:
		cmp [fgm], 0x01
		jne Return
		mov dword ptr [esp], offset PGMHook
		jmp dword ptr [MCHookRet]

PGMHook:
		test eax, eax
		jne Godmode
		push ebp
		mov eax, dword ptr [PGMCall1]
		call dword ptr [eax]
		test ebx, ebx
		je Godmode
		mov edx, dword ptr [ebx]
		mov eax, dword ptr [edx]
		push 0x01
		mov ecx, ebx
		call eax

Godmode:
		mov dword ptr [esp+0x000000B8], 0x00000000
		lea ecx, [edi+0x00002230]
		mov dword ptr [esp+0x20], ecx
		lea ecx, [esi+0x00007A90]
		call dword ptr [PGMCall2]
		test eax, eax
		je PGMExit
		jmp dword ptr [PGMExit1]

PGMExit:
		jmp dword ptr [PGMExit2]

Freeze:
		cmp [freeze], 0x01
		jne Aggro
		mov dword ptr [esi+0x00000340], 0x00000004

Aggro:
		cmp [deaggro], 0x01
		jne ExitCode1
		mov dword ptr [esi+0x00000348], 0x00000000
		
ExitCode1:
		jmp dword ptr [MCExit]

ExitCode2:
		jmp dword ptr [UMPExit]

Unlimited:
		cmp [gnd], 0x01
		jne ExitCode2
		mov eax, dword ptr [esp+0x64]
		mov ecx, dword ptr [eax+0x00001A00]
		push ecx
		add eax, 0x000019F8
		push eax
		call dword ptr [UMPCall1]
		add esp, 0x08
		test eax, eax
		jne JMP1
		mov eax, dword ptr [esp+0x5C]
		mov edx, dword ptr [eax+0x3D]
		push edx
		add eax, 0x39
		push eax
		call dword ptr [UMPCall2]
		movzx eax, ax
		cwde
		push eax
		call dword ptr [UMPCall3]
		add esp, 0x0C
		test eax, eax
		je JMP1
		test ebp, ebp
		je ExitJMP1
		mov esi, dword ptr [ebp+0x00000174]
		mov dword ptr [esp+0x2C], 0x00000000
		mov byte ptr [esp+0x54], 0x03
		test edi, edi
		jle JMP2
		cmp dword ptr [ebp+0x000001B4], 0x00
		je JMP2
		lea ecx, [esp+0x28]
		push ecx
		lea edx, [esp+0x6C]
		push edx
		lea ecx, [ebp+0x000001A8]
		call dword ptr [UMPCall4]
		test eax, eax
		je JMP2
		mov eax, dword ptr [esp+0x2C]
		add esi, dword ptr [eax+0x3C]

JMP2:
		imul esi,edi
		mov eax, 0xAE147AE1
		imul esi
		sar edx, 0x05
		mov ecx, edx
		shr ecx, 0x1F
		add ecx, edx
		add edi, ecx
		test edi, edi
		jg JMP3
		xor edi, edi

JMP3:
		lea ecx, [esp+0x28]
		mov byte ptr [esp+0x54], 0x02
		call dword ptr [UMPCall5]

JMP1:
		test ebp, ebp
		je JMP2
		mov ebx, dword ptr [esp+0x68]
		push ebx
		mov ecx, ebp
		call dword ptr [UMPCall6]
		push ebx
		mov ecx, ebp
		mov esi, eax
		call dword ptr [UMPCall7]
		imul esi, edi
		jnl JMP4

JMP4:
		xor edi, edi
		jmp dword ptr [UMPExit]

ExitJMP1:
		jmp dword ptr [UMPExit1]
	}
}

void ToggleMC()
{
	unsigned long protect;
	VirtualProtect((void*)(MCEntry), 4, PAGE_EXECUTE_READWRITE, &protect);
	*(DWORD*)(MCEntry) = (fgm || freeze || deaggro || gnd) ? (DWORD)&MCHook : MCHookRet;
	VirtualProtect((void*)(MCEntry), 4, protect, new DWORD);
}

DWORD MGMEntry = 0x01892A30;
DWORD MGMHookRet = *(DWORD*)(MGMEntry);

__declspec(naked) void MGMHook()
{
	__asm
	{
		//Follow 4th call below
		//55 8D ? ? ? 83 ? ? 6A ? 68 ? ? ? ? 64 ? ? ? ? ? 50 83 ? ? A1 ? ? ? ? 33 ? 89 ? ? 53 56 57 50 8D ? ? 64 ? ? ? ? ? 8B ? 8B ? ? ? ? ? 8B ? ? ? ? ? 51 [2nd Result]
		//Address below 1st call (edx) from ^
		cmp dword ptr [esp], 0x00983854
		jne Return
		//Address below 4th call below
		//55 8D ? ? ? 83 ? ? 6A ? 68 ? ? ? ? 64 ? ? ? ? ? 50 83 ? ? A1 ? ? ? ? 33 ? 89 ? ? 53 56 57 50 8D ? ? 64 ? ? ? ? ? 8B ? 8B ? ? ? ? ? 8B ? ? ? ? ? 51 [2nd Result]
		cmp dword ptr [esp+0x4C], 0x0099F3CF
		jne Return
		//Address in 1st jne below
		//55 8D ? ? ? 83 ? ? 6A ? 68 ? ? ? ? 64 ? ? ? ? ? 50 83 ? ? A1 ? ? ? ? 33 ? 89 ? ? 53 56 57 50 8D ? ? 64 ? ? ? ? ? 8B ? 8B ? ? ? ? ? 8B ? ? ? ? ? 51 [2nd Result]
		mov dword ptr [esp+0x4C], 0x009A05CD

Return:
		jmp dword ptr [MGMHookRet]
	}
}

void ToggleMGM()
{
	unsigned long protect;
	VirtualProtect((void*)(MGMEntry), 4, PAGE_EXECUTE_READWRITE, &protect);
	*(DWORD*)(MGMEntry) = fgm ? (DWORD)&MGMHook : MGMHookRet;
	VirtualProtect((void*)(MGMEntry), 4, protect, new DWORD);
}

void NoDelay()
{
	if (*(DWORD*)0x01B450AC != 0)
	{
		*(DWORD*)(*(DWORD*)0x01B450AC + 0x00002148) = 0;
		*(DWORD*)(*(DWORD*)0x01B450AC + 0x0000214C) = 0;
	}
}

void WINAPI MainThread()
{
	unsigned long protect;
	VirtualProtect((void*)(Entry), 4, PAGE_EXECUTE_READWRITE, &protect);
	*(DWORD*)(Entry) = (DWORD)&Hook;
	VirtualProtect((void*)(Entry), 4, protect, new DWORD);
	VirtualProtect((void*)(MovementEntry), 4, PAGE_EXECUTE_READWRITE, &protect);
	*(DWORD*)(MovementEntry) = (DWORD)&MovementHook;
	VirtualProtect((void*)(MovementEntry), 4, protect, new DWORD);
    Main();
}

BOOL APIENTRY DllMain(HMODULE hModule, DWORD dwReason, LPVOID lpvReserved)
{
	if(dwReason == DLL_PROCESS_ATTACH)
	{
		DisableThreadLibraryCalls(hModule);
		CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)&MainThread, NULL, 0, NULL);
	}
	return TRUE;
}
