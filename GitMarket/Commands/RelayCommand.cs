using System;

namespace GitMarket.Commands
{
    public class RelayCommand : Base.BaseCommand
    {
        private readonly Action<object>? _Execute;
        private readonly Func<object, bool>? _CanExecute;
        public RelayCommand(Action<object>? Execate, Func<object, bool>? CanExecate = null)
        {
            _Execute = Execate ?? throw new ArgumentNullException(nameof(Execate));
            _CanExecute = CanExecate;
        }
        public override bool CanExecute(object? parameter) => _CanExecute?.Invoke(parameter) ?? true;
        public override void Execute(object? parameter) => _Execute(parameter);
    }
}
