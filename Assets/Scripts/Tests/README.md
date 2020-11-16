# Tests

Unit tests should go into either the `EditMode/` or `PlayMode/` folder.  `PlayMode/` is for tests that need to run in an active scene (most common if you're subclassing `MonoBehaviour`), and `EditMode/` is for everything else.

You can find documentation for how to write the tests [here](https://docs.unity3d.com/Packages/com.unity.test-framework@1.1/manual/index.html).

## Possible Issues

If you create a new folder for code, the tester won't see it at first.  You need to add an Assembly Definition to that folder and add a reference to that definition to the Assembly Definitions in `PlayMode/` and `EditMode/`
