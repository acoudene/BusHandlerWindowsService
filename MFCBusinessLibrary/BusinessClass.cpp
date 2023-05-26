#include "pch.h"
#include "BusinessClass.h"

namespace MFCBusinessLbrary
{
  CBusinessClass::CBusinessClass()
  {

  }

  //virtual 
  CBusinessClass::~CBusinessClass()
  {

  }

  const CString CBusinessClass::GetDescription() const
  {
    return _T("You're doing a business thing!");
  }
}