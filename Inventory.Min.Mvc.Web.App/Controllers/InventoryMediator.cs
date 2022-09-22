using Inventory.Min.Mvc.Web.App.Models;

namespace Inventory.Min.Mvc.Web.App.Controllers;

public class InventoryMediator : IMediator
{
    public async Task<ItemsFullVM> Items(IApiClient api)
    {
        var client = api.GetClinet();
        var fullModel = new ItemsFullVM();
        fullModel.Items = await api.GetItemsAsync(client);
        fullModel.Categories = await api.GetCategoriesAsync(client);
        fullModel.Currencies = await api.GetCurrenciesAsync(client);
        fullModel.States = await api.GetStatesAsync(client);
        fullModel.Tags = await api.GetTagsAsync(client);
        fullModel.Units = await api.GetUnitsAsync(client);
        return fullModel;
    }

    public async Task<RelatedItemsVM> Related(IApiClient api, int? parentId)
    {
        var client = api.GetClinet();
        var model = new RelatedItemsVM();
        model.Items = await api.GetRelatedItemsAsync(client, parentId);
        model.Lexicon = await api.GetLexicinsAsync(client);
        return model;
    }
}