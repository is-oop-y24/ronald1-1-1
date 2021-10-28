using System.Collections.Generic;
using Isu;
using Isu.Tools;

namespace IsuExtra
{
    public class Course
    {
        private char _faculty;
        private string _name;
        private List<Flow> _flows;

        public Course(string name, char faculty)
        {
            _name = name;
            _faculty = faculty;
            _flows = new List<Flow>();
        }

        public char Faculty => _faculty;
        public List<Flow> Flows => _flows;

        public Flow AddFlow(string name)
        {
            Flow flow = new Flow(this, name);
            _flows.Add(flow);
            return flow;
        }
    }
}