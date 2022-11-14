namespace MonkeyFinder.ViewModel;

//[INotifyPropertyChanged]
public partial class BaseViewModel : ObservableObject// : INotifyPropertyChanged
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;

    [ObservableProperty]
    string title;

    public bool IsNotBusy => !isBusy;

    #region This code was replaced by Community Toolkit
    //public bool IsBusy 
    //{
    //    get => isBusy;
    //    set 
    //    {
    //        if (isBusy == value)
    //            return;
    //        isBusy = value;
    //        OnPropertyChanged();
    //        // Same as OnPropertyChanged(nameof(IsBusy))
    //        // Or OnPropertyChanged("IsBusy")
    //        OnPropertyChanged(nameof(IsNotBusy));
    //    }
    //}

    //public string Title 
    //{
    //    get => title;
    //    set 
    //    {
    //        if (title == value)
    //            return;

    //        title = value;
    //        OnPropertyChanged();
    //    }
    //}

    //public event PropertyChangedEventHandler PropertyChanged;

    // CallerMemeberName attribute provides member name automatically
    //public void OnPropertyChanged([CallerMemberName] string propertyName = null) => 
    //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    #endregion
}
