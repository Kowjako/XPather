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
                          .ApplyCondition(x => x.Not(y => y.WhereAttribute("name")
                                                           .IsEqualTo("wlodek")
                                                           .Or()
                                                           .WhereAttribute("age")
                                                           .IsGreaterThan(21)))
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
                           .ApplyCondition(x => x.Not(y => y.WithInnerText("ss-name")))
                           .IndexFromGlobalCollection(1);

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
                           .ApplyCondition(x => x.WhereAttribute("Name")
                                                 .IsEqualTo("fieldName")
                                                 .And()
                                                 .WhereAttribute("ClassName")
                                                 .IsEqualTo("TextBlock"))
                           .WithFollowingSibling()
                           .OfTypeAny()
                           .WithChild()
                           .OfTypeAny()
                           .ApplyCondition(x => x.WhereAttribute("ClassName")
                                                 .IsEqualTo("MLTextView"));

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
                           .ApplyCondition(x => x.WhereAttribute("id")
                                                 .IsGreaterThan(1))
                           .IndexFromLocalCollection(1)
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
                           .ApplyCondition(x => x.WhereAttributeContain("class", "password-group"))
                           .WithAncestorOrSelf()
                           .OfType("div")
                           .WithDescendant()
                           .OfType("input")
                           .ApplyCondition(x => x.WhereAttribute("type")
                                                 .IsEqualTo("email"));

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
                           .ApplyCondition(x => x.WhereAttribute("value")
                                                 .IsEqualTo("Founder/CXO"))
                           .WithPrecedingSibling()
                           .OfType("option")
                           .IndexFromLocalCollection(1);

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
                           .IndexFromLocalCollection(1)
                           .WithChild()
                           .OfType("tex")
                           .ApplyCondition(x => x.WithInnerText("sas"))
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
                           .ApplyCondition(x => x.WhereAttribute("AutomationId")
                                                 .IsEqualTo("PART_Tab"))
                           .WithChild()
                           .OfType("TabItem")
                           .ApplyCondition(x => x.Not(y => y.WhereAttributeContain("Name", "Motorola")))
                           .IndexFromGlobalCollection(^0);

            // Act
            var result = x.BuildPath();

            // Assert
            Assert.Equal("(.//Tab[@AutomationId='PART_Tab']/TabItem[not(contains(@Name, 'Motorola'))])[last() - 0]", result);
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
                           .ApplyCondition(x => x.WithInnerTextContains("odamax"))
                           .IndexFromGlobalCollection(^1)
                           .WithFollowingSibling()
                           .OfType("strong")
                           .ApplyCondition(x => x.WhereAttribute("class")
                                                 .IsEqualTo("deals-price"));

            // Act
            var result = x.BuildPath();

            // Assert
            Assert.Equal("(//span[contains(text(), 'odamax')])[last() - 1]/following-sibling::strong[@class='deals-price']", result);
        }
    }
}