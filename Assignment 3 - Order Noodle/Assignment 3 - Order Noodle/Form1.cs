using System;
using System.Windows.Forms;

namespace Assignment_3___Order_Noodle
{
    public partial class Menu : Form
    {
        const double AUD_TO_USD = 0.77;
        const double USD_TO_AUD = 1 / 0.77;
        double priceOfNoodle = 5.5;
        double priceAddingMeat = 0;
        double priceOfExtraVeg = 0;
        double eachExtraVeg = 0.5;
        double biggerServe = 1.5;
        int VegSelected = 0;
        double totalPrice;
        
        bool isAnyButtonChecked = false;

        public Menu()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Choose meat in the lower left section.
        /// </summary>
        private void chooseMeat()
        {
            if (rbChicken.Checked)
                priceAddingMeat = 5;
            else if (rbPork.Checked)
                priceAddingMeat = 5;
            else if (rbBeef.Checked)
                priceAddingMeat = 5;
            else if (rbSeafood.Checked)
                priceAddingMeat = 7;
            else // "None" is chosen
                priceAddingMeat = 0;
        }
        /// <summary>
        /// Choose vegetables in the checkbox list.
        /// </summary>
        private void chooseVegetable()
        {            
            VegSelected = 0;
            // record the number of vegetables selected
            foreach (CheckBox vegetables in gbxVegetables.Controls) {
                if (vegetables.Checked == true) {
                    VegSelected += 1;
                }
            }
            // each additional vegetable charge $0.5 if more than 4 are chosen
            if (VegSelected >= 4) { 
                priceOfExtraVeg = eachExtraVeg * (VegSelected - 4);
            }
        }
        /// <summary>
        /// Calculate the total price by sum up the noodles, extra meat and vegetables.
        /// </summary>
        private void payTheBill()
        {
            totalPrice = priceOfNoodle + priceAddingMeat + priceOfExtraVeg;
            if (cbBiggerServe.Checked)
                totalPrice *= biggerServe;
        }
        /// <summary>
        /// check whether the noodle type, flavour selected and the customer's name before display the price
        /// </summary>
        private void DisplayThePrice()
        {
            
            foreach (RadioButton noodle in gbxNoodleType.Controls) {
                if (noodle.Checked == true) { 
                    isAnyButtonChecked = true;
                }
            }
            if (!isAnyButtonChecked) {
                MessageBox.Show("Please choose a noodle!");
            } else if (cmbChooseFlavour.SelectedIndex == -1) { // no option selected
                MessageBox.Show("Please choose a Flavour for your noodle!");
            } else if (tbYourName.Text == "") {
                MessageBox.Show("Please supply a name!");
            } else {
                lblTotalPrice.Visible = true;
                tbTotalPrice.Visible = true;
                rbAUD.Visible = true;
                rbUSD.Visible = true;
                groupBoxCurrency.Visible = true;
                tbTotalPrice.Text = "$" + totalPrice.ToString("#0.00");
                
            }
        }
        /// <summary>
        /// Display the total price in AUD.
        /// </summary>
        private void rbAUD_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAUD.Checked)
                totalPrice *= USD_TO_AUD;
            tbTotalPrice.Text = "$" + totalPrice.ToString("#0.00");
        }
        /// <summary>
        /// Display the total price in USD.
        /// </summary>
        private void rbUSD_CheckedChanged(object sender, EventArgs e)
        {
            if (rbUSD.Checked)
                totalPrice *= AUD_TO_USD;
            tbTotalPrice.Text = "$" + totalPrice.ToString("#0.00");
        }
        /// <summary>
        /// Determine if customers want to have a bigger serve
        /// </summary>
        private void cbBiggerServe_CheckedChanged(object sender, EventArgs e)
        {
            if (cbBiggerServe.Checked) {
                totalPrice *= biggerServe;
                tbTotalPrice.Text = "$" + totalPrice.ToString("#0.00");
            } else {
                totalPrice /= biggerServe;
                tbTotalPrice.Text = "$" + totalPrice.ToString("#0.00");
            }
        }
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            rbAUD.Checked = true;
            chooseMeat();
            chooseVegetable();
            payTheBill();
            DisplayThePrice();
        }

        private void gbxMeat_Enter(object sender, EventArgs e)
        {
            foreach (Control ctrl in gbxMeat.Controls) {
                if (ctrl is RadioButton)
                {
                    RadioButton meat = (RadioButton)ctrl;
                    if (meat.Checked)
                    {
                        lblTotalPrice.Visible = false;
                        tbTotalPrice.Visible = false;
                        rbAUD.Visible = false;
                        rbUSD.Visible = false;
                        groupBoxCurrency.Visible = false;
                    }
                }
            }
        }
        int howManyVeg = 0;
        private void gbxVegetables_Enter(object sender, EventArgs e)
        {
            
            foreach (CheckBox ctrl in gbxVegetables.Controls)
            {
                if (ctrl.Checked == true)
                {
                    howManyVeg += 1;
                }
            }
            if (howManyVeg > 4)
            {
                lblTotalPrice.Visible = false;
                tbTotalPrice.Visible = false;
                rbAUD.Visible = false;
                rbUSD.Visible = false;
                groupBoxCurrency.Visible = false;
            }
        }
    }
}
