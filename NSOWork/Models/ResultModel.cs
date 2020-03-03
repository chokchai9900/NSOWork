using System;
using System.Collections.Generic;
using System.Text;

namespace NSOWork.Models
{
    public class ResultModel
    {
        public string Zippath { get; set; }
        public IEnumerable<string> ContainerName { get; set; }
    }
}
