class ToTitleButton : SceneTransitionButton
{
    public override void SendForSceneLoad()
    {
        SceneChangerObject.LoadTitle();
    }
}
