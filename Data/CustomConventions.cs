using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;

namespace Data
{
	public class LowerCaseColumnConvention : IStoreModelConvention<EdmProperty>
	{
		public void Apply(EdmProperty property, DbModel model)
		{
			property.Name = property.Name.ToLower();
		}
	}
}