using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<OperationClaim>> GetClaims(User user);
        IDataResult<List<User>> GetAll();
        void Add(User user);
        User GetByMail(string email);
    }
   
}
