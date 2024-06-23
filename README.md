# Event Bus System

This package provides you with a ready-made event management system.
Originally made in Owlcat Games and modified for your own needs.

## INSTALLATION

There are 3 ways to install this plugin:

- import [EventBus.unitypackage](https://github.com/llarean/EventBus/releases) via *Assets-Import Package*
- clone/[download](https://github.com/llarean/EventBus/archive/master.zip) this repository and move the *Plugins* folder to your Unity project's *Assets* folder
- *(via Package Manager)* add the following line to *Packages/manifest.json*:
  - `"com.llarean.eventbus": "https://github.com/llarean/EventBus.git",`

## HOW TO and EXAMPLE CODE

Simply create a new **NativeShare** object and customize it by chaining the following functions as you like (see example code):

1. Create an interface that will be inherited from IGlobalSubscriber

```csharp
using EventBus;

public interface IExampleHandler : IGlobalSubscriber
{
    void HandleExample();
}
```

2. The class that should respond to events should inherit from the created interface

```csharp
using EventBus;
using UnityEngine;

public class ListenerExample : MonoBehaviour, IExampleHandler
{
    public void HandleExample()
    {
        // Some code that needs to be executed when the event is Invoked
    }
    
    private void OnEnable()
    {
        EventBus.EventBus.Subscribe(this);
    
    private void OnDisable()
    {
        EventBus.EventBus.Unsubscribe(this);
    }
}
```

3. The class that triggers the events

```csharp
using EventBus;
using UnityEngine;

public class EventCallerExample : MonoBehaviour
{
    [SerializeField] private Button _save;

    private void Start()
    {
        _save.onClick.AddListener(RaiseSaveEvent);
    }

    private void RaiseSaveEvent()
    {
        EventBus.EventBus.RaiseEvent<IExampleHandler>(handler => handler.HandleSave());
    }
}
```

## SAMPLES
## PLANS