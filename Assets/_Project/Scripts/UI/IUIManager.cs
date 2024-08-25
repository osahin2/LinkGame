namespace UI
{
    public interface IUIManager
    {
        AlphaTransition AlphaTransition { get; }
        void ShowView(ViewTypes viewTypes);
        void HideView(ViewTypes viewTypes);
    }
}
