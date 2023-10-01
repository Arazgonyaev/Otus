using System;

namespace Otus_7_exceptions;

public class BaseException : Exception
{
    public BaseException(string msg) : base(msg) {}
}
