# SceneManagement

The **SceneChanger** class allows any GameObject to change to any scene at any time with little overhead.  It implements a method `static void ChangeToScene(SceneManifest scene)` that will switch to a new scene, and an event `static event EventHandler LoadFinished` that is invoked when a scene has finished loading.  Make sure to unsubscribe from the event when you're done with it (e.g. in `MonoBehaviour.OnDestroy`).  You also shouldn't subscribe lambdas to this event, since they cannot be unsubscribed.

**SceneManifest** is an enum located in `/Assets/Scenes/SceneManifest.cs` that gives a code-referable name to all scenes.  You can pass a scene with `SceneManifest.YourScene`.  When a designer wants to add a scene to the game, they need to make sure to also add it to `SceneManifest.cs`.  A good future thing to do with this is have all entries in the scene manifest added to the Build Settings at compile time, which is possible since C# has type reflection.

**SceneTransitionButton** is a component you can attach to a GameObject related to the UI.  It has a Scene property that you set in the inspector, and it will switch the game to that scene on press.  It requires that you have a **Button** component attached to that GameObject.
