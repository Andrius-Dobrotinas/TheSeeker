using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using TheSeeker.Configuration;

namespace TheSeeker.Initialization
{
    /// <summary>
    /// Class for dynamic instantiation of Search Manager on application startup
    /// </summary>
    public static class SearchManagerFactory
    {
        /// <summary>
        /// Instantiates search components from the supplied config and builds a search manager.
        /// Returns a structure containing both a search manager instance and the original input components
        /// </summary>
        /// <param name="config">Configuration section for the desired search type</param>
        /// <returns></returns>
        public static ISearchManager CreateNew(ICurrentSearchConfiguration currentConfig, ISearchTypeConfiguration searchTypeConfig)
        {
            if (currentConfig == null)
                throw new ArgumentNullException(nameof(currentConfig));
            if (searchTypeConfig == null)
                throw new ArgumentNullException(nameof(searchTypeConfig));
            if (string.IsNullOrEmpty(currentConfig.SearchType))
                throw new ConfigurationErrorsException("\"SearchType\" cannot be empty");

            Type searchType = Type.GetType(currentConfig.SearchType, true);

            // Get types and create instances
            TypeInstance searchEngine = default(TypeInstance);
            TypeInstance operationTracker = default(TypeInstance);
            TypeInstance searchEngineWrapper = default(TypeInstance);
            ISearchManager searchManager = null;

            try
            {
                // Search Engine
                Type iSearchEngineGenericType = typeof(ISearchEngine<>).MakeGenericType(searchType);
                searchEngine = CreateTypeInstance(searchTypeConfig, c => c.SearchEngineType, iSearchEngineGenericType, null, null);

                // Search Engine Wrapper
                searchEngineWrapper = new TypeInstance
                {
                    Type = typeof(SearchEngineWrapper<>).MakeGenericType(searchType)
                };
                searchEngineWrapper.Instance = Activator.CreateInstance(searchEngineWrapper.Type, searchEngine.Instance);

                // Operation Tracker
                operationTracker = CreateTypeInstance(currentConfig, c => c.OperationTrackerType, typeof(IOperationTracker), null, currentConfig.ListRefreshInterval);

                // Search Manager Factory
                Type iSearchManagerGenericType = typeof(ISearchManager<>).MakeGenericType(searchType);
                Type[] genericTypeArgs = new[] { searchType };
                searchManager = (ISearchManager)CreateTypeInstance(searchTypeConfig, c => c.SearchManagerType, iSearchManagerGenericType, genericTypeArgs).Instance;

                // Get Create method
                Type[] argumentTypes = new[] { searchEngineWrapper.Type, operationTracker.Type };
                MethodInfo method = iSearchManagerGenericType.GetMethod("Create", argumentTypes);

                // Create Search results consumer
                object[] arguments = new[] { searchEngineWrapper.Instance, operationTracker.Instance };
                method.Invoke(searchManager, arguments);

                return searchManager;
            }
            catch
            {
                (searchEngine.Instance as IDisposable)?.Dispose();
                (operationTracker.Instance as IDisposable)?.Dispose();
                (searchEngineWrapper.Instance as IDisposable)?.Dispose();
                (searchManager as IDisposable)?.Dispose();

                throw;
            }
        }

        /// <summary>
        /// Creates an instance of the desired type checking if it implements the supplied interface
        /// </summary>
        /// <typeparam name="TObject">Type of config object</typeparam>
        /// <param name="config">An object that contains a property that contains the name of the type to instantiate</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"When there are problems instantiating the type</exception>
        private static TypeInstance CreateTypeInstance<TObject>(TObject config, Expression<Func<TObject, string>> typeNameProperty, Type requiredInterfaceType, Type[] genericTypeArguments, params object[] arguments)
        {
            if (config == null)
                throw new ArgumentOutOfRangeException(nameof(config));

            // Get value from config
            MemberExpression mex = (MemberExpression)typeNameProperty.Body;
            PropertyInfo pinfo = (PropertyInfo)mex.Member;

            try
            {
                string typeName = pinfo.GetValue(config) as string;

                if (string.IsNullOrEmpty(typeName))
                    throw new ConfigurationErrorsException("No type name specified");
            
                return TypeInstantiator.CreateInstance(typeName, requiredInterfaceType, genericTypeArguments, arguments);
            }
            catch (Exception e)
            {
                throw new ArgumentException(typeNameProperty.Name, e);
            }
        }
    }
}