using System;

namespace Sistema2020.Services.Exceptions
{
    public class DbConcurrencyException: ApplicationException
    {
        public DbConcurrencyException(string msg) : base(msg)
        {

        }
    }
}
