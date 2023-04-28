﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace N5Now.Common.Resources {
    using System;
    
    
    /// <summary>
    ///   Clase de recurso fuertemente tipado, para buscar cadenas traducidas, etc.
    /// </summary>
    // StronglyTypedResourceBuilder generó automáticamente esta clase
    // a través de una herramienta como ResGen o Visual Studio.
    // Para agregar o quitar un miembro, edite el archivo .ResX y, a continuación, vuelva a ejecutar ResGen
    // con la opción /str o recompile su proyecto de VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class GeneralMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal GeneralMessages() {
        }
        
        /// <summary>
        ///   Devuelve la instancia de ResourceManager almacenada en caché utilizada por esta clase.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("N5Now.Common.Resources.GeneralMessages", typeof(GeneralMessages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Reemplaza la propiedad CurrentUICulture del subproceso actual para todas las
        ///   búsquedas de recursos mediante esta clase de recurso fuertemente tipado.
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
        ///   Busca una cadena traducida similar a Usuario no autenticado correctamente.
        /// </summary>
        public static string Error401 {
            get {
                return ResourceManager.GetString("Error401", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Ha ocurrido un error interno, por favor vuelva a intentarlo..
        /// </summary>
        public static string Error500 {
            get {
                return ResourceManager.GetString("Error500", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Ha ocurrido un error interno..
        /// </summary>
        public static string Error5001 {
            get {
                return ResourceManager.GetString("Error5001", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Registro eliminado satisfactoriamente..
        /// </summary>
        public static string ItemDeleted {
            get {
                return ResourceManager.GetString("ItemDeleted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Registro guardado satisfactoriamente..
        /// </summary>
        public static string ItemInserted {
            get {
                return ResourceManager.GetString("ItemInserted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a No fue posible eliminar el registro, por favor intentalo de nuevo..
        /// </summary>
        public static string ItemNoDeleted {
            get {
                return ResourceManager.GetString("ItemNoDeleted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Item no encontrado, por favor verificar los registros enviados..
        /// </summary>
        public static string ItemNoFound {
            get {
                return ResourceManager.GetString("ItemNoFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a No fue posible guardar el registro, por favor intentalo de nuevo..
        /// </summary>
        public static string ItemNoInserted {
            get {
                return ResourceManager.GetString("ItemNoInserted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a No fue posible actualizar el registro, por favor intentalo de nuevo..
        /// </summary>
        public static string ItemNoUpdated {
            get {
                return ResourceManager.GetString("ItemNoUpdated", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Registro actualizado satisfactoriamente..
        /// </summary>
        public static string ItemUpdated {
            get {
                return ResourceManager.GetString("ItemUpdated", resourceCulture);
            }
        }
    }
}