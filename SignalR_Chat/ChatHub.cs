using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;

namespace SignalR_Chat
{
    
    public class ChatHub : Hub
    {
        private ChatContext db;
        static List<User> Users = new List<User>();
       static List<string> history = new List<string>();
        public ChatHub(ChatContext context)
        {
            db = context;
        }

        
        public async Task Send(string username, string message, string date)
        {
            
           
            var newMessage = new Messages
            {
                
                Message = username + ": " + message + " " + date
                
            };
           
            db.Messages.Add(newMessage);
            db.SaveChanges();
            await Clients.All.SendAsync("AddMessage", username, message,date);
        }

        public async Task Connect(string userName)
        {
            var id = Context.ConnectionId;

           
            if (!Users.Any(x => x.ConnectionId == id))
            {
                Users.Add(new User { ConnectionId = id, Name = userName });

              

                
                await Clients.Caller.SendAsync("Connected", id, userName, Users, history);

              
                await Clients.AllExcept(id).SendAsync("NewUserConnected", id, userName);
            }
        }
        public async Task History()
        {
            //var id = Context.ConnectionId;

            var mess =  db.Messages.Select(m => m.Message).ToList();
             history.AddRange(mess);
            await Clients.Caller.SendAsync("ShowHistory", history);
        }

       
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var item = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                Users.Remove(item);
                var id = Context.ConnectionId;
               
                await Clients.All.SendAsync("UserDisconnected", id, item.Name);
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
