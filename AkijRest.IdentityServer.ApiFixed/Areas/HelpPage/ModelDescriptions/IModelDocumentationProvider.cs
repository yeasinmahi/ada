using System;
using System.Reflection;

namespace AkijRest.IdentityServer.ApiFixed.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}