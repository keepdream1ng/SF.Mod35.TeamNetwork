using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF.Mod35.TeamNetwork.ClassLibrary.Models;

// The Skillfactory example had only entity for messages, with two foreign key for users.
// I think my approach is a bit more efficiant in turms of db optimization.
public class Dialog
{
	public int Id { get; set; }

	public string User1Id { get; set; }
	public User User1 { get; set; }

	public string User2Id { get; set; }
	public User User2 { get; set; }
	public List<Message> Messages { get; set; }

    public Dialog(User user1, User user2)
    {
		User1 = user1;
		User1Id = user1.Id;
		User2 = user2;
		User2Id = user2.Id;
		Messages = new List<Message>();
    }
}
