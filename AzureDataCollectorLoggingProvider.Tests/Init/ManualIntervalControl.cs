namespace AzureDataCollectorLoggingProvider.Tests.Init
{
    internal sealed class ManualIntervalControl
    {
        #region Public Properties

        public System.Threading.Tasks.Task Pause
        {
            get
            {
#if DEBUG
                System.Console.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
#endif
                return this._pauseCompletionSource.Task;
            }
            [JetBrains.Annotations.UsedImplicitly]
            // ReSharper disable once ValueParameterNotUsed
            set
            {
#if DEBUG
                System.Console.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
#endif
                throw new System.NotImplementedException();
            }
        }

        #endregion Public Properties

        #region Private Fields

        private System.Threading.Tasks.TaskCompletionSource<System.Object> _pauseCompletionSource =
            new System.Threading.Tasks.TaskCompletionSource<System.Object>();

        private System.Threading.Tasks.TaskCompletionSource<System.Object> _resumeCompletionSource;

        #endregion Private Fields

        #region Public Methods

        public async System.Threading.Tasks.Task IntervalAsync()
        {
#if DEBUG
            System.Console.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
#endif

            this._resumeCompletionSource = new System.Threading.Tasks.TaskCompletionSource<System.Object>();
            this._pauseCompletionSource.SetResult(null);

            await this._resumeCompletionSource.Task;
        }

        public void Resume()
        {
#if DEBUG
            System.Console.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
#endif

            this._pauseCompletionSource = new System.Threading.Tasks.TaskCompletionSource<System.Object>();
            this._resumeCompletionSource.SetResult(null);
        }

        #endregion Public Methods
    }
}
