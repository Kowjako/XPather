using System.Text;

namespace XPather
{
    public static class Condition
    {
        public static string Create(Action<ConditionBuilder> builder)
        {
            var b = new ConditionBuilder();
            builder(b);
            return b.Compile();
        }
    }

    public class ConditionBuilder 
    {
        private readonly StringBuilder _builder = new();

        internal ConditionBuilder()
        {
            _builder.Append("[");
        }

        public string Compile()
        {
            _builder.Append("]");
            return _builder.ToString();
        }

        public ConditionBuilder WithInnerText(string text)
        {
            _builder.Append($"text()='{text}'");
            return this;
        }

        public ConditionBuilder WithInnerTextContains(string text)
        {
            _builder.Append($"contains(text(), '{text}')");
            return this;
        }
        public ConditionBuilder And()
        {
            _builder.Append(" and ");
            return this;
        }

        public ConditionBuilder Or()
        {
            _builder.Append(" or ");
            return this;
        }

        public ConditionBuilder WhereAttribute(string attrName)
        {
            _builder.Append($"@{attrName}");
            return this;
        }

        public ConditionBuilder IsEqualTo(string value)
        {
            _builder.Append($"='{value}'");
            return this;
        }

        public ConditionBuilder IsNotEqualTo(string value)
        {
            _builder.Append($"!='{value}'");
            return this;
        }

        public ConditionBuilder IsGreaterThan(float value)
        {
            _builder.Append($">{value}");
            return this;
        }

        public ConditionBuilder IsGreaterThanOrEqual(float value)
        {
            _builder.Append($">={value}");
            return this;
        }

        public ConditionBuilder IsLessThan(float value)
        {
            _builder.Append($"<{value}");
            return this;
        }

        public ConditionBuilder IsLessThanOrEqual(float value)
        {
            _builder.Append($"<={value}");
            return this;
        }

        public ConditionBuilder WhereAttributeContain(string attrName, string value)
        {
            _builder.Append($"contains(@{attrName}, '{value}')");
            return this;
        }

        public ConditionBuilder IsStartsWith(string attrName, string value)
        {
            _builder.Append($"starts-with(@{attrName}, '{value}')");
            return this;
        }

        public ConditionBuilder Not(Action<ConditionBuilder> builder)
        {
            _builder.Append("not(");
            builder(this);
            _builder.Append(")");
            return this;
        }
    }  
}
