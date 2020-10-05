using System;

namespace SMO_Library
{
    public class BaseExceptionProperties
    {
        protected bool mHasException;
        public bool HasException { get { return mHasException; } }
        protected Exception mLastException;
        public Exception LastException { get; set; }

    }
}
