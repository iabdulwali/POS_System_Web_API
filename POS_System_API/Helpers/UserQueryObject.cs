namespace POS_System_API.Helpers
{
    public class UserQueryObject
    {
        public string? Username { get; set; } = null;
        public string? FullName { get; set; } = null;
        public string? Phone {  get; set; } = null;
        public string? SortBy { get; set; } = string.Empty;
        public bool isDescending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 3;

    }
}
