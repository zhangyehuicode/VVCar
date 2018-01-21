using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using VVCar.Areas.HelpPage.ModelDescriptions;

namespace VVCar.Areas.HelpPage
{
    /// <summary>
    /// 多xml文档加载器
    /// </summary>
    public class MultiXmlDocumentationProvider : IDocumentationProvider, IModelDocumentationProvider
    {
        /********* 
        ** Properties 
        *********/
        /// <summary>The internal documentation providers for specific files.</summary>
        private readonly XmlDocumentationProvider[] Providers;

        /********* 
        ** Public methods 
        *********/
        /// <summary>Construct an instance.</summary>  
        /// <param name="xmlDocFilesPath">The physical paths to the XML documents.</param>  
        public MultiXmlDocumentationProvider(string xmlDocFilesPath)
        {
            //this.Providers = paths.Select(p => new XmlDocumentationProvider(p)).ToArray();
            var paths = System.IO.Directory.GetFiles(xmlDocFilesPath, "*.xml");
            this.Providers = paths.Select(p => new XmlDocumentationProvider(p)).ToArray();
        }

        /// <summary>Gets the documentation for a subject.</summary>  
        /// <param name="subject">The subject to document.</param>  
        public string GetDocumentation(MemberInfo subject)
        {
            return this.GetFirstMatch(p => p.GetDocumentation(subject));
        }

        /// <summary>Gets the documentation for a subject.</summary>  
        /// <param name="subject">The subject to document.</param>  
        public string GetDocumentation(Type subject)
        {
            return this.GetFirstMatch(p => p.GetDocumentation(subject));
        }

        /// <summary>Gets the documentation for a subject.</summary>  
        /// <param name="subject">The subject to document.</param>  
        public string GetDocumentation(HttpControllerDescriptor subject)
        {
            return this.GetFirstMatch(p => p.GetDocumentation(subject));
        }

        /// <summary>Gets the documentation for a subject.</summary>  
        /// <param name="subject">The subject to document.</param>  
        public string GetDocumentation(HttpActionDescriptor subject)
        {
            return this.GetFirstMatch(p => p.GetDocumentation(subject));
        }

        /// <summary>Gets the documentation for a subject.</summary>  
        /// <param name="subject">The subject to document.</param>  
        public string GetDocumentation(HttpParameterDescriptor subject)
        {
            return this.GetFirstMatch(p => p.GetDocumentation(subject));
        }

        /// <summary>Gets the documentation for a subject.</summary>  
        /// <param name="subject">The subject to document.</param>  
        public string GetResponseDocumentation(HttpActionDescriptor subject)
        {
            return this.GetFirstMatch(p => p.GetDocumentation(subject));
        }

        /********* 
        ** Private methods 
        *********/
        /// <summary>Get the first valid result from the collection of XML documentation providers.</summary>  
        /// <param name="expr">The method to invoke.</param>  
        private string GetFirstMatch(Func<XmlDocumentationProvider, string> expr)
        {
            return this.Providers
                .Select(expr)
                .FirstOrDefault(p => !String.IsNullOrWhiteSpace(p));
        }
    }
}