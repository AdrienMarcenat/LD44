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
        public EAction m_Action;

        public Option()
        { }

        public Option(string text, string destinationNodeID, EAction action)
        {
            m_Text = text;
            m_DestinationNodeID = destinationNodeID;
            m_Action = action;
        }
    }
}
