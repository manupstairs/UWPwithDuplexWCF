using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleService
{
    [ServiceContract(SessionMode = SessionMode.Required,
                 CallbackContract = typeof(IBrightnessDuplexCallback))]
    interface IBrightnessNotificationService
    {
        [OperationContract(IsOneWay = true)]
        void SubscribeBrightnessNotification();
    }

    interface IBrightnessDuplexCallback
    {
        [OperationContract(IsOneWay = true)]
        void NotifyBrightness(int value);
    }
}
