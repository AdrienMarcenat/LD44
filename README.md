# 2DGameToolkit

A 2D game toolkit for game jam purposes.

## Content

### Summary

* Event system with a user friendly declarative syntax (using reflection)
* Update system which allow to split the classic Unity Update into as many pass as you want, to ensure the order of execution. This also use a declarative syntax (using reflection again).
* An HSM (Hybrid State Machine) system, to control the gameflow, the AI...
* An input system, that allow user reconfiguration
* A simple sound manager
* A simple dialogue system
* A simple level generator that allows to script events like spawning, changing music, trigger dialogue
* A 2D game camera with zoom, shaking, focusing, and lane features.
* A bunch of common gameplay elements, such as health, moving object, weapon system, bullets...
* Several menus : main menu, input configuration, pause, end level, game over.
* A bunch of syntaxic sugar using extension methods

### How to use it

Check the doxygen documentation.
The unittest (in **2DGameToolKit/** folder) also provides examples of how to use the code.
Open the scene MainScene in Unity for a demo of all the features.

### Folder structure

The scripts are split into three pretty self explanatory folders:
* Engine : here are all the game agnostic scripts.
* Gameplay : here is where to put your all the game specific scripts. A few common scripts and a basic gameflow are already there, they give you an idea of ow to use the toolkit.
* UI : all the UI related scripts, some basic menu are already there

### Intended use

This toolkit is made for event base design. You should rely on event to maintain nice decoupled architecture. The UI code for instance has to be removable without changing any gameplay related code.

AI is supposed to be driven with the HSM system.

## Contributing 

### Intended design

The goal is to provide a user friendly, easy to debug framework, with a bunch of basic gameplay element that every game need.
To that extent a declarative syntax is enforced using reflection. This is not great for performance but it does not matter for 2D games made for a game jam. Debugging is made easier with the help of assertion, unittest, and the event base structure that allow easy decoupling.
This project should remain game agnostic as much as possible.

### Naming

| Item          | Prefix  |   Case     |
|---------------|---------|------------|
| class         |         | PascalCase |
| method        |         | PascalCase |
| Interface     |   I     | PascalCase |
| Enum          |   E     | PascalCase |
| static member |   ms_   | PascalCase |
| other member  |   m_    | PascalCase |

### Brackets

Always place on a newline

### if statement

Always use brackets even if their is only one line:

if(condition)
{
    a = 1;
}

### Comments

Always use // for one line and /** **/ for multiline.

### UnitTest

Unit testing is done with the **MSTest** framework.
You will find unit test in the **2DGameToolKit/** folder.
Only Unity agnostic functionalities is tested (i.e. scripts in the **Engine/** folder).
Please always make torough test when you add new features, as it provides both stability and a good example of how to use your code.

### Documentation

Documentation is done with **Doxygen**.
Please always document your code using the /** **/ comment style.
Do not document self explanatory method such as getter / setter.
