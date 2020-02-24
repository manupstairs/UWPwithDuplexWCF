using Microsoft.Management.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MMILibrary
{
    public class MMIWrapper
    {
        private CimSession CimSession => CimSession.Create("localhost");

        private IDisposable TouchScreenDisposeAble { get; set; }

        
        
        public IValueChangedNotify SubscribeBrightnessEvent()
        {
            string namespaceName = "root\\WMI";
            string query = "SELECT Brightness from WmiMonitorBrightnessEvent";

            IObservable<CimSubscriptionResult> queryInstances = CimSession.SubscribeAsync(namespaceName, "WQL", query);
            var observer = new MMIObserver<CimSubscriptionResult>();
            TouchScreenDisposeAble = queryInstances.Subscribe(observer);
            return observer;
        }

    }
}
