namespace DevTubeCommerce.API.Forms.Catalogs.Products
{
    public class AddProductForm
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public double Price { get; set; }
        public List<ProductFeatureForm> ProductFeatures { get; set; }
    }
    public class ProductFeatureForm
    {
        public Guid FeatureId { get; set; }
        public string value { get; set; }
    }
}
