using System;

namespace Sistema2020.Services.Exceptions
{
    public class NotFoundException: ApplicationException
    {
            public NotFoundException(string msg) : base(msg)
            {
            }
    }
}
