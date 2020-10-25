using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithFilterForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFilterForCountSpecification(ProductSpecParams productParams)
         : base( x=> 
        (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains
        (productParams.Search)) &&
        (!productParams.brandId.HasValue || x.ProductBandId == productParams.brandId) && 
        (!productParams.typeId.HasValue || x.ProductTypeId == productParams.typeId) 
        )
        {
            
        }
    }
}