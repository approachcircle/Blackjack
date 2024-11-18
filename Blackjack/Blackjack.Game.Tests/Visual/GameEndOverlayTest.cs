using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK.Graphics;

namespace Blackjack.Game.Tests.Visual;

[TestFixture]
public sealed partial class GameEndOverlayTest : BlackjackTestScene
{
    private readonly GameEndOverlay overlayContainer;

    public GameEndOverlayTest()
    {
        overlayContainer = new GameEndOverlay(Colour4.BlueViolet, "test overlay");
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
