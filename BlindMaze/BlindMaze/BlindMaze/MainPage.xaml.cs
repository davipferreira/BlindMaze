using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BlindMaze
{
    public partial class MainPage : ContentPage
    {
        #region Atributos e Propriedades
        private int _ContadorMovimentos;
        public int ContadorMovimentos
        {
            get { return _ContadorMovimentos; }
            set { _ContadorMovimentos = value; }
        }

        private int _ContadorMovimentosDicas;
        public int ContadorMovimentosDicas
        {
            get { return _ContadorMovimentosDicas; }
            set { _ContadorMovimentosDicas = value; }
        }

        private int _ExitPositionX;
        public int ExitPositionX
        {
            get { return _ExitPositionX; }
            set { _ExitPositionX = value; }
        }

        private int _ExitPositionY;
        public int ExitPositionY
        {
            get { return _ExitPositionY; }
            set { _ExitPositionY = value; }
        }

        private int _PlayerPositionX;
        public int PlayerPositionX
        {
            get { return _PlayerPositionX; }
            set { _PlayerPositionX = value; }
        }

        private int _PlayerPositionY;
        public int PlayerPositionY
        {
            get { return _PlayerPositionY; }
            set { _PlayerPositionY = value; }
        }

        private bool _Started;
        public bool Started
        {
            get { return _Started; }
            set { _Started = value; }
        }
        #endregion

        #region Construtor
        public MainPage()
        {
            InitializeComponent();

            InicializeApp();
        }
        #endregion

        #region Métodos Auxiliares
        void InicializeApp()
        {
            //Exibe mensagem com informação de como jogar e zera parâmetros do jogo
            lblDicas.Text = "O JOGO É SIMPLES!" + Environment.NewLine + "Imagine que está em um quarto imenso e escuro e precisa encontrar 'A Porta' pra sair. Você só pode ir PARA FRENTE, PARA TRÁS, PARA A DIREITA e PARA A ESQUERDA... mas de alguma forma a cada 20 movimentos você encontra uma dica da sua localização atual e para qual lado está 'A Porta'.";
            this.Started = false;
            EnableDisableButtons(Started);
        }        

        void EnableDisableButtons(bool p_Option)
        {
            btnAhead.IsEnabled = Started;
            btnBack.IsEnabled = Started;
            btnRight.IsEnabled = Started;
            btnLeft.IsEnabled = Started;
        }

        void GetPlayerPositions()
        {
            Random r = new Random();
            this.PlayerPositionX = r.Next(33, 65);
            this.PlayerPositionY = r.Next(33, 65);
        }

        void GetExitPositions()
        {
            Random r = new Random();
            this.ExitPositionX = r.Next(0, 99);
            this.ExitPositionY = r.Next(0, 99);
        }

        void ValidaSaidaOuDica()
        {
            lblContadorMovimentos.Text = this.ContadorMovimentos.ToString();
            lblDicas.FontSize = 14;

            if (this.ExitPositionX == this.PlayerPositionX && this.ExitPositionY == this.PlayerPositionY)
            {
                lblDicas.Text = "Parabéns!! Você achou 'A Porta'!!" + Environment.NewLine +  "Se quiser pode jogar de novo, ela sempre aparece em um lugar diferente.";
            }
            else
            {

                if (this.PlayerPositionX > 99 || this.PlayerPositionX < 0 || this.PlayerPositionY > 99 || this.PlayerPositionY < 0)
                {

                    if (this.PlayerPositionX > 99)
                        this.PlayerPositionX = 100;

                    if (this.PlayerPositionY > 99)
                        this.PlayerPositionY = 100;

                    if (this.PlayerPositionX < 0)
                        this.PlayerPositionX = -1;

                    if (this.PlayerPositionY < 0)
                        this.PlayerPositionY = -1;

                    if (this.ContadorMovimentosDicas == 20)                    
                        this.ContadorMovimentosDicas = 1;


                    lblDicas.Text = "Você bateu com a cabeça na parede!" + Environment.NewLine + "Não tente fazer uma porta com sua testa, ache 'A Porta'.";
                    lblDicas.FontSize = 24;
                }
                else
                {

                    if (this.ContadorMovimentosDicas == 20)
                    {
                        this.ContadorMovimentosDicas = 0;

                        if (this.ExitPositionX < this.PlayerPositionX)
                            lblDicas.Text = "Tente 'Ir Para Esquerda' algumas vezes!";
                        else if (this.ExitPositionX > this.PlayerPositionX)
                            lblDicas.Text = "Tente 'Ir Para Direita' algumas vezes!";
                        else if (this.ExitPositionY < this.PlayerPositionY)
                            lblDicas.Text = "Tente 'Ir Para Trás' algumas vezes!";
                        else if (this.ExitPositionY > this.PlayerPositionY)
                            lblDicas.Text = "Tente 'Ir Para Frente' algumas vezes!";
                    }
                    else
                    {
                        lblDicas.Text = this.ContadorMovimentosDicas.ToString();
                        lblDicas.FontSize = 60;
                    }
                }
            }
        }
        #endregion

        #region Eventos Botões
        private void Start_Clicked(object sender, EventArgs e)
        {
            if (this.Started)
            {
                this.Started = false;
                EnableDisableButtons(Started);
                btnStart.Text = "Começar";
            }
            else
            {
                this.Started = true;
                EnableDisableButtons(Started);
                GetPlayerPositions();
                GetExitPositions();
                btnStart.Text = "Recomeçar";
                lblDicas.Text = this.ContadorMovimentosDicas.ToString();
                lblDicas.FontSize = 60;
            }
        }

        private void Ahead_Clicked(object sender, EventArgs e)
        {
            this.PlayerPositionY++;
            this.ContadorMovimentos++;
            this.ContadorMovimentosDicas++;
            ValidaSaidaOuDica();
        }

        private void Back_Clicked(object sender, EventArgs e)
        {
            this.PlayerPositionY--;
            this.ContadorMovimentos++;
            this.ContadorMovimentosDicas++;
            ValidaSaidaOuDica();
        }

        private void Right_Clicked(object sender, EventArgs e)
        {
            this.PlayerPositionX++;
            this.ContadorMovimentos++;
            this.ContadorMovimentosDicas++;
            ValidaSaidaOuDica();
        }

        private void Left_Clicked(object sender, EventArgs e)
        {
            this.PlayerPositionX--;
            this.ContadorMovimentos++;
            this.ContadorMovimentosDicas++;
            ValidaSaidaOuDica();
        }
        #endregion
    }
}
