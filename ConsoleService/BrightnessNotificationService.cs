using MMILibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class BrightnessNotificationService : IBrightnessNotificationService
    {
        private IBrightnessDuplexCallback Callback {get;set;}

        public BrightnessNotificationService()
        {
            Callback = OperationContext.Current.GetCallbackChannel<IBrightnessDuplexCallback>();
        }

        public void SubscribeBrightnessNotification()
        {
            var mmiWrapper = new MMIWrapper();
            var brightnessChangedNotify = mmiWrapper.SubscribeBrightnessEvent();
            brightnessChangedNotify.BrightnessChangedEvent += BrightnessChangedNotify_BrightnessChangedEvent;
            Console.WriteLine("SubscribeBrightnessNotification");
        }

        private void BrightnessChangedNotify_BrightnessChangedEvent(object sender, int e)
        {
            Callback.NotifyBrightness(e);
            Console.WriteLine($"BrightnessChangedNotify_BrightnessChangedEvent:{e}");
        }
    }
}
