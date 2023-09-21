namespace BackEnd_Angular.Utilities
{
	public interface IAzureStorage
	{
		Task DeleteFile(string route, string content);
		Task<string> EditFile(string content, IFormFile file, string route);
		Task<string> SaveFile(string content, IFormFile file);
	}
}