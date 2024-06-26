﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudioTools.Project.Automation;

namespace Microsoft.VisualStudioTools.Project
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class CommonProjectNodeProperties : ProjectNodeProperties, IVsCfgBrowseObject, VSLangProj.ProjectProperties
    {
        private OAProjectConfigurationProperties _activeCfgSettings;

        internal CommonProjectNodeProperties(ProjectNode node)
            : base(node)
        {
        }

        #region properties
        /// <summary>
        /// Returns/Sets the StartupFile project property
        /// </summary>
        [SRCategoryAttribute(SR.General)]
        [SRDisplayName(SR.StartupFile)]
        [SRDescriptionAttribute(SR.StartupFileDescription)]
        public string StartupFile
        {
            get
            {
                return this.Node.Site.GetUIThread().Invoke(() =>
                {
                    var res = this.Node.ProjectMgr.GetProjectProperty(CommonConstants.StartupFile, true);
                    if (res != null && !Path.IsPathRooted(res))
                    {
                        res = CommonUtils.GetAbsoluteFilePath(this.Node.ProjectMgr.ProjectHome, res);
                    }
                    return res;
                });
            }
            set
            {
                this.Node.Site.GetUIThread().Invoke(() =>
                {
                    this.Node.ProjectMgr.SetProjectProperty(
                        CommonConstants.StartupFile,
                        CommonUtils.GetRelativeFilePath(
                            this.Node.ProjectMgr.ProjectHome,
                            Path.Combine(this.Node.ProjectMgr.ProjectHome, value)
                        )
                    );
                });
            }
        }

        /// <summary>
        /// Returns/Sets the WorkingDirectory project property
        /// </summary>
        [SRCategoryAttribute(SR.General)]
        [SRDisplayName(SR.WorkingDirectory)]
        [SRDescriptionAttribute(SR.WorkingDirectoryDescription)]
        public string WorkingDirectory
        {
            get
            {
                return this.Node.Site.GetUIThread().Invoke(() =>
                {
                    return this.Node.ProjectMgr.GetProjectProperty(CommonConstants.WorkingDirectory, true);
                });
            }
            set
            {
                this.Node.Site.GetUIThread().Invoke(() =>
                {
                    this.Node.ProjectMgr.SetProjectProperty(CommonConstants.WorkingDirectory, value);
                });
            }
        }

        /// <summary>
        /// Returns/Sets the PublishUrl project property which is where the project is published to
        /// </summary>
        [Browsable(false)]
        public string PublishUrl
        {
            get
            {
                return this.Node.Site.GetUIThread().Invoke(() =>
                {
                    return this.Node.ProjectMgr.GetProjectProperty(CommonConstants.PublishUrl, true);
                });
            }
            set
            {
                this.Node.Site.GetUIThread().Invoke(() =>
                {
                    this.Node.ProjectMgr.SetProjectProperty(CommonConstants.PublishUrl, value);
                });
            }
        }

        //We don't need this property, but still have to provide it, otherwise
        //Add New Item wizard (which seems to be unmanaged) fails.
        [Browsable(false)]
        public string RootNamespace
        {
            get
            {
                return "";
            }
            set
            {
                //Do nothing
            }
        }

        /// <summary>
        /// Gets the home directory for the project.
        /// </summary>
        [SRCategoryAttribute(SR.Misc)]
        [SRDisplayName(SR.ProjectHome)]
        [SRDescriptionAttribute(SR.ProjectHomeDescription)]
        public string ProjectHome => this.Node.ProjectMgr.ProjectHome;

        #endregion

        #region IVsCfgBrowseObject Members

        int IVsCfgBrowseObject.GetCfg(out IVsCfg ppCfg)
        {
            return this.Node.ProjectMgr.ConfigProvider.GetCfgOfName(
                this.Node.ProjectMgr.CurrentConfig.GetPropertyValue(ProjectFileConstants.Configuration),
                this.Node.ProjectMgr.CurrentConfig.GetPropertyValue(ProjectFileConstants.Platform),
                out ppCfg);
        }

        #endregion

        #region ProjectProperties Members

        [Browsable(false)]
        public string AbsoluteProjectDirectory => this.Node.ProjectMgr.ProjectFolder;

        [Browsable(false)]
        public VSLangProj.ProjectConfigurationProperties ActiveConfigurationSettings
        {
            get
            {
                if (this._activeCfgSettings == null)
                {
                    this._activeCfgSettings = new OAProjectConfigurationProperties(this.Node.ProjectMgr);
                }
                return this._activeCfgSettings;
            }
        }

        [Browsable(false)]
        public string ActiveFileSharePath => throw new NotImplementedException();
        [Browsable(false)]
        public VSLangProj.prjWebAccessMethod ActiveWebAccessMethod => throw new NotImplementedException();
        [Browsable(false)]
        public string ApplicationIcon
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        [Browsable(false)]
        public string AssemblyKeyContainerName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        [Browsable(false)]
        public string AssemblyName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        [Browsable(false)]
        public string AssemblyOriginatorKeyFile
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        [Browsable(false)]
        public VSLangProj.prjOriginatorKeyMode AssemblyOriginatorKeyMode
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        [Browsable(false)]
        public VSLangProj.prjScriptLanguage DefaultClientScript
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        [Browsable(false)]
        public VSLangProj.prjHTMLPageLayout DefaultHTMLPageLayout
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        [Browsable(false)]
        public string DefaultNamespace
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        [Browsable(false)]
        public VSLangProj.prjTargetSchema DefaultTargetSchema
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        [Browsable(false)]
        public bool DelaySign
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        [Browsable(false)]
        public new object ExtenderNames => throw new NotImplementedException();
        [Browsable(false)]
        public string FileSharePath
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        [Browsable(false)]
        public bool LinkRepair
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        [Browsable(false)]
        public string LocalPath => throw new NotImplementedException();
        [Browsable(false)]
        public string OfflineURL => throw new NotImplementedException();
        [Browsable(false)]
        public VSLangProj.prjCompare OptionCompare
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        [Browsable(false)]
        public VSLangProj.prjOptionExplicit OptionExplicit
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        [Browsable(false)]
        public VSLangProj.prjOptionStrict OptionStrict
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        [Browsable(false)]
        public string OutputFileName => throw new NotImplementedException();
        [Browsable(false)]
        public VSLangProj.prjOutputType OutputType
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        [Browsable(false)]
        public VSLangProj.prjProjectType ProjectType => throw new NotImplementedException();
        [Browsable(false)]
        public string ReferencePath
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        [Browsable(false)]
        public string ServerExtensionsVersion => throw new NotImplementedException();
        [Browsable(false)]
        public string StartupObject
        {
            get
            {
                return this.Node.Site.GetUIThread().Invoke(() =>
                {
                    return this.Node.ProjectMgr.GetProjectProperty(CommonConstants.StartupFile);
                });
            }
            set
            {
                this.Node.Site.GetUIThread().Invoke(() =>
                {
                    this.Node.ProjectMgr.SetProjectProperty(
                        CommonConstants.StartupFile,
                        CommonUtils.GetRelativeFilePath(this.Node.ProjectMgr.ProjectHome, value)
                    );
                });
            }
        }

        [Browsable(false)]
        public string URL => CommonUtils.MakeUri(this.Node.ProjectMgr.Url, false, UriKind.Absolute).AbsoluteUri;
        [Browsable(false)]
        public VSLangProj.prjWebAccessMethod WebAccessMethod
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        [Browsable(false)]
        public string WebServer => throw new NotImplementedException();
        [Browsable(false)]
        public string WebServerVersion => throw new NotImplementedException();
        [Browsable(false)]
        public string __id => throw new NotImplementedException();
        [Browsable(false)]
        public object __project => throw new NotImplementedException();
        [Browsable(false)]
        object VSLangProj.ProjectProperties.Extender => throw new NotImplementedException();

        #endregion
    }
}
