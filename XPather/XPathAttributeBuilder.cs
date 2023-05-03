using System.Text;

namespace XPather
{
    public class XPathAttributeBuilder
    {
        private readonly StringBuilder _builder;
        private readonly XPathRootBuilder _parent;

        public XPathAttributeBuilder(StringBuilder builder, XPathRootBuilder root)
        {
            _builder = builder;
            _parent = root;

            _builder.Append("[");
        }

        public XPathAttributeBuilder WhereAttribute(string attrName)
        {
            _builder.Append($"@{attrName}");
            return this;
        }

        public XPathAttributeBuilder IsEqualTo(string value)
        {
            _builder.Append($"='{value}'");
            return this;
        }

        public XPathAttributeBuilder IsNotEqualTo(string value)
        {
            _builder.Append($"!='{value}'");
            return this;
        }

        public XPathAttributeBuilder IsGreaterThan(float value)
        {
            _builder.Append($">{value}");
            return this;
        }

        public XPathAttributeBuilder IsGreaterThanOrEqual(float value)
        {
            _builder.Append($">={value}");
            return this;
        }

        public XPathAttributeBuilder IsLessThan(float value)
        {
            _builder.Append($"<{value}");
            return this;
        }

        public XPathAttributeBuilder IsLessThanOrEqual(float value)
        {
            _builder.Append($"<={value}");
            return this;
        }

        public XPathAttributeBuilder WhereAttributeContain(string attrName, string value)
        {
            _builder.Append($"contains(@{attrName}, '{value}')");
            return this;
        }

        public XPathAttributeBuilder IsStartsWith(string attrName, string value)
        {
            _builder.Append($"starts-with(@{attrName}, '{value}')");
            return this;
        }

        public XPathAttributeBuilder WithInnerText(string text)
        {
            _builder.Append($"text()='{text}'");
            return this;
        }

        public XPathAttributeBuilder WithInnerTextContains(string text)
        {
            _builder.Append($"contains(text(), '{text}')");
            return this;
        }

        public XPathAttributeBuilder StartNotCondition()
        {
            _builder.Append("not(");
            return this;
        }

        public XPathAttributeBuilder FinishNotCondition()
        {
            _builder.Append(")");
            return this;
        }

        public XPathAttributeBuilder And()
        {
            _builder.Append(" and ");
            return this;
        }

        public XPathAttributeBuilder Or()
        {
            _builder.Append(" or ");
            return this;
        }

        public XPathRootBuilder CloseAttributeBuilder()
        {
            _builder.Append("]");
            return _parent;
        }
    }
}
