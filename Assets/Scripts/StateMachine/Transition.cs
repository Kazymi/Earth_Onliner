using System;

public class Transition
{
    public State StateTo { get; set; }
    public Func<bool> Condition { get; set; }
}