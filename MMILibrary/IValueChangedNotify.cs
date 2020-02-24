using System;
using System.Collections.Generic;
using System.Text;

namespace MMILibrary
{
    public interface IValueChangedNotify
    {
        event EventHandler<int> BrightnessChangedEvent;
    }
}
