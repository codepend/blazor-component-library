﻿using System;
using System.Collections.Generic;
using System.Text;

namespace One1Lion.Samples.SharedLib.Search.QueryExpressions {
  public class QueryExpressionGroup : QueryElement, IQueryExpressionGroup {
    public QueryExpressionGroup() : base() {
      Children = new List<IQueryElement>();
    }

    public string Name { get; set; }
    public bool NotGroup { get; set; }

    public List<IQueryElement> Children { get; set; }

    public override string FormattedDisplay() {
      if (Children is null || Children.Count == 0) { return string.Empty; }

      var sb = new StringBuilder();
      for (var i = 0; i < Children.Count; i++) {
        var child = Children[i];
        sb.Append(child.FormattedDisplay());
        if (i < Children.Count - 1) { sb.Append(child.AndWithNext ? " AND " : " OR "); }
      }
      sb.Insert(0, $"{(NotGroup ? "NOT " : "")}(");
      sb.Append(")");
      return sb.ToString();
    }

    public void AddChild(IQueryElement toAdd, int atIndex = -1) {
      if (Children is null) { Children = new List<IQueryElement>(); }
      if (atIndex < 0) { atIndex = Children.Count; }
      atIndex = Math.Min(Children.Count, Math.Max(0, atIndex));
      Children.Insert(atIndex, toAdd);
      toAdd.Parent = this;
    }

    public override string ToString() {
      return FormattedDisplay();
    }

    public static IQueryElement NewItem() {
      return new QueryExpressionItem();
    }

    public static IQueryElement NewGroup() {
      return new QueryExpressionGroup();
    }
  }
}
