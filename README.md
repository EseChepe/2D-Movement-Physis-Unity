# 2D-Movement-Physis-Unity

Attach the "PlayerMovement.cs" script to the Player
Attach the "GoThroughPlatform.cs" script to the platforms in which the player will be able to jump through from below
Attach the "CameraFollowPlayer.cs" script to the Main Camera

Create a new layer in your Unity project and call it "Ground" and then assign it to the ground and every other platform.

Create 2 children objects from the Player object, it doesn't matter how you name them. Just now that one should be located at the bottom-center of the player and the other to the right.
Attach those previous elements to Player > Player Movement (Script) > Ground Check Collider and Front Check and assign the Ground Layer to Ground. 

The 'Go through platforms' should have a Platform Effector 2D component, and the Box Collider 2D component should have the "Used by Effector" checkbox checked.

In the "GoThroughPlatform.cs" script you'll notice that I use Input.GetButton("Down")/Input.GetButton("Up")/Input.GetButton("Left")/Input.GetButton("Right") functions. This are button calls that I configured myself in Edit > Project Settings > Input Manager. To get the code to run properly you either have to configure it yourself or you can change Input.Getbutton to Input.GetKey.


My resources:

https://www.youtube.com/watch?v=KCzEnKLaaPc

https://www.youtube.com/watch?v=QGDeafTx5ug&t=302s

https://www.youtube.com/watch?v=E_c539LwXuE&t=437s

https://www.youtube.com/watch?v=M_kg7yjuhNg&t=244s

https://www.youtube.com/watch?v=_QnPY6hw8pA&list=LL&index=2&t=502s

https://www.youtube.com/watch?v=w4YV8s9Wi3w&t=312s
