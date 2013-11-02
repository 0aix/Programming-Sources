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
bool pgm = false;
bool freeze = false;
bool deaggro = false;

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
}

//8B 44 24 ? 3D ? ? ? ? 0F 8F ? ? ? ? 0F 84 ? ? ? ? 8D 90 ? ? ? ? 83 FA
DWORD PacketEntry = 0x014714D0;
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
		cmp eax, 0x000001E1
		je Mining
		cmp eax, 0x00000256
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

DWORD Entry = 0x016B6D2C;
DWORD HookRet = *(DWORD*)(Entry);
DWORD Exit1 = 0x005DEBD2;
DWORD Exit2 = 0x005DEA27;
DWORD Call1 = 0x00793E50;
DWORD Call2 = 0x01040800;
DWORD ExitCall = 0x005DD330;
DWORD TubiExit = 0x005DEBBA;
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
		cmp dword ptr [esp], 0x005DEB3C //Item Return Hook Address
		jne Return
		cmp [kamiloot], 0x00
		je HookEnd
		cmp [Kami], 0x01
		je HookEnd
		mov eax, dword ptr [esp+0x08]
		mov dword ptr [ItemX], eax
		mov eax, dword ptr [esp+0x0C]
		add eax, 0x0B
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
		cmp dword ptr [esi+0x30], 0x00
		jne JMP1
		mov eax, dword ptr [esi+0x34]
		mov ecx, 0x016A1EA0
		mov ecx, dword ptr [ecx]
		push eax
		call dword ptr [Call1]
		jmp JMP2

JMP1:
		xor eax, eax

JMP2:
		mov ecx, dword ptr [esi+0x20]
		mov edx, dword ptr [esp+0x4C]
		push eax
		push ecx
		mov ecx, dword ptr [esp+0x20]
		push edx
		call dword ptr [Call2]
		cmp [tubi], 0x00
		je TubiExitCode
		mov eax, 0x016A2230 //Server Base = Char Base - 0x04
		mov eax, dword ptr [eax]
		mov dword ptr [eax+0x00002138], 0x00 //Tubi Offset - 83 ? ? ? ? ? 00 75 ? 83 7C ? ? 00 75 ? 8B ? ? ? ? ? 8B ? ? 51 83 C0 ? 50
		mov dword ptr [eax+0x0000213C], 0x00 //Global Delay Offset - Tubi Offset + 4

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

DWORD MovementEntry = 0x016B6D88;
DWORD MovementHookRet = *(DWORD*)(MovementEntry);
DWORD MovementExit = 0x00FE139D;
DWORD* CharInfoBase = (DWORD*)0x016A2234; //8B 3D ? ? ? ? 8B 40
DWORD SetData = 0x00518EE0; //56 8B ? 8B ? ? ? ? ? 41 [3rd Result]
DWORD LockX = 0;
DWORD LockY = 0;
//Attack Count  - 89 ? ? ? 00 00 C7 ? ? ? 00 00 ? 00 00 00 89 ? ? ? 00 00 89 ? ? ? 00 00 89 ? ? ? 00 00 C7 ? ? ? 00 00 ? 00 00 00 89 ? ? ? 00 00 8D

