using System;
using System.Threading.Tasks;

namespace TestServer.Common.Extensions
{
    public static class TaskExtensions
    {
        public static async void FireAndForgetSafeAsync(this Task task)
        {
            try
            {
                await task.ConfigureAwait(false);
            }
            catch (Exception)
            {

            }
        }
    }
}
