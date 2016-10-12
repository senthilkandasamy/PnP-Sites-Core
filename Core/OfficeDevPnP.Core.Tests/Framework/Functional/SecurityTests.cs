﻿using Microsoft.SharePoint.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfficeDevPnP.Core.Enums;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using OfficeDevPnP.Core.Framework.Provisioning.ObjectHandlers;
using OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml;
using OfficeDevPnP.Core.Tests.Framework.Functional.Validators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace OfficeDevPnP.Core.Tests.Framework.Functional
{
    /// <summary>
    /// Test cases for the provisioning engine security functionality
    /// </summary>
    [TestClass]
   public class SecurityTests : FunctionalTestBase
    {
        #region Construction
        public SecurityTests()
        {
            //debugMode = true;
            //centralSiteCollectionUrl = "https://crtlab2.sharepoint.com/sites/source2";
            //centralSubSiteUrl = "https://crtlab2.sharepoint.com/sites/source2/sub2";
        }
        #endregion

        #region Test setup
        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            ClassInitBase(context);
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            ClassCleanupBase();
        }
        #endregion

        #region Site collection test cases
        /// <summary>
        /// Security Test
        /// </summary>
        [TestMethod]
        public void SecurityTest()
        {
            using (var cc = TestCommon.CreateClientContext(centralSiteCollectionUrl))
            {
                ProvisioningTemplateCreationInformation ptci = new ProvisioningTemplateCreationInformation(cc.Web);
                ptci.IncludeSiteGroups = true;
                ptci.HandlersToProcess = Handlers.SiteSecurity;

                var result = TestProvisioningTemplate(cc, "security_add.xml", Handlers.SiteSecurity,null,ptci);
                SecurityValidator sv= new SecurityValidator();
                Assert.IsTrue(sv.Validate(result.SourceTemplate.Security, result.TargetTemplate.Security,result.TargetTokenParser,cc));
            }
        }
        #endregion
    }
}
