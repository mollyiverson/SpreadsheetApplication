// <copyright file="GlobalSuppressions.cs" company="Molly Iverson:11775649">
// Copyright (c) Molly Iverson:11775649. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "We were told to make our Cell data members protected, not private.", Scope = "member", Target = "~F:SpreadsheetEngine.Cell.columnIndex")]

[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "We were told to make our Cell data members protected, not private.", Scope = "member", Target = "~F:SpreadsheetEngine.Cell.rowIndex")]

[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "We were told to make our Cell data members protected, not private.", Scope = "member", Target = "~F:SpreadsheetEngine.Cell.value")]

[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "We were told to make our Cell data members protected, not private.", Scope = "member", Target = "~F:SpreadsheetEngine.Cell.text")]

[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1130:Use lambda syntax", Justification = "We were taught to use delegate instead of lambda syntax.", Scope = "member", Target = "~E:SpreadsheetEngine.Cell.PropertyChanged")]

[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1130:Use lambda syntax", Justification = "We were taught to use delegate instead of lambda syntax.", Scope = "member", Target = "~E:SpreadsheetEngine.Spreadsheet.CellPropertyChanged")]

[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "The operation symbol is protected so the concrete subclasses can access it.", Scope = "member", Target = "~F:SpreadsheetEngine.OperatorNode.operatorSymbol")]

[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "The left node is protected so the concrete subclasses can access it.", Scope = "member", Target = "~F:SpreadsheetEngine.OperatorNode.left")]

[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "The right node is protected so the concrete subclasses can access it.", Scope = "member", Target = "~F:SpreadsheetEngine.OperatorNode.right")]

[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "The precedence value is protected so the concrete subclasses can access it.", Scope = "member", Target = "~F:SpreadsheetEngine.OperatorNode.precedence")]

[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "The association value is protected so the concrete subclasses can access it.", Scope = "member", Target = "~F:SpreadsheetEngine.OperatorNode.association")]

[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1130:Use lambda syntax", Justification = "We were taught to use delegate instead of lambda syntax.", Scope = "member", Target = "~E:SpreadsheetEngine.Cell.CellValueChanged")]

[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1130:Use lambda syntax", Justification = "We were taught to use delegate instead of lambda syntax.", Scope = "member", Target = "~E:SpreadsheetEngine.Spreadsheet.SCell.DependentCellPropertyChanged")]

[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "We were told to make our Cell data members protected, not private.", Scope = "member", Target = "~F:SpreadsheetEngine.Cell.color")]

[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1130:Use lambda syntax", Justification = "We were taught to use delegate instead of lambda syntax.", Scope = "member", Target = "~E:SpreadsheetEngine.Cell.PropertyChangedForDependents")]
