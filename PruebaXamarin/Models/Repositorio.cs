﻿using Android.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PruebaXamarin.Models
{
    public class Repositorio
    {
        SQLite.SQLiteAsyncConnection DataBase;
        public Repositorio()
        {
            string DbFilePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "localdb.db3");
            DataBase = new SQLite.SQLiteAsyncConnection(DbFilePath);
            DataBase.CreateTableAsync<Logs>().Wait();
        }

        #region API
        public async Task<Productos> GetProductosByIdAsync(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetAsync("https://pruebaxamarinapi.azurewebsites.net/api/productos/" + id);
                var jsonstring = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Productos>(jsonstring);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ObservableCollection<Productos>> GetProductosAsync()
        {
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetAsync("https://pruebaxamarinapi.azurewebsites.net/api/productos");


                var jsonstring = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ObservableCollection<Productos>>(jsonstring);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HttpStatusCode> UpdateProducto(int id, Productos producto)
        {
            try
            {
                HttpClient client = new HttpClient();
                var _producto = JsonConvert.SerializeObject(producto);
                var response = await client.PutAsync($"https://pruebaxamarinapi.azurewebsites.net/api/productos/{id}", new StringContent(_producto, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    return HttpStatusCode.OK;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return default;
        }

        public async Task<HttpStatusCode> DeleteProductoAsync(int? id)
        {
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.DeleteAsync($"https://pruebaxamarinapi.azurewebsites.net/api/productos/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return HttpStatusCode.OK;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return default;
        }

        public async Task<HttpStatusCode> CreateProductoAsync(Productos producto)
        {
            try
            {
                HttpClient client = new HttpClient();
                var _producto = JsonConvert.SerializeObject(producto, new JsonSerializerSettings() { ContractResolver = new IgnorePropertiesResolver(new[] { "Id" }) });
                var response = await client.PostAsync("https://pruebaxamarinapi.azurewebsites.net/api/productos", new StringContent(_producto, System.Text.Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    return HttpStatusCode.OK;
                }

            }
            catch (Exception)
            {

                throw;
            }
            return default;
        }
        #endregion

        #region SQLite
        public Task<int> CreateLogAsync(Logs log)
        {
            return DataBase.InsertAsync(log);
        }

        public Task<List<Logs>> GetLogsAsync()
        {
            return DataBase.Table<Logs>().ToListAsync();
        }

        public Task<int> UpdateLogAsync(Logs log)
        {
            return DataBase.UpdateAsync(log);
        }

        public Task<int> DeleteLogAsync(Logs log)
        {
            return DataBase.DeleteAsync(log);
        }

        public Task<Logs> GetLogByIdAsync(int id)
        {
            return DataBase.Table<Logs>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }
        #endregion

    }
    public class IgnorePropertiesResolver : DefaultContractResolver
    {
        private IEnumerable<string> _propsToIgnore;
        public IgnorePropertiesResolver(IEnumerable<string> propNamesToIgnore)
        {
            _propsToIgnore = propNamesToIgnore;
        }
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);
            property.ShouldSerialize = (x) => { return !_propsToIgnore.Contains(property.PropertyName); };
            return property;
        }
    }
}
