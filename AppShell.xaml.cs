using Whack_A_Mole.Views;

namespace Whack_A_Mole;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(fourByFour), typeof(fourByFour));
        Routing.RegisterRoute(nameof(fiveByFive), typeof(fiveByFive));
    }
}
