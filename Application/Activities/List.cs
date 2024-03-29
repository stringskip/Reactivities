using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.Activities
{
    public class List
    {
        public class Query : IRequest<List<Activity>>{}

        public class Handler : IRequestHandler<Query, List<Activity>>
        {
            private readonly DataContext _context;
            private readonly ILogger<List> _logger;

            public Handler(DataContext context, ILogger<List> logger)
            {
                _logger = logger;
                _context = context;
            }

            public async Task<List<Activity>> Handle(Query request, CancellationToken cancellationToken)
            {
                List<Activity> rvalue = null;

                try
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    var activities = await _context.Activities.ToListAsync();
                    if(activities != null && activities.Count > 0)
                    {
                        rvalue = activities;
                    }
                }
                catch (System.Exception)
                {
                    _logger.LogInformation("Task was cancelled");
                    throw;                    
                }

                return rvalue;
            }
        }
    }
}