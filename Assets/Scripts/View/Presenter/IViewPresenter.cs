using SnakeGame.Config;

namespace SnakeView
{
    public interface IViewPresenter
    {
        void DrawBoard (GameConfiguration configuration, System.Action successCallback);
    }
}