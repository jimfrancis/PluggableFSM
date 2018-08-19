# PluggableFSM
PluggableFSM

`PluggableFSM` is a free tool to use for your unity projects where a Finite State Machine solution is required for discrete transitions between states.

Open the `Default Scene` that comes with the project to view/ edit a working demo.

## Features

- Complete Plug and Play: States, Actions and Decisions.
- Build up complex behaviours by stringing together actions and transitions.
- Simple interface and flow, `Drag-and-Drop` agency unto any GameObject
- Leverages SOLID programming principles to allow extension and substition for your own implementation of State Machines or State Actions.

## Installation

Simply clone this repo or pull down the files to add them into your Unity project. I recommend keeping the file structure, however it can be adapted to whatever framework you might be using.

## Getting Started

The project offers a straight-forward approach to begin adding AI to your scene. For an in-depth description of how a FSM operates have a read of [this article](https://gamedevelopment.tutsplus.com/tutorials/finite-state-machines-theory-and-implementation--gamedev-11867).

Simply follow the steps below to begin adding agency to a gameobject. The functionality in this project leverages Unity's `Scriptable Object` class, which allows us to create assests of our scripts and pass them as static files to your agent.

1. Create your gameobject in your scene. Now give it an empty transform as a child and set its position to be just in front of its parent AND facing the same direction. I recommend (0, 0.5, 0.5) relative to the scale of your parent.
2. Drag the 'Agent.cs' script unto your gameobject, this will open up (3) public fields in the inspector; Eyes, Stats and Initial State.

`Eyes`: gameobjects child transform in the hierachy.

`Stats`: an instance of the `AgentStats.cs` class as a scriptable object.

`Initial State`: an instance of the `State.cs` class as a scriptable object.

## Instantiate a Scriptable Object

An instance of a scriptable object can be created by right-clicking within the project tab and navigating to "Create/Finite State Machine/..." where you can select the relevant file to create.

## Creating a State

A state object is comprised of three major components: Actions, Decisions and Transitions.

`Actions`: Are the steps to execute during the uptick of any particular state.

`Decisions`: Contains the logic for transitioning states.

`Transitions`: Stores the list of Decisions for a state.

When a state object is created it must be populated by existing instances of Actions and Decisions.

