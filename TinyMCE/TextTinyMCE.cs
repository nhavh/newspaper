using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moxiecode.TinyMCE.Web;
using System.Web.UI.WebControls;

namespace Moxiecode.TinyMCE
{
    public class TextTinyMCE
    {
        TextArea textbox;
        public System.Web.UI.Control HTMLEditor
        {
            get { return textbox; }
        }

        public Unit Width
        {
            get{ return textbox.Width;}
            set{textbox.Width = value;}
        }

        public Unit Height
        {
            get { return textbox.Height; }
            set { textbox.Height = value; }
        }

        public string Text
        {
            get { return textbox.Value; }
            set{textbox.Value = value;}
        }

        public string ControlID
        {
            get{return textbox.ID;}
            set{textbox.ID=value;}
        }

        public bool UploadFile
        {
            set
            {
                if (value == true)
                {
                    textbox.Config.UploadFile = "true";
                }
                else
                    textbox.Config.UploadFile = "false";

            }
        }

        public bool UploadImage
        {
            set {
                if (value == true)
                {
                    textbox.Config.UploadImage = "true";
                }
                else
                    textbox.Config.UploadImage = "false";

            }
        }


        public string Toolbar
        {
            set {
                if (value.ToLower() == "basic")
                    textbox.Config.ToolBarBasic();
                else if (value.ToLower() == "basic1")
                    textbox.Config.ToolBarBasic1();
                else if (value.ToLower() == "full")
                    textbox.Config.ToolBarFull();
                else
                    textbox.Config.ToolBarDefault();
            }
        }

        public TextTinyMCE()
        {
            textbox = new TextArea();            
            textbox.Config = new TinyMCEConfig();
        }       
    }
}
