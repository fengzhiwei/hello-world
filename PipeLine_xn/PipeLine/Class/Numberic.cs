using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PipeLine.Class
{
    class Numberic
    {
        public bool isNumberic(string message, out double result)
        {
            
            result = -1;   //result 定义为out 用来输出值
            try
            {
                result = Convert.ToDouble(message);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