__declspec(naked) void MovementHook()
{
	__asm
	{
		cmp dword ptr [esp], 0x00FE139D
		jne Return
		mov dword ptr [esp], offset ReturnHook

Return:
		jmp dword ptr [MovementHookRet]

ReturnHook:
		cmp [KamiLock], 0x01
		jne UA
		push eax
		mov eax, dword ptr [ItemX]
		mov dword ptr [LockX], eax
		push eax
		mov ecx, dword ptr [CharInfoBase]
		mov ecx, dword ptr [ecx]
		lea ecx, [ecx+0x00008AB8]
		call dword ptr [SetData]
		mov eax, dword ptr [ItemY]
		mov dword ptr [LockY], eax
		push eax
		mov ecx, dword ptr [CharInfoBase]
		mov ecx, dword ptr [ecx]
		lea ecx, [ecx+0x00008AAC]
		call dword ptr [SetData]
		push 0x01
		mov ecx, dword ptr [CharInfoBase]
		mov ecx, dword ptr [ecx]
		lea ecx, [ecx+0x00008A94]
		call dword ptr [SetData]
		mov [KamiLock], 0x00
		pop eax

UA:
		push ecx
		//
		mov ecx, 0x016A2230
		mov ecx, dword ptr [ecx]
		mov dword ptr [ecx+0x0000213C], 0x00
		//
		mov ecx, dword ptr [CharInfoBase]
		mov ecx, dword ptr [ecx]
		add ecx, 0x00008CF8
		cmp dword ptr [ecx], 0x50
		jl ExitCode
		mov dword ptr [ecx], 0x00000000

ExitCode:
		pop ecx
		jmp dword ptr [MovementExit]
	}
}

DWORD GNDEntry = 0x01690F94;
DWORD GNDHookRet = *(DWORD*)(GNDEntry);
DWORD GNDExit = 0x00F0D4A0;

