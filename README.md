# RoboFroggy

## Summary ##

**A paragraph-length pitch for your game.**

RoboFroggy is an action-packed 2D side-scrolling adventure! Take control of a robot ninja frog as you navigate through a series of treacherous levels teeming with deadly traps and formidable obstacles. Brace yourself for menacing spikes and sawblades, and treacherous moving platforms. Stay on your toes as spike ball shooters, massive stone contraptions armed with deadly projectiles, try to thwart your progress. Master the art of precision and finesse with the game's fluid movement mechanics, allowing you to execute double jumps, wall jumps, and dashes. Try utilizing the unique "pogo" ability to bounce off different perilous objects. Only the most skilled players will be able to conquer RoboFroggy's daunting trials and emerge victorious.

## Gameplay Explanation ##

**In this section, explain how the game should be played. Treat this as a manual within a game. It is encouraged to explain the button mappings and the most optimal gameplay strategy.**

In RoboFroggy, the objective is simple: guide the robot ninja frog to reach the flag and advance to the next level. However, prepare to face a relentless barrage of dangerous obstacles and traps. To assist you on this adventure, we have equipped you with a range of movement mechanics that will help you traverse each level.

Moving left and right: Utilize the left and right arrow keys or the 'a' and 'd' keys to control the lateral movement of the ninja frog.

Jump: Press the space bar to jump. Remember, the duration you hold the space bar determines the height of your jump.

Double Jump: Take advantage of the ninja frog's ability to execute a second jump while airborne. After initiating the first jump, tap the space bar again to perform a double jump.

Wall Slide: When approaching a wall, your ninja frog can slide down its surface and slow its descent. This maneuver allows for a controlled descent and buys you time to plan your next move.

Wall Jump: While executing a wall slide, press the space bar to wall jump and gain extra height or distance.

Dash: Activate the dash ability by pressing the shift key. Experience a burst of horizontal velocity, allowing you to traverse hazardous gaps or evade fast-moving obstacles. 

Pogo: When confronted with landing on an obstacle during mid-air descent, invoke the pogo move by quickly pressing the left mouse button. This ability enables you to rebound off the obstacle and avoid taking damage.

Remember, success in RoboFroggy lies in mastering these movement mechanics. Strategize, and combine these abilities to overcome each level's challenges. Good luck and have fun!

**If you did work that should be factored in to your grade that does not fit easily into the proscribed roles, add it here! Please include links to resources and descriptions of game-related material that does not fit into roles here.**

### Ian - Level Design

*Tutorial Level (Scene0)* - 

*Level 1 (Scene1)* - 

*Level 2 (Scene2)* - 

*Level 3 (Scene3)* - 

*Victory Level (Scene4)* - 

# Main Roles #

Your goal is to relate the work of your role and sub-role in terms of the content of the course. Please look at the role sections below for specific instructions for each role.

Below is a template for you to highlight items of your work. These provide the evidence needed for your work to be evaluated. Try to have at least 4 such descriptions. They will be assessed on the quality of the underlying system and how they are linked to course content. 

