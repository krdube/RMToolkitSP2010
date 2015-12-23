using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.Office.RecordsManagement.PolicyFeatures;
using Microsoft.Office.RecordsManagement.RecordsRepository;
using Microsoft.SharePoint.Administration;

namespace CustomRetentionFormula
{
    class RMToolkitImmediateExpiration:IExpirationFormula
    {
        public DateTime? ComputeExpireDate(SPListItem item, System.Xml.XmlNode parametersData)
        {
            DateTime? expires = null;
            expires = System.DateTime.Now;
            Log("RMToolkitImmediateExpiration", TraceSeverity.High, EventSeverity.Information, item.Name + ", expiry date " + expires.ToString());
           return expires;
        }

        private void Log(string source, TraceSeverity traceSeverity, EventSeverity eventSeverity, string logMessage)
        {
            try
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory(source, traceSeverity, eventSeverity), traceSeverity, logMessage, null);
            }
            catch (Exception)
            {
                // maybe write to Event Log?
            }
        }


    }
}
