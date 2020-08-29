using AutoService.DAL.Data;
using AutoService.Workers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoService.Quartz
{
    public class DataJob:IJob
    {

        private readonly IServiceScopeFactory serviceScopeFactory;
        public DataJob(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }
        public async Task Execute(IJobExecutionContext context )
        {

            using var scope = serviceScopeFactory.CreateScope();
            var emailSender = scope.ServiceProvider.GetService<IEmailSender>();
            await emailSender.SendEmailAsync("dashabriliova@ya.ru", "рассылка", "привет");
        }
    }
}