*Short Description* - Long description of your work item that includes how it is relevant to topics discussed in class. [link to evidence in your repository](https://github.com/dr-jam/ECS189L/edit/project-description/ProjectDocumentTemplate.md)

Here is an example:  
*Procedural Terrain* - The background of the game consists of procedurally-generated terrain that is produced with Perlin noise. This terrain can be modified by the game at run-time via a call to its script methods. The intent is to allow the player to modify the terrain. This system is based on the component design pattern and the procedural content generation portions of the course. [The PCG terrain generation script](https://github.com/dr-jam/CameraControlExercise/blob/513b927e87fc686fe627bf7d4ff6ff841cf34e9f/Obscura/Assets/Scripts/TerrainGenerator.cs#L6).

You should replay any **bold text** with your relevant information. Liberally use the template when necessary and appropriate.

## Producer

**Describe the steps you took in your role as producer. Typical items include group scheduling mechanism, links to meeting notes, descriptions of team logistics problems with their resolution, project organization tools (e.g., timelines, depedency/task tracking, Gantt charts, etc.), and repository management methodology.**

## User Interface

**Describe your user interface and how it relates to gameplay. This can be done via the template.**

## Movement/Physics

**Describe the basics of movement and physics in your game. Is it the standard physics model? What did you change or modify? Did you make your movement scripts that do not use the physics system?**

### Ian - Movement/Physics

*Physics Environment* - For managing the physics of player gravity and collisions, I utilized RigidBody 2D and Collider 2D. The player has a rigid body giving it physics properties for gravity and has a combination of a box and capsule collider for simulating contacts with the environment. The level terrain has a Tilemap Collider and most of the traps like the saws, moving platforms, and spike ball shooter have a collider so that the player will interact with them. The player has the "Slippery" material making it have no friction with the environment to prevent unwanted friction forces. However, the level terrain still has friction so that spike balls will roll on the ground. 

*Moving Left and Right* - Moving left and right is fairly simple and I referenced this [piece of code I found here](https://gist.github.com/bendux/b6d7745ad66b3d48ef197a9d261dc8f6#file-playermovement-cs-L53). The way it works is that it gets the horizontal axis input from the user (either left and right arrows or 'a' and 'd' keys) and then sets the player's velocity to match the corresponding lateral direction. 

*Jumping* - To implement jumping,  To make the player jump is fairly simple and just requires setting an upward velocity for the player when the user presses the "Jump" input. However, you must also check that the player is touching the ground before they can jump. To do this, I referenced the video and added a small visual rectangle that was located around the player's feet. Then, I could call the function "Physics2D.OverlapBox()" and check if the visual rectangle overlapped with the terrain layer to tell if the player was touching the ground. This way the player would only jump when grounded.

*Double Jump* - To implement double jumping, I referenced the [code I found in this video](https://www.youtube.com/watch?v=QGDeafTx5ug). The way the code works

*Wall Slide* - To implement wall sliding, I referenced the [code found here](https://gist.github.com/bendux/b6d7745ad66b3d48ef197a9d261dc8f6)

*Wall Jump* - To implement wall jumping, I referenced the [code found here](https://gist.github.com/bendux/b6d7745ad66b3d48ef197a9d261dc8f6)

*Dash* - To implement wall jumping, I referenced the [code found here](https://gist.github.com/bendux/aa8f588b5123d75f07ca8e69388f40d9)

## Animation and Visuals

**List your assets including their sources and licenses.**

**Describe how your work intersects with game feel, graphic design, and world-building. Include your visual style guide if one exists.**

### Ian - Animation and Visuals

*Level Visuals* - For level visuals, I used [assets found here](https://assetstore.unity.com/packages/2d/characters/pixel-adventure-1-155360). I used this [video here](https://www.youtube.com/watch?v=QkbGr1rAya8&list=PLrnPJCHvNZuCVTz6lvhR81nnaf1a-b67U&index=2) to figure out how to use the tile palette and draw the levels. 

*Player Visuals and Animations* - For player visuals, I used [assets found here](https://assetstore.unity.com/packages/2d/characters/pixel-adventure-1-155360). I used this [video here](https://www.youtube.com/watch?v=GChUpPnOSkg&list=PLrnPJCHvNZuCVTz6lvhR81nnaf1a-b67U&index=4) to figure out how to use Animations and the Animator. Describe animation state machine

*Trap/Obstacles Visuals and Animations* - For obstacle visuals, I used [assets found here](https://assetstore.unity.com/packages/2d/characters/pixel-adventure-1-155360).

## Input

**Describe the default input configuration.**

**Add an entry for each platform or input style your project supports.**

### Ian - Input

*Player Movement Input* - describe reasoning behind input

## Game Logic

### Ian - Game Logic

*Spike Ball Shooter Logic* - 

*Spike Ball Logic* -

*Sticky Platform Logic* - I referenced [this video here](https://www.youtube.com/watch?v=UlEE6wjWuCY&list=PLrnPJCHvNZuCVTz6lvhR81nnaf1a-b67U&index=9) to implement the logic that allows players to stick to a platform while it moves. The way it works is that it 

*Waypoint Follower Logic* - I referenced [this video here](https://www.youtube.com/watch?v=UlEE6wjWuCY&list=PLrnPJCHvNZuCVTz6lvhR81nnaf1a-b67U&index=9) to implement the logic that allows obstacles to move and follow a set of waypoints.

*Camera Controller Logic* - I referenced [this video here](https://www.youtube.com/watch?v=Uv5tfMSKlnU&list=PLrnPJCHvNZuCVTz6lvhR81nnaf1a-b67U&index=3) to create a position locked camera that will lock its position to the player. The way it works is that. I also added a feature so that the camera won't go below a set minimum value. This is valuable because

*Out of Bounds Death/Reset* - I added a third tilemap behind "Background" and "Terrain" 

**Document what game states and game data you managed and what design patterns you used to complete your task.**

# Sub-Roles

## Cross-Platform

**Describe the platforms you targeted for your game release. For each, describe the process and unique actions taken for each platform. What obstacles did you overcome? What was easier than expected?**

## Audio

**List your assets including their sources and licenses.**

**Describe the implementation of your audio system.**

**Document the sound style.** 

## Gameplay Testing

**Add a link to the full results of your gameplay tests.**

**Summarize the key findings from your gameplay tests.**

## Narrative Design

**Document how the narrative is present in the game via assets, gameplay systems, and gameplay.** 

## Press Kit and Trailer

**Include links to your presskit materials and trailer.**

**Describe how you showcased your work. How did you choose what to show in the trailer? Why did you choose your screenshots?**



## Game Feel

**Document what you added to and how you tweaked your game to improve its game feel.**

### Ian - Game Feel

*Coyote Time* - https://www.youtube.com/watch?v=RFix_Kg2Di0

*Jump Buffer* - https://www.youtube.com/watch?v=RFix_Kg2Di0

*Fall Gravity* - https://www.youtube.com/watch?v=2S3g8CgBG1g

*Hurt/Damaged Mechanic* -

*Death Mechanic* -

*Spike Balls and Spike Ball Shooter Features* -
