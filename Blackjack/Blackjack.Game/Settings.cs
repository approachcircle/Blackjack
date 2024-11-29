using osu.Framework.Bindables;

namespace Blackjack.Game;

public static class Settings
{
    public static Bindable<bool> SFXEnabled { get; } = new(true);
    public static Bindable<bool> FiveCardCharlie { get; } = new(false); // temporarily disabled by default
    public static Bindable<bool> FlashingButtons { get; } = new(true);
}
