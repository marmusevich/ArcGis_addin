//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.18408
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WorckWithReestr {
    using ESRI.ArcGIS.Framework;
    using ESRI.ArcGIS.ArcMapUI;
    using System;
    using System.Collections.Generic;
    using ESRI.ArcGIS.Desktop.AddIns;
    
    
    /// <summary>
    /// A class for looking up declarative information in the associated configuration xml file (.esriaddinx).
    /// </summary>
    internal static class ThisAddIn {
        
        internal static string Name {
            get {
                return "WorckWithReestr";
            }
        }
        
        internal static string AddInID {
            get {
                return "{a3901e4d-bd63-4196-a8ae-2624c565cdae}";
            }
        }
        
        internal static string Company {
            get {
                return "TKC http://itservis.od.ua ";
            }
        }
        
        internal static string Version {
            get {
                return "15.10.12.0850";
            }
        }
        
        internal static string Description {
            get {
                return "Работа с реестром";
            }
        }
        
        internal static string Author {
            get {
                return "Владимиров С. К., Мармусевич А.В.";
            }
        }
        
        internal static string Date {
            get {
                return "12.10.2015";
            }
        }
        
        internal static ESRI.ArcGIS.esriSystem.UID ToUID(this System.String id) {
            ESRI.ArcGIS.esriSystem.UID uid = new ESRI.ArcGIS.esriSystem.UIDClass();
            uid.Value = id;
            return uid;
        }
        
        /// <summary>
        /// A class for looking up Add-in id strings declared in the associated configuration xml file (.esriaddinx).
        /// </summary>
        internal class IDs {
            
            /// <summary>
            /// Returns 'TKC_WorckWithReestr_arcBtn_Open_FizLicList', the id declared for Add-in Button class 'arcBtn_Open_FizLicList'
            /// </summary>
            internal static string arcBtn_Open_FizLicList {
                get {
                    return "TKC_WorckWithReestr_arcBtn_Open_FizLicList";
                }
            }
            
            /// <summary>
            /// Returns 'TKC_WorckWithReestr_arcBtn_Open_JurLicList', the id declared for Add-in Button class 'arcBtn_Open_JurLicList'
            /// </summary>
            internal static string arcBtn_Open_JurLicList {
                get {
                    return "TKC_WorckWithReestr_arcBtn_Open_JurLicList";
                }
            }
            
            /// <summary>
            /// Returns 'TKC_WorckWithReestr_arcBtn_Open_TipDoc', the id declared for Add-in Button class 'arcBtn_Open_TipDoc'
            /// </summary>
            internal static string arcBtn_Open_TipDoc {
                get {
                    return "TKC_WorckWithReestr_arcBtn_Open_TipDoc";
                }
            }
            
            /// <summary>
            /// Returns 'TKC_WorckWithReestr_arcBtn_Open_ReestrZayav', the id declared for Add-in Button class 'arcBtn_Open_ReestrZayav'
            /// </summary>
            internal static string arcBtn_Open_ReestrZayav {
                get {
                    return "TKC_WorckWithReestr_arcBtn_Open_ReestrZayav";
                }
            }
            
            /// <summary>
            /// Returns 'TKC_WorckWithReestr_arcBtn_Open_ReestrVedomostey', the id declared for Add-in Button class 'arcBtn_Open_ReestrVedomostey'
            /// </summary>
            internal static string arcBtn_Open_ReestrVedomostey {
                get {
                    return "TKC_WorckWithReestr_arcBtn_Open_ReestrVedomostey";
                }
            }
        }
    }
    
internal static class ArcMap
{
  private static IApplication s_app = null;
  private static IDocumentEvents_Event s_docEvent;

  public static IApplication Application
  {
    get
    {
      if (s_app == null)
        s_app = Internal.AddInStartupObject.GetHook<IMxApplication>() as IApplication;

      return s_app;
    }
  }

  public static IMxDocument Document
  {
    get
    {
      if (Application != null)
        return Application.Document as IMxDocument;

      return null;
    }
  }
  public static IMxApplication ThisApplication
  {
    get { return Application as IMxApplication; }
  }
  public static IDockableWindowManager DockableWindowManager
  {
    get { return Application as IDockableWindowManager; }
  }
  public static IDocumentEvents_Event Events
  {
    get
    {
      s_docEvent = Document as IDocumentEvents_Event;
      return s_docEvent;
    }
  }
}

namespace Internal
{
  [StartupObjectAttribute()]
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
  public sealed partial class AddInStartupObject : AddInEntryPoint
  {
    private static AddInStartupObject _sAddInHostManager;
    private List<object> m_addinHooks = null;

    [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
    public AddInStartupObject()
    {
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override bool Initialize(object hook)
    {
      bool createSingleton = _sAddInHostManager == null;
      if (createSingleton)
      {
        _sAddInHostManager = this;
        m_addinHooks = new List<object>();
        m_addinHooks.Add(hook);
      }
      else if (!_sAddInHostManager.m_addinHooks.Contains(hook))
        _sAddInHostManager.m_addinHooks.Add(hook);

      return createSingleton;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override void Shutdown()
    {
      _sAddInHostManager = null;
      m_addinHooks = null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
    internal static T GetHook<T>() where T : class
    {
      if (_sAddInHostManager != null)
      {
        foreach (object o in _sAddInHostManager.m_addinHooks)
        {
          if (o is T)
            return o as T;
        }
      }

      return null;
    }

    // Expose this instance of Add-in class externally
    public static AddInStartupObject GetThis()
    {
      return _sAddInHostManager;
    }
  }
}
}
