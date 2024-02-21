' NX 12.0.0.27
' Journal created by wangjian on Thu Aug 31 14:00:42 2023 Malay Peninsula Standard Time
'
Imports System
Imports NXOpen

Public Class countour1
    'Public Shared extraced_face_id As String
    Public Shared Sub Main()
        Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
        Dim workPart As NXOpen.Part = theSession.Parts.Work
        Dim displayPart As NXOpen.Part = theSession.Parts.Display
        Dim lw As ListingWindow = theSession.ListingWindow
        lw.Open()
        If theSession.ApplicationName.ToString IsNot "UG_APP_MODELING" Then
            Try
                theSession.ApplicationSwitchImmediate("UG_APP_MODELING")
                'Dim resultx As Boolean = Nothing
                'resultx = theSession.Is
            Catch ex As Exception
                lw.WriteLine(ex.ToString)
                lw.WriteLine("App name: " & theSession.ApplicationName.ToString)
            End Try

        End If
        ' ----------------------------------------------
        '   Menu: Application->Manufacturing->Manufacturing
        ' ----------------------------------------------
        theSession.ApplicationSwitchImmediate("UG_APP_MANUFACTURING")
        Dim result1 As Boolean = Nothing
        result1 = theSession.IsCamSessionInitialized()
        Dim kinematicConfigurator1 As NXOpen.SIM.KinematicConfigurator = workPart.CreateKinematicConfigurator()

        ' ----------------------------------------------
        '   Menu: Insert->Operation...
        ' ----------------------------------------------
        Dim nCGroup1 As NXOpen.CAM.NCGroup = CType(workPart.CAMSetup.CAMGroupCollection.FindObject("NC_PROGRAM"), NXOpen.CAM.NCGroup)
        Dim method1 As NXOpen.CAM.Method = CType(workPart.CAMSetup.CAMGroupCollection.FindObject("METHOD"), NXOpen.CAM.Method)
        Dim tool1 As NXOpen.CAM.Tool = CType(workPart.CAMSetup.CAMGroupCollection.FindObject("D2R1L20"), NXOpen.CAM.Tool)
        Dim orientGeometry1 As NXOpen.CAM.OrientGeometry = CType(workPart.CAMSetup.CAMGroupCollection.FindObject("MCS_MILL"), NXOpen.CAM.OrientGeometry)
        Dim operation1 As NXOpen.CAM.Operation = Nothing
        operation1 = workPart.CAMSetup.CAMOperationCollection.Create(nCGroup1, method1, tool1, orientGeometry1, "mill_multi-axis", "VARIABLE_CONTOUR", NXOpen.CAM.OperationCollection.UseDefaultName.True, "VARIABLE_CONTOUR")

        Dim surfaceContour1 As NXOpen.CAM.SurfaceContour = CType(operation1, NXOpen.CAM.SurfaceContour)
        Dim surfaceContourBuilder1 As NXOpen.CAM.SurfaceContourBuilder = Nothing
        surfaceContourBuilder1 = workPart.CAMSetup.CAMOperationCollection.CreateSurfaceContourBuilder(surfaceContour1)
        Dim toolAxisVariable1 As NXOpen.CAM.ToolAxisVariable = Nothing
        toolAxisVariable1 = surfaceContourBuilder1.ToolAxisVariable

        ' ----------------------------------------------
        '   Dialog Begin Variable Contour - [VARIABLE_CONTOUR]
        ' ----------------------------------------------
        surfaceContourBuilder1.CutAreaGeometry.InitializeData(False)
        Dim geometrySetList1 As NXOpen.CAM.GeometrySetList = Nothing
        geometrySetList1 = surfaceContourBuilder1.CutAreaGeometry.GeometryList

        ' ----------------------------------------------
        '   Dialog Begin Cut Area
        ' ----------------------------------------------

        '-----------find face which was named EXTRACT01----------
        Dim extraced_face_id As String = Nothing
        For Each myFeature As Object In theSession.Parts.Work.Features
            lw.WriteLine(myFeature.GetType.ToString)
            lw.WriteLine(myFeature.Name)
            If myFeature.GetType.ToString = "NXOpen.Features.ExtractFace" Then
                lw.WriteLine("=======extractedface found========")
                If myFeature.name = "EXTRACT01" Then

                    extraced_face_id = myFeature.JournalIdentifier.ToString
                    lw.WriteLine("===myFeature.getfeature.name===" & extraced_face_id)
                End If

            End If
        Next

        Dim faceid As String = extraced_face_id
        Dim faces1(0) As NXOpen.Face
        Dim extractedface1 As NXOpen.Features.ExtractFace = CType(workPart.Features.FindObject(faceid), NXOpen.Features.ExtractFace)
        Dim face1 As NXOpen.Face = CType(extractedface1.FindObject("FACE 1 {(31.9,3.5,17.2) " & faceid & "}"), NXOpen.Face)
        'face1.Highlight()
        faces1(0) = face1

