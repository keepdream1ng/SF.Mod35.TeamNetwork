using Bogus;
using SF.Mod35.TeamNetwork.ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF.Mod35.TeamNetwork.Testing;

public class DataGenerator
{
    public Faker<User> UserFaker;

    public DataGenerator()
    {
        UserFaker = new Faker<User>()
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.DateOfBirth, f => f.Date.Past(70))
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.Status, f => f.Lorem.Sentence(5, 5))
            .RuleFor(u => u.About, f => f.Lorem.Sentences(3))
            .RuleFor(u => u.UserName, (f, u) => u.Email)
            .RuleFor(u => u.ImageUrl, (f, u) => $"https://i.pravatar.cc/200?u={u.Email}")
            ;
    }
}
