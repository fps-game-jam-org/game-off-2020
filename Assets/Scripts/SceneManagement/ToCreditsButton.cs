class ToCreditsButton : SceneTransitionButton
{
    public override void SendForSceneLoad()
    {
        SceneChangerObject.LoadCredits();
    }
}
