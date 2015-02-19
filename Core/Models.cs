using OrigoDB.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeBasicOrigoDbApp.Core
{
	[Serializable]
	public class Models : Model
	{
		private IDictionary<Type, IList<IIdentifiableByNumber>> _objects = new Dictionary<Type, IList<IIdentifiableByNumber>>();
		public void Save(object obj) 
		{
			var t = obj.GetType();
            if (!_objects.ContainsKey(t))
			{
				_objects[t] = new List<IIdentifiableByNumber>();
			}
			_objects[t].Add((IIdentifiableByNumber)obj);
		}

		public IEnumerable<T> QueryOver<T>()
		{
			return _objects[typeof(T)].Cast<T>();
		}

		public T Get<T>(int v) where T : IIdentifiableByNumber
		{
			return QueryOver<T>().SingleOrDefault(t => t.Id == v);
		}
	}

	[Serializable]
	public class AddCommand : Command<Models> 
	{
		public object Object { get; set; }

		public override void Execute(Models model)
		{
			model.Save(Object);
		}
	}

	[Serializable]
	public class Get<T> : Query<Models, T> where T : IIdentifiableByNumber
	{
		public int Id { get; set; }

		public override T Execute(Models model) 
		{
			return model.Get<T>(Id);
		}
	}

}
