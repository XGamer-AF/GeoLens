﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MasterWebApp.Resource {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class EventTypes {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal EventTypes() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MasterWebApp.Resource.EventTypes", typeof(EventTypes).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to م.
        /// </summary>
        public static string eventTypeID {
            get {
                return ResourceManager.GetString("eventTypeID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to اسم تصنيف الحدث ( عربي ).
        /// </summary>
        public static string eventTypeNameAr {
            get {
                return ResourceManager.GetString("eventTypeNameAr", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to اسم تصنيف الحدث ( انجليزي ).
        /// </summary>
        public static string eventTypeNameEn {
            get {
                return ResourceManager.GetString("eventTypeNameEn", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to بحث.
        /// </summary>
        public static string Search {
            get {
                return ResourceManager.GetString("Search", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to حالة السجل.
        /// </summary>
        public static string statusID {
            get {
                return ResourceManager.GetString("statusID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to تصنيف الحدث.
        /// </summary>
        public static string title {
            get {
                return ResourceManager.GetString("title", resourceCulture);
            }
        }
    }
}
