using FitnessApp.Bus;
using FitnessApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.Events
{
    public class UserMessage : IMessage
    {
        public User User { get; set; }
        public UserMessage(User user)
        {
            this.User = user;
        }
    }
}
