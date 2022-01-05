using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProiectWPFModel;
using System.Data.Entity;
using System.Data;

namespace ProiectWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    enum ActionState
    {
        New,
        Edit,
        Delete,
        Nothing
    }
    public partial class MainWindow : Window
    {
        ActionState action = ActionState.Nothing;
        ProiectWPFEntitiesModel ctx = new ProiectWPFEntitiesModel();
        CollectionViewSource clientVSource, employeeVSource;
        CollectionViewSource clientAppointmentsVSource;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
            BindingOperations.ClearBinding(firstNameCTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(lastNameCTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(firstNameETextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(lastNameETextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(serviceNameTextBox, TextBox.TextProperty);
            SetValidationBinding();

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;
            BindingOperations.ClearBinding(firstNameCTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(lastNameCTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(firstNameETextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(lastNameETextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(serviceNameTextBox, TextBox.TextProperty);
            SetValidationBinding();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            clientVSource.View.MoveCurrentToPrevious();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            clientVSource.View.MoveCurrentToNext();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            clientVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("clientViewSource")));
            clientVSource.Source = ctx.Clients.Local;
            ctx.Clients.Load();
            employeeVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("employeeViewSource")));
            employeeVSource.Source = ctx.Employees.Local;
            ctx.Employees.Load();
            clientAppointmentsVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("clientAppointmentsViewSource")));
            //clientAppointmentsVSource.Source = ctx.Appointments.Local;
            ctx.Appointments.Load();
            cmbClients.ItemsSource = ctx.Clients.Local;
            //cmbClients.DisplayMemberPath = "FirstNameC";
            cmbClients.SelectedValuePath = "ClientId";
            cmbEmployees.ItemsSource = ctx.Employees.Local;
            //cmbEmployees.DisplayMemberPath = "EmployeeCod";
            cmbEmployees.SelectedValuePath = "EmployeeId";
        }

        private void btnPrev1_Click(object sender, RoutedEventArgs e)
        {
            employeeVSource.View.MoveCurrentToPrevious();
        }

        private void btnNext1_Click(object sender, RoutedEventArgs e)
        {
            employeeVSource.View.MoveCurrentToNext();
        }

        private void SaveClients()
        {
            Client client = null;
            if (action == ActionState.New)
            {
                try
                {
                    client = new Client()
                    {
                        FirstNameC = firstNameCTextBox.Text.Trim(),
                        LastNameC = lastNameCTextBox.Text.Trim(),
                        PhoneC = phoneCTextBox.Text.Trim(),
                        ServiceName = serviceNameTextBox.Text.Trim(),
                        DateStart = dateStartDatePicker.DisplayDate,
                        DateEnd = dateEndDatePicker.DisplayDate,
                    };
                    ctx.Clients.Add(client);
                    clientVSource.View.Refresh();
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
           if (action == ActionState.Edit)
            {
                try
                {
                    client = (Client)clientDataGrid.SelectedItem;
                    client.FirstNameC = firstNameCTextBox.Text.Trim();
                    client.LastNameC = lastNameCTextBox.Text.Trim();
                    client.PhoneC = phoneCTextBox.Text.Trim();
                    client.ServiceName = serviceNameTextBox.Text.Trim();
                    client.DateStart = dateStartDatePicker.DisplayDate;
                    client.DateEnd = dateEndDatePicker.DisplayDate;
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    client = (Client)clientDataGrid.SelectedItem;
                    ctx.Clients.Remove(client);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                clientVSource.View.Refresh();
            }
        }
        private void SaveEmployees()
        {
            Employee employee = null;
            if (action == ActionState.New)
            {
                try
                {
                    employee = new Employee()
                    {
                        FirstNameE = firstNameETextBox.Text.Trim(),
                        LastNameE = lastNameETextBox.Text.Trim(),
                        PhoneE = phoneETextBox.Text.Trim(),
                        EmployeeCod = employeeCodTextBox.Text.Trim(),
                    };
                    ctx.Employees.Add(employee);
                    employeeVSource.View.Refresh();
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
           if (action == ActionState.Edit)
            {
                try
                {
                    employee = (Employee)employeeDataGrid.SelectedItem;
                    employee.FirstNameE = firstNameETextBox.Text.Trim();
                    employee.LastNameE = lastNameETextBox.Text.Trim();
                    employee.PhoneE = phoneETextBox.Text.Trim();
                    employee.EmployeeCod = employeeCodTextBox.Text.Trim();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    employee = (Employee)employeeDataGrid.SelectedItem;
                    ctx.Employees.Remove(employee);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                employeeVSource.View.Refresh();
            }
        }

        private void gbOperations_Click(object sender, RoutedEventArgs e)
        {
            Button SelectedButton = (Button)e.OriginalSource;
            Panel panel = (Panel)SelectedButton.Parent;

            foreach (Button B in panel.Children.OfType<Button>())
            {
                if (B != SelectedButton)
                    B.IsEnabled = false;
            }
            gbActions.IsEnabled = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ReInitialize();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = tbCtrlProiectWPF.SelectedItem as TabItem;

            switch (ti.Header)
            {
                case "Clients":
                    SaveClients();
                    break;
                case "Employees":
                    SaveEmployees();
                    break;
                case "Appointments":
                    SaveAppointments();
                    break;
            }
            ReInitialize();
        }

        private void ReInitialize()
        {

            Panel panel = gbOperations.Content as Panel;
            foreach (Button B in panel.Children.OfType<Button>())
            {
                B.IsEnabled = true;
            }
            gbActions.IsEnabled = false;
        }

        private void SaveAppointments()
        {
            Appointment appointment = null;
            if (action == ActionState.New)
            {
                try
                {
                    Client client = (Client)cmbClients.SelectedItem;
                    Employee employee = (Employee)cmbEmployees.SelectedItem;
                    appointment = new Appointment()
                    {

                        ClientId = client.ClientId,
                        EmployeeId = employee.EmployeeId
                    };
                    ctx.Appointments.Add(appointment);
                    ctx.SaveChanges();
                    BindDataGrid();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
           if (action == ActionState.Edit)
            {
                dynamic selectedAppointment = appointmentsDataGrid.SelectedItem;
                try
                {
                    int curr_id = selectedAppointment.AppointmentId;
                    var editedAppointment = ctx.Appointments.FirstOrDefault(s => s.AppointmentId == curr_id);
                    if (editedAppointment != null)
                    {
                        editedAppointment.ClientId = Int32.Parse(cmbClients.SelectedValue.ToString());
                        editedAppointment.EmployeeId = Convert.ToInt32(cmbEmployees.SelectedValue.ToString());
                        ctx.SaveChanges();
                    }
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                BindDataGrid();
                clientAppointmentsVSource.View.MoveCurrentTo(selectedAppointment);
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    dynamic selectedAppointment = appointmentsDataGrid.SelectedItem;
                    int curr_id = selectedAppointment.AppointmentId;
                    var deletedAppointment = ctx.Appointments.FirstOrDefault(s => s.AppointmentId == curr_id);
                    if (deletedAppointment != null)
                    {
                        ctx.Appointments.Remove(deletedAppointment);
                        ctx.SaveChanges();
                        MessageBox.Show("Appointment Deleted Successfully", "Message");
                        BindDataGrid();
                    }
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void BindDataGrid()
        {
            var queryOrder = from app in ctx.Appointments
                             join cli in ctx.Clients on app.ClientId equals cli.ClientId
                             join emp in ctx.Employees on app.EmployeeId equals emp.EmployeeId
                             select new
                             {
                                 app.AppointmentId,
                                 app.ClientId,
                                 app.EmployeeId,
                                 cli.FirstNameC,
                                 cli.LastNameC,
                                 cli.DateStart,
                                 cli.DateEnd,
                                 cli.ServiceName,
                                 emp.EmployeeCod,
                                 emp.FirstNameE,
                             };
            clientAppointmentsVSource.Source = queryOrder.ToList();
        }
        private void SetValidationBinding()
        {
            Binding firstNameCValidationBinding = new Binding();
            firstNameCValidationBinding.Source = clientVSource;
            firstNameCValidationBinding.Path = new PropertyPath("FirstNameC");
            firstNameCValidationBinding.NotifyOnValidationError = true;
            firstNameCValidationBinding.Mode = BindingMode.TwoWay;
            firstNameCValidationBinding.UpdateSourceTrigger =  UpdateSourceTrigger.PropertyChanged;
            firstNameCValidationBinding.ValidationRules.Add(new StringNotEmpty());
            firstNameCTextBox.SetBinding(TextBox.TextProperty, firstNameCValidationBinding);

            Binding lastNameCValidationBinding = new Binding();
            lastNameCValidationBinding.Source = clientVSource;
            lastNameCValidationBinding.Path = new PropertyPath("LastNameC");
            lastNameCValidationBinding.NotifyOnValidationError = true;
            lastNameCValidationBinding.Mode = BindingMode.TwoWay;
            lastNameCValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            lastNameCValidationBinding.ValidationRules.Add(new StringMinLengthValidator());
            lastNameCTextBox.SetBinding(TextBox.TextProperty, lastNameCValidationBinding); 

            Binding firstNameEValidationBinding = new Binding();
            firstNameEValidationBinding.Source = employeeVSource;
            firstNameEValidationBinding.Path = new PropertyPath("FirstNameE");
            firstNameEValidationBinding.NotifyOnValidationError = true;
            firstNameEValidationBinding.Mode = BindingMode.TwoWay;
            firstNameEValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            firstNameEValidationBinding.ValidationRules.Add(new StringNotEmpty());
            firstNameETextBox.SetBinding(TextBox.TextProperty, firstNameEValidationBinding);

            Binding lastNameEValidationBinding = new Binding();
            lastNameEValidationBinding.Source = employeeVSource;
            lastNameEValidationBinding.Path = new PropertyPath("LastNameE");
            lastNameEValidationBinding.NotifyOnValidationError = true;
            lastNameEValidationBinding.Mode = BindingMode.TwoWay;
            lastNameEValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            lastNameEValidationBinding.ValidationRules.Add(new StringMinLengthValidator());
            lastNameETextBox.SetBinding(TextBox.TextProperty, lastNameEValidationBinding);

            Binding serviceNameValidationBinding = new Binding();
            serviceNameValidationBinding.Source = clientVSource;
            serviceNameValidationBinding.Path = new PropertyPath("ServiceName");
            serviceNameValidationBinding.NotifyOnValidationError = true;
            serviceNameValidationBinding.Mode = BindingMode.TwoWay;
            serviceNameValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            serviceNameValidationBinding.ValidationRules.Add(new StringNotEmpty());
            serviceNameTextBox.SetBinding(TextBox.TextProperty, serviceNameValidationBinding);


            Binding employeeCodValidationBinding = new Binding();
            employeeCodValidationBinding.Source = employeeVSource;
            employeeCodValidationBinding.Path = new PropertyPath("FirstName");
            employeeCodValidationBinding.NotifyOnValidationError = true;
            employeeCodValidationBinding.Mode = BindingMode.TwoWay;
            employeeCodValidationBinding.UpdateSourceTrigger =UpdateSourceTrigger.PropertyChanged;
            //string required
            employeeCodValidationBinding.ValidationRules.Add(new StringNotEmpty());
            employeeCodTextBox.SetBinding(TextBox.TextProperty, employeeCodValidationBinding);
        }
    }
}
