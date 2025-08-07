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

### Unity Package Manager (Recommended)

1. Open Unity Package Manager
2. Select "Add package from git URL"
3. Enter: https://github.com/llarean/signal-system.git

### Alternative Methods

- Unity Package: Download and import [SignalSystem.unitypackage](https://github.com/llarean/signal-system/releases) via Assets -> Import Package
- Manual: Clone or [download](https://github.com/llarean/signal-system/archive/master.zip) the repository and copy the Plugins folder to your Assets directory
- Manifest: Add to `Packages/manifest.json`:
```
json{
  "dependencies": {
    "com.llarean.eventbus": "https://github.com/llarean/signal-system.git"
  }
}
```

## Quick Start

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
## Advanced Usage  

Multiple Event Handling
A single class can handle multiple event types:

```csharp
public class GameManager : MonoBehaviour, IPlayerDeathHandler, ILevelCompleteHandler
{
    public void HandlePlayerDeath(int playerId, Vector3 position) { }
    public void HandleLevelComplete(int levelId, float completionTime) { }

    private void OnEnable() => SignalSystem.Subscribe(this);
    private void OnDisable() => SignalSystem.Unsubscribe(this);
}
```

## Performance Tips

- Always unsubscribe in OnDisable() or equivalent to prevent memory leaks
- Use specific event interfaces rather than generic ones for better performance
- Consider using object pooling for frequently raised events
- Avoid heavy computations in event handlers

## Troubleshooting
### Common Issues

**Events not firing?**

- Ensure the listener is subscribed with `SignalSystem.Subscribe(this)`
- Check that the interface inherits from `IGlobalSubscriber`
- Verify the event is being raised correctly

**Memory leaks?**

- Always call SignalSystem.Unsubscribe(this) in cleanup methods
- Use OnDisable() for MonoBehaviour components

**Performance issues?**

- Avoid subscribing/unsubscribing frequently
- Keep event handlers lightweight

## Testing

The system includes comprehensive unit tests. To run them:  
*Using Unity Test Runner*  
```Window → General → Test Runner```

## Contributing
We welcome contributions! Please:

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## License

This project is licensed under the MIT License - see the [LICENSE.txt](https://github.com/LLarean/signal-system?tab=MIT-1-ov-file) file for details.

## Acknowledgments
Originally developed at Owlcat Games and adapted for the open-source community.

## Additional Resources

- [Unity Events Documentation](https://docs.unity3d.com/Manual/unity-events.html)
- [EventBus Pattern Explanation](https://en.wikipedia.org/wiki/Publish%E2%80%93subscribe_pattern)
- [C# Event Handling Best Practices](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/events/)

---

<div align="center">

**Made with ❤️ for the community**

⭐ If this project helped you, please consider giving it a star!

</div>
