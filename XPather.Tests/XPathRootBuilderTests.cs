namespace XPather.Tests
{
    public class XPathRootBuilderTests
    {
        private XPathRootBuilder _target;

        public XPathRootBuilderTests()
        {
            _target = new XPathRootBuilder();
        }

        [Fact]
        public void XPath_Test_1()
        {
            // Arrange
            var x = _target.FromCurrentNode()
                           .WithChild()
                           .OfType("app")
                           .WithChild()
                           .OfType("description")
                           .WithChild()
                           .OfType("subject")
                           .OpenAttributeBuilder()
                           .StartNotCondition()
                           .WhereAttribute("name")
                           .IsEqualTo("wlodek")
                           .Or()
                           .WhereAttribute("age")
                           .IsGreaterThan(21)
                           .FinishNotCondition()
                           .CloseAttributeBuilder()
                           .WithSeveralPath()
                           .WithDescendant()
                           .OfType("app")
                           .WithChild()
                           .OfType("extra-notes");

            // Act
            var result = x.BuildPath();

            // Assert
            Assert.Equal("./app/description/subject[not(@name='wlodek' or @age>21)] | //app/extra-notes", result);
        }

        [Fact]
        public void XPath_Test_2()
        {
            // Arrange
            var x = _target.FromCurrentNode()
                           .WithChild()
                           .OfType("name")
                           .WithChild()
                           .OfType("description")
                           .WithChild()
                           .OfType("subject")
                           .OpenAttributeBuilder()
                           .StartNotCondition()
                           .WithInnerText("ss-name")
                           .FinishNotCondition()
                           .CloseAttributeBuilder()
                           .FirstFromGlobalCollection();

            // Act
            var result = x.BuildPath();

            // Assert
            Assert.Equal("(./name/description/subject[not(text()='ss-name')])[1]", result);
        }

        [Fact]
        public void XPath_Test_3()
        {
            // Arrange
            var x = _target.WithDescendant()
                           .OfTypeAny()
                           .OpenAttributeBuilder()
                           .WhereAttribute("Name")
                           .IsEqualTo("fieldName")
                           .And()
                           .WhereAttribute("ClassName")
                           .IsEqualTo("TextBlock")
                           .CloseAttributeBuilder()
                           .WithFollowingSibling()
                           .OfTypeAny()
                           .WithChild()
                           .OfTypeAny()
                           .OpenAttributeBuilder()
                           .WhereAttribute("ClassName")
                           .IsEqualTo("MLTextView")
                           .CloseAttributeBuilder();

            // Act
            var result = x.BuildPath();

            // Assert
            Assert.Equal("//*[@Name='fieldName' and @ClassName='TextBlock']/following-sibling::*/*[@ClassName='MLTextView']", result);
        }

        [Fact]
        public void XPath_Test_4()
        {
            // Arrange
            var x = _target.FromCurrentNode()
                           .WithChild()
                           .OfType("app")
                           .WithChild()
                           .OfType("extra-notes")
                           .WithChild()
                           .OfType("note")
                           .OpenAttributeBuilder()
                           .WhereAttribute("id")
                           .IsGreaterThan(1)
                           .CloseAttributeBuilder()
                           .FirstFromLocalCollection()
                           .WithChild()
                           .OfType("value");

            // Act
            var result = x.BuildPath();

            // Assert
            Assert.Equal("./app/extra-notes/note[@id>1][1]/value", result);
        }

        [Fact]
        public void XPath_Test_5()
        {
            // Arrange
            var x = _target.WithDescendant()
                           .OfType("div")
                           .OpenAttributeBuilder()
                           .WhereAttributeContain("class", "password-group")
                           .CloseAttributeBuilder()
                           .WithAncestorOrSelf()
                           .OfType("div")
                           .WithDescendant()
                           .OfType("input")
                           .OpenAttributeBuilder()
                           .WhereAttribute("type")
                           .IsEqualTo("email")
                           .CloseAttributeBuilder();

            // Act
            var result = x.BuildPath();

            // Assert
            Assert.Equal("//div[contains(@class, 'password-group')]/ancestor-or-self::div//input[@type='email']", result);
        }

        [Fact]
        public void XPath_Test_6()
        {
            // Arrange
            var x = _target.WithDescendant()
                           .OfType("option")
                           .OpenAttributeBuilder()
                           .WhereAttribute("value")
                           .IsEqualTo("Founder/CXO")
                           .CloseAttributeBuilder()
                           .WithPrecedingSibling()
                           .OfType("option")
                           .FirstFromLocalCollection();

            // Act
            var result = x.BuildPath();

            // Assert
            Assert.Equal("//option[@value='Founder/CXO']/preceding-sibling::option[1]", result);
        }

        [Fact]
        public void XPath_Test_7()
        {
            // Arrange
            var x = _target.WithDescendant()
                           .OfType("app")
                           .WithChild()
                           .OfType("description")
                           .WithChild()
                           .OfType("subject")
                           .FirstFromLocalCollection()
                           .WithChild()
                           .OfType("tex")
                           .OpenAttributeBuilder()
                           .WithInnerText("sas")
                           .CloseAttributeBuilder()
                           .WithChild()
                           .OfAttributeType("id");

            // Act
            var result = x.BuildPath();

            // Assert
            Assert.Equal("//app/description/subject[1]/tex[text()='sas']/@id", result);
        }

        [Fact]
        public void XPath_Test_8()
        {
            // Arrange
            var x = _target.FromCurrentNode()
                           .WithDescendant()
                           .OfType("Tab")
                           .OpenAttributeBuilder()
                           .WhereAttribute("AutomationId")
                           .IsEqualTo("PART_Tab")
                           .CloseAttributeBuilder()
                           .WithChild()
                           .OfType("TabItem")
                           .OpenAttributeBuilder()
                           .StartNotCondition()
                           .WhereAttributeContain("Name", "Motorola")
                           .FinishNotCondition()
                           .CloseAttributeBuilder()
                           .LastFromGlobalCollection();

            // Act
            var result = x.BuildPath();

            // Assert
            Assert.Equal("(.//Tab[@AutomationId='PART_Tab']/TabItem[not(contains(@Name, 'Motorola'))])[last()]", result);
        }

        [Fact]
        public void XPath_Test_9()
        {
            // Arrange
            var x = _target.WithDescendant()
                           .OfType("app")
                           .WithChild()
                           .OfType("description")
                           .WithChild()
                           .OfTypeAny()
                           .WithChild()
                           .OfType("t")
                           .WithChild()
                           .OfTypeParent();

            // Act
            var result = x.BuildPath();

            // Assert
            Assert.Equal("//app/description/*/t/..", result);
        }

        [Fact]
        public void XPath_Test_10()
        {
            // Arrange
            var x = _target.WithDescendant()
                           .OfType("span")
                           .OpenAttributeBuilder()
                           .WithInnerTextContains("odamax")
                           .CloseAttributeBuilder()
                           .IndexFromGlobalCollectionEnd(1)
                           .WithFollowingSibling()
                           .OfType("strong")
                           .OpenAttributeBuilder()
                           .WhereAttribute("class")
                           .IsEqualTo("deals-price")
                           .CloseAttributeBuilder();

            // Act
            var result = x.BuildPath();

            // Assert
            Assert.Equal("(//span[contains(text(), 'odamax')])[last() - 1]/following-sibling::strong[@class='deals-price']", result);
        }
    }
}