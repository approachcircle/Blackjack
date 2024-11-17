using osu.Framework.Graphics;
using osu.Framework.Input.Events;
using osu.Framework.Screens;
using osuTK;
using osuTK.Input;

namespace Blackjack.Game;

public partial class BlackjackScreen : Screen
{
    private const float shrink_size = 0.8f;
    private const int animation_time = 200;
    public override void OnEntering(ScreenTransitionEvent e)
    {
        Scale = new Vector2(shrink_size, shrink_size);
        this.ScaleTo(1f, animation_time, Easing.OutQuint);
        this.FadeInFromZero(animation_time, Easing.OutQuint);
    }

    public override bool OnExiting(ScreenExitEvent e)
    {
        this.ScaleTo(shrink_size, animation_time, Easing.InQuint);
        this.FadeOut(animation_time, Easing.InQuint);
        return false;
    }

    // public override void OnSuspending(ScreenTransitionEvent e)
    // {
    //     this.ScaleTo(0.8f, 200, Easing.OutQuint).FadeOut(200, Easing.OutQuint);
    // }

    public override void OnResuming(ScreenTransitionEvent e)
    {
        Alpha = 0f;
        Scale = new Vector2(shrink_size, shrink_size);
        this
            .Delay(animation_time)
            .ScaleTo(1f, animation_time, Easing.OutQuint)
            .FadeInFromZero(animation_time, Easing.OutQuint);
    }

    protected override bool OnKeyDown(KeyDownEvent e)
    {
        if (!e.Repeat)
        {
            if (e.Key == Key.Escape)
            {
                this.Exit();
                return true;
            }
        }
        return base.OnKeyDown(e);
    }
}
