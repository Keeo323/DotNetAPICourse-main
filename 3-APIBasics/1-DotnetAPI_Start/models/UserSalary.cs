namespace DotnetAPI.Models
{
    public partial class UserSalary : User
    {
        public int UserId { get; set; }
        public decimal Salary { get; set; }
        public decimal AvgSalary { get; set; }

    }
}