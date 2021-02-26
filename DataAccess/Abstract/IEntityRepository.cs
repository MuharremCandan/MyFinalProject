using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    // Generic constraint cenerik kısıtlama T yerine sadece Entity/ Concrete içindekinler verilerbilir
    // class anlamı referans tip demek sınıflara bölmek onları reeranslarıa parçalamak ve referanslar üzerinden işlem yapmak
    // new(): newlenebilir olması IEntity interface , İnterfaceler newlenemez
    public interface IEntityRepository<T> where T : class ,IEntity, new()
    {  
        List<T> GetAll(Expression<Func<T,bool>> filter = null);
        T Get(Expression<Func<T, bool>> filter );
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
