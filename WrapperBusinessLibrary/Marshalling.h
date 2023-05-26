#pragma once

using namespace System;

namespace WrapperBusinessLibrary
{
  class CMarshalling
  {
  public:

    // Marhsall .Net String to MFC CString
    static CString DotNetStringToAnsi(String^ strString_P);

    // Marhsall MFC CString to .Net String
    static String^ AnsiStringToDotNet(const CString& strString_P);

    // Marhsall LPCTSTR to .Net String
    static String^ AnsiStringToDotNet(LPCTSTR lpszString_P);

  };
}

