﻿using Nt.Utils.ControlInterfaces;

namespace Nt.Controls.UserProfile;

public class UserProfileViewModel:NtViewModelBase<UserProfileControl> 
{
    public string UserName
    {
        get => TypedControl.UserName;
        set => TypedControl.UserName = value;
    }

    public string UserDisplayName 
    {
        get => TypedControl.UserDisplayName;
        set => TypedControl.UserDisplayName = value;
    }

    public string Bio
    {
        get => TypedControl.Bio;
        set => TypedControl.Bio = value;
    }
}
