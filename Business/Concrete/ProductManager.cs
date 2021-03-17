using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.CCS;
using Business.Constans;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;
        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;

        }
        // loglama = yapılan metoddaki işlemleri kaydetme yöntemi
        [SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")] // ilkte database den getiriir veriyi ikincide ise eski cache deki veriyi getiriri
        public IResult Add(Product product)
        {
            //business codes  -->iş ihtiyaçlarına uygunluk, meselaehliyet vermek için gerekli ölçütlere sahip mi
            // validation codes --> doğrulama kısmı isim şu kadar olmalı vs vs min max karakter.(Yapısı ile ilgili olnlar bura)

            IResult result = BusinessRules.Run(/*CheckIfProductNameExists(product.ProductName),
                CheckIfProductCountOfCategoryCorrect(product.CategoryId), CheckIfCategoryNumberMuch()*/);
            if (result != null)
            {
                return result;
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);

        }

       // [PerformanceAspect(5)]
      //  [CacheAspect]
        public IDataResult<List<Product>> GetAll()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
        }
        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }

        [CacheAspect]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {
            if (CheckIfProductCountOfCategoryCorrect(product.CategoryId).Success)
            {
                _productDal.Update(product);
                return new SuccessResult(Messages.ProductUpdated);
            }
            return new ErrorResult();
        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 100)
            {
                return new ErrorResult(Messages.ProductCountOfCategoyError);

            }
            return new SuccessResult();
        }

        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExist);
            }
            return new SuccessResult();
        }
        private IResult CheckIfCategoryNumberMuch()
        {

            var result = _categoryService.GetAll().Data.Count;
            if (result > 15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }

        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Product product)
        {

            Add(product);
            if (product.UnitPrice < 10)
            {
                throw new Exception("");
            }

            Add(product);
            return null;
        }
    }
}
