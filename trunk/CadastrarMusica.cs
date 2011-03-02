using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;
using System.IO;

namespace Karaoke
{
    public partial class CadastrarMusica : Form
    {
        private String caminhoMusica;
        private List<Frase> frases;

        internal List<Frase> Frases
        {
            get { return frases; }
            set { frases = value; }
        }
        private Frase frase;

        internal Frase Frase
        {
            get { return frase; }
            set { frase = value; }
        }

        public CadastrarMusica()
        {
            frase = null;
            caminhoMusica = null;
            frases = new List<Frase>();
            InitializeComponent();
            atualizaListView();
        }

        private void btnProcurar_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                caminhoMusica = openFileDialog1.FileName;
                txtCaminho.Text = caminhoMusica;
            }
        }

        public void atualizaListView()
        {
            lstFrases.Items.Clear();
            foreach (Frase frase in frases)
            {
                lstFrases.Items.Add(frase.ToString());
            }
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            this.frase = null;
            new CadastrarPalavra(this).ShowDialog(this);
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtCaminho.Text) || String.IsNullOrEmpty(txtNome.Text))
            {
                MessageBox.Show("Informe o nome e o arquivo da música.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            String caminhoOrigem = txtCaminho.Text;            
            String nomeArquivo = caminhoOrigem.Substring(caminhoOrigem.LastIndexOf("\\") + 1).Replace(' ', '_');
            String caminhoDestino = Path.Combine(Application.StartupPath + "\\media", nomeArquivo);
            try
            {
                if (FileSystem.FileExists(Application.StartupPath + "\\musicas\\" + txtNome.Text + ".kar"))
                {
                    MessageBox.Show("Nome de música já existe. Escolha outro.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (FileSystem.FileExists(caminhoDestino))
                {
                    MessageBox.Show("Arquivo de música já existente. Altere o nome do arquivo a ser importado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                FileSystem.CopyFile(caminhoOrigem, caminhoDestino, UIOption.AllDialogs);
                StreamWriter wr = new StreamWriter(Application.StartupPath + "\\musicas\\" + txtNome.Text + ".kar", false);
                wr.WriteLine(txtNome.Text);
                wr.WriteLine(nomeArquivo);
                foreach (Frase frase in frases)
                {
                    wr.WriteLine(frase.Texto + "#" + frase.TempoInicio.ToString() + "#" + frase.TempoFim.ToString());
                }
                wr.Close();
                MessageBox.Show("Música cadastrada com sucesso.","Concluído",MessageBoxButtons.OK,MessageBoxIcon.Information);
                this.Dispose();
            }
            catch
            {
                MessageBox.Show("Não foi possível cadastrar a música.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (lstFrases.SelectedIndices.Count > 0)
            {
                this.frase = frases[lstFrases.SelectedIndices[0]];
                frases.RemoveAt(lstFrases.SelectedIndices[0]);
                new CadastrarPalavra(this).ShowDialog(this);
            }
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (lstFrases.SelectedIndices.Count > 0)
            {
                for(int i = 0; i < lstFrases.SelectedIndices.Count; i++)
                {
                    frases.RemoveAt(lstFrases.SelectedIndices[i]);
                }
                atualizaListView();
            }            
        }
    }
}
