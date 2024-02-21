' NX 12.0.0.27
' Journal created by wangjian on Sat Oct 21 16:24:37 2023 Malay Peninsula Standard Time
Imports System
Imports NXOpen

Module NXJournal
    Sub Main(ByVal args() As String)

        Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
        Dim workPart As NXOpen.Part = theSession.Parts.Work
        Dim displayPart As NXOpen.Part = theSession.Parts.Display
        Dim lw As ListingWindow = theSession.ListingWindow
        lw.Open()

        '---------- hide all objects----------------
        Dim visibleObjects() As DisplayableObject = Nothing
        visibleObjects = workPart.Views.WorkView.AskVisibleObjects
        theSession.DisplayManager.BlankObjects(visibleObjects)

        '--------Menu: Application->Design->Modeling-------
        theSession.ApplicationSwitchImmediate("UG_APP_MODELING")

        '-----Menu: Insert->Offset/Scale->Thicken...-----------
        Dim nullNXOpen_Features_Feature As NXOpen.Features.Feature = Nothing
        Dim thickenBuilder1 As NXOpen.Features.ThickenBuilder = Nothing
        thickenBuilder1 = workPart.Features.CreateThickenBuilder(nullNXOpen_Features_Feature)
        thickenBuilder1.Tolerance = 0.01
        thickenBuilder1.FirstOffset.RightHandSide = "3" 'radius of probe 
        thickenBuilder1.SecondOffset.RightHandSide = "0"
        thickenBuilder1.BooleanOperation.Type = NXOpen.GeometricUtilities.BooleanOperation.BooleanType.Create
        Dim targetBodies1(0) As NXOpen.Body
        Dim nullNXOpen_Body As NXOpen.Body = Nothing
        targetBodies1(0) = nullNXOpen_Body
        thickenBuilder1.BooleanOperation.SetTargetBodies(targetBodies1)
        thickenBuilder1.RegionToPierce.DistanceTolerance = 0.01
        thickenBuilder1.RegionToPierce.ChainingTolerance = 0.0095

        '-------find face id by it's name --------
        Dim through_courve_id As String = Nothing
        For Each myFeature As Object In theSession.Parts.Work.Features
            lw.WriteLine(myFeature.GetType.ToString)
            lw.WriteLine(myFeature.Name)
            If myFeature.GetType.ToString = "NXOpen.Features.ThroughCurves" Then
                If myFeature.name = "FACE01" Then

                    through_courve_id = myFeature.JournalIdentifier.ToString
                    lw.WriteLine("===myFeature.getfeature.name===" & through_courve_id)
                End If

            End If
        Next

        '------build thicken body-------
        Dim body1 As NXOpen.Body = CType(workPart.Bodies.FindObject(through_courve_id), NXOpen.Body)
        Dim faceBodyRule1 As NXOpen.FaceBodyRule = Nothing
        faceBodyRule1 = workPart.ScRuleFactory.CreateRuleFaceBody(body1)

        Dim rules1(0) As NXOpen.SelectionIntentRule
        rules1(0) = faceBodyRule1
        thickenBuilder1.FaceCollector.ReplaceRules(rules1, False)

        Dim nXObject1 As NXOpen.NXObject = Nothing
        nXObject1 = thickenBuilder1.Commit()

        thickenBuilder1.Destroy()
        nXObject1.SetName("THICKEN01")

        '--------blank thicken body-------
        Dim objects1(0) As NXOpen.DisplayableObject
        Dim body_to_blank As NXOpen.Body = CType(workPart.Bodies.FindObject(nXObject1.JournalIdentifier.ToString), NXOpen.Body)
        objects1(0) = body_to_blank
        theSession.DisplayManager.BlankObjects(objects1)

        ' ----------------------------------------------
        '   Menu: Insert->Associative Copy->Extract Geometry...
        ' ----------------------------------------------

        Dim extractFaceBuilder1 As NXOpen.Features.ExtractFaceBuilder = Nothing
        extractFaceBuilder1 = workPart.Features.CreateExtractFaceBuilder(nullNXOpen_Features_Feature)
        extractFaceBuilder1.ParentPart = NXOpen.Features.ExtractFaceBuilder.ParentPartType.WorkPart
        extractFaceBuilder1.Associative = True
        extractFaceBuilder1.FixAtCurrentTimestamp = False
        extractFaceBuilder1.HideOriginal = False
        extractFaceBuilder1.DeleteHoles = False
        extractFaceBuilder1.InheritDisplayProperties = False
        extractFaceBuilder1.Type = NXOpen.Features.ExtractFaceBuilder.ExtractType.Face

        Dim thicken1 As NXOpen.Features.Thicken = CType(nXObject1, NXOpen.Features.Thicken)
        'Dim face1 As NXOpen.Face = CType(thicken1.FindObject("FACE 1 {(31.9,3.5,17.25) THICKEN_SHEET(49)}"), NXOpen.Face)
        'Dim face1 As NXOpen.Face = CType(thicken1.FindObject("FACE 1 {(31.9,3.5,17.25) THICKEN_SHEET(012345)}"), NXOpen.Face)
        Dim face1 As NXOpen.Face = CType(thicken1.FindObject("FACE 1"), NXOpen.Face)
        Dim added1 As Boolean = Nothing
        added1 = extractFaceBuilder1.ObjectToExtract.Add(face1)

        Dim nXObject2 As NXOpen.NXObject = Nothing
        nXObject2 = extractFaceBuilder1.Commit()
        extractFaceBuilder1.Destroy()
        nXObject2.SetName("EXTRACT01")

        ' ----------------------------------------------
        '   Menu: Tools->Journal->Stop Recording
        ' ----------------------------------------------

    End Sub
    Public Function GetUnloadOption(ByVal dummy As String) As Integer
        'Unloads the image immediately after execution within NX
        GetUnloadOption = NXOpen.Session.LibraryUnloadOption.Immediately
    End Function

End Module