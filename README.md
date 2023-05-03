# XPather
Let's build xpath fluently...  
Idea powstania potrzeby takiego buildera wynika z tego, że tak naprawde nikt nie pamieta wszystkiego co sie dzieje w XPath albo pisze dziwne sciezki ktore dzialaja tylko w specyficznych przypadkach.
## Przykłady wykorzystania
Ścieżka: **./app/description/subject[not(@name='wlodek' or @age>21)] | //app/extra-notes**  
Kod:   
```java
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
```  

Ścieżka: **//\*[@Name='fieldName' and @ClassName='TextBlock']/following-sibling::*/\*[@ClassName='MLTextView']**  
Kod:   
```java
var x = _target.WithDescendant()
               .OfType("*")
               .OpenAttributeBuilder()
               .WhereAttribute("Name")
               .IsEqualTo("fieldName")
               .And()
               .WhereAttribute("ClassName")
               .IsEqualTo("TextBlock")
               .CloseAttributeBuilder()
               .WithFollowingSibling()
               .OfType("*")
               .WithChild()
               .OfType("*")
               .OpenAttributeBuilder()
               .WhereAttribute("ClassName")
               .IsEqualTo("MLTextView")
               .CloseAttributeBuilder();
```

Ścieżka: **(.//Tab[@AutomationId='PART_Tab']/TabItem[not(contains(@Name, 'Motorola'))])[last()]**  
Kod:
```java
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
```

Ścieżka: **//div[contains(@class, 'password-group')]/ancestor-or-self::div//input[@type='email']**  
Kod:  
```java
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
```
