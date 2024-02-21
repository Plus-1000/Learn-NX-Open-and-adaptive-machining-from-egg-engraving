Imports System
Imports NXOpen
Imports NXOpen.UF
Imports NXOpen.Assemblies
Imports NXOpen.Features
Imports System.IO
Public Class MainClass
    Public Shared theUi As UI
    Public Shared theSession As Session
    Public Shared theUF As UFSession
    Public Shared workPart As Part
    Public Shared lw As ListingWindow
    Public Shared Sub Main()   '(ByVal cmdArgs() As String)
        theSession = Session.GetSession()
        theUF = UFSession.GetUFSession
        theUi = UI.GetUI()
        workPart = theSession.Parts.Work
        lw = theSession.ListingWindow
        lw.Open()

        '----------read points from txt (csv) file---------
        Dim ptFile As String = "C:\WangJian_ARTC\01_Projects\11_NX_Open\05_NXOpen_tips\01_read_pts_from_txt\p1-20.csv"
        '20 probing points stored with in below formate in file p1-20.csv: 
        'x1, y1, z1,
        'x2, y2, z2,
        '...
        'x20, y20, z20

        Dim collected_Pts() As Point3d  'new array contains 20 points
        collected_Pts = readPt(ptFile) 'call readPt function, the "pt" returned, which is an array of 20 point3d

        '----------create points from Point3d array "collected_Pts"----------
        Dim count As Integer = collected_Pts.Length
        Dim ptTag As Tag
        lw.WriteLine("points number to be readed: " + count.ToString)
        For i As Integer = 0 To count - 1   ' i is from 0 to 19, 20 points in total
            lw.WriteLine("impo0rted point" + i.ToString + ": " +
                  collected_Pts(i).X.ToString + "," + collected_Pts(i).Y.ToString + "," + collected_Pts(i).Z.ToString)  ' for display
            theUF.Curve.CreatePoint({collected_Pts(i).X, collected_Pts(i).Y, collected_Pts(i).Z}, ptTag)  ' create point from point3d
            lw.WriteLine(ptTag.ToString)

            '------- rename points--------
            Dim obj As TaggedObject = NXOpen.Utilities.NXObjectManager.Get(ptTag)
            Dim myPoint As Point = CType(obj, Point)  ' locate point from it's tag
            myPoint.SetName("newPt_" & i + 1)
        Next

        'lw.Close()
        Threading.Thread.Sleep(2000)
        'theUF.Ui.ExitListingWindow()
    End Sub
    '---------- read point file and create point3d array--------
    Public Shared Function readPt(ByVal ptFile As String)
        Dim delim As Char() = {","}  'seperator
        Dim lines_count As Integer = File.ReadAllLines(ptFile).Length  ' lines_counts=20
        Dim pt() As Point3d  ' define array for Point3d

        lw.WriteLine(" lines of csv:  " & lines_count)
        Using sr As StreamReader = New StreamReader(ptFile)
            Try
                For i As Integer = 0 To lines_count - 1
                    Dim line As String = sr.ReadLine()
                    Dim strings As String() = line.Split(delim)

                    ReDim Preserve pt(i)   ' 0-19, 20 points in total
                    pt(i).X = Double.Parse(strings(0))
                    pt(i).Y = Double.Parse(strings(1))
                    pt(i).Z = Double.Parse(strings(2))
                    lw.WriteLine("inside Func read point" + i.ToString + ";   x,y,z: " +
                                           pt(i).X.ToString + "," + pt(i).Y.ToString + "," + pt(i).Z.ToString)
                Next
            Catch E As Exception
                MsgBox(E.Message)
            End Try
        End Using
        Return pt  ' return array of point3d, which contains 20 points
        'x1, y1, z1,
        'x2, y2, z2,
        '...
        'x20, y20, z20

    End Function
    Public Shared Function GetUnloadOption(ByVal arg As String) As Integer
        Return CType(Session.LibraryUnloadOption.Immediately, Integer)
    End Function
End Class
