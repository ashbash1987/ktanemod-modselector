using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;

public class FreeplayCommander
{
    #region Constructors
    static FreeplayCommander()
    {
        _freeplayDeviceType = ReflectionHelper.FindType("FreeplayDevice");
        _maxModuleField = _freeplayDeviceType.GetField("maxModBombModules", BindingFlags.NonPublic | BindingFlags.Instance);
        _MAXSECONDSFIELD = _freeplayDeviceType.GetField("MAX_SECONDS_TO_SOLVE", BindingFlags.Public | BindingFlags.Static);
    }

    public FreeplayCommander(MonoBehaviour freeplayDevice)
    {
        FreeplayDevice = freeplayDevice;
    }
    #endregion

    #region Public Helpers

    public void SetMaxModules(int max, bool multipleBombs)
    {
        if (multipleBombs)
        {
            _MAXSECONDSFIELD.SetValue(null, 600f + ((max - 1) * 60));
        }
        else
        {
            _MAXSECONDSFIELD.SetValue(null, 600f);
        }
        _maxModuleField.SetValue(FreeplayDevice, max);
    }
    #endregion

    #region Readonly Fields
    public readonly MonoBehaviour FreeplayDevice = null;
    #endregion

    #region Private Static Fields
    private static Type _freeplayDeviceType = null;
    private static FieldInfo _maxModuleField = null;
    private static FieldInfo _MAXSECONDSFIELD = null;
    #endregion
}
