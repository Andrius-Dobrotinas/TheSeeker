using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using TheSeeker.Configuration;

namespace TheSeeker.Startup
{
    /// <summary>
    /// Class for dynamic instantiation of Search Manager on application startup
    /// </summary>
    public static class Factory
    {
        /// <summary>
        /// Instantiates search components from the supplied config and builds a search manager.
        /// Returns a structure containing both a search manager instance and the original input components
        /// </summary>
        /// <param name="config">Configuration section for the desired search type</param>
        /// <returns></returns>
        public static StartupObjects CreateSearchManager(CurrentSearchConfiguration config)
        {
            if (string.IsNullOrEmpty(config.SearchType))
                throw new ConfigurationErrorsException("\"SearchType\" cannot be empty");

            Type searchType = Type.GetType(config.SearchType, true);

            var searchTypeConfig = ((SearchTypesConfigurationSection)ConfigurationManager.GetSection(SearchTypeElement.Name)).SearchTypes[searchType.FullName];

            // Get types and create instances
            TypeInstance searchEngine = default(TypeInstance);
            TypeInstance operationTracker = default(TypeInstance);
            TypeInstance searchResultsConsumer = default(TypeInstance);
            TypeInstance searchFactory = default(TypeInstance);
            ISearchManager searchManager = null;

            try
            {
            // Search Engine
                Type iSearchEngineGenericType = typeof(ISearchEngine<>).MakeGenericType(searchType);
                searchEngine = CreateTypeInstance(searchTypeConfig, c => c.SearchEngineType, iSearchEngineGenericType, null, null);

                // Operation Tracker
                operationTracker = CreateTypeInstance(config, c => c.OperationTrackerType, typeof(IOperationTracker), null, config.ListRefreshInterval);

                // Search Results Consumer
                Type iSearchResultsConsumerGenericType = typeof(ISearchResultsConsumer<>).MakeGenericType(searchType);
                searchResultsConsumer = CreateTypeInstance(searchTypeConfig, c => c.SearchResultsConsumerType, iSearchResultsConsumerGenericType, new Type[] { searchType });

                // Search Manager Factory
                Type iSearchFactoryGenericType = typeof(ISearchManagerFactory<,>).MakeGenericType(searchType, searchResultsConsumer.Type);
                Type[] genericTypeArgs = new[] { searchType, searchResultsConsumer.Type };
                searchFactory = CreateTypeInstance(searchTypeConfig, c => c.SearchManagerFactoryType, iSearchFactoryGenericType, genericTypeArgs);

                // Get Create method
                Type[] argumentTypes = new[] { searchEngine.Type, searchResultsConsumer.Type, operationTracker.Type };
                MethodInfo method = iSearchFactoryGenericType.GetMethod("Create", argumentTypes);

                // Create Search Manager
                object[] arguments = new[] { searchEngine.Instance, searchResultsConsumer.Instance, operationTracker.Instance };
                searchManager = (ISearchManager)method.Invoke(searchFactory.Instance, arguments);

                return new StartupObjects
                {
                    SearchManager = searchManager,
                    OriginalInputObjects = arguments
                };
            }
            finally
            {
                (searchEngine.Instance as IDisposable)?.Dispose();
                (operationTracker.Instance as IDisposable)?.Dispose();
                (searchResultsConsumer.Instance as IDisposable)?.Dispose();
                (searchFactory.Instance as IDisposable)?.Dispose();
                (searchManager as IDisposable)?.Dispose();
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