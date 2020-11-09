class ToCreditsButton : SceneTransitionButton
{
    public override void OnButtonPress()
    {
        SceneChangerObject.LoadCredits();
    }
}
