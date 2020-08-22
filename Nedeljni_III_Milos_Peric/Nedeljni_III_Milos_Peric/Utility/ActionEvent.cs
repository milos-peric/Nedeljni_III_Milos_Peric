using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_III_Milos_Peric.Utility
{
    internal class ActionEvent
    {
        public delegate void ActionPerformedEventHandler(object source, ActionEventArgs args);
        public event ActionPerformedEventHandler ActionPerformed;

        public void OnActionPerformed(string logMessage, string userName)
        {
            ActionPerformed?.Invoke(this, new ActionEventArgs() { LogMessage = logMessage, UserName = userName });
        }
    }
}
