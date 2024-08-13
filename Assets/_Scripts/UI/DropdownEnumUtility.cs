using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public static class DropdownEnumUtility
{
    public static void SetupDropdown<T>(TMP_Dropdown dropdown, T defaultValue = default) where T : Enum
    {
        List<string> enumNames = Enum.GetNames(typeof(T)).ToList();
        dropdown.ClearOptions();
        dropdown.AddOptions(enumNames);
        dropdown.value = Convert.ToInt32(defaultValue);
    }

    public static T GetSelectedEnumValue<T>(TMP_Dropdown dropdown) where T : Enum
    {
        return (T)Enum.ToObject(typeof(T), dropdown.value);
    }
}
