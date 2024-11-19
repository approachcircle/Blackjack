using System;
using NUnit.Framework;

namespace Blackjack.Game.Tests.Visual;

[TestFixture]
public sealed partial class GameEndOverlayTest : BlackjackTestScene
{
    private GameEndOverlay overlayContainer;

    [Test]
    public void EachOverlayTypeTest()
    {
        foreach (HandState handState in Enum.GetValues(typeof(HandState)))
        {
            if (handState is HandState.Active or HandState.NotReady or HandState.Standing) continue;
            AddStep($"add overlay for '{handState.ToString()}'", () =>
            {
                overlayContainer = new GameEndOverlay(handState);
                Add(overlayContainer);
            });
            showAndCheckOverlay();
        }
    }

    private void showAndCheckOverlay()
    {
        AddStep("show overlay", () =>
        {
            overlayContainer.Show();
        });
        AddUntilStep("ensure overlay is present", () => overlayContainer.IsPresent);
        AddUntilStep("ensure overlay is now hidden", () => !overlayContainer.IsPresent);
        AddStep("clean-up old overlay", () => overlayContainer?.Expire());
    }
}
