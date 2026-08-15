using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.SeedData.Configurations
{
    public class DummyUserConfiguration : IEntityTypeConfiguration<DummyUser>
    {
        public void Configure(EntityTypeBuilder<DummyUser> builder)
        {
            builder.HasData(
                new DummyUser()
                {
                    Id = 1,
                    UserName = "Norbert Nikolaus"
                },
                new DummyUser()
                {
                    Id = 2,
                    UserName = "Jean Millet"
                },
                new DummyUser()
                {
                    Id = 3,
                    UserName = "August Auriel"
                },
                new DummyUser()
                {
                    Id = 4,
                    UserName = "Lucien Delacroix"
                },
                new DummyUser()
                {
                    Id = 5,
                    UserName = "Logged In User"
                }
            );
        }
    }
}
