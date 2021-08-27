# 2D-Movement-Physis-Unity

Attach the "PlayerMovement.cs" script to the Player
Attach the "GoThroughPlatform.cs" script to the platforms in which the player will be able to jump through from below

Create a new layer in your Unity project and call it "Ground" and then assign it to the ground and every other platform.

Create 2 children objects from the Player object, it doesn't matter how you name them. Just now that one should be located at the bottom-center of the player and the other to the right.
Attach those previous elements to Player > Player Movement (Script) > Ground Check Collider and Front Check and assign the Ground Layer to Ground. 
