namespace Whack_A_Mole;

public partial class MainPage : ContentPage
{
    private Random random;
    private int score = 0;
    private int countdown = 5;
    private System.Timers.Timer timer;
    private int interval = 1000;
    private List<Score> scoreboard;


    public MainPage()
    {
        InitializeComponent();
        random = new Random();
        SetUpTimers();
        scoreboard = new List<Score>();
        MakeFakeList();

    }

    private void MakeFakeList()
    {
        scoreboard.Add(new Score("Donny", 5000000));
        scoreboard.Add(new Score("Paul", 5));
        scoreboard.Add(new Score("John", 0));
        scoreboard.Add(new Score("Ian", 90));


    }
    private void SetUpTimers()
    {
        /*Dispatcher.StartTimer(TimeSpan.FromMilliseconds(1000),
             () =>
             {
                 TimerFunction();
                 return true;
             }
             );*/
        timer = new System.Timers.Timer
        {
            Interval = interval
        };
        timer.Elapsed += Timer_Elapsed;


    }

    private void Timer_Elapsed(object sender, EventArgs e)
    {
        Dispatcher.Dispatch(
            () =>
            {
                TimerFunction();
            }
            );
        //TimerFunction();
    }

    private void TimerFunction()
    {
        if (countdown != 0)
        {
            --countdown;
            timer_lbl.Text = countdown.ToString();
        }
        else
        {
            timer.Stop();
        }

    }
    public void MoveTheMole()
    {
        int r, c;
        r = random.Next(3);
        c = random.Next(0, 3);
        soyjack.SetValue(Grid.RowProperty, r);
        soyjack.SetValue(Grid.ColumnProperty, c);
        soyjack.IsVisible = true;
        IncreaseScore();
    }
    private void ImageTapped(object sender, EventArgs e)
    {
        MoveTheMole();
    }

    private void StartBtn_Clicked(object sender, EventArgs e)
    {
        MoveTheMole();
        timer.Start();
        //SetUpTimers();
    }

    private void SwitchBtn_Clicked(object sender, EventArgs e)
    {

    }

    private void IncreaseScore()
    {
        score += 10;
        score_lbl.Text = score.ToString();
    }

    private async void ShowLeaderboard_CLicked(object sender, EventArgs e)
    {
        //await NavigationPage.
    }
}


