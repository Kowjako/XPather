﻿using System.Text;

namespace XPather
{
    public class XPathRootBuilder
    {
        private readonly StringBuilder _builder;

        public XPathRootBuilder()
        {
            _builder = new StringBuilder();
        }

        public XPathRootBuilder FromCurrentNode()
        {
            _builder.Append(".");
            return this;
        }

        public XPathRootBuilder WithDescendant()
        {
            _builder.Append("//");
            return this;
        }

        public XPathRootBuilder WithChild()
        {
            _builder.Append("/");
            return this;
        }

        public XPathRootBuilder WithFollowingSibling()
        {
            _builder.Append($"/following-sibling::");
            return this;
        }

        public XPathRootBuilder WithPrecedingSibling()
        {
            _builder.Append($"/preceding-sibling::");
            return this;
        }

        public XPathRootBuilder WithDescendantOrSelf()
        {
            _builder.Append($"/descendant-or-self::");
            return this;
        }

        public XPathRootBuilder WithAncestor()
        {
            _builder.Append($"/ancestor::");
            return this;
        }

        public XPathRootBuilder WithAncestorOrSelf()
        {
            _builder.Append($"/ancestor-or-self::");
            return this;
        }

        public XPathRootBuilder OfType(string type)
        {
            _builder.Append($"{type}");
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

        public XPathRootBuilder FirstFromGlobalCollection()
        {
            _builder.Insert(0, "(");
            _builder.Append(")[1]");
            return this;
        }

        public XPathRootBuilder IndexFromGlobalCollection(int index)
        {
            _builder.Insert(0, "(");
            _builder.Append($")[{index.ToString()}]");
            return this;
        }

        public XPathRootBuilder IndexFromGlobalCollectionEnd(int index)
        {
            _builder.Insert(0, "(");
            _builder.Append($")[last() - {index.ToString()}]");
            return this;
        }

        public XPathRootBuilder LastFromGlobalCollection()
        {
            _builder.Insert(0, "(");
            _builder.Append(")[last()]");
            return this;
        }

        public XPathRootBuilder FirstFromLocalCollection()
        {
            _builder.Append("[1]");
            return this;
        }

        public XPathRootBuilder IndexFromLocalCollection(int index)
        {
            _builder.Append($"[{index.ToString()}]");
            return this;
        }

        public XPathRootBuilder IndexFromLocalCollectionEnd(int index)
        {
            _builder.Append($"[last() - {index.ToString()}]");
            return this;
        }

        public XPathRootBuilder LastFromLocalCollection()
        {
            _builder.Append("[last()]");
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