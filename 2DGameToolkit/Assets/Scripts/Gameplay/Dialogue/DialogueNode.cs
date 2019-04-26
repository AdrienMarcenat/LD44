using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dialogue
{
    public class Node
    {
        public string m_ID = "";
        public string m_Name;
        public string m_Text;
        public string m_NextNodeID = "";
        public List<Option> m_Options = new List<Option>();

        public Node()
        { }

        public Node(string name, string text, string id)
        {
            m_Name = name;
            m_Text = text;
            m_ID = id;
        }

        public void AddOption(Option option)
        {
            m_Options.Add(option);
        }
        public void RemoveOption(Option option)
        {
            m_Options.Remove(option);
        }
    }
}
