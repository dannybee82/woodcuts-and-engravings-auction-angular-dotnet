using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Application.Services
{
    public interface IBackgroundJobScheduler
    {
        void Enqueue<T>(Expression<Func<T, Task>> methodCall);
        void Schedule<T>(Expression<Func<T, Task>> methodCall, DateTimeOffset enqueueAt);
    }
}
