namespace SF.Mod35.TeamNetwork.ClassLibrary.Models;

public class Connection
{
	public int Id { get; set; }
	public string UserId { get; set; }
	public User User { get; set; }

	public string ConnectedUserId { get; set; }

	public User ConnectedUser { get; set; }

	public ConnectionStatus Status { get; set; }
}