//E8 ? ? ? ? 50 8D 8D ? ? ? ? E8 ? ? ? ? C6 45 FC 03 8D 8D ? ? ? ? E8 ? ? ? ? 89 45 D8
__declspec(naked) void GNDHook()
{
	__asm
	{
		cmp dword ptr [esp+0x2C], 0x00F0D4A0
		jne Return
		mov dword ptr [esp+0x2C], offset ReturnHook
Return:
		jmp dword ptr [GNDHookRet]

ReturnHook:
		push eax
		movzx eax, byte ptr [ebp-0x00003598]
		neg eax
		sbb eax, eax
		add eax, 0x01
		mov byte ptr [ebp-0x00003598], al
		mov byte ptr [ebp-0x54], al
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

DWORD MCEntry = 0x012C609C;
DWORD MCHookRet = *(DWORD*)(MCEntry);
DWORD MCExit = 0x00FDAB06;
DWORD PGMExit1 = 0x00F38960;
DWORD PGMExit2 = 0x00F39771;
DWORD PGMCall1 = 0x012C6098;
DWORD PGMCall2 = 0x0043E790;


//Mob Control - E8 ? ? ? ? 8B 86 ? ? ? ? ? 00 01 00 00 00
//Movement Offset - 83 BE ? ? 00 00 06 0F 85 ? ? 00 00 8B
//Aggro Offset - Movement Offset + 0x08
__declspec(naked) void MCHook()
{
	__asm
	{
		cmp dword ptr [esp], 0x00F3891F
		je PGM
		cmp dword ptr [esp+0x14], 0x00FDAB06
		jne Return
		mov dword ptr [esp+0x14], offset Freeze

Return:
		jmp dword ptr [MCHookRet]

PGM:
		cmp [pgm], 0x01
		jne Return
		mov dword ptr [esp], offset PGMHook
		jmp dword ptr [MCHookRet]

PGMHook:
		test eax, eax
		jne Godmode
		push ebp
		mov eax, dword ptr [PGMCall1]
		call dword ptr [eax]
		test esi, esi
		je Godmode
		mov edx, dword ptr [esi]
		mov eax, dword ptr [edx]
		push 0x01
		mov ecx, esi
		call eax

Godmode:
		mov dword ptr [esp+0x000000A4], 0x00000000
		add ebx, 0x00002220
		lea ecx, [edi+0x000063F4]
		mov dword ptr [esp+0x2C], ebx
		call dword ptr [PGMCall2]
		test eax, eax
		je PGMExit
		jmp dword ptr [PGMExit1]

PGMExit:
		jmp dword ptr [PGMExit2]

Freeze:
		cmp [freeze], 0x01
		jne Aggro
		mov dword ptr [esi+0x00000318], 0x00000006

Aggro:
		cmp [deaggro], 0x01
		jne Exit
		mov dword ptr [esi+0x00000320], 0x00000000
		
Exit:
		jmp dword ptr [MCExit]
	}
}

void ToggleMC()
{
	unsigned long protect;
	VirtualProtect((void*)(MCEntry), 4, PAGE_EXECUTE_READWRITE, &protect);
	*(DWORD*)(MCEntry) = (pgm || freeze || deaggro) ? (DWORD)&MCHook : MCHookRet;
	VirtualProtect((void*)(MCEntry), 4, protect, new DWORD);
}

DWORD CPUEntry = 0x012C631C;
DWORD CPUHookRet = *(DWORD*)(CPUEntry);
DWORD LoadTiles = 0x0080E710;
DWORD LoadObjects = 0x008170D0;
DWORD CPUExit1 = 0x00817981;
DWORD CPUExit2 = 0x0086BC3E;
DWORD CPUCall1 = 0x004076B0;
DWORD CPUCall2 = 0x004019F0;
DWORD CPUCall3 = 0x00404DD0;
DWORD CPUCall4 = 0x00404E20;

//Background
//8B CF E8 ? ? FF FF 8B CF E8 ? ? FF FF 8B CF E8 ? ? FF FF 8B CF E8 ? ? FF FF 8B CF E8 ? ? FF FF 8B CF
//Skill Effects
//85 FF 74 ? 83 7D ? 00 0F 85 ? ? 00 00 8B 4D [Up 7 Calls]
//8B ? ? ? ? 2F 5D 20 00 //jne below above address
__declspec(naked) void CPUHook()
{
	__asm
	{
		cmp dword ptr [esp], 0x010C7201
		je SetHook1
		cmp dword ptr [esp], 0x004050E3
		je SetHook2
		jmp dword ptr [CPUHookRet]

SetHook1:
		cmp dword ptr [esp+0x000001E4], 0x0081796C
		jne Return
		mov dword ptr [esp+0x000001E4], offset ReturnHook1
		jmp dword ptr [CPUHookRet]

ReturnHook1:
		mov ecx, edi
		call dword ptr [LoadTiles]
		mov ecx, edi
		call dword ptr [LoadObjects]
		jmp dword ptr [CPUExit1]

SetHook2:
		cmp dword ptr [esp+0x00000038], 0x0086BA3C
		jne Return
		mov dword ptr [esp+0x00000038], offset ReturnHook2
		jmp dword ptr [CPUHookRet]

ReturnHook2:
		push eax
		mov byte ptr [ebp-0x04], 0x0C
		call dword ptr [CPUCall1]
		mov edi, eax
		add esp, 0x08
		neg edi
		sbb edi, edi
		lea ecx, [ebp-0x44]
		neg edi
		call dword ptr [CPUCall2]
		lea ecx, [ebp-0x14]
		call dword ptr [CPUCall3]
		lea ecx, [ebp-0x18]
		call dword ptr [CPUCall4]
		lea ecx, [ebp-0x54]
		call dword ptr [CPUCall2]
		lea ecx, [ebp-0x2C]
		mov byte ptr [ebp-0x04], 0x00
		call dword ptr [CPUCall2]
		jmp dword ptr [CPUExit2]

Return:
		jmp dword ptr [CPUHookRet]
	}
}

void ToggleCPU()
{
	unsigned long protect;
	VirtualProtect((void*)(CPUEntry), 4, PAGE_EXECUTE_READWRITE, &protect);
	*(DWORD*)(CPUEntry) = (*(DWORD*)(CPUEntry) == CPUHookRet) ? (DWORD)&CPUHook : CPUHookRet;
	VirtualProtect((void*)(CPUEntry), 4, protect, new DWORD);
}

void NoDelay()
{
	if (*(DWORD*)0x016A2230 != 0)
	{
		*(DWORD*)(*(DWORD*)0x016A2230 + 0x00002138) = 0;
		*(DWORD*)(*(DWORD*)0x016A2230 + 0x0000213C) = 0;
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
