using Coffee.Domain.Models.User;
using Coffee.Domain.ValueObjects;
using System.Linq.Expressions;

namespace Coffee.Domain.Queries;

public static class UserQueries
{
    public static Expression<Func<User, bool>> GetByEmail(Email email)
    {
        return x => x.Email.Address == email.Address;
    }

    public static Expression<Func<User, bool>> GetById(Guid id)
    {
        return x => x.Id == id;
    }


    public static Expression<Func<User, bool>> ExistsEmail(Email email)
    {
        return x => x.Email.Address == email.Address;
    }

    public static Expression<Func<User, bool>> GetByLink(string link)
    {
        return x => x.Link == link;
    }

}