namespace MonkeyFinder.View;

public partial class MainPage : ContentPage
{
	// This constructor will cause the MonkeyService that the MonkeysViewModel
	// is dependent on to be created since the singleton for MonkeysViewModel has been added to the MauiApp.
	public MainPage(MonkeysViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}

