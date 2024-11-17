using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK.Graphics;

namespace Blackjack.Game.Tests.Visual;

[TestFixture]
public sealed partial class OverlayTest : BlackjackTestScene
{
    private readonly OverlayContainer overlayContainer;

    public OverlayTest()
    {
        overlayContainer = new TestOverlay();
        overlayContainer.Add(new Box
        {
            Width = 300,
            Height = 150,
            Colour = Color4.DimGray,
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
        });
        overlayContainer.Add(new SpriteText
        {
            Text = "hello overlay",
            Font = FontUsage.Default.With(size: 52),
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre
        });
        Add(overlayContainer);
    }

    [Test]
    public void BasicPopoverTest()
    {
        AddStep("show overlay", () =>
        {
            overlayContainer.Show();
        });
        AddUntilStep("ensure overlay is present", () => overlayContainer.IsPresent);
        AddWaitStep("let it soak", 10);
        AddStep("hide overlay", () =>
        {
            overlayContainer.Hide();
        });
        AddUntilStep("ensure overlay is hidden", () => !overlayContainer.IsPresent);
    }
}

internal sealed partial class TestOverlay : OverlayContainer
{
    public TestOverlay()
    {
        AutoSizeAxes = Axes.Both;
        Anchor = Anchor.Centre;
        Origin = Anchor.Centre;
    }
    protected override void PopIn()
    {
        this.MoveToY(0, 400, Easing.OutQuint);
        this.FadeInFromZero(400);
    }

    protected override void PopOut()
    {
        this.MoveToY(Width, 400, Easing.InQuint);
        this.FadeOut(400);
    }
}
