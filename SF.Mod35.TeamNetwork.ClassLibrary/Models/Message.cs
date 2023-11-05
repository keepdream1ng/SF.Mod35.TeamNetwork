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
}
