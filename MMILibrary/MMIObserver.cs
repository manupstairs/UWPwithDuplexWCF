using Microsoft.Management.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MMILibrary
{
    public class MMIObserver<T> : IObserver<T>, IDisposable, IValueChangedNotify
    {
        private readonly ManualResetEventSlim doneEvent = new ManualResetEventSlim(false);

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                doneEvent.Dispose();
            }

            disposed = true;
        }

        private bool disposed = true;

        public event EventHandler<int> BrightnessChangedEvent;

        public void OnCompleted()
        {
            this.doneEvent.Set();
        }

        public void OnError(Exception error)
        {
            this.doneEvent.Set();
            throw error;
        }

        public void OnNext(T value)
        {
            CimSubscriptionResult subscriptionResult = value as CimSubscriptionResult;
            if (subscriptionResult != null)
            {
                
                var cimInstance = subscriptionResult.Instance;
                foreach (var item in cimInstance.CimInstanceProperties)
                {
                    Console.WriteLine(item.Name);
                    Console.WriteLine(item.Value);
                    int test = (byte)item.Value; 
                    BrightnessChangedEvent?.Invoke(this, test);
                }
            }
        }
    }
}
