#pragma once

using namespace System;

namespace WrapperBusinessLibrary
{
  public ref class WrapperBusinessClass
  {
  public:

    WrapperBusinessClass(void);
    virtual ~WrapperBusinessClass(void);

    // Property to manage the computer name
    virtual String^ GetDescription();
  };
}
