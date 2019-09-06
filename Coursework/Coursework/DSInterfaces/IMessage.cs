using System;
using System.Collections.Generic;
using System.Text;

namespace Coursework.DSInterfaces
{
    public interface IMessage
    {
        void LongAlert(string message);
        void ShortAlert(string message);  
    }
}
