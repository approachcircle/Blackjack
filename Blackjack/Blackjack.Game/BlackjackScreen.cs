using osu.Framework.Graphics;
using osu.Framework.Input.Events;
using osu.Framework.Screens;
using osuTK;
using osuTK.Input;

namespace Blackjack.Game;

public partial class BlackjackScreen : Screen
{
    public override void OnEntering(ScreenTransitionEvent e)
    {
        Scale = new Vector2(0.8f, 0.8f);
        this.ScaleTo(1f, 200, Easing.OutQuint);
        this.FadeInFromZero(200, Easing.OutQuint);
    }

    public override bool OnExiting(ScreenExitEvent e)
    {
        this.ScaleTo(0.8f, 200, Easing.InQuint);
        this.FadeOut(200, Easing.InQuint);
        return false;
    }

    // public override void OnSuspending(ScreenTransitionEvent e)
    // {
    //     this.ScaleTo(0.8f, 200, Easing.OutQuint).FadeOut(200, Easing.OutQuint);
    // }

    public override void OnResuming(ScreenTransitionEvent e)
    {
        Alpha = 0f;
        Scale = new Vector2(0.8f, 0.8f);
        this.Delay(200).ScaleTo(1f, 200, Easing.OutQuint).FadeInFromZero(200, Easing.OutQuint);
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
