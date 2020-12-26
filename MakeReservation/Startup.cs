// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.10.3

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using MakeReservation.Bots;
using Microsoft.Bot.Builder.Dialogs;
using MakeReservation.Dialogs;

namespace MakeReservation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();

            //Add Dialogs
            services.AddTransient<MakeReservationDialog>();
            services.AddTransient<IntroductionDialog>();
            services.AddTransient<BaseDialog>();
            services.AddTransient<BrowseMenuDialog>();

            // Create the Bot Framework Adapter with error handling enabled.
            services.AddSingleton<IBotFrameworkHttpAdapter, AdapterWithErrorHandler>();

            var dataStorage = new MemoryStorage();
            services.AddSingleton<IStorage>(dataStorage);

            var conversationState = new ConversationState(dataStorage);
            services.AddSingleton(conversationState);

            var userState = new UserState(dataStorage);
            services.AddSingleton(userState);

          var botAccessor = new BotAccessor(conversationState)
            {
                DialogStateAccessor = conversationState.CreateProperty<DialogState>(BotAccessor.DialogStateAccessorName)
            };

           // Create the bot as a transient. In this case the ASP Controller is expecting an IBot.
            services.AddTransient<IBot, MakeReservationBot>();

           services.AddSingleton(serviceProvider => botAccessor);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles()
                .UseStaticFiles()
                .UseWebSockets()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });

            // app.UseHttpsRedirection();
        }
    }
}
