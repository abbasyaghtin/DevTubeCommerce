using DevTubeCommerce.Domain.Core.Base;
using DevTubeCommerce.Domain.Core.Catalogs.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTubeCommerce.Domain.Core.Catalogs.Categories
{
    public class Category : AggregateRoot<CategoryId>
    {
        public string CategoryName { get; private set; }
        public bool IsActive { get; private set; }
        public string Description { get; private set; }

        private readonly List<CategoryFeature> _categoryFeatures = new List<CategoryFeature>();
        public IReadOnlyList<CategoryFeature> CategoryFeatures => _categoryFeatures;


        public static Category CreateNew(string categoryName, bool isActive, string desscription, List<FeatureId> features)
        {
            var categoryId = new CategoryId(Guid.NewGuid());
            return new Category(categoryId, categoryName, isActive, desscription, features);
        }

        private void BuildFeatures(List<FeatureId> featureData)
        {
            featureData.ForEach(featureId =>
            {
                var newFeature = CategoryFeature.CreateNew(Id, featureId);
                _categoryFeatures.Add(newFeature);
            });
        }

        private void RemoveFeatures(List<FeatureId> featureData)
        {
            var categoryFeatures = _categoryFeatures.Where(x => featureData.Contains(x.FeatureId)).ToList();
            categoryFeatures.ForEach(categoryFeature =>
            {
                _categoryFeatures.Remove(categoryFeature);
            });
        }

        private Category(CategoryId id, string categoryName, bool isActive, string desscription, List<FeatureId> features)
        {
            //validation....
            Id = id;
            CategoryName = categoryName;
            IsActive = isActive;
            Description = desscription;
            BuildFeatures(features);
        }

        public void Update(CategoryId id, string categoryName, bool isActive, string desscription, List<FeatureId> oldFeatureIds, List<FeatureId> CurrentfeatureIds)
        {
            //validation
            Id = id;
            CategoryName = categoryName;
            IsActive = isActive;
            Description = desscription;
            if (oldFeatureIds.Count > 0)
                RemoveFeatures(oldFeatureIds);

            BuildFeatures(CurrentfeatureIds);
        }

        private Category()
        {
        }
    }
}
