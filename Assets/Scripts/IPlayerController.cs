using System;
using UnityEngine;

public interface IPlayerController {

    event EventHandler<NavigationControlArgs>LeftControl;
    event EventHandler<NavigationControlArgs> RightControl;
    event EventHandler<NavigationControlArgs>LevitateUp;
    event EventHandler<NavigationControlArgs> LevitateDown;
}

public class NavigationControlArgs : EventArgs
{
    public Vector3 movementVector;
}