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

*Jumping* - To make the player jump is fairly simple and just requires setting an upward velocity for the player when the user presses the "Jump" input. However, you must also check that the player is touching the ground before they can jump. To do this, I [referenced this video](https://www.youtube.com/watch?v=FXpUb-H54Oc) and added a small visual rectangle that was located around the player's feet. Then, I could call the function "Physics2D.OverlapBox()" and check if the visual rectangle overlapped with the terrain layer to tell if the player was touching the ground. This way the player would only jump when grounded.

*Double Jump* - To implement double jumping, I referenced the [code I found in this video](https://www.youtube.com/watch?v=QGDeafTx5ug). The way the code works is that you have a jump counter that keeps track of how many extra jumps you have. The player is then set to have an extra jump anytime it is detected to be grounded. If a player jumps while they are grounded we won't decrement the jump counter. However, if a player isn't grounded and the jump counter is greater than 0, they are allowed to double jump. The double jump is implemented the same as a jump where we set an upwards velocity on the player.

*Wall Slide* - To implement wall sliding, I referenced the [code found here](https://gist.github.com/bendux/b6d7745ad66b3d48ef197a9d261dc8f6). Wall sliding requires a visual rectangle similar to the check if the grounded visual rectangle. This rectangle is placed to the side of the player in the direction it is facing. This way whenever the player was touching and facing a wall, I could verify this by calling "Physics2D.OverlapBox()" with the visual rectangle and the terrain layer. If a player was detected to be wall sliding, I coded it so that the minimum velocity of the player would be clamped, slowing down the descent of the player to a fixed speed, referencing [this code here](https://www.youtube.com/watch?v=O6VX6Ro7EtA). This simulated the player clinging to the wall and bracing their fall.

*Wall Jump* - To implement wall jumping, I referenced the [code found here](https://gist.github.com/bendux/b6d7745ad66b3d48ef197a9d261dc8f6). The way the code works is that if you detect you are wall sliding and the user presses the jump input, the player will jump upwards and away from the wall. I do this by setting the velocity of the player to be upwards and in the opposite direction the player is facing. I will also flip the direction of the player when they jump. Another important feature of the wall jump is that you want to force the player in the wall jump direction and prevent them from cancelling the jump. This makes the timing the jump more important and makes the jump feel more responsive and impactful. To do this we set a flag for when the player is wall jumping. While this flag is set, the player won't be able to move laterally. Then we call an invoke some duration after the wall jump that will reset the flag so that the player can move normally.

*Dash* - To implement the dash, I referenced the [code found here](https://gist.github.com/bendux/aa8f588b5123d75f07ca8e69388f40d9). The way the dash works is that when the player presses the "shift" key, we will fix the player's horizontal velocity to some large value in the direction the player is facing for some duration. While the player dashes, we prevent other movement like jumping and changing directions. We also add a cooldown duration that prevents the player from continuously dashing. To do this, we call a dash coroutine which will manage setting the player velocity, enabling and disabling the dash flag, and wait for the desired durations required for the dash itself and the dash cooldown. Another special feature with dashing is the ability to dash while wall sliding. This is a special case where we must flip the direction of the player before they dash otherwise they will dash towards the wall.

## Animation and Visuals

**List your assets including their sources and licenses.**

**Describe how your work intersects with game feel, graphic design, and world-building. Include your visual style guide if one exists.**

### Ian - Animation and Visuals

*Level Visuals* - For level visuals, I used [assets found here](https://assetstore.unity.com/packages/2d/characters/pixel-adventure-1-155360). I used this [video here](https://www.youtube.com/watch?v=QkbGr1rAya8&list=PLrnPJCHvNZuCVTz6lvhR81nnaf1a-b67U&index=2) to figure out how to use the tile palette and draw the levels. For level visuals, I tried to mix up the looks for each level. The tutorial level has a very common looking color scheme similar to that of Super Mario Bros. Level 1 has a pink and orange color scheme, Level 2 has a white and brown color scheme, and Level 3 has a underground Super Mario theme to it. All levels have two tile map objects, one for the physical terrain that the player interacts with and another for the background which the player can't interact with.

*Player Visuals and Animations* - For player visuals, I used [the Pixel Adventure 1 asset](https://assetstore.unity.com/packages/2d/characters/pixel-adventure-1-155360). I used this [video here](https://www.youtube.com/watch?v=GChUpPnOSkg&list=PLrnPJCHvNZuCVTz6lvhR81nnaf1a-b67U&index=4) to figure out how to use Animations and the Animator. The player animation state machine is pretty complicated and includes states for running, falling, jumping, double jumping, wall sliding, dashing, getting hit, and dying. There are eight different animations being used for the frog and are all from the Pixel Adventure 1 asset. In addition to player animation sprites, I also added a trail renderer that will be displayed whenever the player is dashing. This gives a nice responsive visual cue to the player that they initiated the dash.

*Trap/Obstacles Visuals and Animations* - For obstacle visuals, I used [the Pixel Adventure 1 asset](https://assetstore.unity.com/packages/2d/characters/pixel-adventure-1-155360). This helped provide me with the necessary sprites for spikes, spike balls, the spike ball shooter, moving saws, and moving platforms. Spikes and spike balls didn't require any animations. The moving saws and platforms required a single animation state for a rotating effect that gave them more life. Finally the spike ball shooter had two states, one which was an idle state where it would just blink and another that was triggered when it fired a spike ball. 

## Input

**Describe the default input configuration.**

**Add an entry for each platform or input style your project supports.**

### Ian - Input

*Player Movement Input* - The default input is for the standard keyboard. I organized the inputs in the input manager so that they can be generalized to other platforms in the future. "Horizontal" is for the player's lateral movement and can be controlled with the left and right arrow keys. "Fire3" is for the player dash and can be controlled with the shift key. Finally "Jump" is for the player jump, double jump, and wall jump which can be controlled with the space bar. Inputs for RoboFroggy were designed to be simple and easy to learn.

## Game Logic

### Ian - Game Logic

*Spike Ball Shooter and Spike Ball Logic* - The code for the spike ball shooter was actually more complicated than imagined. The first step was rotating the spike ball shooter to follow the player. This involved finding the angle of the vector between the player transform and the spike ball shooter transform. I didn't want the spike ball shooter to always instaneously stick to the player's direction so I used Unity's RotateTowards() function which rotates the spike ball shooter with some rotational speed. The next step was firing spikeballs which would come out with a specified velocity in the direction the spike ball shooter was facing. This required some more math of calculating the spike ball's velocity vector and setting its magnitude. However, to fire a spike ball required that the spike ball come out of the spike ball shooter. This was a problem because both the spike ball and the spike ball shooter have a 2D collider so I needed to disable this collision. However, I didn't want to completely disable all collisions with either the spike ball or spike ball shooter since I wanted both game objects to be interactive with each other and the player. So what I did instead was code the spike ball shooter to disable collisions with itself and its fired spike ball for a brief duration. That way the spike ball would come cleanly out of the shooter and then I would reenable that collision so that the spike ball would continue interacting with the shooter in case they ever touched afterwards. Another problem with the spike ball was that if the shooter fired too many spike balls, over time there would be too many spike balls in the game which would ruin the experience. To resolve this problem I would remove every spike ball that was fired after some period. I also included a fade out effect that would decrease the alpha color value of the spike ball over time.


*Sticky Platform Logic* - I referenced [this video here](https://www.youtube.com/watch?v=UlEE6wjWuCY&list=PLrnPJCHvNZuCVTz6lvhR81nnaf1a-b67U&index=9) to implement the logic that allows players to stick to a platform while it moves. The way it works is that I add a trigger collider at the top of the platform that will trigger whenever a player lands on it. Once triggered, the player's transform parent will be set to that of the platform so that the player will move along with the plaform. Once the player leaves the platform, I will reset its transform parent, "unsticking" it from the platform. 

*Waypoint Follower Logic* - I referenced [this video here](https://www.youtube.com/watch?v=UlEE6wjWuCY&list=PLrnPJCHvNZuCVTz6lvhR81nnaf1a-b67U&index=9) to implement the logic that allows obstacles to move and follow a set of waypoints.

*Trap Collisions/Triggers and Player Hit Detection* - 

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

*Jump Cut* - 

*Hurt/Damaged Mechanic* -

*Death Mechanic* -

*Spike Balls and Spike Ball Shooter Features* -
