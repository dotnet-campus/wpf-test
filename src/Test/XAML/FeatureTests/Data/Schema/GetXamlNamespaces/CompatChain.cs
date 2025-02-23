// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Windows.Markup;
using FooNamespace;

[assembly: XmlnsDefinition("http://xmlns1", "FooNamespace")]
[assembly: XmlnsDefinition("http://xmlns2", "FooNamespace")]
[assembly: XmlnsDefinition("http://xmlns3", "FooNamespace")]
[assembly: XmlnsDefinition("http://xmlns4", "FooNamespace")]
[assembly: XmlnsDefinition("http://xmlns5", "FooNamespace")]
[assembly: XmlnsDefinition("http://xmlns6", "FooNamespace")]
[assembly: XmlnsDefinition("http://xmlns7", "FooNamespace")]
[assembly: XmlnsDefinition("http://xmlns8", "FooNamespace")]
[assembly: XmlnsDefinition("http://xmlns9", "FooNamespace")]
[assembly: XmlnsCompatibleWith("http://xmlns1", "http://xmlns2")]
[assembly: XmlnsCompatibleWith("http://xmlns2", "http://xmlns3")]
[assembly: XmlnsCompatibleWith("http://xmlns3", "http://xmlns4")]
[assembly: XmlnsCompatibleWith("http://xmlns4", "http://xmlns5")]
[assembly: XmlnsCompatibleWith("http://xmlns5", "http://xmlns6")]
[assembly: XmlnsCompatibleWith("http://xmlns6", "http://xmlns7")]
[assembly: XmlnsCompatibleWith("http://xmlns7", "http://xmlns8")]
[assembly: XmlnsCompatibleWith("http://xmlns8", "http://xmlns9")]

namespace Microsoft.Test.Xaml.Schema
{
    [SchemaTest]
    class CompatChain : GetXamlNamespacesTest
    {
        public CompatChain() : base(typeof(FooType)) { }

        protected override void SetExpectedNamespacesOverride()
        {
            ExpectedNamespaces.Add("http://xmlns9");
            ExpectedNamespaces.Add("http://xmlns8");
            ExpectedNamespaces.Add("http://xmlns7");
            ExpectedNamespaces.Add("http://xmlns6");
            ExpectedNamespaces.Add("http://xmlns5");
            ExpectedNamespaces.Add("http://xmlns4");
            ExpectedNamespaces.Add("http://xmlns3");
            ExpectedNamespaces.Add("http://xmlns2");
            ExpectedNamespaces.Add("http://xmlns1");
        }
    }
}

