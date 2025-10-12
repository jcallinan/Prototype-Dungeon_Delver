# Prototype-Dungeon_Delver

This project is a Unity Example Game from the book [Introduction to Game Design, Prototyping, and Development, 2nd Edition](https://book.prototools.net/), by Jeremy Gibson Bond.

It is a classic action-adventure game prototype, inspired by the [The Legend of Zelda](https://en.wikipedia.org/wiki/The_Legend_of_Zelda_(video_game)) game.

You can play it here — [Dungeon Delver Live](https://jcallinan.github.io/Prototype-Dungeon_Delver/)


[Another example online](https://shaman37.itch.io/prototype-dungeon-delver)



<p align="center">
  <img src="https://user-images.githubusercontent.com/17680666/166609608-863d84a2-1dfc-4917-a3b3-fc06a774c7e0.png" />
</p>

## How to play

- Dray has a total of 6 health, shown by the _health balls_ displayed on the panel to the right of the screen.
- Use the _arrow keys_ to move Dray around the dungeon. 
- Press the 'Z' button to attack enemies with your sword, dealing 2 damage to enemies and knocking them back.
- Press the 'X' button to use your grappler weapon, after you pick it up. The _grappler_ deals 1 damage to enemies and pulls Dray towards walls.

If you land on a red tile, after grappling to a wall, your position will be reseted to the last safe location you were in, typically the beggining of the room
you were in, and suffer a health penalty.

If you die, your position will be reseted to the beggining of the dungeon.

## Enemies
The prototype has 2 types of enemies:

- ### Skeletos
<p align="center">
  <img src="https://user-images.githubusercontent.com/17680666/166610118-7488261e-01e6-4e33-887d-0b42fbc3dbed.png" />
</p>

The _Skeletos_ enemy inflicts 1 damage and knocks Dray back, on collision.

- ### Spiker
<p align="center">
  <img src="https://user-images.githubusercontent.com/17680666/166610124-97cfcccc-5b06-4e3a-b3a0-dc6298f42e8a.png" />
</p>

The _Spiker_ enemy senses Dray, and when he is near a Spiker, it will charge at him in a single direction.

It deals 1 damage and also knocks Dray back.

## Items
Along the dungeon you will find items laying around or you will kill monsters that will drop them.

These items can be:

- A key, which opens locked doors in the dungeon;
<p align="center">
  <img src="https://user-images.githubusercontent.com/17680666/166610101-6831fa46-2b65-4ef8-be93-ddacfde33797.png"/>
</p>

- A health item, which restores 2 health to Dray;
<p align="center">
  <img src="https://user-images.githubusercontent.com/17680666/166610004-69ff7720-1388-4a69-b160-e605bc2e8747.gif"/>
</p>

## Example

https://user-images.githubusercontent.com/17680666/166610677-2c6ff56e-47df-41b7-b262-1813728adf50.mp4
