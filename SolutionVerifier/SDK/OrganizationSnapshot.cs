﻿namespace Cinteros.Xrm.SolutionVerifier.SDK
{
    using System.Linq;
    using Cinteros.Xrm.SolutionVerifier.Utils;
    using McTools.Xrm.Connection;
    using Microsoft.Xrm.Client;
    using Microsoft.Xrm.Client.Services;
    using Microsoft.Xrm.Sdk;

    public class OrganizationSnapshot
    {
        #region Public Fields

        public ConnectionDetail ConnectionDetail
        {
            get;
            set;
        }

        /// <summary>
        /// Array of solutions available in the organization
        /// </summary>
        public Solution[] Solutions
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets assemblies linked to solution
        /// </summary>
        public PluginAssembly[] Assemblies
        {
            get;
            set;
        }

        #endregion Public Fields

        #region Public Constructors

        public OrganizationSnapshot()
        {
        }

        public OrganizationSnapshot(ConnectionDetail connectionDetail, Solution[] reference)
        {
            Solution[] solutions = null;
            PluginAssembly[] assemblies = null;
            DataCollection<Entity> entities = null;

            var organizationService = new OrganizationService(CrmConnection.Parse(connectionDetail.GetOrganizationCrmConnectionString()));

            entities = organizationService.RetrieveMultiple(Helpers.CreateSolutionsQuery()).Entities;
            solutions = entities.ToArray<Entity>().Select(x => new Solution(x)).ToArray<Solution>();

            entities = organizationService.RetrieveMultiple(Helpers.CreateAssembliesQuery()).Entities;
            assemblies = entities.ToArray<Entity>().Select(x => new PluginAssembly(x)).ToArray<PluginAssembly>();

            //var entities = instance.RetrieveMultiple(solutionsQuery).Entities;
            //solutions = entities.ToArray<Entity>().Select(x => new Solution(x)).ToArray<Solution>();
            solutions = solutions.Where(x => reference.Where(y => y.UniqueName == x.UniqueName).Count() > 0).ToArray<Solution>();

            //entities = instance.RetrieveMultiple(assembliesQuery).Entities;
            //assemblies = entities.ToArray<Entity>().Select(x => new PluginAssembly(x)).ToArray<PluginAssembly>();
            assemblies = assemblies.Where(x => solutions.Where(y => y.Id == x.SolutionId).Count() > 0).ToArray<PluginAssembly>();

            this.ConnectionDetail = connectionDetail;
            this.Solutions = solutions;
            this.Assemblies = assemblies;
        }

        public OrganizationSnapshot(ConnectionDetail connectionDetail)
        {
            var organizationService = new OrganizationService(CrmConnection.Parse(connectionDetail.GetOrganizationCrmConnectionString()));

            var solutions = organizationService.RetrieveMultiple(Helpers.CreateSolutionsQuery()).Entities.Select(x => new Solution(x)).ToArray<Solution>();

            var assemblies = organizationService.RetrieveMultiple(Helpers.CreateAssembliesQuery()).Entities.Select(x => new PluginAssembly(x)).ToArray<PluginAssembly>();
            assemblies = assemblies.Where(x => solutions.Where(y => y.Id == x.SolutionId).Count() > 0).ToArray<PluginAssembly>();

            this.ConnectionDetail = connectionDetail;
            this.Solutions = solutions;
            this.Assemblies = assemblies;
        }

        #endregion Public Constructors
    }
}