using DevTubeCommerce.Domain.Core.Base;
using DevTubeCommerce.Framework.Exceptions;
using DevTubeCommerce.Framework.Global;

namespace DevTubeCommerce.Domain.Core.Catalogs.Features
{
    public class Feature : AggregateRoot<FeatureId>
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int SortOrder { get; set; }

        public static Feature CreateNew(string title, string description, int sortOrder)
        {
            if (string.IsNullOrEmpty(title))
                throw new InvalidStateException(Error.InvalidTitle);

            if (string.IsNullOrEmpty(description))
                throw new InvalidStateException(Error.InvalidDescription);

            if (sortOrder <= 0)
                throw new InvalidStateException(Error.InvalidSortOrder);
            var featureId = new FeatureId(Guid.NewGuid());
            return new Feature(featureId, title, description, sortOrder);
        }

        public static Feature CreateNewForUpdate(Guid id, string title, string description, int sortOrder)
        {
            var featureId = new FeatureId(id);
            return new Feature(featureId, title, description, sortOrder);
        }

        public static Feature CreateNewForDelete(FeatureId id)
        {
            return new Feature(id);
        }

        public void Update(Feature newValue)
        {
            Title = newValue.Title;
            Description = newValue.Description;
            SortOrder = newValue.SortOrder;
        }
        public void Update(string title, string description, int sortOrder)
        {
            if (string.IsNullOrEmpty(title))
                throw new InvalidStateException(Error.InvalidTitle);

            if (string.IsNullOrEmpty(description))
                throw new InvalidStateException(Error.InvalidDescription);

            if (sortOrder <= 0)
                throw new InvalidStateException(Error.InvalidSortOrder);

            Title = title;
            Description = description;
            SortOrder = sortOrder;
        }

        private Feature(FeatureId id, string title, string description, int sortOrder)
        {
            //validation
            Id = id;
            Title = title;
            Description = description;
            SortOrder = sortOrder;
        }
        private Feature(FeatureId featureId)
        {
            Id = featureId;
        }

        private Feature() { }
    }
}
