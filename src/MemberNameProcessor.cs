using System.Text;

namespace LgkProductions.Inspector;

public static class MemberNameProcessor
{
    public static string ProcessMemberName(string memberName)
    {
        var chars = memberName.Trim().ToCharArray().ToList();
        if (chars.Contains('_') || chars.All(char.IsLower))
            return ProcessMemberName(chars, NamingConvention.SnakeCase);
        return ProcessMemberName(chars, NamingConvention.PascalCase);
        
    }

    public static string ProcessMemberName(List<char> memberName, NamingConvention convention)
    {
        var sb = new StringBuilder();
        var wordStart = false;
        sb.Append(char.ToUpper(memberName[0]));
        for (var i = 1; i < memberName.Count; i++)
        {
            switch (convention)
            {
                case NamingConvention.PascalCase:
                    if (char.IsUpper(memberName[i]))
                        sb.Append(' ');
                    sb.Append(memberName[i]);
                    break;
                case NamingConvention.SnakeCase:
                    if (memberName[i] == '_')
                    {
                        sb.Append(' ');
                        wordStart = true;
                    }
                    else if (wordStart)
                    {
                        wordStart = false;
                        sb.Append(memberName[i]);
                    }
                    else
                    {
                        sb.Append(char.ToLower(memberName[i]));
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(convention), convention, null);
            }
        }
        return sb.ToString();
    }
}

public enum NamingConvention
{
    PascalCase,
    SnakeCase
}