# Warship
A game made for the Dutch Navy.

In this repository you will find the information for this project.

For this project we will be making a turn based strategy game. The goal of this project is to entertain/train marines and allow the navy to market them selves at conventions.


# Created Game Parts

Student Bob Hoogenboom:
  * [Hexagon-Grid](https://github.com/Bob-Hoogenboom/Warship/tree/master/WarshipGame/Assets/Scripts/Gameplay/Grid)
  * [Movement](https://github.com/Bob-Hoogenboom/Warship/blob/master/WarshipGame/Assets/Scripts/Gameplay/Movement.cs)  
  * [Camera Movement](https://github.com/Bob-Hoogenboom/Warship/tree/master/WarshipGame/Assets/Scripts/Gameplay/Camera)

Student Diego Ramon:
  * [UI](https://github.com/Bob-Hoogenboom/Warship/tree/master/WarshipGame/Assets/Scripts/UI)
  * [Audio](https://github.com/Bob-Hoogenboom/Warship/tree/master/WarshipGame/Assets/Scripts/UI/Sound)
  * [Ally Information](https://github.com/Bob-Hoogenboom/Warship/blob/master/WarshipGame/Assets/Scripts/UI/AllyInformationHandler.cs)
  * [VFX Usage](https://github.com/Bob-Hoogenboom/Warship/tree/master/WarshipGame/Assets/Scripts/Gameplay)

Student Robin Knol:
  * [Turn Manager](https://github.com/Bob-Hoogenboom/Warship/blob/master/WarshipGame/Assets/Scripts/Gameplay/TurnManager.cs)
  * [Attack System Player](https://github.com/Bob-Hoogenboom/Warship/blob/master/WarshipGame/Assets/Scripts/Gameplay/Player.cs)
  * [Attack System Enemy](https://github.com/Bob-Hoogenboom/Warship/blob/master/WarshipGame/Assets/Scripts/AI/Enemy.cs)

All three developers:
  * [Ship](https://github.com/Bob-Hoogenboom/Warship/blob/master/WarshipGame/Assets/Scripts/Gameplay/Ship.cs)

## Hexagon Grid System System, by Bob Hoogenboom

## Movement, by Bob Hoogenboom

## Camera Movement, by Bob Hoogenboom


***

## User Interface, by Diego Ramon

Within the game, the first element to notice is the User Interface. This will be seen in all three current scenes: <br>
* BasicUI (Main Menu)
* Level 'De Haven'
* Level 'Boor Eiland' 
<br>
Throughout the game, the user interface will assist the player with multiple functions and other information that is required to play the game. This creates a better experience for the player and informs the player about everything they need to know of our video game.

<img src="https://github.com/Bob-Hoogenboom/Warship/assets/54799111/ee93402a-e86f-43d4-a583-3b38e1ab2e97)" width=65% height=auto> <br>

<img src="https://github.com/Bob-Hoogenboom/Warship/assets/54799111/f94fe310-ca5d-4c34-92ec-8707b5319582)" width=65% height=auto>

## Audio, by Diego Ramon

With every action there will be some audio element played, in the audio folder is every script located which controller, changed and saves the audio. All of these script work together in order to let the player save their audio settings, apply the settings and actually hear result. Every individual audio setting is changed within a profile object and is saved within Unity itself, the next time the player opens the game, this profile is applied to the current audio settings. <br>

<img src="https://github.com/Bob-Hoogenboom/Warship/assets/54799111/52960db2-0456-400c-aec5-30378275f40a" width=65% height=auto>

## Ally Information, by Diego Ramon

If a player is in game, they will need information about their boats and enemy boats, so we created a User Interface panel which informs the payer about the current selected ship, their health and the active enemy ships. This panel changed its values based on which ship the player selects within the levels. In these panels we update three different values/elements: <br>
* Ally Ship Profile Picture.
* Health.
* Current active Enemy Ships. <br>


<img src="https://github.com/Bob-Hoogenboom/Warship/assets/54799111/ed7fe5a0-6bc2-4407-89cd-ff1d5da43afb" width=45% height=auto> <br>


## VFX Usage, by Diego Ramon

Within our game we make use of VFX effect to give the game more life. These effects are usually played when an action takes place or a function is invoked within an Unity Event. Each of these effects will need to be assigned individually in the Unity editor. 
These Unity events are created and Invoked in three different scripts; `Player.cs`, `Enemy.cs` and `Ship.cs`.

<img src="https://github.com/Bob-Hoogenboom/Warship/assets/54799111/d69e802b-13d9-44a5-addd-e553a48dc04b" width=45% height=auto> <br>

Most of these VFX effects are particle systems made by artists, and implemented by the developer. 


***

## Turn Manager, by Robin Knol

## Attack System, by Robin Knol

