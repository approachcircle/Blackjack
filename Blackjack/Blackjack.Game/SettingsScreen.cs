using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.UserInterface;
using osuTK;

namespace Blackjack.Game;

public partial class SettingsScreen : BlackjackScreen
{
    [BackgroundDependencyLoader]
    private void load()
    {
        Anchor = Anchor.Centre;
        Origin = Anchor.Centre;
        InternalChildren =
        [
            new FillFlowContainer
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Direction = FillDirection.Vertical,
                Spacing = new Vector2(10, 10),
                Children = [
                    new BasicCheckbox
                    {
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        LabelText = "SFX",
                        Current = { BindTarget = Settings.SFXEnabled },
                    },
                    new BasicCheckbox
                    {
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        LabelText = "Five card charlie (currently disabled)", // disabled until its fixed
                        // Current = { BindTarget = Settings.FiveCardCharlie }
                    },
                    new BasicCheckbox
                    {
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        LabelText = "Flashing buttons",
                        Current = { BindTarget = Settings.FlashingButtons }
                    }
                ]
            }
        ];
    }
}
