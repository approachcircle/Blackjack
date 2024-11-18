using System.ComponentModel;
using osu.Framework.Allocation;
using osu.Framework.Audio.Track;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Utils;
using osuTK;
using Box = osu.Framework.Graphics.Shapes.Box;

namespace Blackjack.Game;

public partial class GameEndOverlay(HandState handState) : OverlayContainer
{
    public Bindable<float> GlowRadius { get; set; } = new(0.0f);
    private bool startGlowIncrease = false;
    private const int enter_exit_duration = 400;
    private const int stay_alive_duration = 800;
    public HandState HandStateReflected => handState;
    private Track bustSample;

    [BackgroundDependencyLoader]
    private void load(ITrackStore tracks)
    {
        bustSample = tracks.Get("text_splash_fx.mp3");
        bustSample.Volume.Value = 0.5f;
        Colour4 overlayColour;
        string overlayText;
        switch (handState)
        {
            case HandState.Standing:
                overlayColour = Colour4.Blue;
                overlayText = "Standing";
                break;
            case HandState.Bust:
                overlayColour = Colour4.Red;
                overlayText = "Bust";
                break;
            case HandState.Blackjack:
            case HandState.TwentyOne:
                overlayColour = Colour4.Green;
                overlayText = "Twenty One";
                break;
            case HandState.Pushed:
                overlayColour = Colour4.BlueViolet;
                overlayText = "Push";
                break;
            default:
                throw new InvalidEnumArgumentException("missing overlay layouts for the enum value: " + handState);
        }
        Masking = true;
        GlowRadius.BindValueChanged((e) =>
        {
            EdgeEffect = new EdgeEffectParameters
            {
                Type = EdgeEffectType.Glow,
                Colour = overlayColour.Opacity(0.5f),
                Radius = e.NewValue
            };
        }, true);
        AutoSizeAxes = Axes.Y;
        RelativeSizeAxes = Axes.X;
        Anchor = Anchor.TopCentre;
        Origin = Anchor.TopCentre;
        Add(new Box
        {
            RelativeSizeAxes = Axes.X,
            Scale = new Vector2(1.0f, 55),
            Colour = overlayColour,
            Alpha = 0.75f,
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre
        });
        Add(new SpriteText
        {
            Colour = Colour4.White,
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            Text = overlayText,
            Font = FontUsage.Default.With(size: 32),
        });
    }

    protected override void Update()
    {
        base.Update();
        if (startGlowIncrease)
            GlowRadius.Value = Interpolation.ValueAt(Clock.ElapsedFrameTime, GlowRadius.Value, 200.0f, 0, 2000);
    }

    public override void Show()
    {
        base.Show();
        // game ending states (with accompanying audio to determine how long they stay alive for)
        if (HandStateReflected == HandState.Bust)
        {
            bustSample.Start();
            bustSample.Completed += () =>
            {
                Scheduler.Add(Hide);
            };
        }
        // game ending states (without accompanying audio)
        else if (HandStateReflected is HandState.TwentyOne or HandState.Blackjack or HandState.Pushed)
        {
            Scheduler.AddDelayed(Hide, stay_alive_duration * 1.5f);
        }
        // every other overlay
        else
        {
            Scheduler.AddDelayed(Hide, stay_alive_duration);
        }
    }

    protected override void PopIn()
    {
        this.ScaleTo(1f, enter_exit_duration, Easing.OutQuint).FadeInFromZero(enter_exit_duration, Easing.OutQuint);
        startGlowIncrease = true;
        // this.TransformTo(nameof(GlowRadius), 200.0f, 2000);
    }

    protected override void PopOut()
    {
        // this.ScaleTo(0f, 400, Easing.InQuint);
        this.FadeOut(enter_exit_duration, Easing.InQuint);
        startGlowIncrease = false;
    }

    protected override void Dispose(bool isDisposing)
    {
        base.Dispose(isDisposing);
        bustSample?.Dispose();
    }
}