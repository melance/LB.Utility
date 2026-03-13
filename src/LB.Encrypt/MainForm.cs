namespace LB.Encrypt
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnEncrypt_Click(Object sender, EventArgs e)
        {
            var aes = new Utility.Encryption.Symmetric(Convert.FromBase64String(txtKey.Text), Convert.FromBase64String(txtIV.Text));
            txtResult.Text = aes.Encrypt(txtText.Text);
        }

        private void btnDecrypt_Click(Object sender, EventArgs e)
        {
            var aes = new Utility.Encryption.Symmetric(Convert.FromBase64String(txtKey.Text), Convert.FromBase64String(txtIV.Text));
            txtResult.Text = aes.Decrypt(txtText.Text);
        }

        private void btnHash_Click(Object sender, EventArgs e)
        {
            txtResult.Text = Utility.Encryption.Asymmetric.CreateHash(txtText.Text);
        }

        private void btnCopyResult_Click(Object sender, EventArgs e)
        {
            Clipboard.SetText(txtResult.Text);
        }
    }
}