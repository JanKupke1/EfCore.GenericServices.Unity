using Prism;
using ShowRoom.Core.CommandBases.Base;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShowRoom.Core.CommandBases
{
    public abstract class AsyncCommandBase : DelegateCommandBase, ICommand, IActiveAware
    {


        protected Func<bool> _CanExecuteMethod;

        /// <summary>
        /// Creates a new instance of <see cref="AsyncCommandBase"/> with the <see cref="Action"/> to invoke on execution.
        /// </summary>
        /// <param name="executeMethod">The <see cref="Action"/> to invoke when <see cref="ICommand.Execute(object)"/> is called.</param>
        public AsyncCommandBase()
            : this(() => true)
        {

        }

        /// <summary>
        /// Creates a new instance of <see cref="AsyncCommandBase"/> with the <see cref="Action"/> to invoke on execution
        /// and a <see langword="Func" /> to query for determining if the command can execute.
        /// </summary>
        /// <param name="executeMethod">The <see cref="Action"/> to invoke when <see cref="ICommand.Execute"/> is called.</param>
        /// <param name="canExecuteMethod">The <see cref="Func{TResult}"/> to invoke when <see cref="ICommand.CanExecute"/> is called</param>
        public AsyncCommandBase(Func<bool> canExecuteMethod)
            : base()
        {

            _CanExecuteMethod = canExecuteMethod;
        }

        ///<summary>
        /// Executes the command.
        ///</summary>
        public virtual void Execute()
        {
            Execute(null);
        }

        /// <summary>
        /// Determines if the command can be executed.
        /// </summary>
        /// <returns>Returns <see langword="true"/> if the command can execute,otherwise returns <see langword="false"/>.</returns>
        public bool CanExecute()
        {
            return !IsExecuting && _CanExecuteMethod();
        }

        /// <summary>
        /// Handle the internal invocation of <see cref="ICommand.Execute(object)"/>
        /// </summary>
        /// <param name="parameter">Command Parameter</param>
        protected override async void Execute(object parameter)
        {
            IsExecuting = true;

            await ExecuteAsync(parameter);



            IsExecuting = false;
        }


        /// <summary>
        /// Handle the internal invocation of <see cref="ICommand.CanExecute(object)"/>
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns><see langword="true"/> if the Command Can Execute, otherwise <see langword="false" /></returns>
        protected override bool CanExecute(object parameter)
        {
            return CanExecute();
        }

        /// <summary>
        /// Observes a property that implements INotifyPropertyChanged, and automatically calls AsyncCommandBaseBase.RaiseCanExecuteChanged on property changed notifications.
        /// </summary>
        /// <typeparam name="T">The object type containing the property specified in the expression.</typeparam>
        /// <param name="propertyExpression">The property expression. Example: ObservesProperty(() => PropertyName).</param>
        /// <returns>The current instance of AsyncCommandBase</returns>
        public AsyncCommandBase ObservesProperty<T>(Expression<Func<T>> propertyExpression)
        {
            ObservesPropertyInternal(propertyExpression);
            return this;
        }

        /// <summary>
        /// Observes a property that is used to determine if this command can execute, and if it implements INotifyPropertyChanged it will automatically call AsyncCommandBaseBase.RaiseCanExecuteChanged on property changed notifications.
        /// </summary>
        /// <param name="canExecuteExpression">The property expression. Example: ObservesCanExecute(() => PropertyName).</param>
        /// <returns>The current instance of AsyncCommandBase</returns>
        public AsyncCommandBase ObservesCanExecute(Expression<Func<bool>> canExecuteExpression)
        {
            _CanExecuteMethod = canExecuteExpression.Compile();
            ObservesPropertyInternal(canExecuteExpression);
            return this;
        }







        private bool _IsExecuting;
        public bool IsExecuting
        {
            get
            {
                return _IsExecuting;
            }
            set
            {
                _IsExecuting = value;
                RaiseCanExecuteChanged();
            }
        }



        public abstract Task ExecuteAsync(object parameter);
    }
}
