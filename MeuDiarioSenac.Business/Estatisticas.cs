namespace MeuDiarioSenac.Business;

public record ContagemMes(string Mes, int Quantidade);
public record ContagemTag(string Tag, int Quantidade);
public record EstatisticasResponse(int Total, List<ContagemMes> PorMes, List<ContagemTag> PorTag);
