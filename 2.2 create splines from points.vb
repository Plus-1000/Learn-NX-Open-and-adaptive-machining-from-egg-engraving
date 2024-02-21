Imports System
Imports NXOpen
Module NXJournal
    Dim theSession = Session.GetSession()
    Dim workPart = theSession.Parts.Work
    Dim lw As ListingWindow = theSession.ListingWindow
    Sub Main(ByVal args() As String)
        lw.Open()
        Dim amount_of_pts_in_each_spline As Integer = 4  'each spline contains 5 pts

        create_spline(1, amount_of_pts_in_each_spline) ' create 1st spline (it will be the section 1 for through curve face)
        create_spline(6, amount_of_pts_in_each_spline)
        create_spline(11, amount_of_pts_in_each_spline)
        create_spline(16, amount_of_pts_in_each_spline)
    End Sub
    Private Sub create_spline(ByVal i As Integer, ByVal j As Integer)
        Dim builder As NXOpen.Features.StudioSplineBuilderEx
        builder = workPart.Features.CreateStudioSplineBuilderEx(Nothing)
        builder.IsSingleSegment = True
        builder.IsAssociative = True
        builder.Degree = 3
        builder.Type = builder.Types.ThroughPoints

        Dim pt_name As String
        For j1 As Integer = 0 To j   '-----points count of a spline, the spline will contain 5 points-----
            pt_name = "NEWPT_" & (i + j1).ToString
            lw.WriteLine("looking for: " & pt_name)
            For Each newPt As Point In workPart.Points
                If newPt.Name = pt_name Then
                    lw.WriteLine(pt_name & " found")
                    Dim pt1 As New NXOpen.Point3d(newPt.Coordinates.X, newPt.Coordinates.Y, newPt.Coordinates.Z)
                    AddPoint(builder, pt1)
                    lw.WriteLine("------------------pt added " & j)
                End If
            Next
        Next

        Dim result As NXOpen.Features.StudioSpline = builder.Commit
        builder.Destroy()
        result.SetName("NEW_SPLINE" & i.ToString)
    End Sub
    ' add point to studiosplinebuilderEX, !!!
    Private Sub AddPoint(builder As Features.StudioSplineBuilderEx, coords As NXOpen.Point3d)
        Dim workPart As NXOpen.Part = NXOpen.Session.GetSession.Parts.Work
        Dim point As NXOpen.Point = workPart.Points.CreatePoint(coords)
        Dim geomCon As NXOpen.Features.GeometricConstraintData
        geomCon = builder.ConstraintManager.CreateGeometricConstraintData
        geomCon.Point = point
        builder.ConstraintManager.Append(geomCon)
    End Sub
    Public Function GetUnloadOption(ByVal dummy As String) As Integer
        'Unloads the image immediately after execution within NX
        GetUnloadOption = NXOpen.Session.LibraryUnloadOption.Immediately
    End Function
End Module
