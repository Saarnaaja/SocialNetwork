using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Models;
public class UserRegistrationData
{
    public string FirstName { set; get; }
    public string LastName { set; get; }
    public string Password { set; get; }
    public string Email { set; get; }
}
