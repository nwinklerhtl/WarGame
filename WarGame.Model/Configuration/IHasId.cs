namespace WarGame.Model.Configuration;

public interface IHasId
{
    public int Id { get; set; }
}

public interface IHasIdGetter
{
    public int Id { get; }
}