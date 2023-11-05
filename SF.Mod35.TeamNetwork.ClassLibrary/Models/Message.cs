using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF.Mod35.TeamNetwork.ClassLibrary.Models;

public  class Message
{
	public int Id { get; set; }
	public int DialogId { get; set; }
	public Dialog Dialog { get; set; }
	public bool FromUser1 { get; set; }
	public string Text { get; set; }

    public Message(Dialog dialog, string text, bool fromUser1)
    {
		Dialog = dialog;
		DialogId = dialog.Id;
		Text = text;
		FromUser1 = fromUser1;
		dialog.Messages.Add(this);
    }
}
