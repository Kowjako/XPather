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
            XPathAttributeBuilder OpenAttributeBuilder();
            XPathRootBuilder FirstFromGlobalCollection();
            XPathRootBuilder IndexFromGlobalCollection(int index);
            XPathRootBuilder IndexFromGlobalCollectionEnd(int index);
            XPathRootBuilder LastFromGlobalCollection();
            XPathRootBuilder FirstFromLocalCollection();
            XPathRootBuilder IndexFromLocalCollection(int index);
            XPathRootBuilder IndexFromLocalCollectionEnd(int index);
            XPathRootBuilder LastFromLocalCollection();
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

        /// <summary>
        /// Available options during condition building
        /// </summary>
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
