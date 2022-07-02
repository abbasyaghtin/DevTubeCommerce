namespace DevTubeCommerce.API.Forms.Catalogs.Products
{
    public class UpdateProductForm
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public double Price { get; set; }
        public List<ProductFeatureForm> ProductFeatures { get; set; }
    }
}
