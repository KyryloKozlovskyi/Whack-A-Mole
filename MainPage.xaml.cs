using Whack_A_Mole.Views;
using Whack_A_Mole;
namespace Whack_A_Mole; public partial class MainPage : ContentPage
{
    public MainPage()
    { InitializeComponent(); }
    private void btnFourByFour_Clicked(object sender, EventArgs e)
    { Shell.Current.GoToAsync(nameof(fourByFour)); }
    private void btnFiveByFive_Clicked(object sender, EventArgs e)
    { Shell.Current.GoToAsync(nameof(fiveByFive)); }
}