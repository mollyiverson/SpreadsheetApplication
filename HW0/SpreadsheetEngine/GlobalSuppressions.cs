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
