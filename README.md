# ![StateLogic](https://github.com/user-attachments/assets/10211b69-3cdf-441a-8a3f-198bf901a677)

A lightweight state machine for Unity projects

## Features

- Uses regular `MonoBehaviour` scripts as states.
- Allow transitions by events.
- All events are broadcast to all state machines on the scene.

## Introdution

*StateLogic* is a lightweight finite state machine for Unity projects compatible with Unity 2021+ and 6.

It provides the *bare bones* for creating a simple yet effective state management for your games.

Not sure if a state machine is the right choice for your project? [This chapter](https://gameprogrammingpatterns.com/state.html) from the *Game Programming Patterns* book will help you.

An [example](Assets/StateLogic/Example/) is available with the basics about the plugin.

## Quick start

1. Create an empty `GameObject` in the scene and add a `State Machine/State Machine Manager` component.

2. A settings asset will be created on `Resources/Editor/StateLogic/StateLogicEditorSettings.asset`. Clicking on the `Events` button in the State Machine inspector will open the settings file.

3. Add transition events to the settings file.

4. Create state scripts by inheriting from `StateLogic.State`:

> [!NOTE]  
> A state is just a regular `MonoBehaviour`, so it's possible to use built-in methods like `Update()`.

```cs
using StateLogic;
using UnityEngine;

public class MyState : State {
    public override void OnEnter() {
        // Called when entering the state.
        
        // You can use SendEvent("eventName") to transition between states:
        SendEvent("MyTransitionEventName");
    }

    public override void OnExit() {
        // Called when exiting the state.
    }
}
```

5. Add the state script in the `GameObject` the State Machine is and set transition events.

## License

Licensed under the [The MIT License (MIT)](http://opensource.org/licenses/MIT). Please see [LICENSE](https://raw.githubusercontent.com/intentor/statelogic/main/LICENSE) for more information.
