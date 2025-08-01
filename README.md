# Signal System (formerly EventBus)

[![Releases](https://img.shields.io/github/v/release/llarean/EventBus)](https://github.com/llarean/signal-system/releases)
[![License](https://img.shields.io/badge/license-MIT-green.svg)](https://github.com/LLarean/signal-system/blob/master/LICENSE.txt)
![stability-stable](https://img.shields.io/badge/stability-stable-green.svg)
[![Tests](https://img.shields.io/badge/Tests-NUnit-green.svg)]()
[![CodeFactor](https://www.codefactor.io/repository/github/llarean/signal-system/badge)](https://www.codefactor.io/repository/github/llarean/signal-system)
[![Maintenance](https://img.shields.io/badge/Maintained%3F-yes-green.svg)](https://GitHub.com/llarean/signal-system/graphs/commit-activity)

**Signal System** is a ready-to-use event management system for Unity and .NET, based on the EventBus pattern.  
Originally developed at Owlcat Games and adapted for open use.

The system allows you to easily implement event subscription and broadcasting between different parts of your application without tight coupling between objects.

## Features

- Simple integration into Unity and .NET projects
- Automatic detection of all subscriber interfaces via `IGlobalSubscriber`
- High performance through subscriber type caching
- Safe handling of null values and types
- Unit-test support (NUnit)
- Flexible and extensible architecture

## Installation

There are several ways to install:
- Import [SignalSystem.unitypackage](https://github.com/llarean/signal-system/releases) via *Assets-Import Package*
- Clone or [download](https://github.com/llarean/signal-system/archive/master.zip) the repository and move the *Plugins* folder to your Unity project's *Assets* folder
- Using Package Manager:
  - Select "Add package from git URL" and enter:
    `"https://github.com/llarean/signal-system.git"`
  - Or add to *Packages/manifest.json*:
    `"com.llarean.eventbus": "https://github.com/llarean/signal-system.git",`

## Usage Example

1. Create an interface that inherits from `IGlobalSubscriber`:

```csharp
using GameSignals;

public interface IExampleHandler : IGlobalSubscriber
{
    void HandleExample();
}
```

2. The class that should respond to events implements this interface:

```csharp
using GameSignals;
using UnityEngine;

public class ListenerExample : MonoBehaviour, IExampleHandler
{
    public void HandleExample()
    {
        // Code to execute when the event is raised
    }

    private void OnEnable()
    {
        SignalSystem.Subscribe(this);
    }

    private void OnDisable()
    {
        SignalSystem.Unsubscribe(this);
    }
}
```

3. The class that triggers the event:

```csharp
using GameSignals;
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
        SignalSystem.Raise<IExampleHandler>(handler => handler.HandleExample());
    }
}
```
