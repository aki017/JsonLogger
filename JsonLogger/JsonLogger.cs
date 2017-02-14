namespace JsonLogger
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics;
  using System.IO;

  using Microsoft.Build.Framework;
  using Microsoft.Build.Utilities;

  using Newtonsoft.Json;

  public class JsonLogger : Logger
  {
    private readonly List<Entry> result = new List<Entry>();

    public override void Initialize(IEventSource eventSource)
    {
      Debug.Assert(eventSource != null, "eventSource != null");
      eventSource.WarningRaised += this.WarningRaised;
      eventSource.ErrorRaised += this.ErrorRaised;
      eventSource.BuildFinished += this.BuildFinished;
    }

    private void WarningRaised(object sender, BuildWarningEventArgs e)
    {
      this.result.Add(new Entry
      {
        Category = "warning",
        SubCategory = e.Subcategory,
        Code = e.Code,
        File = e.File,
        LineNumber = e.LineNumber,
        ColumnNumber = e.ColumnNumber,
        EndLineNumber = e.EndLineNumber,
        EndColumnNumber = e.EndColumnNumber,
        Message = e.Message,
      });
    }

    private void ErrorRaised(object sender, BuildErrorEventArgs e)
    {
      this.result.Add(new Entry
      {
        Category = "warning",
        SubCategory = e.Subcategory,
        Code = e.Code,
        File = e.File,
        LineNumber = e.LineNumber,
        ColumnNumber = e.ColumnNumber,
        EndLineNumber = e.EndLineNumber,
        EndColumnNumber = e.EndColumnNumber,
        Message = e.Message,
      });
    }

    private void BuildFinished(object sender, BuildFinishedEventArgs e)
    {
      Console.ForegroundColor = ConsoleColor.White;
      File.WriteAllText("JsonLogger.Output.json", JsonConvert.SerializeObject(this.result));
    }

#pragma warning disable 414
    private struct Entry
    {
      public string Category;

      public string SubCategory;

      public string Code;

      public string File;

      public int LineNumber;

      public int ColumnNumber;

      public int EndLineNumber;

      public int EndColumnNumber;

      public string Message;
    }
#pragma warning restore 414
  }
}
