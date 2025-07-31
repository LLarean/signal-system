# Event Bus System

[![License](https://img.shields.io/badge/license-MIT-green.svg)](https://github.com/LLarean/EventBus/blob/master/LICENSE.txt)
[![CodeFactor](https://www.codefactor.io/repository/github/llarean/eventbus/badge)](https://www.codefactor.io/repository/github/llarean/eventbus)
[![Maintenance](https://img.shields.io/badge/Maintained%3F-yes-green.svg)](https://GitHub.com/Naereen/StrapDown.js/graphs/commit-activity)
[![Releases](https://img.shields.io/github/v/release/llarean/EventBus)](https://github.com/llarean/EventBus/releases)
![stability-unstable](https://img.shields.io/badge/stability-stable-green.svg)

This package provides you with a ready-made event management system.
Originally made in Owlcat Games and modified for your own needs.

## INSTALLATION

There are 3 ways to install this plugin:

- import [EventBus.unitypackage](https://github.com/llarean/EventBus/releases) via *Assets-Import Package*
- clone/[download](https://github.com/llarean/EventBus/archive/master.zip) this repository and move the *Plugins* folder to your Unity project's *Assets* folder
- *(via Package Manager)* Select Add package from git URL from the add menu. A text box and an Add button appear. Enter a valid Git URL in the text box:
  - `"https://github.com/llarean/EventBus.git"`
- *(via Package Manager)* add the following line to *Packages/manifest.json*:
  - `"com.llarean.eventbus": "https://github.com/llarean/EventBus.git",`

## HOW TO and EXAMPLE CODE


1. Create an interface that will be inherited from IGlobalSubscriber

```csharp
using EventBusSystem;

public interface IExampleHandler : IGlobalSubscriber
{
    void HandleExample();
}
```

2. The class that should respond to events should inherit from the created interface

```csharp
using EventBusSystem;
using UnityEngine;

public class ListenerExample : MonoBehaviour, IExampleHandler
{
    public void HandleExample()
    {
        // Some code that needs to be executed when the event is Invoked
    }
    
    private void OnEnable()
    {
        EventBus.Subscribe(this);
    
    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }
}
```

3. The class that triggers the events

```csharp
using EventBusSystem;
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
        EventBus.RaiseEvent<IExampleHandler>(handler => handler.HandleSave());
    }
}
```