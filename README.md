# 2D-Movement-Physis-Unity

Attach the "PlayerMovement.cs" script to the Player
Attach the "GoThroughPlatform.cs" script to the platforms in which the player will be able to jump through from below

Create a new layer in your Unity project and call it "Ground" and then assign it to the ground and every other platform.

Create 2 children objects from the Player object, it doesn't matter how you name them. Just now that one should be located at the bottom-center of the player and the other to the right.
Attach those previous elements to Player > Player Movement (Script) > Ground Check Collider and Front Check and assign the Ground Layer to Ground. 

The 'Go through platforms' should have a Platform Effector 2D component, and the Box Collider 2D component should have the "Used by Effector" checkbox checked.

In the "GoThroughPlatform.cs" script you'll notice that I use the Input.GetButton("Down"); function on lines 19, 25 and 36. This is a button call that I configured myself and it would be complicated to explain, so instead change that code to Input.GetKey("down"); or Input.GetKey("s"); depending on how you like it more.

My resources:
https://www.youtube.com/watch?v=KCzEnKLaaPc

https://www.youtube.com/watch?v=QGDeafTx5ug&t=302s

https://www.youtube.com/watch?v=E_c539LwXuE&t=437s

https://www.youtube.com/watch?v=M_kg7yjuhNg&t=244s
