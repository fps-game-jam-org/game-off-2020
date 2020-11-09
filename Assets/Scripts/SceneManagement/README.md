# SceneManagement

The scene management allows you to specify a title scene, a credits scene, and any number of level scenes.  This works by calling the `SceneChanger.LoadTitle()`, `SceneChanger.LoadCredits()`, and `SceneChanger.LoadLevel(int index)` repectively.

There are button prefabs (`/Assets/Prefabs/UIPrefabs/To Credits Button.prefab`, `To Title Button.prefab`, and `To Level Button.prefab`) that call these methods on press.

Improvements can be made in way the `SceneTransitionButton` script works.  It gets subclassed e.g. by `ToTitleButton` and one of the members of `SceneTransitionButton` has one of its methods called.  Seems awkward to me.

It'd also be good to add a retry button that just reloads the current level.
