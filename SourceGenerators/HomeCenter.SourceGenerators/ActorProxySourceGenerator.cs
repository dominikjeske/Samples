using HomeCenter.Abstractions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeCenter.SourceGenerators
{
    [Generator]
    public class ActorProxySourceGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new ActorSyntaxReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            using var sourceGenContext = SourceGeneratorContext<ActorProxySourceGenerator>.Create(context);

            if (context.SyntaxReceiver is ActorSyntaxReceiver actorSyntaxReciver)
            {
                foreach (var proxy in actorSyntaxReciver.CandidateProxies)
                {
                    var source = GenearteProxy(proxy, sourceGenContext);

                    context.AddSource(source.FileName, SourceText.From(source.SourceCode, Encoding.UTF8));
                }
            }
        }

        private GeneratedSource GenearteProxy(ClassDeclarationSyntax proxy, SourceGeneratorContext<ActorProxySourceGenerator> context)
        {
            try
            {
                var proxyModel = GetModel(proxy, context.GeneratorExecutionContext.Compilation);

                var templateString = ResourceReader.GetResource("ActorProxy.scriban");

                var result = TemplateGenerator.Execute(templateString, proxyModel);

                context.TryLogSourceCode(proxy, result);

                return new GeneratedSource(result, proxyModel.ClassName);
            }
            catch (Exception ex)
            {
                context.TryLogException(proxy, ex);
                return context.GenerateErrorSourceCode(ex, proxy);
            }
        }

        private ProxyModel GetModel(ClassDeclarationSyntax classSyntax, Compilation compilation)
        {
            var root = classSyntax.GetCompilationUnit();
            var classSemanticModel = compilation.GetSemanticModel(classSyntax.SyntaxTree);
            var classSymbol = classSemanticModel.GetDeclaredSymbol(classSyntax);

            var proxyModel = new ProxyModel
            {
                ClassBase = classSyntax.GetClassName(),

                ClassName = $"{classSyntax.GetClassName()}{ProxyAttribute.Name}",

                ClassModifier = classSyntax.GetClassModifier(),

                Usings = root.GetUsings(),

                Namespace = root.GetNamespace(),

                Commands = GetMethodWithParameter(classSyntax, classSemanticModel, nameof(Command)),

                Queries = GetMethodWithParameter(classSyntax, classSemanticModel, nameof(Query)),

                Events = GetMethodWithParameter(classSyntax, classSemanticModel, nameof(Event)),

                ConstructorParameters = GetConstructor(classSymbol),

                InjectedProperties = GetInjectedProperties(classSymbol),

                Subscriptions = GetSubscriptions(classSyntax, classSemanticModel)
            };

            return proxyModel;
        }

        private List<SubscriptionDescriptor> GetSubscriptions(ClassDeclarationSyntax classSyntax, SemanticModel model)
        {
            var commands = GetMethodWithParameter(classSyntax, model, "Command", "Subscribe");
            var events = GetMethodWithParameter(classSyntax, model, "Event", "Subscribe");
            var queries = GetMethodWithParameter(classSyntax, model, "Query", "Subscribe");

            var subscriptions = commands.Select(c => new SubscriptionDescriptor
            {
                MessageType = c.ParameterType,
                SubscribeOnParent = IsSubscribeOnParent(c)
            }).Union
            (
                queries.Select(c => new SubscriptionDescriptor
                {
                    MessageType = c.ParameterType,
                    ReturnType = c.ReturnType,
                    ReturnTypeGenericArgument = c.ReturnTypeGenericArgument,
                    SubscribeOnParent = IsSubscribeOnParent(c)
                })
            ).Union
            (
                events.Select(c => new SubscriptionDescriptor
                {
                    MessageType = c.ParameterType,
                    SubscribeOnParent = IsSubscribeOnParent(c)
                })
            ).ToList();

            return subscriptions;
        }

        private bool IsSubscribeOnParent(MethodDescription methodDescription)
        {
            return methodDescription.Attributes.Where(a => a.Name == "Subscribe").Any(x => x.Values.Any(val => val.IndexOf("true", StringComparison.OrdinalIgnoreCase) > -1));
        }

        private List<ParameterDescriptor> GetConstructor(INamedTypeSymbol classSymbol)
        {
            IMethodSymbol baseConstructor = classSymbol.Constructors.OrderByDescending(p => p.Parameters.Length).FirstOrDefault();

            var parList = baseConstructor.Parameters.Select(par => new ParameterDescriptor()
            {
                Name = par.Name,
                Type = par.Type.ToString()
            }).ToList();

            return parList;
        }

        private List<PropertyAssignDescriptor> GetInjectedProperties(INamedTypeSymbol classSymbol)
        {
            var dependencyProperties = classSymbol.GetAllMembers()
                                                  .Where(x => x.Kind == SymbolKind.Property && x.GetAttributes().Any(a => a.AttributeClass.Name == nameof(DIAttribute)))
                                                  .OfType<IPropertySymbol>()
                                                  .Select(par => new PropertyAssignDescriptor()
                                                  {
                                                      Destination = par.Name,
                                                      Type = par.Type.ToString(),
                                                      Source = par.Name.ToCamelCase()
                                                  }).ToList();
            return dependencyProperties;
        }

        private List<MethodDescription> GetMethodWithParameter(ClassDeclarationSyntax classSyntax, SemanticModel model, string parameterType, string attributeType = null)
        {
            var filter = classSyntax.DescendantNodes()
                                    .OfType<MethodDeclarationSyntax>()
                                    .Where(m => m.ParameterList.Parameters.Count == 1 && !m.Modifiers.Any(x => x.ValueText == "private"));

            if (attributeType != null)
            {
                filter = filter.Where(m => m.AttributeLists.Any(a => a.Attributes.Any(x => x.Name.ToString() == attributeType)));
            }

            var result = filter.Select(method => new
            {
                Name = method.Identifier.ValueText,
                ReturnType = model.GetTypeInfo(method.ReturnType).Type as INamedTypeSymbol,
                Parameter = model.GetDeclaredSymbol(method.ParameterList.Parameters.FirstOrDefault()),
                Attributes = method.AttributeLists.SelectMany(a => a.Attributes.Select(ax => new AttributeDescriptor
                {
                    Name = ax.Name.ToString(),
                    Values = (ax?.ArgumentList?.Arguments.Select(x => x.Expression.ToFullString()) ?? Enumerable.Empty<string>()).ToList()
                }))
            }).Where(x => x.Parameter.Type.BaseType?.Name == parameterType || x.Parameter.Type.BaseType?.BaseType?.Name == parameterType || x.Parameter.Type.Name == parameterType)
            .Select(c => new MethodDescription
            {
                MethodName = c.Name,
                ParameterType = c.Parameter.Type.Name,
                ReturnType = c.ReturnType.Name,
                ReturnTypeGenericArgument = c.ReturnType.TypeArguments.FirstOrDefault()?.ToString(),
                Attributes = c.Attributes.ToList()
                // TODO write recursive base type check
            }).ToList();

            return result;
        }
    }
}