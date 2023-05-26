#include "pch.h"
#include "WrapperBusinessClass.h"
#include "Marshalling.h"
#include "../MFCBusinessLibrary/BusinessClass.h"

using namespace System;
using namespace MFCBusinessLbrary;

namespace WrapperBusinessLibrary
{
  WrapperBusinessClass::WrapperBusinessClass(void)
  {

  }
  //virtual 
  WrapperBusinessClass::~WrapperBusinessClass(void)
  {

  }

  // Property to manage the computer name
  //virtual 
  String^ WrapperBusinessClass::GetDescription()
  {
    CBusinessClass oBusinessClass;
    return CMarshalling::AnsiStringToDotNet(oBusinessClass.GetDescription());
  }
};
