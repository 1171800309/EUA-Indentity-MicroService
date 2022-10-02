using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EUA.Application.Interface
{
    public interface ISysUser
    {
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="UserID">用户编号</param>
        /// <returns></returns>
        Task<object> GetUserInfo(string UserID);

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="LoginName"></param>
        /// <param name="PWD"></param>
        /// <returns></returns>
        Task<object> Login(string LoginName, string PWD);
    }
}
