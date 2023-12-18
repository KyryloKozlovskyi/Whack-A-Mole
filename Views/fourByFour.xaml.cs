using System;
using System.Diagnostics;

namespace Whack_A_Mole.Views;

public partial class fourByFour : ContentPage
{
    //random
    private Random random;
    private int score = 0;
    private int countdown = 15;
    //timer
    private System.Timers.Timer timer;
    private int interval = 2000;
    bool isTapEnabled = true;


    public fourByFour()
    {
        InitializeComponent();
        random = new Random();
        SetUpTimers();
        setTopScore(); //sets top score on launch
    }

    private void btnFourByFourStart_Clicked(object sender, EventArgs e) //start btn
    {

        MovetheMole();
        timer.Start();
        //disables start button
        btnFourByFourStart.IsEnabled = false;
        //enables reset button
        btnFourByFourReset.IsVisible = true;
        btnFourByFourReset.IsEnabled = true;
    }

    public void setTopScore()
    {
        topScoreLbl.Text = Convert.ToString(Preferences.Default.Get("top_score", 0));  //Gets the top score from Preferences and sets it
    }

    async private void btnFourByFourReset_Clicked(object sender, EventArgs e)
    {
        timer.Stop(); //stops the timer to ask reset q
        bool answer = await DisplayAlert("Game Reset", "Do you want to reset the game?", "Yes", "No");
        if (answer)
        {
            //resets the timer, score
            //enables start button
            countdown = 15;
            timerLbl.Text = "15";
            score = 0;
            scoreLbl.Text = "0";
            moleInGrid.IsVisible = false;
            btnFourByFourReset.IsVisible = false;
            btnFourByFourReset.IsEnabled = false;
            btnFourByFourStart.IsEnabled = true;
        }
        else
        {
            //continues the game
            timer.Start();

        }
    }
    async private void TapGestureRecognizer_Tapped(object sender, EventArgs e) //taps
    {
        if (isTapEnabled)
        {
            if (countdown > 0 && btnFourByFourStart.IsEnabled == false)
            {
                isTapEnabled = false;
                await moleInGrid.FadeTo(0, 300); //fade out on tap
                IncreaseScore();
                MovetheMole();
                //IncreaseScore();

            }
        }
    }
    private void SetUpTimers() //timer set up
    {
        timer = new System.Timers.Timer
        {
            Interval = interval
        };
        timer.Elapsed += Timer_Elapsed;
    }
    private void Timer_Elapsed(object sender, EventArgs e) //timer dispartcher
    {
        Dispatcher.Dispatch(
          () =>
          {
              TimerFunction();
          }
        );
    }
    async private void TimerFunction()
    {
        if (countdown != 0)
        {
            await moleInGrid.FadeTo(0, 750); //fade out on tick
            MovetheMole();
            --countdown;
            timerLbl.Text = countdown.ToString(); //updates timer string
        }
        else if (countdown == 0)
        {
            timer.Stop();

            if (score > Preferences.Default.Get("top_score", 0))
            {
                Preferences.Default.Set("top_score", score); //updates top score
            }
            setTopScore(); //sets top score
            //popup
            await DisplayAlert("Out Of Time!", "Your score: " + Convert.ToString(score) + "\nTop score: " + Convert.ToString(Preferences.Default.Get("top_score", 0)) + "\n\nPress the \"Reset\" button to continue", "OK");
        }
    }

    async private void MovetheMole()
    {
        int r, c;
        r = random.Next(4);
        c = random.Next(0, 4);
        moleInGrid.SetValue(Grid.RowProperty, r);
        moleInGrid.SetValue(Grid.ColumnProperty, c);
        moleInGrid.IsVisible = true;
        await moleInGrid.FadeTo(1, 400); // fade in on move
        isTapEnabled = true;
    }
    private void IncreaseScore() //score increase func
    {
        score += 10;
        scoreLbl.Text = score.ToString();
    }

    //cleanup
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        timer.Stop();

        // Place your code to stop execution or perform cleanup here.
    }
}