using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTubeCommerce.Application.Contract.Dto.Product
{
    public class ProductFeatureDto
    {
        public Guid FeatureId { get; set; }
        public string Value { get; set; }
    }
}
