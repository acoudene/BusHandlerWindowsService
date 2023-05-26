#pragma once

namespace MFCBusinessLbrary
{
  class CBusinessClass
  {
  public:
    CBusinessClass();
    virtual ~CBusinessClass();

    const CString GetDescription() const;
  };
}

