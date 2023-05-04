using System.Text;

namespace XPather
{
    public class XPathRootBuilder : Contracts.INode,
                                    Contracts.IAxeNode,
                                    Contracts.ISelector,
                                    Contracts.IOptions
    {
        private readonly StringBuilder _builder;

        public XPathRootBuilder()
        {
            _builder = new StringBuilder();
        }

        public Contracts.ISelector FromCurrentNode()
        {
            _builder.Append(".");
            return this;
        }

        public Contracts.INode WithDescendant()
        {
            _builder.Append("//");
            return this;
        }

        public Contracts.INode WithChild()
        {
            _builder.Append("/");
            return this;
        }

        public Contracts.IAxeNode WithFollowingSibling()
        {
            _builder.Append($"/following-sibling::");
            return this;
        }

        public Contracts.IAxeNode WithPrecedingSibling()
        {
            _builder.Append($"/preceding-sibling::");
            return this;
        }

        public Contracts.IAxeNode WithDescendantOrSelf()
        {
            _builder.Append($"/descendant-or-self::");
            return this;
        }

        public Contracts.IAxeNode WithAncestor()
        {
            _builder.Append($"/ancestor::");
            return this;
        }

        public Contracts.IAxeNode WithAncestorOrSelf()
        {
            _builder.Append($"/ancestor-or-self::");
            return this;
        }

        public Contracts.IOptions OfType(string type)
        {
            _builder.Append($"{type}");
            return this;
        }

        public Contracts.IOptions OfTypeParent()
        {
            _builder.Append($"..");
            return this;
        }

        public Contracts.IOptions OfTypeAny()
        {
            _builder.Append($"*");
            return this;
        }

        public XPathAttributeBuilder OpenAttributeBuilder()
        {
            var attrBuilder = new XPathAttributeBuilder(_builder, this);
            return attrBuilder;
        }

        public XPathRootBuilder OfAttributeType(string type)
        {
            _builder.Append($"@{type}");
            return this;
        }

        public XPathRootBuilder IndexFromGlobalCollection(Index index)
        {
            _builder.Insert(0, "(");
            _builder.Append(index.IsFromEnd ? $")[last() - {index.Value}]" : $")[{index.Value}]");
            return this;
        }

        public XPathRootBuilder IndexFromLocalCollection(Index index)
        {
            _builder.Append(index.IsFromEnd ? $"[last() - {index.Value}]" : $"[{index.Value}]");
            return this;
        }

        public XPathRootBuilder WithSeveralPath()
        {
            _builder.Append(" | ");
            return this;
        }

        public string BuildPath() => _builder.ToString();
    }
}
