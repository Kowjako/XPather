namespace XPather
{
    /// <summary>
    /// This contracts are used to change scope of visible methods from builder
    /// to prevent unexpected behavior
    /// </summary>
    public static class Contracts
    {
        public interface IAxeNode
        {
            XPathRootBuilder OfType(string type);
            XPathRootBuilder OfTypeAny();
        }

        public interface INode : IAxeNode
        {
            XPathRootBuilder OfTypeParent();
            XPathRootBuilder OfAttributeType(string type);
        }

        public interface ICondition
        {
            XPathAttributeBuilder IsEqualTo(string value);
            XPathAttributeBuilder IsNotEqualTo(string value);
            XPathAttributeBuilder IsGreaterThan(float value);
            XPathAttributeBuilder IsGreaterThanOrEqual(float value);
            XPathAttributeBuilder IsLessThan(float value);
            XPathAttributeBuilder IsLessThanOrEqual(float value);
        }
    }
}
