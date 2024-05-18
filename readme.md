# Project Notes - Carrot animation scene


- I am using some 3rd party assets for doing animation

[DOTween](https://dotween.demigiant.com)


## Animation

- appearance of carrots
- throw
    - use trail renderer & apply physics
    - if the mouse velocity is low, use vertical animation

### Sequenncing

## appearance of carrots

- spawn carrots one by one
  - got some code from online which will queue coroutines.

- using multiple sequences to animate each carrot

## animating leaves
- when the carrot fly upwards, have the leaves detach & fall back to earth
- TODO: have the leaves not pass through the ground

### Cinemachine

I may not have time to implement this.

- when scene is loaded have camera zoomed in on the planter
- after the carrots are spawned switch to different camera

[Unity Cinemachine](https://unity.com/unity/features/editor/art-and-design/cinemachine)

## Technical

- I experienced a confounding issue when doing animation; the game object would get deleted.
 - it turns out you cannot have a trail renderer active when performing scaling animations using DOTween or other systems; I don't know if this is a bug in Unity or they way it works
 - the solution is to set the emitter to turned off until needed:

 ```
gameObject.GetComponent<TrailRenderer>().emitting = false;
 ```

 [Unity Trail Renderer](https://docs.unity3d.com/Manual/class-TrailRenderer.html)