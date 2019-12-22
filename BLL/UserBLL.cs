using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetcore.study.BLL
{
    public class UserBLL : IUserBLL
    {
        public void RegisterUser()
        {
            Console.WriteLine("注册一个用户！");
        }
    }
}
