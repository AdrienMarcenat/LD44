using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dialogue
{
    public class Option
    {
        public string m_Text;
        public string m_DestinationNodeID;

        public Option()
        { }

        public Option(string text, string destinationNodeID)
        {
            m_Text = text;
            m_DestinationNodeID = destinationNodeID;
        }
    }
}
