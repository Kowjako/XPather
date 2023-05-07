using System.Text;

namespace XPather
{
    public static class Condition
    {
        public static string Create(Action<Contracts.ICondition> builder)
        {
            var b = new ConditionBuilder();
            builder(b);
            return b.Compile();
        }
    }

    public class ConditionBuilder : Contracts.ICondition
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

        public Contracts.ICondition WithInnerText(string text)
        {
            _builder.Append($"text()='{text}'");
            return this;
        }

        public Contracts.ICondition WithInnerTextContains(string text)
        {
            _builder.Append($"contains(text(), '{text}')");
            return this;
        }
        public Contracts.ICondition And()
        {
            _builder.Append(" and ");
            return this;
        }

        public Contracts.ICondition Or()
        {
            _builder.Append(" or ");
            return this;
        }

        public Contracts.ICondition WhereAttribute(string attrName)
        {
            _builder.Append($"@{attrName}");
            return this;
        }

        public Contracts.ICondition IsEqualTo(string value)
        {
            _builder.Append($"='{value}'");
            return this;
        }

        public Contracts.ICondition IsEqualTo(int value)
        {
            _builder.Append($"={value}");
            return this;
        }

        public Contracts.ICondition IsNotEqualTo(string value)
        {
            _builder.Append($"!='{value}'");
            return this;
        }

        public Contracts.ICondition IsGreaterThan(float value)
        {
            _builder.Append($">{value}");
            return this;
        }

        public Contracts.ICondition IsGreaterThanOrEqual(float value)
        {
            _builder.Append($">={value}");
            return this;
        }

        public Contracts.ICondition IsLessThan(float value)
        {
            _builder.Append($"<{value}");
            return this;
        }

        public Contracts.ICondition IsLessThanOrEqual(float value)
        {
            _builder.Append($"<={value}");
            return this;
        }

        public Contracts.ICondition WhereAttributeContain(string attrName, string value)
        {
            _builder.Append($"contains(@{attrName}, '{value}')");
            return this;
        }

        public Contracts.ICondition IsStartsWith(string attrName, string value)
        {
            _builder.Append($"starts-with(@{attrName}, '{value}')");
            return this;
        }

        public Contracts.ICondition Not(Action<Contracts.ICondition> builder)
        {
            _builder.Append("not(");
            builder(this);
            _builder.Append(")");
            return this;
        }
    }  
}
