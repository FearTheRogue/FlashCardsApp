using System;
using System.Collections.Generic;
using System.Text;

namespace Flash_Cards
{
    public class CardCell
    {
        public string Question { get; set; }
        public string Answer { get; set; }

        public CardCell(string _question, string _answer) => (Question, Answer) = (_question, _answer);
    }
}
