﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;

namespace dotnetcore.study
{
    /// <summary>
    /// 功能描述    ：配置文件管理器.net core版
    /// 创 建 者    ：lind
    /// 创建日期    ：2017/9/15 15:58:55
    /// 最后修改者  ：lind
    /// 最后修改日期：2017/9/15 15:58:55
    /// Microsoft.Extensions.Configuration.FileExtensions
    /// Microsoft.Extensions.Configuration.Json
    /// Microsoft.Extensions.Options.ConfigurationExtensions
    /// </summary>
    public static class ConfigFileHelper
    {
        #region Private Fields

        private static IConfigurationRoot config;

        #endregion Private Fields

        #region Public Methods

        /// <summary>
        /// 得到对象属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Get<T>(string name = null) where T : class, new()
        {
            try
            {
                //节点名称
                var sectionName = string.IsNullOrWhiteSpace(name) ? typeof(T).Name : name;
                if (typeof(T).IsGenericType)
                {
                    Type[] genericArgTypes = typeof(T).GetGenericArguments();
                    sectionName = genericArgTypes[0].Name;
                }
                //判断配置文件是否有节点
                if (config.GetChildren().FirstOrDefault(i => i.Key == sectionName) == null)
                    return null;

                //节点对象反序列化
                var spList = new ServiceCollection().AddOptions()
                                               .Configure<T>(config.GetSection(sectionName))
                                               .BuildServiceProvider();
                return spList.GetService<IOptions<T>>().Value;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 得到简单类型的属性
        /// </summary>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        public static string Get(string sectionName)
        {
            return config.GetSection(sectionName).Value;
        }

        /// <summary>
        /// 设置配置项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="file"></param>
        /// <param name="env"></param>
        public static void Set(string file = "appsettings.json", IHostingEnvironment env = null)
        {
            if (env != null)
            {
                config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                         .AddJsonFile(file, optional: true, reloadOnChange: true)
                         .AddJsonFile($"{file.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries)[0]}.{env.EnvironmentName}.json", optional: true)
                         .Build();
            }
            else
            {
                config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                         .AddJsonFile(file, optional: true, reloadOnChange: true)
                         .Build();
            }
        }

        #endregion Public Methods
    }
}