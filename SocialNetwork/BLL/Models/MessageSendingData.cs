using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Models;
public class MessageSendingData
{
    public int SenderId { set; get; }
    public string Content { set; get; }
    public string RecipientEmail { set; get; }
}
