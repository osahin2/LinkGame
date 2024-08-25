using Service_Locator;
namespace App
{
    public interface IGameContext
    {
        IServiceLocator Locator { get; }
    }
}
