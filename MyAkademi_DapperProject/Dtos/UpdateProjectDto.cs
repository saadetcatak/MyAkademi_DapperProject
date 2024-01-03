namespace MyAkademi_DapperProject.Dtos
{
    public class UpdateProjectDto
    {
        public int ProjectID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ProjectCategory { get; set; }
        public int CompleteDay { get; set; }
        public decimal Price { get; set; }
    }
}
