using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetApiCallExample
{
    public partial class frmMain : Form
    {
        private int maxNumber = 0;
        private int currentNumber = 0;


        public frmMain()
        {
            InitializeComponent();
            ApiHelper.InitializeClient();
            btnNext.Enabled = false;
        }

        private async Task LoadImage(int imageNumber = 0)
        {
            var comic = await ComicProcessor.LoadComic(imageNumber);

            if (imageNumber == 0)
            {
                maxNumber = comic.Num;
            }

            currentNumber = comic.Num;

            var uriSource = new Uri(comic.Img, UriKind.Absolute);
            string url = uriSource.ToString();
            comicImage.Load(uriSource.ToString());
        }

        private async void Form1_LoadAsync(object sender, EventArgs e)
        {
            await LoadImage(0);
        }

        private async void btnPrevious_Click(object sender, EventArgs e)
        {
            if (currentNumber > 1)
            {
                currentNumber -= 1;
                btnNext.Enabled = true;
                await LoadImage(currentNumber);

                if (currentNumber == 1)
                {
                    btnPrevious.Enabled = false;
                }
            }
        }

        private async void btnNext_Click(object sender, EventArgs e)
        {
            if (currentNumber < maxNumber)
            {
                currentNumber += 1;
                btnPrevious.Enabled = true;
                await LoadImage(currentNumber);

                if (currentNumber == maxNumber)
                {
                    btnNext.Enabled = false;
                }
            }
        }
    }
}
