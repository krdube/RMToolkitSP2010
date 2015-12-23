using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;
using Microsoft.SharePoint.WorkflowActions;
using Microsoft.SharePoint.Administration;


namespace RMToolkitDispositionToArchiveListWF.Workflow1
{
    public sealed partial class ArchiveWF : SequentialWorkflowActivity
    {
        public ArchiveWF()
        {
            InitializeComponent();
        }

        public Guid workflowId = default(System.Guid);
        public SPWorkflowActivationProperties workflowProperties = new SPWorkflowActivationProperties();

        private void writeToExternalList_ExecuteCode(object sender, EventArgs e)
        {
            string siteURL;
            siteURL = "";
            SPWebApplication wa = SPContext.Current.Site.WebApplication;
            SPFarm farm = SPContext.Current.Site.WebApplication.Farm;




            if (farm.Properties.ContainsKey("RMToolkitSite"))
            {
                siteURL = farm.Properties["RMToolkitSite"].ToString();
            }


            if (siteURL != "")
            {

                SPListItem MyItem = workflowProperties.Item;
                SPDocumentLibrary MyLibrary = (SPDocumentLibrary)MyItem.ParentList;
               

                SPSite RMSite = new SPSite(siteURL);
                SPWeb RMWeb = RMSite.RootWeb;
                SPList RMList = RMWeb.Lists["RMToolkitArchiveList"];
                SPListItem item = RMList.Items.Add();

                item["Title"] = MyItem["Created"].ToString() + "_" + MyItem.Name;
                string url = MyItem["EncodedAbsUrl"].ToString();
                item["DocumentURL"] = url;
                item["DocumentLibrary"] = MyLibrary.Title;
                item["Site"] = MyLibrary.ParentWeb.Title;
                item["SiteCollectionURL"] = MyLibrary.ParentWeb.Site.Url;
                item["CertificateName"] = "default";
                item["ParentFolder"] = MyItem.Url.Substring(0, MyItem.Url.LastIndexOf('/'));
                item.Update();
            }
        }
    }
}

