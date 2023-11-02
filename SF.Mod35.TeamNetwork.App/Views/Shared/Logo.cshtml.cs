namespace SF.Mod35.TeamNetwork.App.Views.Shared;
public class LogoViewModel
{
	public string CustomStyle { get; set; }
    public LogoViewModel(string style = "")
    {
        CustomStyle = style;
    }
}
