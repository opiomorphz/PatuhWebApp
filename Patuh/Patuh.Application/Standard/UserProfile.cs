
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
//===========================================================================
//# DESCRIPTION  : Class to maintain user profile
//# INSTRUCTION  : Put all properties of UserProfile that could be used
//#                globaly in APP Web Application
//===========================================================================

using APP.Framework.Application;


public class UserProfile : AbstractUserProfile
{
    #region "Data Member"
    #endregion
    private bool m_isPA02User;

    #region "Properties"
    public bool IsPA02User
    {
        get { return m_isPA02User; }
        set { m_isPA02User = value; }
    }
    #endregion
}

