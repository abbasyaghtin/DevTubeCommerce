using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTubeCommerce.Application.Contract.Dto.Catalog
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public List<Guid> FeatureIds { get; set; }
    }
}
