Imports System
Imports System.IO.Ports


Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        GetSerialPortNames()


    End Sub

    Sub GetSerialPortNames()
        ' Show all available COM ports.

        Dim AvailablePorts() As String = SerialPort.GetPortNames()
        Dim port As String
        For Each port In AvailablePorts

            ComboBox1.Items.Add(port)
            ComboBox2.Items.Add(port)
        Next port


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim MyCOMPortin As SerialPort


        Dim comin = ComboBox1.SelectedItem

        If ComboBox1.SelectedItem = "" Then
            comin = ComboBox1.SelectedItem(1)
        End If

        MyCOMPortin = New SerialPort()
        MyCOMPortin.PortName = comin   'Assign the port name to the MyCOMPort object
        MyCOMPortin.BaudRate = 9600     'Assign the Baudrate to the MyCOMPort object
        MyCOMPortin.Parity = Parity.None   'Parity bits = none  
        MyCOMPortin.DataBits = 8             'No of Data bits = 8
        MyCOMPortin.StopBits = StopBits.One  'No of Stop bits = 1



        AddHandler MyCOMPortin.DataReceived, AddressOf DataReceivedHandler

        MyCOMPortin.Open()

    End Sub



    Private Sub DataReceivedHandler(
                        sender As Object,
                        e As SerialDataReceivedEventArgs)
        Dim sp As SerialPort = CType(sender, SerialPort)
        Dim indata As String = sp.ReadExisting()
        If indata.Length < 2 Then
            Exit Sub

        End If
        If indata.Length > 2 Then
            indata = Strings.Right(indata, 2)

        End If
        SendFreq(indata)

    End Sub

    Sub SendFreq(freq As String)
        Dim MyCOMPortout As SerialPort
        MyCOMPortout = New SerialPort()
        ComboBox2.Invoke(Sub()
                             Dim comout = ComboBox2.SelectedItem

                             If ComboBox2.SelectedItem = "" Then
                                 comout = ComboBox2.SelectedItem(2)
                             End If
                             MyCOMPortout.PortName = comout
                             MyCOMPortout.BaudRate = 9600    'Assign the Baudrate to the MyCOMPort object
                             MyCOMPortout.Parity = Parity.None   'Parity bits = none  
                             MyCOMPortout.DataBits = 8             'No of Data bits = 8
                             MyCOMPortout.StopBits = StopBits.One  'No of Stop bits = 1
                         End Sub)

        MyCOMPortout.Open()


                             Select Case freq
                                 Case "F1"
                MyCOMPortout.Write("FA00137100000;")
                Label3.Invoke(Sub()
                                                       Label3.Text = "137.100 Mhz"
                                                   End Sub)
                                 Case "F3"
                MyCOMPortout.Write("FA00137500000;")
                Label3.Invoke(Sub()

                                                       Label3.Text = "137.500 Mhz"
                                                   End Sub)
                                 Case "F4"
                MyCOMPortout.Write("FA00137620000;")
                Label3.Invoke(Sub()
                                                       Label3.Text = "137.620 Mhz"
                                                   End Sub)
                                 Case "F5"
                MyCOMPortout.Write("FA00137912500;")
                Label3.Invoke(Sub()
                                                       Label3.Text = "137.9125 Mhz"
                                                   End Sub)

                             End Select
                             MyCOMPortout.Close()
                         End Sub

End Class
