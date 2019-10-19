using System;
using System.Diagnostics;
using Reactive.Bindings;

namespace ExtendControl.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public ReactiveCommand ApplyCommand { get; set; } = new ReactiveCommand();

        public ReactiveProperty<int> PastDays { get; set; } = new ReactiveProperty<int>();

        public MainPageViewModel()
        {
            ApplyCommand.Subscribe(_ =>
            {
                Debug.WriteLine("適用ボタンタップ");
                PastDays.Value = 3;
            });
        }

    }
}
