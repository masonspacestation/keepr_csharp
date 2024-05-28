namespace keepr_csharp.Models;

public class Profile : RepoItem<string>
{
  public string Name { get; set; }
  public string Picture { get; set; }
  public string CreatorId { get; set; }
}