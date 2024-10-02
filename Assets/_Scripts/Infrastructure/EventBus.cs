using System;

public static class EventBus
{
    public static Action OnHeroDeath;
    public static Action OnRestart;
    public static Action OnHardRestart;
    public static Action OnStartLevel;
    public static Action OnStartDialogue;
    public static Action OnEndDialogue;
}
