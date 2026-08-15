using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Application.Services
{
    public class HangfireJobScheduler : IBackgroundJobScheduler
    {
        private readonly IBackgroundJobClient _client;
        public HangfireJobScheduler(IBackgroundJobClient client) => _client = client;

        public void Enqueue<T>(Expression<Func<T, Task>> methodCall) =>
            _client.Enqueue(methodCall);

        public void Schedule<T>(Expression<Func<T, Task>> methodCall, DateTimeOffset enqueueAt) =>
            _client.Schedule(methodCall, enqueueAt);
    }
}
