using System;
using System.Collections.Generic;

namespace BFSDomain
{
    public class Row
    {
        private readonly List<string> _columns;

        public Row()
        {
            _columns = new List<string>();
        }

        public void AddColumn(string text)
        {
            _columns.Add(text);
        }

        public IEnumerable<string> Columns => _columns;

        public void AddColumn(int text)
        {
            _columns.Add(text.ToString());
        }
    }
}