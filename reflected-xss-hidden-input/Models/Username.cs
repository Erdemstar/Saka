using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace reflected_xss_input.Models
{
    public class Username
    {
        public string Name { get; set; }

        public bool ControlRegex(string name)
        {
            Regex rg = new Regex(@"^[\w'`=;():]*$");
            bool status = rg.IsMatch(name);

            System.Diagnostics.Debug.WriteLine(status);
            return status;
        }
    }


}
