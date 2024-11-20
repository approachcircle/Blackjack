using osu.Framework.Bindables;

namespace Blackjack.Game;

public class Settings
{
    public static Bindable<bool> SFXEnabled { get; set; } = new(true);
}
