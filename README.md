# Project is temporarily on hold until 2.12.2018 (working on another project)!

The main goal of this project is learning. Working on a "big" project like this helps me to get a better concept of video game development and also improves my programming skills. While also giving me a small dive into other areas like shaders, art and game AI.
My current focus is building a solid fundament as a base for my game. Therefore I want to create and improve most game-systems before starting to push more content like characters and abilities or improving the sprite quality.

My general idea for this game is:
*    The world consists of Tiles which can contain TileContent(e.g. grass). TileContent can have contents(e.g. trees) and characters which react different to damage(e.g. fire damage burns tree).
*    Abilities work against enemies but also against friends. Abilities can deal damage to lots of tiles or put effects on them (e.g. burning), so friendly fire is not always preventable.
*    Player and AI have different rulesets. 
 <br/>Player: can only move or cast abilities a certain amount before the turn ends.
 <br/>AI: each unit has its own AI and can move and cast per turn, but abilities are delayed so that the player is able to react.
*    Level can contain bosses, which can be multiple tiles big or have an objectives in the world.
 
# Future milestones:

* A rework for AI and indicator system which are currently only prototypes and a few extensions to the effect system
* Tile Editor: add or remove new tiles, the world can be more than a rectangle (*v0.2*)
* First boss: boss system, the need for extensions in path finding because of its size (multiple tiles) (*v0.3*)


# v0.1 


![](https://user-images.githubusercontent.com/42123779/47404197-b04b2080-d74c-11e8-8c88-428719e2d15f.gif)

---

Only selfmade assets are used besides two packages: Pixel Perfect Camera and Text Mesh pro.

<p float="left" align="middle">
  <img src="https://user-images.githubusercontent.com/42123779/47404198-b0e3b700-d74c-11e8-9d68-970d2a1a1727.png" width="30%" />
  <img src="https://user-images.githubusercontent.com/42123779/47404199-b0e3b700-d74c-11e8-8a61-3639655b50fb.png" width="30%" /> 
</p>



* Each character and enemy have 2 unique abilities (a fireball which deals damage and burns the tile or a poison wave which coats all nearby tiles with poison)
* 2 different TileContents (water and grass) and Contents (tree and rock)

--- 

<p float="left" align="middle">
  <img src="https://user-images.githubusercontent.com/42123779/47404200-b0e3b700-d74c-11e8-8fc9-2f41c9724514.png" width="30%" />
  <img src="https://user-images.githubusercontent.com/42123779/47404201-b0e3b700-d74c-11e8-8d3a-fb9958ed29f6.png" width="30%" /> 
</p>

* The game is also capable of local multiplayer and a prototype AI looking for the best path and casts one of its abilities
* Indicator for damage and effects which updates dynamically, e.g. on movement, or changing color depending on the damage type.  It also shows possible casts, tiles in range for movement and hover
* Self made path finding with priority queue and a simple UI

---


<p float="left" align="middle">
  <img src="https://user-images.githubusercontent.com/42123779/47404194-b04b2080-d74c-11e8-8496-f69cd8f93b28.png" width="30%" />
  <img src="https://user-images.githubusercontent.com/42123779/47404195-b04b2080-d74c-11e8-991b-0961a7db32ae.png" width="30%" />
</p>

* 4 Effects: thunder which deals also damage to all nearby water tiles, poison and burning deal periodic damage on turn end or by walking over it. Spider webs which prevents movement
* Environmental factor: Fire destroys trees, water tiles halve the fire damage and spiders are immune to poison
* Functional death of Units

---

![](https://user-images.githubusercontent.com/42123779/47404196-b04b2080-d74c-11e8-9cc8-7926bc3b5f0b.png)
* New Editor functionalities: a script changes alpha values on sprite import, helping shaders to draw an outline; a BuildWindow to simplify editing the world with undo and a custom inspector to generate a new clean world

