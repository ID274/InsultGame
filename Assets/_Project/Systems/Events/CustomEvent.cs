using System;
public class CustomEvent
{
    public event Action OnEventTriggered;

    public virtual void Invoke()
    {
        OnEventTriggered?.Invoke();
    }
}
