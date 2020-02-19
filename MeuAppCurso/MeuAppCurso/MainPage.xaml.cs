using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using MeuAppCurso.Servico.Modelo;
using MeuAppCurso.Servico;

namespace MeuAppCurso
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BOTAO.Clicked += BuscarCEP;
        }
        private void BuscarCEP(object sender, EventArgs args) {
            string cep = CEP.Text.Trim();
           
            if (isValidCEP(cep)) {
                try {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);

                    if (end != null) {
                        RESULTADO.Text = string.Format("{2} \n\nBairro: {3} \n\nCidade: {0} \n\nEstado: {1}", end.localidade, end.uf, end.logradouro, end.bairro);
                    }
                    else {
                        DisplayAlert("Erro", "O endereço não foi encontrado para o CEP informado.", "Ok");
                    }                     
                }
                catch (Exception e){
                    DisplayAlert("Erro crítico.", e.Message, "Ok");
                }
            } 
        }
        private bool isValidCEP(string cep) {
            bool valido = true;
            
            if (cep.Length != 8) {
                //erro
                DisplayAlert("ERRO", "CEP inválido! O CEP deve conter 8 caracteres.", "Ok");
                valido = false;
            }
            
            int novoCEP = 0;
            if (!int.TryParse(cep, out novoCEP)) {
                //erro
                DisplayAlert("ERRO", "CEP inválido! O CEP deve conter apenas números.", "Ok");
                valido = false;
            }
            return valido;
        } 
    }
}
