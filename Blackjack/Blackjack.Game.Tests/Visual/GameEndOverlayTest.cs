using NUnit.Framework;

namespace Blackjack.Game.Tests.Visual;

[TestFixture]
public sealed partial class GameEndOverlayTest : BlackjackTestScene
{
    private GameEndOverlay overlayContainer;

    [SetUp]
    public void SetUp()
    {
        overlayContainer?.Expire();
    }

    [Test]
    public void BustOverlayTest()
    {
        AddStep("add bust overlay", () =>
        {
            overlayContainer = new GameEndOverlay(HandState.Bust);
            Add(overlayContainer);
        });
        showAndCheckOverlay();
    }

    [Test]
    public void StandOverlayTest()
    {
        AddStep("add stand overlay", () =>
        {
            overlayContainer = new GameEndOverlay(HandState.Standing);
            Add(overlayContainer);
        });
        showAndCheckOverlay();
    }

    [Test]
    public void PushOverlayTest()
    {
        AddStep("add push overlay", () =>
        {
            overlayContainer = new GameEndOverlay(HandState.Pushed);
            Add(overlayContainer);
        });
        showAndCheckOverlay();
    }

    [Test]
    public void WinOverlayTest()
    {
        AddStep("add win overlay", () =>
        {
            overlayContainer = new GameEndOverlay(HandState.TwentyOne);
            Add(overlayContainer);
        });
        showAndCheckOverlay();
    }

    private void showAndCheckOverlay()
    {
        AddStep("show overlay", () =>
        {
            overlayContainer.Show();
        });
        AddUntilStep("ensure overlay is present", () => overlayContainer.IsPresent);
        AddUntilStep("ensure overlay is now hidden", () => !overlayContainer.IsPresent);
    }
}
