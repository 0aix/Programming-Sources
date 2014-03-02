#include "MainWindow.h"
#include "stdafx.h"
using namespace ItemFilter;

[STAThreadAttribute]

int Main()
{
    Application::EnableVisualStyles();
    Application::SetCompatibleTextRenderingDefault(false); 
    Application::Run(gcnew MainWindow());
    return 0;
}
