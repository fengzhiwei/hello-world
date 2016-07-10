using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PipeLine.Class
{
    /// <summary>
    /// 获取星期几；日期：2016-6-29；冯志伟
    /// </summary>
    class Dayofweek
    {
        public string Week()
        {
            string[] weekdays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            string week = weekdays[Convert.ToInt32(DateTime.Now.DayOfWeek)];
            return week;
        }



    }
}
