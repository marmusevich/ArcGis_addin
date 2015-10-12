//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.18408
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WorckWithCadastr_V6 {
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
                return "WorckWithCadastr_V6";
            }
        }
        
        internal static string AddInID {
            get {
                return "{791197f8-be70-496d-a82c-5c3486208df8}";
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
                return "Работа с адресным кадастром V6";
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
            /// Returns 'TKC_arcBtn_Open_Rej_Bud_Adr', the id declared for Add-in Button class 'arcBtn_Open_Rej_Bud_Adr'
            /// </summary>
            internal static string arcBtn_Open_Rej_Bud_Adr {
                get {
                    return "TKC_arcBtn_Open_Rej_Bud_Adr";
                }
            }
            
            /// <summary>
            /// Returns 'TKC_arcBtn_Open_Rej_Vul', the id declared for Add-in Button class 'arcBtn_Open_Rej_Vul'
            /// </summary>
            internal static string arcBtn_Open_Rej_Vul {
                get {
                    return "TKC_arcBtn_Open_Rej_Vul";
                }
            }
            
            /// <summary>
            /// Returns 'TKC_arcBtn_Open_Rej_Adr_Poh', the id declared for Add-in Button class 'arcBtn_Open_Rej_Adr_Poh'
            /// </summary>
            internal static string arcBtn_Open_Rej_Adr_Poh {
                get {
                    return "TKC_arcBtn_Open_Rej_Adr_Poh";
                }
            }
            
            /// <summary>
            /// Returns 'TKC_arcBtn_Open_Rej_Adr_Osnov', the id declared for Add-in Button class 'arcBtn_Open_Rej_Adr_Osnov'
            /// </summary>
            internal static string arcBtn_Open_Rej_Adr_Osnov {
                get {
                    return "TKC_arcBtn_Open_Rej_Adr_Osnov";
                }
            }
            
            /// <summary>
            /// Returns 'TKC_arcBtn_Open_Rej_Adm_Raj_Mis', the id declared for Add-in Button class 'arcBtn_Open_Rej_Adm_Raj_Mis'
            /// </summary>
            internal static string arcBtn_Open_Rej_Adm_Raj_Mis {
                get {
                    return "TKC_arcBtn_Open_Rej_Adm_Raj_Mis";
                }
            }
            
            /// <summary>
            /// Returns 'TKC_arcBtn_Open_Grm_Bdl', the id declared for Add-in Button class 'arcBtn_Open_Grm_Bdl'
            /// </summary>
            internal static string arcBtn_Open_Grm_Bdl {
                get {
                    return "TKC_arcBtn_Open_Grm_Bdl";
                }
            }
            
            /// <summary>
            /// Returns 'TKC_arcBtn_Open_Kvt', the id declared for Add-in Button class 'arcBtn_Open_Kvt'
            /// </summary>
            internal static string arcBtn_Open_Kvt {
                get {
                    return "TKC_arcBtn_Open_Kvt";
                }
            }
            
            /// <summary>
            /// Returns 'TKC_arcBtn_Open_Obj_Scl_Sft_Ecn', the id declared for Add-in Button class 'arcBtn_Open_Obj_Scl_Sft_Ecn'
            /// </summary>
            internal static string arcBtn_Open_Obj_Scl_Sft_Ecn {
                get {
                    return "TKC_arcBtn_Open_Obj_Scl_Sft_Ecn";
                }
            }
            
            /// <summary>
            /// Returns 'TKC_arcBtn_Open_Vrb_Bdl_Spr', the id declared for Add-in Button class 'arcBtn_Open_Vrb_Bdl_Spr'
            /// </summary>
            internal static string arcBtn_Open_Vrb_Bdl_Spr {
                get {
                    return "TKC_arcBtn_Open_Vrb_Bdl_Spr";
                }
            }
            
            /// <summary>
            /// Returns 'TKC_arcBtn_Open_Ztl_Bdn', the id declared for Add-in Button class 'arcBtn_Open_Ztl_Bdn'
            /// </summary>
            internal static string arcBtn_Open_Ztl_Bdn {
                get {
                    return "TKC_arcBtn_Open_Ztl_Bdn";
                }
            }
            
            /// <summary>
            /// Returns 'TKC_arcTool_ShowObjInfa', the id declared for Add-in Tool class 'arcTool_ShowObjInfa'
            /// </summary>
            internal static string arcTool_ShowObjInfa {
                get {
                    return "TKC_arcTool_ShowObjInfa";
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
