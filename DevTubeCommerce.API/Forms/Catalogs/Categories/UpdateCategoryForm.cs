namespace DevTubeCommerce.API.Forms.Catalogs.Categories
{
    public class UpdateCategoryForm
    {
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public List<Guid> FeatureIds { get; set; }
    }
}
