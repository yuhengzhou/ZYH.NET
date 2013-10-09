using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZYH.WebControl_V
{
    public class CallbackOptions : SerializableObjectBase
    {
        public string PostDataContainer = "NA";
        public AsynchronousCallOptions AsyncCallOption = AsynchronousCallOptions.AbortPreviousCalls;
        public AfterCallActions AfterCallAction = AfterCallActions.NA;
        public bool CanBeAborted = true;
        public bool UpdateViewState = true;
        public bool UpdateControlState = true;
    }
}
