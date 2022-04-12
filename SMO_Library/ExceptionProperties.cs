using System;

namespace SMO_Library
{
    [Obsolete("Not used any more", true)]
    public class BaseExceptionProperties
    {
        protected bool mHasException;
        public bool HasException => mHasException;
        protected Exception mLastException;
        public Exception LastException { get; set; }

    }
}
