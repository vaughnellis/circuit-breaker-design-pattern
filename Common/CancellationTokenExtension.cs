namespace CircuitBreakerDesignPattern.Common
{
    public static class CancellationTokenExtension
    {
        public static async Task<T> WithCancellation<T>(this Task<T> task, CancellationToken cancellationToken)
        {
            var taskCompletion = new TaskCompletionSource<bool>();

            using (cancellationToken.Register(source =>
                                                    (source as TaskCompletionSource<bool>).TrySetResult(true), taskCompletion))
                if (task != await Task.WhenAny(task, taskCompletion.Task))
                    throw new OperationCanceledException(cancellationToken);

            return await task;
        }
    }
}
