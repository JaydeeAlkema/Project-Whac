# Whack-a-Mole – Unity Architecture Assignment

## Foreword
I want to start by saying that I really enjoyed this little assignment. The main reason is that I think the requirements for this assignment are not always met in real projects. Especially when scope cometh creeping.
I was able to really focus and setup the code in a way that would be, in my eyes, ideal. Does this mean that my design choices are without fault? No, absolutely not.

I approached this assignment with the expectation that I would be writing my code in a manner that I would not typically employ.
Because when I make a game, I usually just focus on making it work. Code quality typically suffers as a result. Now, it doesn't *have* to be this way, but in my experience, it usually does.

## Overview
This project is a **mobile-ready Whack-a-Mole game** built with Unity and C#, created as part of a technical assignment.

The focus of this project is **code quality, architecture, and maintainability**, not visuals or polish.  
All systems are designed to be extendable and testable. However, I must admit that I went overboard with these design decisions. As you will most likely come to agree with upon inspection of the code.

Target platform: **Android** (can be built for iOS without code changes).
A build for Android can be found in the `Builds` folder.

## High-Level Architecture
The project follows a **layered architecture** with clear separation between:
- **Core / Domain logic**  
  Pure C# logic containing game rules and state.
- **Unity drivers**  
  MonoBehaviours that bridge Unity’s lifecycle and the core logic.
- **UI layer (UI Toolkit)**  
  Uses an MVVM-inspired structure with explicit lifetimes.
- **Composition root**  
  Central place where core services are created and wired together.

  I really attached to the whole **Testable** part of this assignment. This is why I chose to make sure there is a separation between the pure C# part and the Unity part that makes it work.

## UI Architecture (MVVM)
The UI layer uses an **MVVM-inspired approach** tailored to Unity UI Toolkit:

- **View**  
  UXML + USS files, manipulated only through binders.
- **ViewModel**  
  Holds presentation state and subscribes to domain services.
- **Binder**  
  Binds UI elements to ViewModel data and events.
- **Scope**  
  Owns the lifetime of ViewModels and Binders.

This keeps UI logic free of MonoBehaviours and makes it easy to reason about UI lifetimes.
I must admit, I think almost the majority of my time was put in the UI.
I think I shot myself in the foot a little bit by choosing to use UI Toolkit.
But it was also a good learning experience, so I don't regret it :)

## Event System
An in-house **EventBus** is used for **decoupled communication** between systems.

It supports:
- Typed events
- Priorities
- Sticky events (state replay)
- Disposable subscriptions

The EventBus is primarily used for **broadcasting state changes**, such as:
- Timer ticks
- Mole hits
- Game lifecycle transitions

A direct quote from the `EventBus` class summary:

> I really enjoy using event buses. I'm aware that they can eventually
> be a pain in the ass. However, I figured that for a simple/small
> game like this, it's absolutely fine to make use of. Even though I
> kind of already abused this for “GameOnStartEventArgs” &
> “GameOnRestartEventArgs”. Because those events are meant for control
> flow. The difference between these events and others, is that these
> can be interpreted as commands, not facts. “MoleOnHitEventArgs” is a
> fact, “GameOnStartEventArgs” is not.

I was debating between using an eventbus and adding a simple state machine with direct references. Honestly, I just decided to use an eventbus since I've used it recently for a different project. So it just kind of felt right to use it here as well.

**Note:** _I've used ChatGPT for reviewing my code and making sure I'm keeping to my code standards. I also asked it multiple times for improvement points and applied them when I felt they were good. I also used it to help me structure this little README a bit better. Explaining what I did and why I did it is still a skill I'm lacking in. Oftentimes it comes down to gut feeling, which does not explain anything at all. Therefore, I used it to help me along the way._
