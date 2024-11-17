using osu.Framework.Allocation;
using osu.Framework.Screens;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace Blackjack.Game;

public partial class MainMenuScreen : BlackjackScreen
{
    private FillFlowContainer buttonContainer;
    [BackgroundDependencyLoader]
    private void load()
    {
        Anchor = Anchor.Centre;
        Origin = Anchor.Centre;
        InternalChildren =
        [
            new SpriteText
            {
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                Font = FontUsage.Default.With(size: 72),
                Text = "Blackjack",
                Y = 50
            },
            buttonContainer = new FillFlowContainer
            {
                Direction = FillDirection.Horizontal,
                Spacing = new Vector2(10, 0),
                Anchor = Anchor.BottomCentre,
                Origin = Anchor.BottomCentre,
                AutoSizeAxes = Axes.Both,
                Y = -50
            }
        ];
        buttonContainer.AddRange([
            new BlackjackButton
            {
                Anchor = Anchor.BottomCentre,
                Origin = Anchor.BottomCentre,
                Action = () => this.Push(new MainScreen()),
                Text = "Play"
            },
            new BlackjackButton
            {
                Anchor = Anchor.BottomCentre,
                Origin = Anchor.BottomCentre,
                Action = () => Game.Exit(),
                Text = "Exit"
            }
        ]);
    }

    public override bool OnExiting(ScreenExitEvent e)
    {
        var result = base.OnExiting(e);
        Game.Exit();
        return result;
    }
}
