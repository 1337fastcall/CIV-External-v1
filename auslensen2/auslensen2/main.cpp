#include <iostream>
#include <Windows.h>
#include <TlHelp32.h>

#include "CHackProcess.h"

CHackProcess fProcess;

int main()
{
	fProcess.RunProcess();

	DWORD pBase;
	float vOrigin[3];

	while (!GetAsyncKeyState(VK_F8))
	{
		ReadProcessMemory(fProcess.__HandleProcess,(PBYTE*)(fProcess.__dwordClient + 0xA68A14),&pBase,4,0);
		ReadProcessMemory(fProcess.__HandleProcess,(PBYTE*)(pBase + 0x134),&vOrigin,12,0);
		std::cout << "Origin[0] = " << vOrigin[0] << std::endl;
		std::cout << "Origin[1] = " << vOrigin[1] << std::endl;
		std::cout << "Origin[2] = " << vOrigin[2] << std::endl;
		Sleep(10);
		system("cls");
	}
	return 0;
}