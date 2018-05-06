using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ImageBotBuilderBotFramework.RexExpRecognozerMiddleware
{
  public class RegExLocaleMap
  {
    private Dictionary<string, List<Regex>> _map = new Dictionary<string, List<Regex>>();
    private const string Default_Key = "*";

    public RegExLocaleMap()
    {
    }

    public RegExLocaleMap(List<Regex> items)
    {
      _map[Default_Key] = items;
    }

    public List<Regex> GetLocale(string locale)
    {
      if (_map.ContainsKey(locale))
        return _map[locale];
      else if (_map.ContainsKey(Default_Key))
        return _map[Default_Key];
      else
        return new List<Regex>();
    }

    public Dictionary<string, List<Regex>> Map
    {
      get { return _map; }
    }

  }
}