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
```  

Ścieżka: **//\*[@Name='fieldName' and @ClassName='TextBlock']/following-sibling::*/\*[@ClassName='MLTextView']**  
Kod:   
```java
var x = _target.WithDescendant()
               .OfType("*")
               .ApplyCondition(x => x.WhereAttribute("Name)
                                     .IsEqualTo("fieldName")
                                     .And()
                                     .WhereAttribute("ClassName")
                                     .IsEqualTo("TextBlock")
               .WithFollowingSibling()
               .OfType("*")
               .WithChild()
               .OfType("*")
               .ApplyCondition(x => x.WhereAttribute("ClassName")
                                     .IsEqualTo("MLTextView"));
```

Ścieżka: **(.//Tab[@AutomationId='PART_Tab']/TabItem[not(contains(@Name, 'Motorola'))])[last()]**  
Kod:
```java
var x = _target.FromCurrentNode()
               .WithDescendant()
               .OfType("Tab")
               .ApplyCondition(x => x.WhereAttribute("AutomationId")
                                     .IsEqualTo("PART_Tab"))
               .WithChild()
               .OfType("TabItem")
               .OpenAttributeBuilder()
               .ApplyCondition(x => x.Not(y => y.WhereAttributeContain("Name", "Motorola")))
               .IndexFromGlobalCollection(^0);
```

Ścieżka: **//div[contains(@class, 'password-group')]/ancestor-or-self::div//input[@type='email']**  
Kod:  
```java
var x = _target.WithDescendant()
               .OfType("div")
               .ApplyCondition(x => x.WhereAttributeContain("class", "password-group"))
               .WithAncestorOrSelf()
               .OfType("div")
               .WithDescendant()
               .OfType("input")
               .ApplyCondition(x => x.WhereAttribute("type")
                                     .IsEqualTo("email"));
```
