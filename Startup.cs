using dotnetcore.study.BLL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;

namespace dotnetcore.study
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
  
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //注册一个单例的对象
            //services.AddTransient<UserBLL>();//注册一个类
            services.AddTransient<IUserBLL>(o => new UserBLL());//注册一个接口及它的实现类
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            ConfigFileHelper.Set(env: env);//i注册默认的配置文件
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
