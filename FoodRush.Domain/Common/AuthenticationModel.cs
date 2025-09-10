using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRush.Domain.Common
{
    public class AuthenticationModel
    {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
        public DateTime expierOn { get; set; }
    }
}
