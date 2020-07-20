using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface ISpecification<T>
    {
        //where clause
         Expression<Func<T, bool>> Criteria {get;}

        //This is trying to implement Includes function
        //Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Product, ProductBrand> System.Linq.IQueryable<Product>.Include<Product, ProductBrand>(System.Linq.Expressions.Expression<System.Func<Product, ProductBrand>> navigationPropertyPath)
        //T: product; object: ProductBrand
         List<Expression<Func<T,object>>> Includes {get;}
    }
}