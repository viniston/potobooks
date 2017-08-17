using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Photo.Business.Entities.Model;

namespace Task
{
	/// <summary>
	/// Summary description for ModuleCategoryTask
	/// </summary>
	public static class UtilityTask
	{
		public static string GetArtistFare(int artistID)
        {
            return JsonConvert.SerializeObject(ArtistFareController.Instance.All.FindAll(item => item.ArtistID == artistID).Select(i =>
                                                new {
                                                    Name = i.Product.Name,
                                                    Cost = i.Cost
                                                }).ToList(), Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
	}
}