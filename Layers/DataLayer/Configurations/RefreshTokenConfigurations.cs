using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.Models.JwtModels;
using DataLayer.Configurations.BaseFilterConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Configurations
{
    public class RefreshTokenConfigurations:SoftDeletedFilterConfig<UserRefreshToken>
    {
        public override void Configure(EntityTypeBuilder<UserRefreshToken> builder)
        {
            base.Configure(builder);
            builder.Property(i => i.RefreshToken).IsRequired();
        }
    }
}
