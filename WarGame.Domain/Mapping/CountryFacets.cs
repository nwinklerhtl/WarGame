using Facet;
using WarGame.Model.Configuration;
using WarGame.Model.Models;

namespace WarGame.Domain.Mapping;

[Facet(typeof(Country), 
    Include = [
        nameof(Country.Id), 
        nameof(Country.Name), 
        nameof(Country.Continent)])]
public partial record CountryListDto : IHasId;

[Facet(typeof(Country), 
    Include = [
        nameof(Country.Id), 
        nameof(Country.Name), 
        nameof(Country.Continent)])]
public partial record CountryDetailDto;

[Facet(typeof(Country), 
    exclude: [
        nameof(Country.Factions),
        nameof(Country.Tanks)])]
public partial record CountryCreateDto;