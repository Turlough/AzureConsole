Option Explicit On
Option Strict On

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Data.SqlClient
Imports System.Data

Module Module1
    ' Provide the following information
    Private ReadOnly userName As String = "Agent1"
    Private ReadOnly password As String = "Agent007$"
    Private ReadOnly dataSource As String = "pfokcd6a9l.database.windows.net"
    Private ReadOnly db As String = "Auctioneer"


    Sub Main()
        Console.WriteLine("Connecting to: " & dataSource)
        Console.WriteLine("Database: " & db)
        Console.WriteLine()

        ' Create a connection string for the sample database
        Dim scsb As New SqlConnectionStringBuilder()
        With scsb
            .DataSource = dataSource
            .InitialCatalog = db
            .Encrypt = True
            .TrustServerCertificate = False
            .UserID = userName
            .Password = password
        End With

        ' Connect to the sample database and insert customers
        Using conn As New SqlConnection(scsb.ToString())
            Using command As SqlCommand = conn.CreateCommand()
                conn.Open()
                Console.WriteLine("Connected!")
                Console.WriteLine()

                'request insert values when open
                Console.Write(" Enter Forename:  ")
                Dim fname As String = Console.ReadLine
                Console.Write(" Enter Surname:   ")
                Dim sname As String = Console.ReadLine
                Console.WriteLine()

                ' Insert record
                command.CommandText = "INSERT INTO customer (fname, lname) VALUES ('" & fname & "', '" & sname & "')"
                Dim rowsAdded As Integer = command.ExecuteNonQuery()

                ' Query the table and print the results
                command.CommandText = "SELECT * FROM customer"
                Using reader As SqlDataReader = command.ExecuteReader()

                    ' Loop through result set
                    While reader.Read()
                        Console.WriteLine("|{0}|- {1} {2}", _
                                          reader("id").ToString().Trim(), _
                                          reader("fname").ToString().Trim(), _
                                          reader("lname").ToString().Trim())
                    End While
                    'finish up
                    Console.WriteLine()

                End Using

                conn.Close()
                Console.WriteLine("Disconnected")
            End Using
        End Using

        Console.Write("Press Enter to continue...")
        Console.ReadLine()
    End Sub
End Module
