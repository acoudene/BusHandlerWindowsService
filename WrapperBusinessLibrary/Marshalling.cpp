#include "pch.h"
#include "Marshalling.h"

using namespace System::Runtime::InteropServices;

namespace WrapperBusinessLibrary
{
  //static 
  CString CMarshalling::DotNetStringToAnsi(String^ strString_P)
  {
    CString strReturn;

    if (strString_P != nullptr)
    {
      IntPtr ip = Marshal::StringToHGlobalAnsi(strString_P);
      strReturn = static_cast<const char*>(ip.ToPointer());
      Marshal::FreeHGlobal(ip);
    }

    return strReturn;
  }

  //static
  String^ CMarshalling::AnsiStringToDotNet(const CString& strString_P)
  {
    return gcnew String(strString_P);
  }

  //static
  String^ CMarshalling::AnsiStringToDotNet(LPCTSTR lpszString_P)
  {
    return gcnew String(lpszString_P);
  }
};