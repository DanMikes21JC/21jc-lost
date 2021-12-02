using Lost.Model;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Lost.SharedLib
{
    public static class EntityToViewModel
    {
        public static W FillViewModel<U, W>(U entity) 
            where U : IdEntity
            where W : BaseViewModel, new()
        {
            W viewModel = new W();
            if (entity != null)
            {
                foreach (PropertyInfo entityPropertyInfo in typeof(U).GetProperties())
                {
                    PropertyInfo viewModelPropertyInfo = typeof(W).GetProperty(entityPropertyInfo.Name);
                    if (viewModelPropertyInfo != null)
                    {
                        viewModelPropertyInfo.SetValue(viewModel, entityPropertyInfo.GetValue(entity));
                    }
                }
            }

            return viewModel;
        }

        public static List<W> FillViewModel<U, W>(List<U> entityList)
            where U : IdEntity
            where W : BaseViewModel, new()
        {
            List<W> resultList = new List<W>();
            foreach (U entity in entityList)
            {
                resultList.Add(FillViewModel<U, W>(entity));
            }

            return resultList;
        }

        public static W FillAnyViewModel<U, W>(U entity)
            where U : class
            where W : new()
            {
                W viewModel = new W();
                if (entity != null)
                {
                    foreach (PropertyInfo entityPropertyInfo in typeof(U).GetProperties())
                    {
                        PropertyInfo viewModelPropertyInfo = typeof(W).GetProperty(entityPropertyInfo.Name);
                        if (viewModelPropertyInfo != null)
                        {
                            viewModelPropertyInfo.SetValue(viewModel, entityPropertyInfo.GetValue(entity));
                        }
                    }
                }

                return viewModel;
        }

    }
}
