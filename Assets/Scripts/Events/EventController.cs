using System;

public class EventController
{
    public Action baseEvent;
    public void AddListener(Action listener) => baseEvent += listener;
    public void RemoveListener(Action listener) => baseEvent -= listener;
    public void InvokeEvent() => baseEvent?.Invoke();
}

public class EventController<T>
{
    public Action<T> _baseEvent;
    public void AddListener(Action<T> listener) => _baseEvent += listener;
    public void RemoveListener(Action<T> listener) => _baseEvent -= listener;
    public void InvokeEvent(T type) => _baseEvent?.Invoke(type);
}

