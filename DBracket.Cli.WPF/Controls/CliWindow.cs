using DBracket.Cli.WPF.Input;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;

namespace DBracket.Cli.WPF.Controls;

public class CliWindow : Window, IDisposable
{
    #region "----------------------------- Private Fields ------------------------------"
    private bool _isInitialized;
    #endregion



    #region "------------------------------ Constructor --------------------------------"
    public CliWindow()
    {
        
    }
    #endregion



    #region "--------------------------------- Methods ---------------------------------"
    #region "----------------------------- Public Methods ------------------------------"
    public void Dispose()
    {
        if (InputHandler is not null)
        {
            InputHandler.Dispose();
        }
    }

    public void Initialize()
    {
        // Show the window offscreen when the application starts, so that it doesn't stutter when the user actually wants to show it
        Top = -10000;
        Left = -10000;
        Visibility = Visibility.Hidden;
        ShowInTaskbar = false;
        Show();
    }
    public void Initialize(TextBox inputTextBox, Popup autoComplete)
    {
        if (InputHandler is not null)
            InputHandler.Dispose();
        InputHandler = new CliInputHandler(inputTextBox, autoComplete);

        // Show the window offscreen when the application starts, so that it doesn't stutter when the user actually wants to show it
        Top = -10000;
        Left = -10000;
        Visibility = Visibility.Hidden;
        ShowInTaskbar = false;
        Show();
    }

    public void ShowAtCenter(double topOffset, double leftOffset)
    {
        if (_isInitialized == false)
        {
            _isInitialized = true;
            Top = ((SystemParameters.WorkArea.Height - this.Height) / 2) + topOffset + 100;
            Left = (SystemParameters.WorkArea.Width - this.Width) / 2 + leftOffset;
            Hide();
            Visibility = Visibility.Visible; // Ready to show without stutter
            Opacity = 1.0;
            this.Deactivated += HandleWindowDeactivated;
            Topmost = true;
        }
        else
        {
            Top = ((SystemParameters.WorkArea.Height - this.Height) / 2) + topOffset + 100;
            var t = (SystemParameters.WorkArea.Width - this.ActualWidth);
            var t2 = t / 2;
            var t3 = t2 + leftOffset;
            Left = t3;
        }

        var t4 = 880;
        //Top = ((SystemParameters.WorkArea.Height - this.Height) / 2) + topOffset + 100;
        //Left = (SystemParameters.WorkArea.Width - this.Width) / 2 + leftOffset;

        double targetTop = (SystemParameters.WorkArea.Height - this.Height) / 2 - 100.0;
        var slide = new DoubleAnimation
        {
            From = ((SystemParameters.WorkArea.Height - this.Height) / 2) + topOffset + 100,
            //From = (SystemParameters.WorkArea.Height - this.Height) / 2  + 100,
            To = targetTop,
            Duration = TimeSpan.FromMilliseconds(200),
            //EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
        };
        //this.BeginAnimation(System.Windows.Window.OpacityProperty, fadeIn);
        this.BeginAnimation(System.Windows.Window.TopProperty, slide);

        Task.Run(() =>
        {
            Task.Delay(20).Wait();
            IsShown = true;
            Application.Current.Dispatcher.Invoke(() =>
            {
                Show();
                Activate();
                InputHandler?.Focus();
                //Left = t4;
            });
        });
    }

    public void FadeOutAndClose()
    {
        
        var fadeOut = new DoubleAnimation
        {
            From = this.Left,
            To = this.Left-this.ActualWidth,
            Duration = TimeSpan.FromMilliseconds(200),
        };
        fadeOut.Completed += (s, e) =>
        {
            Hide();
            Opacity = 1.0;
            this.BeginAnimation(System.Windows.Window.LeftProperty, null);
        };
        this.BeginAnimation(System.Windows.Window.LeftProperty, fadeOut);
    }

    public void FadeInAndShow()
    {
        Top = ((SystemParameters.WorkArea.Height - this.Height) / 2) -100;
        
        //Left = (SystemParameters.WorkArea.Width - this.Width) / 2 + 200;
        Show();

        Task.Delay(20).Wait();

        var fadeIn = new DoubleAnimation
        {
            From = (SystemParameters.WorkArea.Width - this.Width) / 2 + Width,
            To = (SystemParameters.WorkArea.Width - this.Width) / 2,
            Duration = TimeSpan.FromMilliseconds(200),
        };
        fadeIn.Completed += (s, e) =>
        {
            Opacity = 1.0;
            IsShown = true;
            this.BeginAnimation(System.Windows.Window.LeftProperty, null);
        };
        this.BeginAnimation(System.Windows.Window.LeftProperty, fadeIn);
    }

    public new void Hide()
    {
        InputHandler?.CloseAutoCompletePopup();
        IsShown = false;
        base.Hide();
    }

    public new void Close()
    {
        InputHandler?.CloseAutoCompletePopup();
        IsShown = false;
        base.Hide();
    }
    #endregion

    #region "----------------------------- Private Methods -----------------------------"

    #endregion

    #region "------------------------------ Event Handling -----------------------------"
    private void HandleWindowDeactivated(object? sender, EventArgs e)
    {
        Debug.WriteLine("Window Deactivated");
        Hide();
    }
    #endregion
    #endregion



    #region "--------------------------- Public Propterties ----------------------------"
    #region "------------------------------- Properties --------------------------------"
    public bool IsShown { get; private set; }
    public CliInputHandler? InputHandler { get; private set; }
    #endregion

    #region "--------------------------------- Events ----------------------------------"

    #endregion
    #endregion
}