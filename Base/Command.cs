 
using System.Windows; 
using System.Windows.Input;
using System.ComponentModel;

namespace Base
{
    public class Command : ICommand
    {
        public Command(Action<object> action)
        {
            CanExecuteFunction = p => true;
            ExecuteFunction = action;
        }

        public Command(Action action)
        {
            CanExecuteFunction = p => true;
            ExecuteFunction = p => action();
        }
        public Command(Action action, Func<bool> canExecute)
        {
            CanExecuteFunction = _ => canExecute();
            ExecuteFunction = _ => action();
        }

        public Action<object> ExecuteFunction {get; set;}
        public Func<object, bool> CanExecuteFunction {get; set;}


        #region ICommand Members

        public bool CanExecute(object parameter) => CanExecuteFunction?.Invoke(parameter) ?? false;


        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }


        public void Execute(object parameter) => ExecuteFunction?.Invoke(parameter);

        #endregion

    }

    //-----------------------------------------------------------------------      
    //-----------------------------------------------------------------------      

    /// This class facilitates associating a key binding in XAML markup to  a command
    /// defined in a View Model by exposing a Command dependency property.
    /// The class derives from Freezable to work around a limitation in WPF when data-binding from XAML.
    /// </summary>
    public class CommandReference : System.Windows.Freezable, ICommand
    {

        public CommandReference()
        {
            // Blank
        }


        public static readonly DependencyProperty CommandProperty = 
            DependencyProperty.Register("Command", typeof(ICommand), typeof(CommandReference), 
            new PropertyMetadata(new PropertyChangedCallback(OnCommandChanged)));


        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }


        #region ICommand Members

        public bool CanExecute(object? parameter)
        {
            if (Command != null)
                return Command.CanExecute(parameter);

            return false;
        }


        public void Execute(object? parameter)
        {
            Command.Execute(parameter);
        }


        public event EventHandler CanExecuteChanged;


        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var commandReference = d as CommandReference;
            var oldCommand = e.OldValue as ICommand;
            var newCommand = e.NewValue as ICommand;

            if (oldCommand != null)
            {
                oldCommand.CanExecuteChanged -= commandReference?.CanExecuteChanged;
            }
            if (newCommand != null)
            {
                newCommand.CanExecuteChanged += commandReference?.CanExecuteChanged;
            }
        }

        #endregion

        #region Freezable


        protected override Freezable CreateInstanceCore()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
