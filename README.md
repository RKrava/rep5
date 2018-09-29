# Tick Tack Toe

C# implementation of legend game Tick Tack Toe with aggressive bot on infinity field. The project was part of my course work on the 3rd year studying in university.

## Folders structure

```
ASTU.DotNet.TickTackToe/
  AI.cs
  Cell.cs
  Field.cs
  IBot.cs

Test.ASTU.DotNet.TickTackToe/
  AITest.cs
  CellTest.cs
  FieldTest.cs

TickTackToe/

UserInterface/

UserInterfacePictureBox/
```

`ASTU.DotNet.TickTackToe` folder contains bot and internal view of playground. 
`AI.cs` is mind of bot. It has `EvaluateFunction` which analize current field stage to make the best turn for bot. Bot also has `AttackFactor` which allow configure his strategy (more defensive or aggressive). 
`AI.cs` implements interface of `IBot.cs` which allows us to make battles between bots :smile:
`Cell.cs` is prepresentation of cell of field so it has state (Empty, Tick or Tack) and manages it during game to make sure that user cannot change used cell.
`Field.cs` is collection of `Cell.cs`. It manages turns, adding new cells if user going after edge of fields to emulate infinity field, checks if some of players wins.

`Test.ASTU.DotNet.TickTackToe` unit test project to test models of `ASTU.DotNet.TickTackToe`. `UserInterfacePictureBox` contains UI form to represent game field.

## Authors

[**Petr Savchenko**](http://petrsavchenko.ru) - retarded full stack dev from Russia :snowflake: