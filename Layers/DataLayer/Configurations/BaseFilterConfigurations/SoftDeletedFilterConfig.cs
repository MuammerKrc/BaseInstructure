using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.Models.BaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Configurations.BaseFilterConfigurations
{
    public abstract  class SoftDeletedFilterConfig<TModel>:IEntityTypeConfiguration<TModel> where TModel:BaseQueryModel
    {
        public virtual void Configure(EntityTypeBuilder<TModel> builder)
        {
            builder.HasQueryFilter(i => !i.IsDeleted);
        }
    }
}
