using System;
using System.Collections.Generic;
using System.Text;

namespace Moxiecode.TinyMCE
{
    public class TinyMCEConfig
    {
        #region varible
        private string _theme = "advanced";
        private string _plugins = "autolink,lists,pagebreak,style,table,save,advhr,advimage,advlink,emotions,inlinepopups,insertdatetime,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template,preview";
        private string _theme_advanced_buttons1 = "bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,undo,redo,cleanup,|,bullist,numlist,|,link,unlink,|,template,removeformat";
        private string _theme_advanced_buttons2 = "cut,copy,paste,pastetext,pasteword,|,search,replace,|,outdent,indent,blockquote,|,forecolor,backcolor,|,styleselect,preview";
        private string _theme_advanced_buttons3 = "tablecontrols,|,hr,removeformat,visualaid,|,sub,sup,|,advhr,charmap,image,media";
        private string _theme_advanced_buttons4 = "styleprops,|,cite,abbr,acronym,del,ins,attribs,|,visualchars,nonbreaking,pagebreak,|,insertdate,inserttime,|,ltr,rtl,|,print,fullscreen,code";
        private string _theme_advanced_toolbar_location = "top";
        private string _theme_advanced_toolbar_align = "left";
        private string _theme_advanced_path_location = "bottom";
        private string _theme_advanced_resizing = "true";
        private string uploadImage = "true";
        private string uploadFile = "true";

        #endregion  

        #region properties

        public string theme
        {
            get { return _theme; }
            set { _theme = value; }
        }

        public string plugins
        {
            set { _plugins = value; }
            get { return _plugins; }
        }

        public string theme_advanced_buttons1
        {
            set { _theme_advanced_buttons1 = value; }
            get { return _theme_advanced_buttons1; }
        }

        public string theme_advanced_buttons2
        {
            set { _theme_advanced_buttons2 = value; }
            get { return _theme_advanced_buttons2; }
        }

        public string theme_advanced_buttons3
        {
            set { _theme_advanced_buttons3 = value; }
            get { return _theme_advanced_buttons3; }
        }

        public string theme_advanced_buttons4
        {
            set { _theme_advanced_buttons4 = value; }
            get { return _theme_advanced_buttons4; }
        }

        public string theme_advanced_toolbar_location
        {
            set { _theme_advanced_toolbar_location = value; }
            get { return _theme_advanced_toolbar_location; }
        }

        public string theme_advanced_toolbar_align
        {
            set { _theme_advanced_toolbar_align = value; }
            get { return _theme_advanced_toolbar_align; }
        }

        public string theme_advanced_path_location
        {
            set { _theme_advanced_path_location = value; }
            get { return _theme_advanced_path_location; }
        }

        public string theme_advanced_resizing
        {
            set { _theme_advanced_resizing = value; }
            get { return _theme_advanced_resizing; }
        }

        public string UploadImage
        {
            get { return this.uploadImage; }
            set { this.uploadImage = value; }
        }

        public string UploadFile
        {
            get { return this.uploadFile; }
            set { this.uploadFile = value; }
        }

        #endregion


        #region method

        public void ToolBarBasic()
        {
            this._theme_advanced_buttons2 = string.Empty;
            this._theme_advanced_buttons3 = string.Empty;
            this._theme_advanced_buttons4 = string.Empty;
        }
        public void ToolBarBasic1()
        {
            this._theme_advanced_buttons2 = "forecolor,|,styleselect";
            this._theme_advanced_buttons3 = string.Empty;
            this._theme_advanced_buttons4 = string.Empty;
        }

        public void ToolBarDefault()
        {            
            this._theme_advanced_buttons3 = string.Empty;
            this._theme_advanced_buttons4 = string.Empty;
        }

        public void ToolBarFull()
        {                       
        }

        #endregion
    }
}