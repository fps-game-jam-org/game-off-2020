class ToLevelButton : SceneTransitionButton
{
    public int levelIndex = 0;

    public override void OnButtonPress()
    {
        LoadLevel(levelIndex);
    }
}
