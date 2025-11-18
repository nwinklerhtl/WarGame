using Facet;
using WarGame.Model.Models;

namespace WarGame.Domain.Mapping;

[Facet(typeof(Country), 
    Include = [
        nameof(Country.Id), 
        nameof(Country.Name), 
        nameof(Country.Continent)])]
public partial record CountryListDto;