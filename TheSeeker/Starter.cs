using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using TheSeeker.Configuration;

namespace TheSeeker.Forms
{
    public class Starter
    {
        /// <summary>
        /// Instantiates
        /// </summary>
        /// <param name="config">Configuration section for the desired search type</param>
        /// <returns></returns>
        public static ISearchManager GetTypes(CurrentSearchConfiguration config)
        {
            if (string.IsNullOrEmpty(config.SearchType))
                throw new ConfigurationErrorsException("\"SearchType\" cannot be empty");

            Type searchType = Type.GetType(config.SearchType, true);

            var searchTypeConfig = ((SearchTypesConfigurationSection)ConfigurationManager.GetSection(SearchTypeElement.Name)).SearchTypes[searchType.FullName];

            // Get types and create instances

            // Search Engine
            Type iSearchEngineGenericType = typeof(ISearchEngine<>).MakeGenericType(searchType);
            TypeInstance searchEngine = CreateTypeInstance(searchTypeConfig, c => c.SearchEngineType, iSearchEngineGenericType, null, null);

            // Operation Tracker
            TypeInstance operationTracker = CreateTypeInstance(config, c => c.OperationTrackerType, typeof(IOperationTracker), null, config.ListRefreshInterval);

            // Search Results Consumer
            Type iSearchResultsConsumerGenericType = typeof(ISearchResultsConsumer<>).MakeGenericType(searchType);
            TypeInstance searchResultsConsumer = CreateTypeInstance(searchTypeConfig, c => c.SearchResultsConsumerType, iSearchResultsConsumerGenericType, new Type[] { searchType });

            // Search Manager Factory
            Type iSearchFactoryGenericType = typeof(ISearchManagerFactory<,>).MakeGenericType(searchType, searchResultsConsumer.Type);
            Type[] genericTypeArgs = new[] { searchType, searchResultsConsumer.Type };
            TypeInstance searchFactory = CreateTypeInstance(searchTypeConfig, c => c.SearchManagerFactoryType, iSearchFactoryGenericType, genericTypeArgs);

            // Get Create method
            Type[] argumentTypes = new[] { searchEngine.Type, searchResultsConsumer.Type, operationTracker.Type };
            MethodInfo method = iSearchFactoryGenericType.GetMethod("Create", argumentTypes);

            // Create Search Manager
            object[] arguments = new[] { searchEngine.Instance, searchResultsConsumer.Instance, operationTracker.Instance };
            ISearchManager searchManager = (ISearchManager)method.Invoke(searchFactory.Instance, arguments);
            
            return searchManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TConfig"></typeparam>
        /// <param name="config"></param>
        /// <param name="typeNameProperty"></param>
        /// <param name="requiredInterfaceType"></param>
        /// <param name="genericTypeArguments"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">In case</exception>
        private static TypeInstance CreateTypeInstance<TConfig>(TConfig config, Expression<Func<TConfig, string>> typeNameProperty, Type requiredInterfaceType, Type[] genericTypeArguments, params object[] arguments)
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
                    throw new System.Configuration.ConfigurationErrorsException("No type name specified");
            
                return TypeInstantiator.CreateInstance(typeName, requiredInterfaceType, genericTypeArguments, arguments);
            }
            catch (Exception e)
            {
                throw new ArgumentException(typeNameProperty.Name, e);
            }
        }
    }
}