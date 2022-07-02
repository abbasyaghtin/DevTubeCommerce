﻿using DevTubeCommerce.Domain.Core.Base;
using DevTubeCommerce.Domain.Core.Catalogs.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTubeCommerce.Domain.Core.Catalogs.Categories
{
    public class CategoryFeature : Entity<Guid>
    {
        public CategoryId CategoryId { get; private set; }
        public FeatureId FeatureId { get; private set; }

        internal static CategoryFeature CreateNew(CategoryId categoryId, FeatureId featureId)
        {
            var id = Guid.NewGuid();
            return new CategoryFeature(id, categoryId, featureId);
        }

        private CategoryFeature(Guid id, CategoryId categoryId, FeatureId featureId)
        {
            Id = id;
            CategoryId = categoryId;
            FeatureId = featureId;
        }

        private CategoryFeature()
        {

        }
    }
}
