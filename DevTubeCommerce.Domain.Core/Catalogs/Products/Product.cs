using DevTubeCommerce.Domain.Core.Base;
using DevTubeCommerce.Domain.Core.Shared;
using DevTubeCommerce.Framework.Exceptions;
using DevTubeCommerce.Framework.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTubeCommerce.Domain.Core.Catalogs.Products
{
    public class Product : AggregateRoot<ProductId>
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Code { get; private set; }
        public double Price { get; private set; }
        private readonly List<ProductFeatureValue> _productFeatureValues = new List<ProductFeatureValue>();
        public IReadOnlyList<ProductFeatureValue> ProductFeatureValues => _productFeatureValues;

        public static Product CreateNew(string title, string description, string code, double price, List<ProductFeatureValueData> productFeatures)
        {
            var productId = new ProductId(Guid.NewGuid());
            return new Product(productId, title, description, code, price, productFeatures);
        }

        public void Update(ProductId id, string title, string description, string code, double price,
                    List<ProductFeatureValueData> oldProductFeatures, List<ProductFeatureValueData> currentProductFeatures)
        {
            if (price < 0) throw new BusinessRuleException(Error.InvalidPrice);
            Id = id;
            Title = title;
            Code = code;
            Description = description;
            Price = price;
            if (oldProductFeatures.Count > 0)
                RemoveFeatures(oldProductFeatures);

            BuildFeatures(currentProductFeatures);
        }

        private void BuildFeatures(List<ProductFeatureValueData> featureData)
        {
            featureData.ForEach(feature =>
            {
                var newFeature = ProductFeatureValue.CreateNew(Id, feature.FeatureId, feature.Value);
                _productFeatureValues.Add(newFeature);
            });
        }
        private void RemoveFeatures(List<ProductFeatureValueData> featureData)
        {
            var productFeatureValues = _productFeatureValues.Where(f => featureData.Any(x=>x.FeatureId==f.FeatureId)).ToList();
            productFeatureValues.ForEach(productFeature =>
            {
                _productFeatureValues.Remove(productFeature);
            });
        }

        private Product(ProductId id,string title, string description, string code, double price, List<ProductFeatureValueData> productFeatures)
        {
            if (price < 0) throw new BusinessRuleException(Error.InvalidPrice);
            Id = id;
            Title = title;
            Code = code;
            Description = description;
            Price = price;
            BuildFeatures(productFeatures);
        }

        private Product(){}
    }
}
