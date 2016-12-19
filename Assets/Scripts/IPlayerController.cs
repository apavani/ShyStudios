using System;

public interface IPlayerController {

    event EventHandler<NavigationControlArgs>LeftControl;
    event EventHandler<NavigationControlArgs> RightControl;
    event EventHandler<NavigationControlArgs>LevitateUp;
    event EventHandler<NavigationControlArgs> LevitateDown;
}