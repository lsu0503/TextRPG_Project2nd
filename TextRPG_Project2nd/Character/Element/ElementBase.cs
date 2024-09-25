using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Project2nd.Character.Element
{
    public interface IElementBase
    {
        string Name { get; }
        int CurDegree { get; set; }

        public void ActElement();
    }
}
