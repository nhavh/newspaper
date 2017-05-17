using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Moxiecode.TinyMCE;

namespace TMV.BackEnd.Controls
{
    public partial class TextEditor : UserControl
    {
        private TextTinyMCE _richTextEditor;
        private bool _uploadFile = true;
        private bool _uploadImage = true;
        private string _toolbar = "Default";

        public bool HtmlEncode { get; set; }

        public override string ClientID
        {
            get
            {
                return base.ClientID + "_" + ID;
            }
        }

        public Unit Height { get; set; }

        public Unit Width { get; set; }

        public string Text
        {
            get
            {
                return Encode(_richTextEditor.Text);
            }
            set
            {
                _richTextEditor.Text = Decode(value);
            }
        }

        public bool UploadFile
        {
            get { return _uploadFile; }
            set { _uploadFile = value; }
        }

        public bool UploadImage
        {
            get { return _uploadImage; }
            set { _uploadImage = value; }
        }

        public string Toolbar
        {
            get { return _toolbar; }
            set { _toolbar = value; }
        }

        public TextEditor()
        {
            Load += Page_Load;
            Init += Page_Init;
            HtmlEncode = true;
        }

        private void Page_Init(object sender, EventArgs e)
        {
            _richTextEditor = new TextTinyMCE
                {
                    ControlID = ID,
                    UploadFile = UploadFile,
                    UploadImage = UploadImage,
                    Toolbar = Toolbar
                };
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            _richTextEditor.Width = Width;
            _richTextEditor.Height = Height;
            plcEditor.Controls.Add(_richTextEditor.HTMLEditor);
        }
        private string Encode(string strHtml)
        {
            return HtmlEncode ? Server.HtmlEncode(strHtml) : strHtml;
        }

        private string Decode(string strHtml)
        {
            return HtmlEncode ? Server.HtmlDecode(strHtml) : strHtml;
        }
    }
}