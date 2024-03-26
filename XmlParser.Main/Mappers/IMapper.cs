namespace XmlParser.Main.Mappers;

public interface IMapper<M, V>
{
    V MapToViewModel(M model);
    M MapToModel(V viewModel);
}
