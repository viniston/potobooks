﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Photo.Resources.RegEx {
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
    public class RegEx {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal RegEx() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Photo.Resources.RegEx.RegEx", typeof(RegEx).Assembly);
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
        ///   Looks up a localized string similar to ^(?=.*[a-z,A-Z])(?=.*\d)[A-Za-z\d`~!@#$%^&amp;*()\-_=+{}[\]\\|;:,./?]{8,}$.
        /// </summary>
        public static string AllowedPasswordCharacters {
            get {
                return ResourceManager.GetString("AllowedPasswordCharacters", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;(.|\n)*?&gt;.
        /// </summary>
        public static string DecodedHtmlTags {
            get {
                return ResourceManager.GetString("DecodedHtmlTags", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &amp;lt;.*?&amp;gt;|&lt;.*?&gt;.
        /// </summary>
        public static string HTMLTags {
            get {
                return ResourceManager.GetString("HTMLTags", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [&lt;&gt;&apos;&quot;].
        /// </summary>
        public static string InvalidPlusPasswordCharacters {
            get {
                return ResourceManager.GetString("InvalidPlusPasswordCharacters", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to \n+.
        /// </summary>
        public static string LineBreaks {
            get {
                return ResourceManager.GetString("LineBreaks", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [^A-Za-z].
        /// </summary>
        public static string NonAlpha {
            get {
                return ResourceManager.GetString("NonAlpha", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [^\w-]+.
        /// </summary>
        public static string NonAlphaNumericCharacters {
            get {
                return ResourceManager.GetString("NonAlphaNumericCharacters", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [^A-Za-z0-9].
        /// </summary>
        public static string NonAlphanumerics {
            get {
                return ResourceManager.GetString("NonAlphanumerics", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [^\w\s,&apos;(\d)\*].
        /// </summary>
        public static string NonAlphanumericsWithProviderCode {
            get {
                return ResourceManager.GetString("NonAlphanumericsWithProviderCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [^A-Za-z0-9 \-_\/\\\:\,\.\(\)\r\n].
        /// </summary>
        public static string NonAlphanumericsWithSpecialCharacters {
            get {
                return ResourceManager.GetString("NonAlphanumericsWithSpecialCharacters", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [^A-Za-z ].
        /// </summary>
        public static string NonAlphaWithSpaces {
            get {
                return ResourceManager.GetString("NonAlphaWithSpaces", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [^0-9.].
        /// </summary>
        public static string NonNumericAndDecimal {
            get {
                return ResourceManager.GetString("NonNumericAndDecimal", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [^0-9].
        /// </summary>
        public static string NonNumericCharacter {
            get {
                return ResourceManager.GetString("NonNumericCharacter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [.]\d*.aspx.
        /// </summary>
        public static string ProductPropertyID {
            get {
                return ResourceManager.GetString("ProductPropertyID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to \s{2,}.
        /// </summary>
        public static string RedundantWhitespaces {
            get {
                return ResourceManager.GetString("RedundantWhitespaces", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to (\[.*\])|(&quot;.*&quot;)|(&apos;.*&apos;)|(\(.*\))|(\{.*\}).
        /// </summary>
        public static string TextEnclosures {
            get {
                return ResourceManager.GetString("TextEnclosures", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{1,}))@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])$.
        /// </summary>
        public static string ValidEmailAddress {
            get {
                return ResourceManager.GetString("ValidEmailAddress", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^((([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{1,}))@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9]))+([,;](((([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{1,}))@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9]))))*$.
        /// </summary>
        public static string ValidMultipleEmailAddresses {
            get {
                return ResourceManager.GetString("ValidMultipleEmailAddresses", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to \s{1,}.
        /// </summary>
        public static string Whitespace {
            get {
                return ResourceManager.GetString("Whitespace", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [^\w&apos;]+.
        /// </summary>
        public static string WordWithNoSpecialCharacterButApostrophe {
            get {
                return ResourceManager.GetString("WordWithNoSpecialCharacterButApostrophe", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^[0-9]+$.
        /// </summary>
        public static string WordWithOnlyNumericCharacter {
            get {
                return ResourceManager.GetString("WordWithOnlyNumericCharacter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^0+[.0]*$.
        /// </summary>
        public static string Zero {
            get {
                return ResourceManager.GetString("Zero", resourceCulture);
            }
        }
    }
}