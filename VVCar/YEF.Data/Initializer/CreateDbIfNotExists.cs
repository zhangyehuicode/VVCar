using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using YEF.Core;

namespace YEF.Data.Initializer
{
    public class CreateDbIfNotExists<TContext> : CreateDatabaseIfNotExists<TContext>
        where TContext : DbContext
    {
        static readonly List<ICreateDBSeedAction> _SeedActions;
        public CreateDbIfNotExists()
            : base()
        {
        }

        static CreateDbIfNotExists()
        {
            _SeedActions = new List<ICreateDBSeedAction>();
            LoadSeedActions();
        }

        protected override void Seed(TContext context)
        {
            if (_SeedActions.Count == 0)
                return;
            foreach (var seedAction in _SeedActions)
            {
                seedAction.Seed(context);
            }
        }

        /// <summary>
        /// 加载种子Actions
        /// </summary>
        static void LoadSeedActions()
        {
            if (AppContext.Settings.ServiceRole == Core.Config.EServiceRole.OnlineStore)
                return;
            var files = System.IO.Directory.GetFiles(System.AppDomain.CurrentDomain.RelativeSearchPath, "*.Data.dll");
            if (files.Length == 0)
                return;
            Type baseType = typeof(ICreateDBSeedAction);
            IList<Type> seedActionTypes;
            List<ICreateDBSeedAction> seedActions = new List<ICreateDBSeedAction>();
            ICreateDBSeedAction seedAction;
            foreach (var file in files)
            {
                var assembly = Assembly.LoadFrom(file);
                if (assembly == null)
                    continue;
                seedActionTypes = assembly.ExportedTypes.Where(t => !t.IsAbstract && baseType.IsAssignableFrom(t)).ToList();
                if (seedActionTypes.Count == 0)
                    continue;
                foreach (var seedType in seedActionTypes)
                {
                    seedAction = Activator.CreateInstance(seedType) as ICreateDBSeedAction;
                    seedActions.Add(seedAction);
                }
            }
            _SeedActions.AddRange(seedActions.OrderBy(a => a.Order));
        }
    }
}
