using Lost.Model;
using System.Reflection;

namespace Lost.SharedLib.Utils.Assembly
{
    public static class ViewModelToEntity
    {
        public static U FillEntity<W, U>(W viewModel)
            where W : BaseViewModel
            where U : IdEntity, new()
        {
            U entity = new U();
            if (viewModel != null)
            {
                foreach (PropertyInfo viewModelPropertyInfo in typeof(W).GetProperties())
                {
                    PropertyInfo entityPropertyInfo = typeof(U).GetProperty(viewModelPropertyInfo.Name);
                    if (entityPropertyInfo != null)
                    {
                        entityPropertyInfo.SetValue(entity, viewModelPropertyInfo.GetValue(viewModel));
                    }
                }
            }

            return entity;
        }
    }
}