#Region "2"
        ' ----------------------------------------------
        '   Dialog Begin Variable Contour - [VARIABLE_CONTOUR]
        ' ----------------------------------------------
        surfaceContourBuilder1.SetDriveMethod(NXOpen.CAM.SurfaceContourBuilder.DriveMethodTypes.SurfaceArea)
        surfaceContourBuilder1.CleanupType = NXOpen.CAM.SurfaceContourBuilder.CleanupTypes.Off
        surfaceContourBuilder1.DmSurfBuilder.StepoverBuilder.StepoverType = NXOpen.CAM.StepoverBuilder.StepoverTypes.Number

        ' ----------------------------------------------
        '   Dialog Begin Surface Area Drive Method
        ' ----------------------------------------------

        Dim surfaceDriveGeometry_x As NXOpen.CAM.SurfaceDriveGeometry = Nothing
        surfaceDriveGeometry_x = surfaceContourBuilder1.DmSurfBuilder.DriveGeometry

        Dim surfaceDriveGeometrySetList1 As NXOpen.CAM.SurfaceDriveGeometrySetList = Nothing
        surfaceDriveGeometrySetList1 = surfaceDriveGeometry_x.GeometryList

        Dim taggedObject_x As NXOpen.TaggedObject = surfaceDriveGeometrySetList1.FindItem(0)
        Dim surfaceDriveGeometrySet1 As NXOpen.CAM.SurfaceDriveGeometrySet = CType(taggedObject_x, NXOpen.CAM.SurfaceDriveGeometrySet)

        ' ----------------------------------------------
        '   Dialog Begin Drive Geometry
        ' ----------------------------------------------
        surfaceDriveGeometrySet1.Surface = face1

        ' ----------------------------------------------
        '   Dialog Begin Surface Area Drive Method
        ' ----------------------------------------------
        surfaceContourBuilder1.DmSurfBuilder.FlipMaterial()
        surfaceContourBuilder1.DmSurfBuilder.StepoverBuilder.NumberOfStepovers = 25

        Dim nXObject3 As NXOpen.NXObject = surfaceContourBuilder1.Commit()
        surfaceContourBuilder1.Destroy()
#End Region

#Region "3"
        Dim surfaceContour3 As NXOpen.CAM.SurfaceContour = CType(nXObject3, NXOpen.CAM.SurfaceContour)
        Dim surfaceContourBuilder3 As NXOpen.CAM.SurfaceContourBuilder = Nothing
        surfaceContourBuilder3 = workPart.CAMSetup.CAMOperationCollection.CreateSurfaceContourBuilder(surfaceContour3)
        Dim toolAxisVariable3 As NXOpen.CAM.ToolAxisVariable = Nothing
        toolAxisVariable3 = surfaceContourBuilder3.ToolAxisVariable
        ' ----------------------------------------------
        '   Dialog Begin Variable Contour - [VARIABLE_CONTOUR]
        ' ----------------------------------------------
        toolAxisVariable3.ToolAxisType = NXOpen.CAM.ToolAxisVariable.Types.NormalToDrive

        ' ----------------------------------------------
        '   Dialog Begin Feeds and Speeds
        ' ----------------------------------------------
        surfaceContourBuilder3.FeedsBuilder.SurfaceSpeedBuilder.Value = 110.0
        surfaceContourBuilder3.FeedsBuilder.FeedPerToothBuilder.Value = 0.1
        surfaceContourBuilder3.FeedsBuilder.RecalculateData(NXOpen.CAM.FeedsBuilder.RecalcuateBasedOn.SurfaceSpeed)

        Dim objects1(0) As NXOpen.CAM.CAMObject
        objects1(0) = surfaceContour3
        surfaceContourBuilder3.Commit()
        workPart.CAMSetup.GenerateToolPath(objects1)
        workPart.CAMSetup.PostprocessWithSetting(objects1, "MILL_5_AXIS", "C:\Users\wangjian\Desktop\a1.ptp",
                                            NXOpen.CAM.CAMSetup.OutputUnits.PostDefined,
                                            NXOpen.CAM.CAMSetup.PostprocessSettingsOutputWarning.PostDefined,
                                            NXOpen.CAM.CAMSetup.PostprocessSettingsReviewTool.PostDefined)
        surfaceContourBuilder3.Destroy()

#End Region


    End Sub
    Public Shared Function GetUnloadOption(ByVal dummy As String) As Integer
        'Unloads the image immediately after execution within NX
        GetUnloadOption = NXOpen.Session.LibraryUnloadOption.Immediately
    End Function

End Class
