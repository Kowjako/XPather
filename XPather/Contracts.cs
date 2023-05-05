namespace XPather
{
    /// <summary>
    /// This contracts are used to change scope of visible methods from builder
    /// to prevent unexpected behavior
    /// </summary>
    public static class Contracts
    {
        /// <summary>
        /// Specify where path is completed and ready to build
        /// </summary>
        public interface IBuildablePath
        {
            string BuildPath();
        }

        /// <summary>
        /// Available options for axes
        /// </summary>
        public interface ISelector
        {
            IAxeNode WithFollowingSibling();
            IAxeNode WithPrecedingSibling();
            IAxeNode WithDescendantOrSelf();
            IAxeNode WithAncestor();
            IAxeNode WithAncestorOrSelf();
            INode WithDescendant();
            INode WithChild();
        }

        /// <summary>
        /// Available options after corrected xpath
        /// </summary>
        public interface IOptions : ISelector, IBuildablePath
        {
            XPathRootBuilder IndexFromGlobalCollection(Index index);
            XPathRootBuilder IndexFromLocalCollection(Index index);
            XPathRootBuilder ApplyCondition(Action<Contracts.ICondition> b);
        }

        /// <summary>
        /// Available options after specified axe
        /// </summary>
        public interface IAxeNode
        {
            IOptions OfType(string type);
            IOptions OfTypeAny();
        }

        /// <summary>
        /// Available options after specified nameless axe like / or //
        /// </summary>
        public interface INode : IAxeNode
        {
            IOptions OfTypeParent();
            XPathRootBuilder OfAttributeType(string type);
        }

        public interface ICondition
        {
            ICondition WithInnerText(string text);
            ICondition WithInnerTextContains(string text);
            ICondition And();
            ICondition Or();
            ICondition WhereAttribute(string attrName);
            ICondition IsEqualTo(string value);
            ICondition IsNotEqualTo(string value);
            ICondition IsGreaterThan(float value);
            ICondition IsGreaterThanOrEqual(float value);
            ICondition IsLessThan(float value);
            ICondition IsLessThanOrEqual(float value);
            ICondition WhereAttributeContain(string attrName, string value);
            ICondition IsStartsWith(string attrName, string value);
            ICondition Not(Action<ICondition> builder);
        }
    }
}
