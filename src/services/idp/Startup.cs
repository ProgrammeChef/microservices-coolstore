﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using IdentityServer4;
using IdentityServer4.Quickstart.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Idp
{
  public class Startup
  {
    public IHostingEnvironment Environment { get; }
    public IConfiguration Configuration { get; }

    public Startup(IHostingEnvironment environment, IConfiguration configuration)
    {
      Environment = environment;
      Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc();

      services.Configure<IISOptions>(options =>
      {
        options.AutomaticAuthentication = false;
        options.AuthenticationDisplayName = "Windows";
      });

      var builder = services.AddIdentityServer(options =>
          {
            options.Events.RaiseErrorEvents = true;
            options.Events.RaiseInformationEvents = true;
            options.Events.RaiseFailureEvents = true;
            options.Events.RaiseSuccessEvents = true;
          })
          .AddTestUsers(TestUsers.Users);

      // in-memory, code config
      builder.AddInMemoryIdentityResources(Config.GetIdentityResources());
      builder.AddInMemoryApiResources(Config.GetApis());
      builder.AddInMemoryClients(Config.GetClients());

      // in-memory, json config
      // builder.AddInMemoryIdentityResources(Configuration.GetSection("IdentityResources"));
      // builder.AddInMemoryApiResources(Configuration.GetSection("ApiResources"));
      // builder.AddInMemoryClients(Configuration.GetSection("clients"));

      //            if (Environment.IsDevelopment())
      //            {

      //TODO: lets it works temporary
      builder.AddDeveloperSigningCredential();

      //            }
      //            else
      //            {
      //                throw new Exception("need to configure key material");
      //            }

      services.AddAuthentication()
          .AddGoogle(options =>
          {
            options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

            options.ClientId = "708996912208-9m4dkjb5hscn7cjrn5u0r4tbgkbj1fko.apps.googleusercontent.com";
            options.ClientSecret = "wdfPY6t8H8cecgjlxud__4Gh";
          });
    }

    public void Configure(IApplicationBuilder app)
    {
      if (Environment.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      string basePath = System.Environment.GetEnvironmentVariable("ASPNETCORE_BASEPATH");
      if (!string.IsNullOrEmpty(basePath))
      {
        app.Use(async (context, next) =>
        {
          context.Request.PathBase = basePath;
          await next.Invoke();
        });
      }

      app.UseIdentityServer();
      app.UseStaticFiles();
      app.UseMvcWithDefaultRoute();
    }
  }
}