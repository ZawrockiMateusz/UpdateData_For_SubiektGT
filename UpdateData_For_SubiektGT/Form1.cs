using System;
using System.Windows.Forms;

namespace UpdateData_For_SubiektGT
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (txtServer.Text.Trim().Length == 0 || txtDataBase.Text.Trim().Length == 0 || txtUser.TextLength == 0 || txtOperator.TextLength == 0)
            {
                MessageBox.Show("Nie podano danych wymaganych do nawiązania połączenia", "ERROR");
            }
            else
            {
                EditDocument();
            }
        }

        private void EditDocument()
        {
            InsERT.GT oGT = new InsERT.GT();
            InsERT.Subiekt oSubGT;
            InsERT.SuDokument oDokum;
            InsERT.SuDokumentyLista oListaDok;
            AddNote addNote = new AddNote();

            //establish connection with SubiektGT
            if (oGT is null)
            {
                MessageBox.Show("Nie udało się utworzyć obiektu GT");
            }
            
            oGT.Produkt = InsERT.ProduktEnum.gtaProduktSubiekt;
            oGT.Serwer = txtServer.Text;
            oGT.Baza = txtDataBase.Text;
            oGT.Autentykacja = InsERT.AutentykacjaEnum.gtaAutentykacjaWindows;
            oGT.Uzytkownik = txtUser.Text;
            oGT.UzytkownikHaslo = txtUsersPassword.Text;
            oGT.Operator = txtOperator.Text;
            oGT.OperatorHaslo = txtOperatorsPassword.Text;
            oSubGT = (InsERT.Subiekt)oGT.Uruchom((int)InsERT.UruchomDopasujEnum.gtaUruchomDopasujOperatora, (int)InsERT.UruchomEnum.gtaUruchom);
            oSubGT.MagazynId = 1;

            //load documents list
            oListaDok = oSubGT.Dokumenty.Wybierz();
            oListaDok.FiltrTypOpcje = InsERT.FiltrSuDokumentOpcjeEnum.gtaFiltrSuDokumentOpcjeZam;
            oListaDok.FiltrOkres = InsERT.FiltrOkresEnum.gtaFiltrOkresNieokreslony;
            oListaDok.MultiSelekcja = false;

            oListaDok.Wyswietl();

            foreach (int i in oListaDok.ZaznaczoneId())
            {
                addNote.ShowDialog();
                //connect to selected document and change its description for the one created in AddNote form
                oDokum = oSubGT.SuDokumentyManager.WczytajDokument(i);
                oSubGT.SuDokumentyManager.ZmienOpisDokumentu(oDokum, InsERT.SuDokumentOpisEnum.sdoUwagi, addNote.Note);

                MessageBox.Show("Uwaga do dokumentu " + oDokum.NumerPelny + " została dodana pomyślnie.", "Sukces");
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}